using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using v_wallet_api.Services;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Controllers.finance
{
    [Route("api/finance/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IFinancialTransactionService _financeTransactionService;

        public TransactionController(IFinancialTransactionService financeService)
        {
            _financeTransactionService = financeService;
        }

        [HttpGet("by_account/{accountId}")]
        public async Task<IActionResult> GetFinancialTransactionsByAccount(string accountId)
        {
            if (string.IsNullOrEmpty(accountId))
            {
                return BadRequest("Failed to fetch account transactions");
            }

            var transactions = await _financeTransactionService.GetTransactionsByFinancialAccountId(accountId);

            return Ok(transactions);
        }

        [HttpGet("by_user/{userId}")]
        public async Task<IActionResult> GetFinancialTransactions(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Failed to fetch transactions");
            }

            var transactions = await _financeTransactionService.GetTransactionsByUserId(userId);

            return Ok(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] FinancialTransactionViewModel transaction)
        {
            if (string.IsNullOrEmpty(transaction.AccountId.ToString()))
            {
                return BadRequest("Failed to create transaction");
            }

            var createdTransaction = await _financeTransactionService.CreateTransaction(transaction);

            return Ok(createdTransaction);
        }
    }
}
