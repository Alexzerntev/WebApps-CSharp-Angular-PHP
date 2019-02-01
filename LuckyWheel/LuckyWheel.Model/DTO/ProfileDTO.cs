using System;
using System.Collections.Generic;
using System.Text;

namespace LuckyWheel.Model.DTO
{
    public class ProfileDTO
    {
        public ProfileDTO(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Balance = user.Balance;
        }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public double Balance { set; get; }

        //To do photo
    }
}
