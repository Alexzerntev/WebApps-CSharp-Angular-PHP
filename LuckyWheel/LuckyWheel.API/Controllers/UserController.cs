using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using LuckyWheel.BLL.Interfaces;
using LuckyWheel.Model.DTO;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace LuckyWheel.API.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly ISpinService spinService;
        
        

        public UserController(IUserService _userService, ISpinService _spinService, IConfiguration _configuration)
        {
            userService = _userService;
            spinService = _spinService;
        }

        [Authorize]
        [HttpGet]
        [Route("Profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
                var result = await userService.GetUserProfile(userId);
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

        [Authorize]
        [HttpGet]
        [Route("Balance")]
        public async Task<IActionResult> GetBalance()
        {
            try
            {
                var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
                var result = await userService.GetBalance(userId);
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

        //[Authorize]
        //[HttpPost]
        //[Route("Balance")]
        //public async Task<IActionResult> UpdateBalance([FromBody]double amountSigned)
        //{
        //    try
        //    {
        //        var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
        //        var result = await userService.UpdateBalance(userId, amountSigned);
        //        if (!result.IsSuccess())
        //        {
        //            return result.ToErrorResponse();
        //        }
        //        return Ok(result.Data);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(StartUpHelper.defaultErrorMessage + e.Message);
        //    }

        //}

        //[Authorize]
        //[HttpPost]
        //[Route("Spin")]
        //public async Task<IActionResult> InsertSpin([FromBody]SpinDTO spin)
        //{
        //    try
        //    {
        //        var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
        //        var result = await spinService.InsertSpin(userId, spin);
        //        if (!result.IsSuccess())
        //        {
        //            return result.ToErrorResponse();
        //        }
        //        return Ok(result.Data);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(StartUpHelper.defaultErrorMessage + e.Message);
        //    }

        //}

        [Authorize]
        [HttpGet]
        [Route("UserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
                var result = await userService.GetUserInfo(userId);
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

        [Authorize]
        [HttpPost]
        [Route("UploadPhoto")]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            try
            {
                var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
                MemoryStream memoryStream = new MemoryStream();
                file.OpenReadStream().CopyTo(memoryStream);
                var byteArray = new byte[file.Length];
                byteArray = memoryStream.ToArray();
                if (!await userService.AzureCheckPhoto(byteArray))
                {
                    return BadRequest("Η φωτογραφία που ανεβάσατε, δεν ειναι φωτογραφία προσώπου");
                }
                var result = await userService.InsertPhoto(userId, byteArray);
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

        [Authorize]
        [HttpGet]
        [Route("DownloadPhoto")]
        public async Task<IActionResult> DownloadPhoto()
        {
            try
            {
                var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
                var result = await userService.GetPhoto(userId);
                if (!result.IsSuccess())
                {
                    return Ok();
                }
                return Ok(result.Data);
            }
            catch (Exception e)
            {
                return BadRequest(StartUpHelper.defaultErrorMessage + e.Message);
            }

        }
        [Authorize]
        [HttpGet]
        [Route("DepositeCode")]
        public async Task<IActionResult> GetDeposits()
        {
            try
            {
                var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
                
                var result = await userService.GetDeposit(userId);
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
        [Authorize]
        [HttpGet]
        [Route("UseDepositeCode/{code}")]
        public async Task<IActionResult> UseDepositeCode(string code)
        {
            try
            {
                var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;

                var result = await userService.UseDeposit(userId, code);
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