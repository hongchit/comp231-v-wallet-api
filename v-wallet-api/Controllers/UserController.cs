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

        private readonly IUserProfileService _userProfileService;

        public AccountController(IAccountService accountService, IUserProfileService userProfileService)
        {
            _accountService = accountService;
            _userProfileService = userProfileService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid username or password");
            }

            var userAccount = await _accountService.Login(loginViewModel);

            if (userAccount == null)
            {
                return BadRequest("Invalid username or password");
            }

            return Ok(userAccount);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // Logout user
            // TODO: Session management (remove session token)
            return Ok();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserProfileViewModel userProfileViewModel)
        {
            //{
            //    "email": "example@example.com",
            //  "password": "password123",
            //  "confirmPassword": "password123",
            //  "firstname": "John",
            //  "lastname": "Doe",
            //  "birthday": "2000-01-01"
            //}
            if (string.IsNullOrEmpty(userProfileViewModel.Email) || string.IsNullOrEmpty(userProfileViewModel.Password))
            {
                return BadRequest("Email or password is invalid");
            }

            if (userProfileViewModel.Password != userProfileViewModel.ConfirmPassword)
            {
                return BadRequest("Password did not match");
            }

            try
            {
                var userProfile = await _userProfileService.Register(userProfileViewModel);

                if (userProfile == null)
                {
                    return BadRequest("Invalid user profile");
                }

                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

    }
}
