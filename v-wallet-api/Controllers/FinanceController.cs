using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using v_wallet_api.Providers;
using v_wallet_api.Services;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    [JwtAuthentication]
    public class FinanceController : ControllerBase
    {
        private readonly IFinancialAccountService _financeService;

        public FinanceController(IFinancialAccountService financeService)
        {
            _financeService = financeService;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{accountId}/transactions")]
        public async Task<IActionResult> GetFinancialTransactionsByAccount(string accountId)
        {
            if (string.IsNullOrEmpty(accountId))
            {
                return BadRequest("Failed to fetch account transactions");
            }

            var transactions = await _financeService.GetTransactionsByFinancialAccountId(accountId);

            return Ok(transactions);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{userProfileId}/transaction")]
        public async Task<IActionResult> GetFinancialTransactions(string userProfileId)
        {
            if (string.IsNullOrEmpty(userProfileId))
            {
                return BadRequest("Failed to fetch transactions");
            }

            var transactions = await _financeService.GetTransactionsByUserProfileId(userProfileId);

            return Ok(transactions);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("transaction")]
        public async Task<IActionResult> CreateTransaction([Microsoft.AspNetCore.Mvc.FromBody] FinancialTransactionViewModel transaction)
        {
            if (string.IsNullOrEmpty(transaction.AccountId.ToString()))
            {
                return BadRequest("Failed to create transaction");
            }

            var createdTransaction = await _financeService.CreateTransaction(transaction);

            return Ok(createdTransaction);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{userId}/account")]
        public async Task<IActionResult> GetAccounts([FromUri] string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Failed to load financial accounts");
            }

            var financialAccounts = await _financeService.GetFinancialAccountsByUserId(userId);

            return Ok(financialAccounts);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{financialAccountId}")]
        public async Task<IActionResult> GetAccount([FromUri] string financialAccountId)
        {
            if (string.IsNullOrEmpty(financialAccountId))
            {
                return BadRequest("Failed to load financial account");
            }

            var financialAccount = await _financeService.GetFinancialAccountByAccountId(financialAccountId);

            return Ok(financialAccount);
        }
    }
}
