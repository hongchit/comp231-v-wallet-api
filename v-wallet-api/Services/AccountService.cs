using v_wallet_api.Providers;
using v_wallet_api.Repositories;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserProfileService _userProfileService;

        public AccountService(IAccountRepository accountRepository, IUserProfileService userProfileService)
        {
            _accountRepository = accountRepository;
            _userProfileService = userProfileService;
        }

        public async Task<AccountViewModel?> Login(LoginViewModel loginViewModel)
        {
            var userAccount = await _accountRepository.GetUser(loginViewModel.Email, loginViewModel.Password);

            if (userAccount == null)
            {
                return null;
            }

            var authenticationModel = new AuthenticateViewModel
            {
                PrimaryId = userAccount.Id.ToString(),
                Username = userAccount.Email,
                Name = userAccount.Email,
                Role = userAccount.AccountType.ToString()
            };

            var token = GlobalIntegrationJwtManager.GenerateToken(authenticationModel);

            var userProfile = await _userProfileService.GetUserProfileByAccountId(userAccount.Id.ToString());

            var userData = new AccountViewModel
            {
                AccountId = userAccount.Id,
                ProfileId = userProfile.Id,
                Email = userAccount.Email,
                Name = userProfile.FullName,
                Token = token
            };

            return userData;
        }

        public async Task<AccountViewModel> GetAccountById(string accountId)
        {
            var account = await _accountRepository.GetAccountById(accountId);

            var accountViewModel = new AccountViewModel
            {
                AccountId = account.Id,
                Email = account.Email,
                Password = account.Password
            };

            return accountViewModel;
        }

        // Logout user
    }
}
