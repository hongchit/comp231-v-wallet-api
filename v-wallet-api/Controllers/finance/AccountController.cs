using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using v_wallet_api.Services;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Controllers.finance
{
    [Route("api/finance/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {

        private readonly IFinancialAccountService _financialAccountService;

        public AccountController(IFinancialAccountService financialAccountService)
        {
            _financialAccountService = financialAccountService;
        }

        // Retrieve all accounts belong to the logged-in user
        [HttpGet]
        public async Task<IActionResult> GetFinancialAccount()
        {
            string userProfileId = ""; // TODO Read from JWT Token Authentication
            var accounts = await _financialAccountService.GetFinancialAccountsByUserId(userProfileId);
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFinancialAccount([FromBody] FinancialAccountViewModel account)
        {
            string userProfileId = ""; // TODO Read from JWT Token Authentication
            var created = await _financialAccountService.CreateFinancialAccount(account);
            if (created != null)
            {
                return Ok(created);
            }
            else
            {
                return BadRequest("Failed to create transaction");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFinancialAccount(string id, [FromBody] FinancialAccountViewModel account)
        {
            if (string.IsNullOrEmpty(id) || !id.Equals(account.Id))
            {
                return BadRequest("Invalid financial account ID");
            }
            var success = await _financialAccountService.UpdateFinancialAccount(account);
            return Ok(success);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinancialAccount(string id)
        {
            var success = await _financialAccountService.DeleteFinancialAccount(id);
            return Ok(success);
        }

    }
}
