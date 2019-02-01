using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LuckyWheel.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LuckyWheel.API.Controllers
{
    [Produces("application/json")]
    [Route("api/History")]
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly ISpinService spinService;
        private readonly ITransactionService transactionService;
        public HistoryController(
            ISpinService _spinService,
            ITransactionService _transactionService
            )
        {
            spinService = _spinService;
            transactionService = _transactionService;
        }
        [Route("Spins")]
        [HttpGet]
        public async Task<IActionResult> GetSpins()
        {
            var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
            var result = await spinService.GetSpins(userId);
                if (!result.IsSuccess())
                {
                    return result.ToErrorResponse();
                }
                return Ok(result.Data);
            }

        [Route("Transactions")]
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
            var result = await transactionService.GetTransactions(userId);
                if (!result.IsSuccess())
                {
                    return result.ToErrorResponse();
                }
                return Ok(result.Data);
            }
    }
}
