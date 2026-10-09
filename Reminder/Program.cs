using System;
using System.Windows.Forms;

namespace Reminder
{
    static class Program
    {
        /// <summary>
        /// 单实例互斥体。必须用静态字段持有引用，防止被 GC 回收导致互斥失效。
        /// </summary>
        private static System.Threading.Mutex singleInstanceMutex;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool createdNew;
            singleInstanceMutex = new System.Threading.Mutex(true,
                @"Local\SedentaryReminder.SingleInstance", out createdNew);
            if (!createdNew)
            {
                // 开机自启动触发的重复启动要静默退出：
                // 此时可能正处于休息遮罩（置顶窗口）中，弹窗会被遮罩永久盖住
                if (!IsAutoRun(args))
                {
                    MessageBox.Show("程序运行中，见右下角系统托盘");
                }
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrm());
        }

        /// <summary>判断是否为开机自启动（注册表命令带 --autorun 参数）</summary>
        private static bool IsAutoRun(string[] args)
        {
            if (args == null) return false;
            foreach (string arg in args)
            {
                if (string.Equals(arg, "--autorun", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
