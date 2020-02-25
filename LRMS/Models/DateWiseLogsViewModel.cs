using System;

namespace LRMS.Models
{
    public class DateWiseLogViewModel
    {
        public string LogDate { get; set; }
        public int ErrorCount { get; set; }
        public int InfoCount { get; set; }
        public int WarningCount { get; set; }
    }

    public enum LogType
    {
        Error = 1,
        Info = 2,
        Warning = 3
    }
}
