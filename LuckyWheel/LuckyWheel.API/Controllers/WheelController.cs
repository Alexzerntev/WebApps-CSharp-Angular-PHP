using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LuckyWheel.DAL;
using LuckyWheel.BLL;
using Microsoft.AspNetCore.Authorization;
using LuckyWheel.BLL.Interfaces;

namespace LuckyWheel.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class WheelController : Controller
    {
        private readonly IWheelService wheelService;
        public WheelController(IWheelService _wheelService)
        {
            wheelService = _wheelService;
        }
        [Route("Settings")]
        [HttpGet]
        public async Task<IActionResult> GetWheelSettings()
        {
            try
            {
                var result = await wheelService.GetWheelSettings();
                if (!result.IsSuccess())
                {
                    return result.ToErrorResponse();
                }
                return Ok(result.Data);
            }
            catch (Exception e)
            {
                return BadRequest(StartUpHelper.defaultErrorMessage + e.Message);
            }
        }
        [Route("SpinResult")]
        [HttpPost]
        public async Task<IActionResult> GetSpinResult([FromBody]double playedAmout)
        {
            try
            {
                var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
                var result = await wheelService.GetSpinResult(userId,playedAmout);
                if (!result.IsSuccess())
                {
                    return result.ToErrorResponse();
                }
                return Ok(result.Data);
            }
            catch (Exception e)
            {
                return BadRequest(StartUpHelper.defaultErrorMessage + e.Message);
            }
        }
        [Route("Multyplyer")]
        [HttpGet]
        public async Task<IActionResult> GetMaxMultyplyer()
        {
            try
            {
                var result = await wheelService.GetMaxMultyplyer();
                if (!result.IsSuccess())
                {
                    return result.ToErrorResponse();
                }
                return Ok(result.Data);
            }
            catch (Exception e)
            {
                return BadRequest(StartUpHelper.defaultErrorMessage + e.Message);
            }
        }
    }
}
