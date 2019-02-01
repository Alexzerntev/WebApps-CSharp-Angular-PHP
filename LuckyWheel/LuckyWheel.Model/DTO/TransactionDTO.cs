using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuckyWheel.Model.DTO
{
   public class TransactionDTO
    {
        public TransactionDTO(Transaction transaction)
        {
            Type = transaction.Type;
            Source = transaction.Source;
            Date = transaction.Date;
            Amount = transaction.Amount;
        }

        public TransactionType Type { get; set; }
        public TransactionSource Source { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }

        public static List<TransactionDTO> ToDTO(List<Transaction> transactions)
        {
            return transactions.Select(x => new TransactionDTO(x)).ToList();
        }
    }
}

