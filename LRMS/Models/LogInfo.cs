using System;

namespace LRMS.Models
{
    public class LogInfo
    {
        public string Type { get; set; }
        public DateTime LogDate { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
    }
}
