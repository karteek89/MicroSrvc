using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LRMS.Contracts;
using LRMS.Models;
using Microsoft.Extensions.Options;

namespace LRMS.Services
{
    public class StatusService : IStatusService
    {
        private readonly Setting _settings;

        public StatusService(IOptions<Setting> settings)
        {
            _settings = settings.Value;
        }

        public async Task<IList<AdaptorStatus>> GetAdaptorStatus()
        {
            foreach (var adaptor in _settings.AdaptorList)
                adaptor.IsRunning = IsProcessRunning(adaptor.ProcessName);

            return await Task.Run(() => { return _settings.AdaptorList; });
        }

        public async Task<bool> GetSftpStatus()
        {
            return await CheckSftpStatus();
        }


        #region Private Section

        private bool IsProcessRunning(string processName)
        {
            var pname = System.Diagnostics.Process.GetProcessesByName(processName);
            if (pname.Length == 0)
                return false;

            return true;
        }

        private Task<bool> CheckSftpStatus()
        {
            var isSftp = false;

            using (FileStream fs = File.Open(_settings.Sftp.FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s.Contains(_settings.Sftp.KeyWord))
                    {
                        isSftp = true;
                        break;
                    }
                }
            }
            GC.Collect();
            return Task.Run(() => { return isSftp; });
        }

        #endregion
    }
}
