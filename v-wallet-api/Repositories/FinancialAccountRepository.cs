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

        public async Task<bool> UpdateFinancialAccount(FinancialAccount financialAccount)
        {
            var account = await _context.FinancialAccount.FirstOrDefaultAsync(x => x.Id == financialAccount.Id);

            if (account != null)
            {
                // Id, UserProfileId cannot be updated
                account.AccountName = financialAccount.AccountName;
                account.AccountNumber = financialAccount.AccountNumber;
                account.InitialValue = financialAccount.InitialValue;
                account.CurrentValue = financialAccount.CurrentValue;
                account.AccountType = financialAccount.AccountType;
                account.Currency = financialAccount.Currency;

                _context.FinancialAccount.Update(account);
                await _context.SaveChangesAsync();

                return true;
            }

            return false; // Account not found
        }

        public async Task<bool> DeleteFinancialAccount(Guid id)
        {
            var account = await _context.FinancialAccount.FirstOrDefaultAsync(x => x.Id == id);

            if (account != null)
            {
                _context.FinancialAccount.Remove(account);
                await _context.SaveChangesAsync();

                return true;
            }

            return false; // Account not found

        }
    }
}
