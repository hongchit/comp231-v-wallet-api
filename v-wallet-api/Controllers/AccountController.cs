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

        /// <summary>
        /// Endpoint for user login
        /// </summary>
        /// <param name="loginViewModel">The login view model containing user credentials</param>
        /// <returns>The user account details if login is successful, otherwise returns a bad request</returns>
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

        /// <summary>
        /// Endpoint for user logout
        /// </summary>
        /// <returns>Ok response indicating successful logout</returns>
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }

        /// <summary>
        /// Endpoint for user registration
        /// </summary>
        /// <param name="userProfileViewModel">The user profile view model containing user registration details</param>
        /// <returns>The registered user profile if registration is successful, otherwise returns a bad request</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserProfileViewModel userProfileViewModel)
        {
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
