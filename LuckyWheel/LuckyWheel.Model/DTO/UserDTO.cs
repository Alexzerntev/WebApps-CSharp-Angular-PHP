using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuckyWheel.Model.DTO
{
    public class UserDTO
    {
        public UserDTO(User user, UserInfo userInfo)
        {
            if (userInfo!=null && user!=null)
            {
                UserId = user.Id;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Balance = user.Balance;
                Email = user.Email;
                Password = user.PasswordHash;
                IBAN = userInfo.IBAN;
                BirthDAte = userInfo.BirthDate;
                PhoneNumber = user.PhoneNumber;
                RegistrationDate = userInfo.RegistrationDate;
            }
            
        }
        public Guid UserId { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public double Balance { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string IBAN { set; get; }
        public DateTime BirthDAte { set; get; }
        public string PhoneNumber { set; get; }
        public DateTime RegistrationDate { get; set; }


        public static List<UserDTO> ToDTO(List<User> users, List<UserInfo> userInfoes)
        {
            var userDTOList = new List<UserDTO>();
            foreach (var user in users)
            {
                var userInfo = userInfoes.Where(x => x.Id == user.Id).FirstOrDefault();
                userDTOList.Add(new UserDTO(user,userInfo));
            }
            return userDTOList;
        }
    }
}
