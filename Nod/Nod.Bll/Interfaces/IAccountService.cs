using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nod.Model.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nod.Bll.Interfaces
{
    public interface IAccountService
    {
        string GetToken(User user);
        JsonResult Error(string message);
    }
}
