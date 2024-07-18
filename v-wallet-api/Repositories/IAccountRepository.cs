using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetUser(string email, string password);
    }
}
