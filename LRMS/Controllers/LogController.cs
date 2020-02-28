using System.Threading.Tasks;
using LRMS.Contracts;
using LRMS.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAll(LoggerRequestModel model)
        {
            var logs = await _logService.GetDateWiseLogs(10, model.FilePath, model.FileExtn);
            return Ok(logs);
        }

        [HttpGet]
        [Route("getLogsBy")]
        public async Task<IActionResult> getLogsBy(LoggerRequestModel model, string logDate, string logLevel, int page, int limit)
        {
            var logs = await _logService.GetLogsBy(logDate, model.FilePath, model.FileExtn, logLevel, limit, page);
            return Ok(logs);
        }
    }
}
