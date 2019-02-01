using LuckyWheel.Model;
using LuckyWheel.Model.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuckyWheel.BLL.Interfaces
{
    public interface IUserService
    {
        Task<Result<ProfileDTO>> GetUserProfile(string userId);
        Task<Result<double>> GetBalance(string userId);
        Task<Result<List<DepositeCodeDTO>>> GetDeposit(string userId);
        Task<Result<double>> UpdateBalance(string userId, double AmountSigned);
        Task<Result<List<UserDTO>>> GetUsers();
        Task<Result<UserDTO>> GetUser(string userId);
        Task<Result<UserInfoDTO>> GetUserInfo(string userId);
        Task<Result<string>> InsertPhoto(string userId, byte[] PhotoByteArray);
        Task<Result<string>> GetPhoto(string userId);
        Task<bool> AzureCheckPhoto(byte[] byteArray);
        Task<Result<bool>> UseDeposit(string userId,string usingCode);
    }
}
