using Microsoft.EntityFrameworkCore;
using v_wallet_api.Data;
using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public class AccountRepository(DataContext context) : IAccountRepository
    {
        public async Task<Account> GetUser(string email, string password)
        {
            var account =
                await context.Account.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);

            return account!;
        }
    }
}
