using System.Threading.Tasks;
using LRMS.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LRMS.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogController : BaseController
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var logs = await _logService.GetDateWiseLogs(10);
            return Ok(logs);
        }

        [HttpGet]
        [Route("getLogsBy")]
        public async Task<IActionResult> getLogsBy(string logDate, string logLevel, int page, int limit)
        {
            var logs = await _logService.GetLogsBy(logDate, logLevel, limit, page);
            return Ok(logs);
        }
    }
}
