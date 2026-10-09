using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace Reminder
{
    public partial class MainFrm : Form
    {
        WorkFrm wrkFrm;

        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            // 必须在 UI 线程且窗体句柄创建之后订阅系统电源/会话事件
            // （SystemEvents 会把回调投递到订阅线程，键盘钩子的操作要求 UI 线程）
            SystemPowerMonitor.Init();

            // 载入上次保存的配置
            AppConfig cfg = AppConfig.Current;
            numWrkTime.Value = ClampToRange(cfg.WorkMinutes, numWrkTime);
            numRstTime.Value = ClampToRange(cfg.RestMinutes, numRstTime);
            ckBoxInput.Checked = cfg.LockKeyboard;
            ckBoxWorkRange.Checked = cfg.WorkRangeEnabled;
            dtpRange1Start.Value = ToDateTime(cfg.WorkRange1Start, new TimeSpan(9, 0, 0));
            dtpRange1End.Value = ToDateTime(cfg.WorkRange1End, new TimeSpan(12, 0, 0));
            ckBoxRange2.Checked = cfg.WorkRange2Enabled;
            dtpRange2Start.Value = ToDateTime(cfg.WorkRange2Start, new TimeSpan(13, 30, 0));
            dtpRange2End.Value = ToDateTime(cfg.WorkRange2End, new TimeSpan(18, 0, 0));
            if (cfg.HealthTips != null) txtHealthTips.Lines = cfg.HealthTips.ToArray();
            // 开机自启动状态以注册表实际值为准
            ckBoxAutoStart.Checked = AutoStartManager.IsEnabled();
            UpdateRangeControlsEnabled();
        }

        private void Btn_start_Click(object sender, EventArgs e)
        {
            // 保存配置
            AppConfig cfg = AppConfig.Current;
            cfg.WorkMinutes = (int)numWrkTime.Value;
            cfg.RestMinutes = (int)numRstTime.Value;
            cfg.LockKeyboard = ckBoxInput.Checked;
            cfg.WorkRangeEnabled = ckBoxWorkRange.Checked;
            cfg.WorkRange1Start = ToTimeString(dtpRange1Start);
            cfg.WorkRange1End = ToTimeString(dtpRange1End);
            cfg.WorkRange2Enabled = ckBoxRange2.Checked;
            cfg.WorkRange2Start = ToTimeString(dtpRange2Start);
            cfg.WorkRange2End = ToTimeString(dtpRange2End);
            cfg.HealthTips = ParseTips(txtHealthTips.Text);
            if (!cfg.Save())
            {
                MessageBox.Show("配置保存失败（请检查磁盘或权限），本次仍按当前设置运行。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 同步开机自启动设置
            if (!AutoStartManager.SetEnabled(ckBoxAutoStart.Checked))
            {
                MessageBox.Show("开机自启动设置失败（注册表写入被拒绝）。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            int wrkTime = (int)this.numWrkTime.Value;
            int rstTime = (int)this.numRstTime.Value;
            bool input_flag = this.ckBoxInput.Checked;
            wrkFrm = new WorkFrm(wrkTime, rstTime, input_flag);
            wrkFrm.Show();
            this.Visible = false;
        }

        /// <summary>
        /// 立即休息：跳过工作倒计时，直接用当前界面参数弹出休息遮罩（便于快速测试遮罩效果）。
        /// 休息结束或被"立即解锁"后，照常开始新一轮工作倒计时。
        /// </summary>
        private void BtnRestNow_Click(object sender, EventArgs e)
        {
            // 关闭可能存在的旧工作倒计时窗口，避免其在本轮休息期间触发新的休息
            List<Form> oldWorkForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
            {
                if (f is WorkFrm) oldWorkForms.Add(f);
            }
            foreach (Form f in oldWorkForms) f.Close();

            // 提示语优先使用文本框当前内容（便于实时预览），留空则用内置默认
            List<string> tips = ParseTips(txtHealthTips.Text);
            if (tips.Count == 0) tips.AddRange(AppConfig.DefaultHealthTips);

            this.Visible = false;
            RestFrm.StartRest((int)numRstTime.Value, (int)numWrkTime.Value,
                ckBoxInput.Checked, tips.ToArray());
        }

        private void 主窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            if (wrkFrm != null)
            {
                wrkFrm.Close();
            }
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //取消关闭窗口
            e.Cancel = true;
            //最小化主窗口
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            //不在系统任务栏显示主窗口图标
            this.ShowInTaskbar = false;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            // 退出前解除键盘锁定（正常路径下遮罩关闭时已解锁，这里兜底）
            KeyboardBlocker.Unlock();
            System.Environment.Exit(0);
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        /// <summary>工作时段相关勾选变化时刷新控件的启用状态</summary>
        private void RangeOption_CheckedChanged(object sender, EventArgs e)
        {
            UpdateRangeControlsEnabled();
        }

        /// <summary>按勾选状态启用/禁用工作时段编辑控件</summary>
        private void UpdateRangeControlsEnabled()
        {
            bool enabled = ckBoxWorkRange.Checked;
            dtpRange1Start.Enabled = enabled;
            dtpRange1End.Enabled = enabled;
            ckBoxRange2.Enabled = enabled;
            bool enabled2 = enabled && ckBoxRange2.Checked;
            dtpRange2Start.Enabled = enabled2;
            dtpRange2End.Enabled = enabled2;
        }

        /// <summary>把 "HH:mm" 文本转换为日期选择器可用的值（日期部分固定，仅取时间）</summary>
        private static DateTime ToDateTime(string text, TimeSpan fallback)
        {
            TimeSpan t = AppConfig.ParseTime(text, fallback);
            return new DateTime(2000, 1, 1).Add(t);
        }

        /// <summary>取日期选择器中的时间并格式化为 "HH:mm"</summary>
        private static string ToTimeString(DateTimePicker picker)
        {
            return picker.Value.ToString("HH:mm", CultureInfo.InvariantCulture);
        }

        /// <summary>把多行文本框内容按行拆分为提示语列表（去除空行与首尾空白）</summary>
        private static List<string> ParseTips(string text)
        {
            List<string> tips = new List<string>();
            string[] lines = text.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
            foreach (string line in lines)
            {
                string t = line.Trim();
                if (t.Length > 0) tips.Add(t);
            }
            return tips;
        }

        /// <summary>把配置值限制在数字输入框的有效范围内</summary>
        private static decimal ClampToRange(int value, NumericUpDown box)
        {
            if (value < box.Minimum) return box.Minimum;
            if (value > box.Maximum) return box.Maximum;
            return value;
        }
    }
}
