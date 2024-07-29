using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountById(string accountId);
        Task<Account?> GetUser(string email);
        Task<Account?> GetUser(string email, string password);
        Task<Account?> CreateAccount(Account account);
    }
}
