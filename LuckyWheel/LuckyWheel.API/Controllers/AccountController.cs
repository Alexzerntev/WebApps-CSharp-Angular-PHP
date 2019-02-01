using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LuckyWheel.Model.DTO;
using LuckyWheel.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using LuckyWheel.BLL;
using LuckyWheel.BLL.Interfaces;
using LuckyWheel.DAL;

namespace LuckyWheel.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        private readonly IAccountService accountService;
        private readonly Context context;

        public AccountController(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IAccountService _accountService,
            Context _context
            )
        {
            context = _context;
            userManager = _userManager;
            signInManager = _signInManager;
            accountService = _accountService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO regData)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = regData.Email, Email = regData.Email, FirstName = regData.FirstName, LastName = regData.LastName, Balance = 100 };
                var userInfo = new UserInfo();
                userInfo.RegistrationDate = DateTime.Now;
                userInfo.User = user;
                
                var result = await userManager.CreateAsync(user, regData.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return new JsonResult(new Dictionary<string, object>
                    {
                        { "access_token", accountService.GetAccessToken(user) },
                        { "id_token", accountService.GetIdToken(user) }
                    });
                }

                await context.UserInfo.AddAsync(userInfo);
                await context.SaveChangesAsync();
                return accountService.Errors(result);

            }
            return accountService.Error("Unexpected error");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginData)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginData.Email, loginData.Password, loginData.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(loginData.Email);
                    return new JsonResult(new Dictionary<string, object>
                    {
                        { "access_token", accountService.GetAccessToken(user) },
                        { "id_token", accountService.GetIdToken(user) }
                    });
                }
                return new JsonResult("Unable to sign in") { StatusCode = 401 };
            }
            return accountService.Error("Unexpected error");
        }
    }
}