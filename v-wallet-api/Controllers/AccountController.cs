using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using v_wallet_api.Services;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid username or password");
            }

            await _accountService.Login(loginViewModel);

            return Ok();
        }
    }
}
