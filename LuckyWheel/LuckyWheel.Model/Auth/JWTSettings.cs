using System;
using System.Collections.Generic;
using System.Text;

namespace LuckyWheel.Model
{
    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
