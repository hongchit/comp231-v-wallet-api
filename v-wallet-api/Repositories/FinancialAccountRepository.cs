using Microsoft.EntityFrameworkCore;
using v_wallet_api.Data;
using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public class FinancialAccountRepository : IFinancialAccountRepository
    {

        private readonly DataContext _context;

        public FinancialAccountRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<List<FinancialAccount>> GetFinancialAccountsByUserId(Guid userId)
        {
            var accounts = _context.FinancialAccount.Where(x => x.UserProfileId == userId).ToList();

            return accounts;
        }

        public async Task<FinancialAccount?> GetFinancialAccount(Guid id)
        {
            var account = await _context.FinancialAccount.FirstOrDefaultAsync(x => x.Id == id);

            return account;
        }

        public async Task<FinancialAccount> CreateFinancialAccount(FinancialAccount account)
        {
            await _context.FinancialAccount.AddAsync(account);

            await _context.SaveChangesAsync();

            return account;
        }
    }
}
