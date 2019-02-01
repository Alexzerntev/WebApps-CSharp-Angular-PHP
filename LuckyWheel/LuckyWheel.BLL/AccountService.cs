using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using LuckyWheel.BLL.Interfaces;
using LuckyWheel.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuckyWheel.BLL
{
    public class AccountService : IAccountService
    {
        private readonly JWTSettings options;

        public AccountService(IOptions<JWTSettings> _optionsAccessor)
        {
            options = _optionsAccessor.Value;
        }

        public string GetIdToken(User user)
        {
            var payload = new Dictionary<string, object>
                {
                    { "id", user.Id },
                    { "sub", user.Email },
                    { "email", user.Email },
                    { "emailConfirmed", user.EmailConfirmed },
                };
            return GetToken(payload);
        }

        public string GetAccessToken(User user)
        {
            var payload = new Dictionary<string, object>
            {
                { "sub", user.Email },
                { "id", user.Id }
            };
            return GetToken(payload);
        }

        public string GetToken(Dictionary<string, object> payload)
        {
            var secret = options.SecretKey;

            payload.Add("iss", options.Issuer);
            payload.Add("aud", options.Audience);
            payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddDays(7)));
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, secret);
        }

        public JsonResult Errors(IdentityResult result)
        {
            var items = result.Errors
                .Select(x => x.Description)
                .ToArray();
            return new JsonResult(items) { StatusCode = 400 };
        }

        public JsonResult Error(string message)
        {
            return new JsonResult(message) { StatusCode = 400 };
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}