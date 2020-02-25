using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LRMS.Contracts;
using LRMS.Extensions;
using LRMS.Models;
using Microsoft.Extensions.Options;

namespace LRMS.Services
{
    public class LogService : ILogService
    {
        private readonly LoggerSettings _loggerSettings;

        public LogService(IOptions<Setting> settings)
        {
            _loggerSettings = settings?.Value.LoggerSetting;
        }

        public async Task<IList<DateWiseLogViewModel>> GetDateWiseLogs(int daysCount)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_loggerSettings.FilePath);
            IList<FileInfo> files = directoryInfo.GetFiles($"*.{_loggerSettings.FileExtn}")
                                            .OrderByDescending(p => p.CreationTime)
                                            .ToList();

            IList<DateWiseLogViewModel> list = new List<DateWiseLogViewModel>();
            foreach (FileInfo file in files)
                list.Add(await GetLogsCountFromFile(file, file.ToLogDate()));

            return list;
        }

        public async Task<IList<LogInfo>> GetLogsBy(string logDate, string logLevel, int limit, int page)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_loggerSettings.FilePath);
            var file = directoryInfo.GetFiles($"{logDate}.{_loggerSettings.FileExtn}")
                                    .FirstOrDefault();

            if (file == null) return null;
            return await BufferedReadLinesFromFile(file, logLevel, limit, page);
        }

        #region Private section

        private Task<IList<LogInfo>> BufferedReadLinesFromFile(FileInfo file, string logLevel, int limit = 10, int page = 1)
        {
            var offset = (page - 1) * limit;
            IList<LogInfo> list = new List<LogInfo>();
            using (FileStream fs = file.OpenRead())
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string s;
                LogInfo newLog = null;
                int logsCount = 0;

                while ((s = sr.ReadLine()) != null)
                {
                    var firstWord = s.Split(' ')
                                     .FirstOrDefault()
                                     .ToLogType();

                    // If first word of a line is following then its a new log.
                    var isNewLog = firstWord == Constants.Info || firstWord == Constants.Warn || firstWord == Constants.Error;

                    // If new log then clear earlier pointer.
                    if (isNewLog)
                        newLog = null;

                    // Getting logs for a particular log level.
                    if (isNewLog && !string.IsNullOrWhiteSpace(logLevel) && logLevel.ToUpper() != firstWord)
                        continue;

                    if (isNewLog)
                    {
                        // If limit reached then break loop
                        if (limit == list.Count) break;

                        logsCount++;

                        // insert only by skipping offset.
                        if (logsCount <= offset) continue;

                        newLog = new LogInfo
                        {
                            Type = firstWord,
                            Message = s.Split(new string[] { "###" }, StringSplitOptions.None)[1],
                            Description = s.Split(new string[] { "[][]" }, StringSplitOptions.None)[1]
                        };

                        list.Add(newLog);
                    }
                    else if (newLog != null)
                        newLog.Description += s;
                }
            }

            GC.Collect();
            return Task.Run(() => { return list; });
        }
        private Task<DateWiseLogViewModel> GetLogsCountFromFile(FileInfo file, string logDate)
        {
            var res = new DateWiseLogViewModel() { LogDate = logDate };
            using (FileStream fs = file.OpenRead())
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    var firstWord = s.Split(' ')
                                     .FirstOrDefault()
                                     .ToLogType();

                    switch (firstWord)
                    {
                        case Constants.Info:
                            res.InfoCount++;
                            break;
                        case Constants.Error:
                            res.ErrorCount++;
                            break;
                        case Constants.Warn:
                            res.WarningCount++;
                            break;
                        default:
                            break;
                    }


                }
            }
            GC.Collect();
            return Task.Run(() => { return res; });
        }

        #endregion
    }
}
