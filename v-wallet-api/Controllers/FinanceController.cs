using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using v_wallet_api.Services;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly IFinancialAccountService _financeService;

        public FinanceController(IFinancialAccountService financeService)
        {
            _financeService = financeService;
        }

        [HttpGet("/{accountId}/transactions")]
        public async Task<IActionResult> GetFinancialTransactionsByAccount(string accountId)
        {
            if (string.IsNullOrEmpty(accountId))
            {
                return BadRequest("Failed to fetch account transactions");
            }

            var transactions = await _financeService.GetTransactionsByFinancialAccountId(accountId);

            return Ok(transactions);
        }

        [HttpGet("/{userId}/transactions")]
        public async Task<IActionResult> GetFinancialTransactions(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Failed to fetch transactions");
            }

            var transactions = await _financeService.GetTransactionsByUserId(userId);

            return Ok(transactions);
        }
    }
}
