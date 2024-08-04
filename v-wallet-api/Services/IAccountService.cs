using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public interface IAccountService
    {
        Task<AccountViewModel?> Login(LoginViewModel loginViewModel);
        Task<AccountViewModel> GetAccountById(string accountId);
    }
}
