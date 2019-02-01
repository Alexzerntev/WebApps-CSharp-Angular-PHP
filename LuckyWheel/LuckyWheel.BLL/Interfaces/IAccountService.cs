using LuckyWheel.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuckyWheel.BLL.Interfaces
{
    public interface IAccountService
    {
        string GetIdToken(User user);
        string GetAccessToken(User user);
        string GetToken(Dictionary<string, object> payload);
        JsonResult Errors(IdentityResult result);
        JsonResult Error(string message);
    }
}
