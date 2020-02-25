using System.Threading.Tasks;
using LRMS.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LRMS.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class StatusController : BaseController
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        [Route("getSftpStatus")]
        public async Task<IActionResult> GetSftpStatus()
        {
            var result = await _statusService.GetSftpStatus();
            return Ok(result);
        }

        [HttpGet]
        [Route("getAdaptorStatus")]
        public async Task<IActionResult> GetAdaptorStatus()
        {
            var result = await _statusService.GetAdaptorStatus();
            return Ok(result);
        }
    }
}
