using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public interface IAccountService
    {
        Task<string> Login(LoginViewModel loginViewModel);
    }
}
