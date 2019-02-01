using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.MongoDB;

namespace Nod.Model.Entities.Authentication
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
