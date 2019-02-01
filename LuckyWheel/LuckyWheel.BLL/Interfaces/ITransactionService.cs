using LuckyWheel.Model;
using LuckyWheel.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuckyWheel.BLL.Interfaces
{
    public interface ITransactionService
    {
        Task<Result<List<TransactionDTO>>> GetTransactions(string UserId);
    }
}
