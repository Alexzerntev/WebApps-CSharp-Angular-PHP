using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LuckyWheel.BLL.Interfaces;

namespace LuckyWheel.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        public UsersController(IUserService _userService)
        {
            userService = _userService;
        }

        [Route("GetUsers")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await userService.GetUsers();
            if (!result.IsSuccess())
            {
                return result.ToErrorResponse();
            }
            return Ok(result.Data);
        }

        [Route("GetUser/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetUser(string userId)
        {
            var result = await userService.GetUser(userId);
            if (!result.IsSuccess())
            {
                return result.ToErrorResponse();
            }
            return Ok(result.Data);
        }
        [Route("GetWheels")]
        [HttpGet]
        public async Task<IActionResult> GetWheels()
        {
            var result = await userService.GetUsers();
            if (!result.IsSuccess())
            {
                return result.ToErrorResponse();
            }
            return Ok(result.Data);
        }
    }
}