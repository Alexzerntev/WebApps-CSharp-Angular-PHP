using System;
using System.Collections.Generic;
using System.Text;

namespace Nod.Model.DTO
{
    public class LoginDTO
    {
        public string Email { set; get; }
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}
