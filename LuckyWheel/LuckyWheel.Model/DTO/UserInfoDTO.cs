using System;
using System.Collections.Generic;
using System.Text;

namespace LuckyWheel.Model.DTO
{
    public class UserInfoDTO
    {
        public UserInfoDTO() { }
        public UserInfoDTO(User user, UserInfo userInfo) {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            IBAN = userInfo.IBAN;
            BirthDate = userInfo.BirthDate;
        }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string IBAN { set; get; }
        public DateTime BirthDate { get; set; }
    }
}
