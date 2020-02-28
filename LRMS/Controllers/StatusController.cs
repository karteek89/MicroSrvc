using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LRMS.Contracts;
using LRMS.Models;
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

        [HttpPost]
        [Route("getSftpStatus")]
        public async Task<IActionResult> GetSftpStatus(SftpRequestModel model)
        {
            try
            {
                var result = await _statusService.GetSftpStatus(model);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }

        [HttpPost]
        [Route("getAdaptorStatus")]
        public async Task<IActionResult> GetAdaptorStatus(AdaptorStatusRequestModel model)
        {
            try
            {
                var result = await _statusService.GetAdaptorStatus(model.List);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
   
}
