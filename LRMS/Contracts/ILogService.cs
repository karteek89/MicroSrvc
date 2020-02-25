using LRMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRMS.Contracts
{
    public interface ILogService
    {
        Task<IList<DateWiseLogViewModel>> GetDateWiseLogs(int daysCount);
        Task<IList<LogInfo>> GetLogsBy(string logDate, string logLevel, int limit, int page);
    }
}
