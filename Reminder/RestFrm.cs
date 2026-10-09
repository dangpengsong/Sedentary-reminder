using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Reminder
{
    /// <summary>
    /// 休息遮罩窗口。一次休息会话会为每个显示器创建一个实例（覆盖全部显示器），
    /// 由主显示器上的实例驱动倒计时并同步到其余实例。
    /// 会话期间键盘被锁定（可选），鼠标保持可用以便点击"立即解锁"按钮。
    /// </summary>
    public partial class RestFrm : Form
    {
        /// <summary>控件布局的设计尺寸，运行时据此把内容整体居中到实际屏幕</summary>
        private const int DesignWidth = 905;
        private const int DesignHeight = 486;

        // ===== 会话共享状态（一次休息 = 一组遮罩窗口）=====

        /// <summary>当前会话的全部遮罩窗口</summary>
        private static readonly List<RestFrm> overlays = new List<RestFrm>();

        /// <summary>会话是否处于活动状态（防重入）</summary>
        private static bool sessionActive = false;

        private static readonly Random rand = new Random();

        private static string sessionMessage = "";
        private static string sessionTip = "";
        private static bool sessionLockKeyboard = true;
        private static int sessionWorkMinutes = 60;
        private static int sessionRestMinutes = 5;

        // ===== 实例状态 =====

        /// <summary>本显示器的遮罩是否为主屏遮罩（负责驱动倒计时）</summary>
        private bool isPrimary;

        /// <summary>本次休息剩余秒数</summary>
        private int remainingSeconds;

        /// <summary>允许关闭的标志：只有会话正常结束才置 true（拦截 Alt+F4 等关闭方式）</summary>
        private bool allowClose;

        /// <summary>倒计时 tick 守卫（休眠/锁屏期间不计时）</summary>
        private readonly TickGuard tickGuard = new TickGuard();

        public RestFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 开始一次休息会话：为每个显示器创建全屏遮罩、锁定键盘（可选），
        /// 倒计时结束或点击"立即解锁"后统一恢复并开始新一轮工作倒计时。
        /// </summary>
        /// <param name="restMinutes">休息时长（分钟）</param>
        /// <param name="workMinutes">刚结束的工作时长（分钟，用于提示文案）</param>
        /// <param name="lockKeyboard">是否锁定键盘</param>
        /// <param name="tips">健康提示语候选列表，本次休息随机取一条</param>
        public static void StartRest(int restMinutes, int workMinutes, bool lockKeyboard, string[] tips)
        {
            if (sessionActive) return;
            sessionActive = true;

            sessionRestMinutes = restMinutes;
            sessionWorkMinutes = workMinutes;
            sessionLockKeyboard = lockKeyboard;
            sessionMessage = "您已久坐" + workMinutes + "分钟了，站起来活动下！";
            sessionTip = (tips != null && tips.Length > 0) ? "健康提示：" + tips[rand.Next(tips.Length)] : "";

            // 副屏遮罩先显示，主屏遮罩最后显示，保证主屏位于最上层并取得焦点
            Screen[] screens = Screen.AllScreens;
            RestFrm primaryForm = null;
            foreach (Screen scr in screens)
            {
                if (scr.Primary) primaryForm = CreateOverlay(scr, true);
                else CreateOverlay(scr, false).Show();
            }
            if (primaryForm != null)
            {
                primaryForm.Show();
                primaryForm.Activate();
            }

            // 防止休息期间屏幕进入屏保/息屏（键盘被锁定后用户无法输入屏保退出方式）
            SystemPowerMonitor.KeepDisplayOn(true);
            if (lockKeyboard) KeyboardBlocker.Lock();
        }

        /// <summary>
        /// 锁屏/远程断开后用户返回时调用：若仍处于休息遮罩中，恢复键盘锁定与屏幕保持点亮。
        /// </summary>
        public static void RelockIfResting()
        {
            if (!sessionActive) return;
            SystemPowerMonitor.KeepDisplayOn(true);
            if (sessionLockKeyboard) KeyboardBlocker.Lock();
        }

        /// <summary>创建单个显示器的遮罩窗口（不显示）</summary>
        private static RestFrm CreateOverlay(Screen screen, bool isPrimaryScreen)
        {
            RestFrm f = new RestFrm();
            f.isPrimary = isPrimaryScreen;
            f.StartPosition = FormStartPosition.Manual;
            f.TopMost = true;
            // 显式覆盖整块显示器（含任务栏）；向外扩 2 像素避免混合 DPI 下的边缘缝隙
            Rectangle bounds = screen.Bounds;
            bounds.Inflate(2, 2);
            f.Bounds = bounds;
            f.lblText.Text = sessionMessage;
            f.lblTip.Text = sessionTip;
            f.lblHint.Text = sessionLockKeyboard
                ? "键盘已锁定，休息结束后自动解锁；紧急情况可点击下方按钮立即恢复工作"
                : "紧急情况可点击下方按钮跳过本次休息，立即恢复工作";
            f.remainingSeconds = sessionRestMinutes * 60;
            overlays.Add(f);
            return f;
        }

        /// <summary>
        /// 结束当前休息会话：解锁键盘、关闭全部遮罩，并按需开始新一轮完整工作倒计时。
        /// 幂等：倒计时自然结束与"立即解锁"按钮可能同时触发。
        /// </summary>
        private static void FinishSession(bool startNextWorkCycle)
        {
            if (!sessionActive) return;
            sessionActive = false;

            // 恢复键盘与电源状态（进程退出时系统也会自动清除钩子，这里显式解锁以防万一）
            KeyboardBlocker.Unlock();
            SystemPowerMonitor.KeepDisplayOn(false);

            List<RestFrm> closing = new List<RestFrm>(overlays);
            overlays.Clear();
            foreach (RestFrm f in closing)
            {
                f.allowClose = true;
                f.Close();
            }

            if (startNextWorkCycle)
            {
                WorkFrm workFrm = new WorkFrm(sessionWorkMinutes, sessionRestMinutes, sessionLockKeyboard);
                workFrm.Show();
            }
        }

        private void RestFrm_Load(object sender, EventArgs e)
        {
            // 控件按设计尺寸排版，全屏时把内容整体居中
            LayoutCenter();
            UpdateDisplayText();
            if (isPrimary)
            {
                timerRst.Enabled = true;
            }
        }

        /// <summary>把设计尺寸下的控件整体平移到当前客户区的中央</summary>
        private void LayoutCenter()
        {
            int offsetX = Math.Max(0, (this.ClientSize.Width - DesignWidth) / 2);
            int offsetY = Math.Max(0, (this.ClientSize.Height - DesignHeight) / 2);
            if (offsetX == 0 && offsetY == 0) return;
            foreach (Control c in this.Controls)
            {
                c.Left += offsetX;
                c.Top += offsetY;
            }
        }

        private void TimerRst_Tick(object sender, EventArgs e)
        {
            if (!isPrimary || !sessionActive) return;
            // 系统休眠/锁屏期间或长时间卡顿后，本次 tick 不递减
            if (!tickGuard.ShouldCount()) return;

            if (remainingSeconds > 0)
            {
                remainingSeconds--;
                UpdateAllDisplays();
            }
            if (remainingSeconds <= 0)
            {
                FinishSession(true);
            }
        }

        /// <summary>把主屏的剩余时间同步到所有显示器的遮罩上</summary>
        private void UpdateAllDisplays()
        {
            foreach (RestFrm f in overlays)
            {
                f.remainingSeconds = this.remainingSeconds;
                f.UpdateDisplayText();
            }
        }

        /// <summary>刷新倒计时文本（mm:ss，等宽字体保证宽度稳定不跳动）</summary>
        private void UpdateDisplayText()
        {
            int sec = remainingSeconds;
            if (sec < 0) sec = 0;
            lblTime.Text = (sec / 60).ToString("00") + ":" + (sec % 60).ToString("00");
        }

        private void BtnUnlock_Click(object sender, EventArgs e)
        {
            // 特殊情况需要立即工作：跳过本次剩余休息，恢复键盘并开始新一轮工作倒计时
            FinishSession(true);
        }

        private void RestFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 休息期间不允许通过 Alt+F4 等方式关闭遮罩，只能由解锁按钮或倒计时结束触发
            if (!allowClose) e.Cancel = true;
        }

        /// <summary>拦截系统关闭命令（Alt+F4、任务栏关闭等）</summary>
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int WM_CLOSE = 0x0010;
            const int SC_CLOSE = 0xF060;
            if (!allowClose)
            {
                if (m.Msg == WM_CLOSE) return;
                if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt32() & 0xFFF0) == SC_CLOSE) return;
            }
            base.WndProc(ref m);
        }

        /// <summary>不显示在 Alt+Tab 切换列表中</summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_APPWINDOW = 0x40000;
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                cp.ExStyle &= (~WS_EX_APPWINDOW);
                cp.ExStyle |= WS_EX_TOOLWINDOW;
                return cp;
            }
        }
    }
}
