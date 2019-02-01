using LuckyWheel.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuckyWheel.Model
{
    public class Spin : Entity
    {
        public Spin() { }
        public Spin(SpinDTO spin, User user) {
            PlayedAmount = spin.PlayedAmount;
            Win = spin.Win;
            TotalAmount = spin.TotalAmount;
            TimeStamp = spin.TimeStamp;
            User = user;
        }
        public User User { get; set; }
        public double PlayedAmount { set; get; }
        public bool Win { set; get; }
        public double TotalAmount { set; get; }
        public DateTime TimeStamp { set; get; }
    }
}
