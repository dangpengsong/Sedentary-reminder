using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace Reminder
{
    /// <summary>
    /// 应用程序配置：工作/休息时长、键盘锁定、工作时间范围、健康提示语等。
    /// 使用 XmlSerializer 持久化到 %APPDATA%\SedentaryReminder\config.xml。
    /// 注意：所有需要序列化的成员必须是 public 字段或带 get/set 的属性，
    /// 只读属性会导致 XmlSerializer 构造时抛异常。
    /// </summary>
    [XmlRoot("SedentaryReminderConfig")]
    public class AppConfig
    {
        /// <summary>配置格式版本号，便于后续升级迁移</summary>
        public int Version = 1;

        /// <summary>工作时长（分钟）</summary>
        public int WorkMinutes = 60;

        /// <summary>休息时长（分钟）</summary>
        public int RestMinutes = 5;

        /// <summary>休息时是否锁定键盘（鼠标不锁，用于点击"立即解锁"按钮）</summary>
        public bool LockKeyboard = true;

        /// <summary>是否启用工作时间范围（非工作时段不计时）</summary>
        public bool WorkRangeEnabled = false;

        /// <summary>工作时段1 开始时间（HH:mm）</summary>
        public string WorkRange1Start = "09:00";

        /// <summary>工作时段1 结束时间（HH:mm）</summary>
        public string WorkRange1End = "12:00";

        /// <summary>是否启用工作时段2（例如午休后的下午时段）</summary>
        public bool WorkRange2Enabled = false;

        /// <summary>工作时段2 开始时间（HH:mm）</summary>
        public string WorkRange2Start = "13:30";

        /// <summary>工作时段2 结束时间（HH:mm）</summary>
        public string WorkRange2End = "18:00";

        /// <summary>用户自定义的健康提示语（每行一条）</summary>
        public List<string> HealthTips = new List<string>();

        /// <summary>内置默认健康提示语（用户未自定义时使用）</summary>
        public static readonly string[] DefaultHealthTips = new string[]
        {
            "站起来走动一下，活动活动腿脚。",
            "眺望 6 米以外的远处 20 秒，缓解眼疲劳。",
            "喝一杯水，补充水分，促进新陈代谢。",
            "缓慢左右转头、上下点头各 5 次，放松颈肩。",
            "双手叉腰，向后伸展腰背 10 次，缓解腰椎压力。",
            "做 10 个深蹲，激活臀腿肌肉。",
            "耸耸肩、转转手腕和脚踝，让关节活动开。",
            "深呼吸 5 次，让身心放松一下。"
        };

        private static readonly string ConfigDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SedentaryReminder");

        private static readonly string ConfigFile = Path.Combine(ConfigDir, "config.xml");

        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(AppConfig));

        private static AppConfig current;

        /// <summary>当前配置（首次访问时自动从磁盘加载）</summary>
        public static AppConfig Current
        {
            get
            {
                if (current == null) Load();
                return current;
            }
        }

        /// <summary>从磁盘加载配置；文件不存在或损坏时使用默认值</summary>
        public static void Load()
        {
            current = null;
            try
            {
                if (File.Exists(ConfigFile))
                {
                    using (FileStream fs = File.OpenRead(ConfigFile))
                    {
                        current = (AppConfig)Serializer.Deserialize(fs);
                    }
                }
            }
            catch
            {
                // 配置文件损坏：备份为 .bad 后回退默认值，不影响程序启动
                current = null;
                try { File.Move(ConfigFile, ConfigFile + ".bad"); }
                catch { }
            }
            if (current == null) current = new AppConfig();
        }

        /// <summary>保存配置到磁盘（先写临时文件再替换，避免写入中断损坏原文件）</summary>
        public bool Save()
        {
            try
            {
                Directory.CreateDirectory(ConfigDir);
                string tmpFile = ConfigFile + ".tmp";
                using (FileStream fs = File.Create(tmpFile))
                {
                    Serializer.Serialize(fs, this);
                }
                if (File.Exists(ConfigFile)) File.Delete(ConfigFile);
                File.Move(tmpFile, ConfigFile);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断给定时刻是否处于工作时间范围内（用于非工作时间不计时）。
        /// 未启用范围限定时恒为 true。
        /// </summary>
        public bool IsWithinWorkTime(DateTime now)
        {
            if (!WorkRangeEnabled) return true;
            TimeSpan t = now.TimeOfDay;
            if (InRange(t, ParseTime(WorkRange1Start, new TimeSpan(9, 0, 0)), ParseTime(WorkRange1End, new TimeSpan(12, 0, 0))))
                return true;
            if (WorkRange2Enabled &&
                InRange(t, ParseTime(WorkRange2Start, new TimeSpan(13, 30, 0)), ParseTime(WorkRange2End, new TimeSpan(18, 0, 0))))
                return true;
            return false;
        }

        /// <summary>获取本次生效的健康提示语列表（用户未设置时使用内置默认列表）</summary>
        public List<string> GetEffectiveTips()
        {
            List<string> tips = new List<string>();
            if (HealthTips != null)
            {
                foreach (string line in HealthTips)
                {
                    if (line != null && line.Trim().Length > 0) tips.Add(line.Trim());
                }
            }
            if (tips.Count == 0) tips.AddRange(DefaultHealthTips);
            return tips;
        }

        /// <summary>解析 "HH:mm" 格式时间，失败时返回默认值</summary>
        public static TimeSpan ParseTime(string text, TimeSpan fallback)
        {
            DateTime parsed;
            if (!string.IsNullOrEmpty(text) &&
                DateTime.TryParseExact(text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
            {
                return parsed.TimeOfDay;
            }
            return fallback;
        }

        /// <summary>格式化时间为 "HH:mm"</summary>
        public static string FormatTime(TimeSpan t)
        {
            return t.ToString(@"hh\:mm", CultureInfo.InvariantCulture);
        }

        /// <summary>判断时间是否在 [start, end) 内；start==end 视为全天，start&gt;end 视为跨夜时段</summary>
        private static bool InRange(TimeSpan t, TimeSpan start, TimeSpan end)
        {
            if (start == end) return true;
            if (start < end) return t >= start && t < end;
            return t >= start || t < end;
        }
    }
}
