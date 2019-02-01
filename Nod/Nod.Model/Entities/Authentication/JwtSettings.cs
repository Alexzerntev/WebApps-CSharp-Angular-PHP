using System;
using System.Collections.Generic;
using System.Text;

namespace Nod.Model.Entities.Authentication
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
