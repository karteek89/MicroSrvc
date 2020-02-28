using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LRMS.Contracts;
using LRMS.Models;

namespace LRMS.Services
{
    public class StatusService : IStatusService
    {
        public StatusService()
        {
        }

        public async Task<IList<Adaptor>> GetAdaptorStatus(IList<Adaptor> list)
        {
            foreach (var adaptor in list)
                adaptor.IsRunning = IsProcessRunning(adaptor.ProcessName);

            return await Task.Run(() => { return list; });
        }

        public async Task<bool> GetSftpStatus(SftpRequestModel model)
        {
            return await CheckSftpStatus(model);
        }


        #region Private Section

        private bool IsProcessRunning(string processName)
        {
            var pname = System.Diagnostics.Process.GetProcessesByName(processName);
            if (pname.Length == 0)
                return false;

            return true;
        }

        private Task<bool> CheckSftpStatus(SftpRequestModel sftp)
        {
            var isSftp = false;

            using (FileStream fs = File.Open(sftp.FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s.Contains(sftp.KeyWord))
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
