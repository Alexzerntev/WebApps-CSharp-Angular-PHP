using LuckyWheel.BLL.Interfaces;
using LuckyWheel.DAL;
using LuckyWheel.Model;
using LuckyWheel.Model.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LuckyWheel.BLL
{
    public class TransactionService : ITransactionService
    {
        private readonly Context context;

        public TransactionService(Context _context)
        {
            context = _context;
        }
        public async Task<Result<List<TransactionDTO>>> GetTransactions(string userId)
        {
            var user = context.Users.Where(x=> x.Id == Guid.Parse(userId)).SingleOrDefault();
            var tr = await context.Transactions.Where(x => x.User == user).ToListAsync();
            if (user == null)
            {
                return Result<List<TransactionDTO>>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης συναλλαγών");
            }
            var transactions = user.Transactions.ToList();
            if (transactions == null)
            {
                return Result<List<TransactionDTO>>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης συναλλαγών");
            }
            return Result<List<TransactionDTO>>.CreateSuccessful(TransactionDTO.ToDTO(tr));
        }
    }
}
