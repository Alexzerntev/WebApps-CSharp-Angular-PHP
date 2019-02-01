using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LuckyWheel.BLL.Interfaces;

namespace LuckyWheel.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/History")]
    public class HistoryController : Controller
    {
        private readonly ISpinService spinService;
        private readonly ITransactionService transactionService;
        public HistoryController(
            ITransactionService _transactionService,
            ISpinService _spinService
            )
        {
            spinService = _spinService;
            transactionService = _transactionService;
        }

        [Route("GetSpins/{userId}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpins(string userId)
        {
            var result = await spinService.GetSpins(userId);
            if (!result.IsSuccess())
            {
                return result.ToErrorResponse();
            }
            return Ok(result.Data);
        }
        

        
        [Route("GetTransactions/{userId}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactions(string userId)
        {
            var result = await transactionService.GetTransactions(userId);
            if (!result.IsSuccess())
            {
                return result.ToErrorResponse();
            }
            return Ok(result.Data);
        }
    }
}