using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nod.Bll.Interfaces;
using Nod.Model.DTO;

namespace Nod.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [Authorize]
        [HttpGet]
        [Route("Devices")]
        public async Task<IActionResult> GetUsersDevicesWithPagination()
        {
            try
            {
                var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
                var result = await _deviceService.GetUserDevices(userId);
                if (!result.IsSuccess())
                {
                    return result.ToErrorResponse();
                }
                return Ok(result.Data);
            }
            catch (Exception e)
            {
                return BadRequest("Error " + e.Message);
            }

        }

        [Authorize]
        [HttpPost]
        [Route("Gpses/{deviceId}")]
        public async Task<IActionResult> GetGpsesByDateTime(string deviceId, [FromBody] DateTimeWindowDTO dateTimeWindow)
        {
            try
            {
                var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
                var result = await _deviceService.GetGpsesByDateTime(deviceId, userId, dateTimeWindow.StartDateTime, dateTimeWindow.EndDateTime);
                if (!result.IsSuccess())
                {
                    return result.ToErrorResponse();
                }
                return Ok(result.Data);
            }
            catch (Exception e)
            {
                return BadRequest("Error " + e.Message);
            }

        }

        [Authorize]
        [HttpPost]
        [Route("HardwareStatuses/{deviceId}")]
        public async Task<IActionResult> GetHardwareStatusesByDateTime(string deviceId, [FromBody] DateTimeWindowDTO dateTimeWindow)
        {
            try
            {
                var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
                var result = await _deviceService.GetHardwareStatusesByDateTime(deviceId, userId, dateTimeWindow.StartDateTime, dateTimeWindow.EndDateTime);
                if (!result.IsSuccess())
                {
                    return result.ToErrorResponse();
                }
                return Ok(result.Data);
            }
            catch (Exception e)
            {
                return BadRequest("Error " + e.Message);
            }

        }
    }
}