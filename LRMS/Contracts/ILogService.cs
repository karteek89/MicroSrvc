using LRMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRMS.Contracts
{
    public interface ILogService
    {
        Task<IList<DateWiseLogViewModel>> GetDateWiseLogs(int daysCount, string filePath, string fileExtn);
        Task<IList<LogInfo>> GetLogsBy(string logDate, string filePath, string fileExtn, string logLevel, int limit, int page);
    }
}
