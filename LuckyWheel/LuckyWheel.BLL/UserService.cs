using LuckyWheel.BLL.Interfaces;
using LuckyWheel.DAL;
using LuckyWheel.Model;
using LuckyWheel.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LuckyWheel.BLL
{
    public class UserService : IUserService
    {
        private readonly Context context;
        private readonly IConfiguration configuration;

        public UserService(Context _context, IConfiguration _configuration)
        {
            context = _context;
            configuration = _configuration;
        }
        #region Client
        public async Task<Result<ProfileDTO>> GetUserProfile(string userId)
        {
            var user = await context.Users.FindAsync(Guid.Parse(userId));
            if (user == null)
            {
                return Result<ProfileDTO>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης χρήστη");
            }
            return Result<ProfileDTO>.CreateSuccessful(new ProfileDTO(user));
        }

        public async Task<Result<UserInfoDTO>> GetUserInfo(string userId)
        {
            var user = await context.Users.FindAsync(Guid.Parse(userId));
            if (user == null)
            {
                return Result<UserInfoDTO>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης χρήστη");
            }
            var userInfo = await context.UserInfo.FindAsync(Guid.Parse(userId));
            if (userInfo == null)
            {
                return Result<UserInfoDTO>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης πληροφοριών χρήστη");
            }
            return Result<UserInfoDTO>.CreateSuccessful(new UserInfoDTO(user, userInfo));
        }
        public async Task<Result<List<DepositeCodeDTO>>> GetDeposit(string userId)
        {
            var owner = context.Users.Where(x => x.Id == Guid.Parse(userId)).SingleOrDefault();
            var dc = await context.DepositCodes.Where(x => x.User == owner && x.IsUsed == false).ToListAsync();
            if (owner == null)
            {
                return Result < List<DepositeCodeDTO>>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης αποθέματος");
            }
            var ownerInfo = await context.UserInfo.FindAsync(Guid.Parse(userId));
            if (ownerInfo == null)
            {
                return Result < List<DepositeCodeDTO>>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης απόθεματος");
            }
            return Result < List < DepositeCodeDTO >>.CreateSuccessful( DepositeCodeDTO.ToDTO(dc));
        }
        public async Task<Result<bool>> UseDeposit(string userId, string usingCode)
        {
            var user = context.Users.Where(x => x.Id == Guid.Parse(userId)).SingleOrDefault();
            var dc = await context.DepositCodes.Where(x => x.User == user && x.UsingCode == usingCode).FirstOrDefaultAsync();
            dc.IsUsed = true;
            await context.SaveChangesAsync();
            await UpdateBalance(userId , dc.Amount);
            if (user == null)
            {
                return Result<bool>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης αποθέματος");
            }
            var ownerInfo = await context.UserInfo.FindAsync(Guid.Parse(userId));
            if (ownerInfo == null)
            {
                return Result<bool>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης απόθεματος");
            }
            return Result<bool>.CreateSuccessful(true);
        }
        public async Task<Result<double>> GetBalance(string userId)
        {
            var user = await context.Users.FindAsync(Guid.Parse(userId));
            if (user == null)
            {
                return Result<double>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης υπολοίπου");
            }
            return Result<double>.CreateSuccessful(user.Balance);
        }
        
        public async Task<Result<double>> UpdateBalance(string userId, double amountSigned)
        {
            var user = await context.Users.FindAsync(Guid.Parse(userId));
            if (user == null)
            {
                return Result<double>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης υπολοίπου");
            }
            user.Balance = user.Balance + amountSigned;
            if (user.Balance<0)
            {
                return Result<double>.CreateFailed(
                    HttpStatusCode.BadRequest, "Αδυναμία ανανέωσης υπολοίπου");
            }
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Result<double>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ενημέρωσης υπολοιίου");
            }
            return Result<double>.CreateSuccessful(user.Balance);
        }

        public async Task<Result<string>> InsertPhoto(string userId, byte[] PhotoByteArray)
        {
            var user = await context.Users.FindAsync(Guid.Parse(userId));
            if (user == null)
            {
                return Result<string>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης υπολοιίου");
            }

            Photo photo = new Photo();
            photo.PhotoFile = PhotoByteArray;
            user.Photo = photo;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Result<string>.CreateFailed(
                    HttpStatusCode.InternalServerError, "Αδυναμία Αποθήκευσης Φωτογραφίας");
            }
            return Result<string>.CreateSuccessful("Η φωτογραφία σας αποθηκεύτηκε επιτυχώς");
        }
        public async Task<Result<string>> GetPhoto(string userId)
        {
            var photo = await context.Photos.FindAsync(Guid.Parse(userId));
            if (photo == null)
            {
                return Result<string>.CreateFailed(
                   HttpStatusCode.NotFound, "Αδυναμία φόρτωσης φωτογραφίας");
            }
            return Result<string>.CreateSuccessful(Convert.ToBase64String(photo.PhotoFile));
        }
        public async Task<bool> AzureCheckPhoto(byte[] byteArray)
        {
            string subscriptionKey = configuration.GetSection("AzureSubscriptionKey").Key;
            const string uriBase = "https://westeurope.api.cognitive.microsoft.com/face/v1.0/detect";
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters. A third optional parameter is "details".
            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            // Assemble the URI for the REST API Call.
            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;

            using (ByteArrayContent content = new ByteArrayContent(byteArray))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();
                if (!String.IsNullOrEmpty(contentString))
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region Admin
        public async Task<Result<List<UserDTO>>> GetUsers()
        {
            var users = await context.Users.ToListAsync();
            if (users == null)
            {
                return Result<List<UserDTO>>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης χρήστη");
            }
            var userinfoes = await context.UserInfo.ToListAsync();
            if (userinfoes == null)
            {
                return Result<List<UserDTO>>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης χρήστη");
            }
            return Result<List<UserDTO>>.CreateSuccessful(UserDTO.ToDTO(users, userinfoes));
        }

        public async Task<Result<UserDTO>> GetUser(string userId)
        {
            var user = await context.Users.FindAsync(Guid.Parse(userId));
            if (user == null)
            {
                return Result<UserDTO>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης χρήστη");
            }
            var userInfo = await context.UserInfo.FindAsync(Guid.Parse(userId));
            if (userInfo == null)
            {
                return Result<UserDTO>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης χρήστη");
            }
            return Result<UserDTO>.CreateSuccessful(new UserDTO(user, userInfo));
        }
        public async Task<Result<List<WheelSegment>>> GetWheels()
        {
            var wheels = await context.WheelSegments.ToListAsync();
            if (wheels == null)
            {
                return Result<List<WheelSegment>>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης χρήστη");
            }

            return Result<List<WheelSegment>>.CreateSuccessful(wheels);
        }
        #endregion
    }
}
