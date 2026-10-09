using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Reminder
{
    /// <summary>
    /// 开机自启动管理：读写 HKCU\Software\Microsoft\Windows\CurrentVersion\Run。
    /// 写入用户级注册表不需要管理员权限。
    /// </summary>
    public static class AutoStartManager
    {
        private const string RunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private const string ValueName = "SedentaryReminder";
        private const string AutoRunArg = "--autorun";

        /// <summary>开机自启动是否已启用（按当前 exe 路径校验，路径变化视为未启用）</summary>
        public static bool IsEnabled()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RunKeyPath, false))
                {
                    if (key == null) return false;
                    object value = key.GetValue(ValueName);
                    if (value == null) return false;
                    return string.Equals(value.ToString(), ExpectedCommandLine(), StringComparison.OrdinalIgnoreCase);
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>设置开机自启动；写入后回读校验，返回是否成功</summary>
        public static bool SetEnabled(bool enabled)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RunKeyPath, true))
                {
                    if (key == null) return false;
                    if (enabled)
                    {
                        key.SetValue(ValueName, ExpectedCommandLine());
                        // 组策略/安全软件可能静默拦截写入，回读校验
                        object check = key.GetValue(ValueName);
                        return check != null &&
                            string.Equals(check.ToString(), ExpectedCommandLine(), StringComparison.OrdinalIgnoreCase);
                    }
                    else
                    {
                        key.DeleteValue(ValueName, false);
                        return key.GetValue(ValueName) == null;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>开机自启动时应写入的命令行（exe 路径带引号 + --autorun 参数）</summary>
        private static string ExpectedCommandLine()
        {
            return "\"" + Application.ExecutablePath + "\" " + AutoRunArg;
        }
    }
}
