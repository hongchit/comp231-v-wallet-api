using System.Security.Authentication;
using v_wallet_api.Providers;
using v_wallet_api.Repositories;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            var userAccount = await _accountRepository.GetUser(loginViewModel.Email, loginViewModel.Password);

            if (userAccount == null)
            {
                throw new AuthenticationException("Invalid email or password");
            }

            var authenticationModel = new AuthenticateViewModel
            {
                PrimaryId = userAccount.Id.ToString(),
                Username = userAccount.Email,
                Name = userAccount.Email,
                Role = userAccount.AccountType.ToString()
            };

            var token = GlobalIntegrationJwtManager.GenerateToken(authenticationModel);

            return token;
        }

        public async Task<AccountViewModel> GetAccountById(string accountId)
        {
            var account = await _accountRepository.GetAccountById(accountId);

            var accountViewModel = new AccountViewModel
            {
                Id = account.Id,
                Email = account.Email,
                Password = account.Password
            };

            return accountViewModel;
        }

        // Logout user
    }
}
