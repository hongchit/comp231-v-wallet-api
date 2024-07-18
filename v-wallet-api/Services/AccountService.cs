using System.Security.Authentication;
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

        public async Task Login(LoginViewModel loginViewModel)
        {
            var userAccount = await _accountRepository.GetUser(loginViewModel.Email, loginViewModel.Password);

            if(userAccount == null)
            {
                throw new AuthenticationException("Invalid email or password");
            }
        }
    }
}
