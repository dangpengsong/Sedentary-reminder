using System;
using System.Runtime.InteropServices;

namespace Reminder
{
    /// <summary>
    /// 键盘锁定器：通过 WH_KEYBOARD_LL 低级键盘钩子屏蔽所有键盘输入（鼠标不受影响，
    /// 用于点击遮罩上的"立即解锁"按钮）。
    /// 相比旧的 user32.BlockInput 方案：
    ///   1. 无需管理员权限，普通用户即可生效；
    ///   2. 按过 Ctrl+Alt+Del 后不会被系统解除；
    ///   3. 进程退出时钩子自动失效，键盘立即恢复。
    /// 注意事项：
    ///   - Lock/Unlock 必须在同一个（UI）线程调用，低级钩子的回调依赖该线程的消息循环；
    ///   - Ctrl+Alt+Del、Win+L 锁屏界面、UAC 安全桌面属于系统保留输入，任何用户态程序都无法拦截；
    ///   - 钩子回调必须尽量快，超时会被系统静默摘除，休眠恢复后通过 Reassert 重装。
    /// </summary>
    public static class KeyboardBlocker
    {
        private const int WH_KEYBOARD_LL = 13;
        private const byte VK_SHIFT = 0x10;
        private const byte VK_CONTROL = 0x11;
        private const byte VK_MENU = 0x12;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        // 静态只读字段持有回调委托，防止被垃圾回收（回收后系统回调会跳进已释放的存根，导致进程崩溃）
        private static readonly LowLevelKeyboardProc HookProc = HookCallback;

        private static IntPtr hookId = IntPtr.Zero;

        /// <summary>键盘当前是否处于锁定状态</summary>
        public static bool IsLocked
        {
            get { return hookId != IntPtr.Zero; }
        }

        /// <summary>锁定键盘（屏蔽所有按键）</summary>
        public static bool Lock()
        {
            if (hookId != IntPtr.Zero) return true;
            hookId = SetWindowsHookEx(WH_KEYBOARD_LL, HookProc, GetModuleHandle(null), 0);
            return hookId != IntPtr.Zero;
        }

        /// <summary>解除键盘锁定（幂等，可在任意时刻调用）</summary>
        public static void Unlock()
        {
            IntPtr h = hookId;
            hookId = IntPtr.Zero; // 先置零：保证幂等，并让回调立即开始放行
            if (h != IntPtr.Zero)
            {
                UnhookWindowsHookEx(h);
                // 补发修饰键抬起事件，避免锁定期间按下的 Alt/Ctrl/Shift 产生"粘滞"状态
                keybd_event(VK_MENU, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
                keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
                keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            }
        }

        /// <summary>重装钩子：休眠恢复后系统可能已因回调超时静默摘除钩子，需要重新安装</summary>
        public static void Reassert()
        {
            if (hookId != IntPtr.Zero)
            {
                Unlock();
                Lock();
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // nCode 为负时必须无条件转交；否则吞掉键盘消息（返回非零值）
            if (nCode < 0) return CallNextHookEx(hookId, nCode, wParam, lParam);
            if (hookId != IntPtr.Zero) return (IntPtr)1;
            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }
    }
}
