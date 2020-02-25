using System.Collections.Generic;

namespace LRMS.Models
{
    public class Setting
    {
        public Setting()
        {
            Sftp = new Sftp();
            LoggerSetting = new LoggerSettings();
            AdaptorList = new List<AdaptorStatus>();
        }
        public Sftp Sftp { get; set; }
        public IList<AdaptorStatus> AdaptorList { get; set; }

        public LoggerSettings LoggerSetting { get; set; }
    }

    public class LoggerSettings
    {
        public string FilePath { get; set; }
        public string FileExtn { get; set; }
    }

    public class Sftp
    {
        public string FilePath { get; set; }
        public string KeyWord { get; set; }
    }

    public class AdaptorStatus
    {
        public string ProcessName { get; set; }
        public string ProcessPath { get; set; }
        public bool? IsRunning { get; set; }
    }
}
