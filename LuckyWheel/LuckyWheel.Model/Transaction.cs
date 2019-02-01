using System;
using System.Collections.Generic;
using System.Text;

namespace LuckyWheel.Model
{
    public class Transaction : Entity
    {
        public User User{ get; set; }
        public TransactionType Type { get; set; }
        public TransactionSource Source { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}
