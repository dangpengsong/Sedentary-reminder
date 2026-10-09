using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Reminder
{
    /// <summary>
    /// 系统电源与用户会话状态监控：用于实现"系统休眠/锁屏期间不计时"。
    /// 必须在 UI 线程（且窗体句柄已创建）之后调用 Init 订阅事件，
    /// 这样 SystemEvents 的回调会回到 UI 线程执行（键盘钩子的锁定/解锁要求同线程）。
    /// 注意：.NET 不会把 PBT_APMRESUMEAUTOMATIC 映射成 Resume 事件，
    /// 因此电源标志带超时兜底，真正保证"休眠不计时"的是 TickGuard 的墙钟间隔判断。
    /// </summary>
    public static class SystemPowerMonitor
    {
        private const uint ES_CONTINUOUS = 0x80000000;
        private const uint ES_DISPLAY_REQUIRED = 0x00000002;

        [DllImport("kernel32.dll")]
        private static extern uint SetThreadExecutionState(uint esFlags);

        private static volatile bool powerSuspended;
        private static bool sessionLocked;
        private static DateTime powerSuspendedSince;

        /// <summary>系统是否处于休眠/待机或锁屏（或远程会话断开）状态</summary>
        public static bool IsSuspended
        {
            get
            {
                // 兜底：若 Resume 事件丢失导致标志卡住，程序仍在运行（定时器在走）超过 10 分钟时自动解除
                if (powerSuspended && (DateTime.UtcNow - powerSuspendedSince).TotalMinutes > 10)
                {
                    powerSuspended = false;
                }
                return powerSuspended || sessionLocked;
            }
        }

        /// <summary>在 UI 线程调用一次即可（静态构造函数完成事件订阅）</summary>
        public static void Init()
        {
        }

        static SystemPowerMonitor()
        {
            SystemEvents.PowerModeChanged += OnPowerModeChanged;
            SystemEvents.SessionSwitch += OnSessionSwitch;
        }

        /// <summary>
        /// 休息遮罩显示期间保持屏幕点亮：键盘被锁定后用户无法输入屏保/锁屏密码，
        /// 避免屏幕保护程序抢占桌面。该 API 基于当前线程，必须在 UI 线程调用。
        /// </summary>
        public static void KeepDisplayOn(bool keepOn)
        {
            try
            {
                if (keepOn) SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED);
                else SetThreadExecutionState(ES_CONTINUOUS);
            }
            catch
            {
            }
        }

        private static void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            try
            {
                if (e.Mode == PowerModes.Suspend)
                {
                    powerSuspended = true;
                    powerSuspendedSince = DateTime.UtcNow;
                }
                else if (e.Mode == PowerModes.Resume)
                {
                    powerSuspended = false;
                    // 休眠（特别是 Modern Standby）恢复后，键盘钩子可能被系统因回调超时静默摘除，重装保证屏蔽有效
                    KeyboardBlocker.Reassert();
                }
            }
            catch
            {
                // SystemEvents 回调抛出的异常会被框架吞掉，这里主动兜底
            }
        }

        private static void OnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            try
            {
                switch (e.Reason)
                {
                    case SessionSwitchReason.SessionLock:
                    case SessionSwitchReason.RemoteDisconnect:
                        // 用户离开：暂停计时；同时解除键盘锁定，保证锁屏界面可以输入密码
                        sessionLocked = true;
                        KeyboardBlocker.Unlock();
                        KeepDisplayOn(false);
                        break;
                    case SessionSwitchReason.SessionUnlock:
                    case SessionSwitchReason.RemoteConnect:
                        sessionLocked = false;
                        // 回到桌面后，若仍处于休息遮罩中则恢复键盘锁定与屏幕保持点亮
                        RestFrm.RelockIfResting();
                        break;
                }
            }
            catch
            {
            }
        }
    }

    /// <summary>
    /// 倒计时 tick 守卫：系统休眠/锁屏期间或系统时钟出现大间隔时，本次 tick 不参与倒计时。
    /// 每个计时窗体各持有一个实例，避免多个计时器共享状态互相干扰。
    /// </summary>
    public sealed class TickGuard
    {
        /// <summary>两次 tick 的正常间隔约 1 秒；超过该秒数视为系统曾挂起或线程被长时间冻结</summary>
        private const double MaxGapSeconds = 2.0;

        private DateTime lastTickUtc;

        public TickGuard()
        {
            lastTickUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// 每次 tick 都应调用（无论是否计数）：
        /// 返回 true 表示本次 tick 可以递减倒计时；返回 false 表示本拍跳过。
        /// </summary>
        public bool ShouldCount()
        {
            DateTime now = DateTime.UtcNow;
            double gap = (now - lastTickUtc).TotalSeconds;
            lastTickUtc = now;

            if (SystemPowerMonitor.IsSuspended) return false;
            // 间隔过大说明系统休眠过或计时线程被冻结（负值说明系统时间被回拨），本拍不计数
            return gap > 0 && gap < MaxGapSeconds;
        }
    }
}
