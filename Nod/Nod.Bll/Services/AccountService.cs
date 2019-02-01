using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nod.Bll.Interfaces;
using Nod.Model.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Nod.Bll.Services
{
    public class AccountService : IAccountService
    {
        private readonly JwtSettings _options;

        public AccountService(IOptions<JwtSettings> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }


        public string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("full_name", user.FirstName + " " + user.LastName),
                    new Claim("id", user.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _options.Issuer,
                Audience = _options.Audience

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public JsonResult Error(string message)
        {
            return new JsonResult(message) { StatusCode = 400 };
        }
    }






























    //public string GetIdToken(User user)
    //{
    //    var payload = new Dictionary<string, object>
    //        {
    //            { "id", user.Id },
    //            { "sub", user.Email },
    //            { "email", user.Email },
    //            { "emailConfirmed", user.EmailConfirmed },
    //        };
    //    return GetToken(payload);
    //}

    //public string GetAccessToken(User user)
    //{
    //    var payload = new Dictionary<string, object>
    //    {
    //        { "sub", user.Email },
    //        { "id", user.Id }
    //    };
    //    return GetToken(payload);
    //}

    //public string GetToken(Dictionary<string, object> payload)
    //{
    //    var secret = _options.SecretKey;

    //    payload.Add("iss", _options.Issuer);
    //    payload.Add("aud", _options.Audience);
    //    payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
    //    payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
    //    payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddDays(7)));
    //    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
    //    IJsonSerializer serializer = new JsonNetSerializer();
    //    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
    //    IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

    //    return encoder.Encode(payload, secret);
    //}

    //public JsonResult Errors(IdentityResult result)
    //{
    //    var items = result.Errors
    //        .Select(x => x.Description)
    //        .ToArray();
    //    return new JsonResult(items) { StatusCode = 400 };
    //}

    //public JsonResult Error(string message)
    //{
    //    return new JsonResult(message) { StatusCode = 400 };
    //}

    //public static double ConvertToUnixTimestamp(DateTime date)
    //{
    //    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    //    TimeSpan diff = date.ToUniversalTime() - origin;
    //    return Math.Floor(diff.TotalSeconds);
    //}
}