using System;
using System.Collections.Generic;
using System.Text;

namespace LuckyWheel.Model
{
    public class DepositCode : Entity
    {
        public User User { get; set; }
        public string UsingCode { set; get; }
        public bool IsUsed { get; set; }
        public double Amount { get; set; }
    }
}
