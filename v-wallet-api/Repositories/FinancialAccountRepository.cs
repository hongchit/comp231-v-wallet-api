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

        public async Task<List<FinancialAccount>> GetFinancialAccountsById(List<string> accountIds)
        {
            var accounts = await _context.FinancialAccount.Where(x => accountIds.Contains(x.Id.ToString())).Join(
                _context.FinancialAccountType, account => account.AccountTypeId, accountType => accountType.Id,
                (account, accountType) => new FinancialAccount
                {
                    Id = account.Id,
                    AccountName = account.AccountName,
                    AccountNumber = account.AccountNumber,
                    InitialValue = account.InitialValue,
                    CurrentValue = account.CurrentValue,
                    AccountTypeId = account.AccountTypeId,
                    CurrencyId = account.CurrencyId,
                    UserProfileId = account.UserProfileId,
                    AccountType = accountType
                }).ToListAsync();

            return accounts;
        }

        public async Task<List<FinancialAccount>> GetFinancialAccountsByUserProfileId(string userId)
        {
            var accounts = await _context.FinancialAccount
                .Where(x => x.UserProfileId == Guid.Parse(userId))
                .Join(_context.FinancialAccountType,
                    account => account.AccountTypeId,
                    accountType => accountType.Id,
                    (account, accountType) => new FinancialAccount
                    {
                        Id = account.Id,
                        AccountName = account.AccountName,
                        AccountNumber = account.AccountNumber,
                        InitialValue = account.InitialValue,
                        CurrentValue = account.CurrentValue,
                        AccountTypeId = account.AccountTypeId,
                        CurrencyId = account.CurrencyId,
                        UserProfileId = account.UserProfileId,
                        AccountType = accountType
                    })
                .ToListAsync();

            return accounts;
        }

        public async Task<FinancialTransaction?> GetFinancialTransaction(Guid id)
        {
            var transaction = await _context.FinancialTransaction.FirstOrDefaultAsync(x => x.Id == id);

            return transaction;
        }

        public async Task<List<FinancialTransaction>> GetFinancialTransactions(List<string> accountIds)
        {
            var ids = accountIds.Select(x => Guid.Parse(x)).ToList();
            var transactions = await _context.FinancialTransaction.Where(x => ids.Contains(x.AccountId)).ToListAsync();

            return transactions;
        }

        public async Task<FinancialTransaction> CreateFinancialTransaction(FinancialTransaction transaction)
        {
            await _context.FinancialTransaction.AddAsync(transaction);

            await _context.SaveChangesAsync();

            return transaction;
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

        public async Task UpdateFinancialAccount(FinancialAccount account)
        {
            _context.FinancialAccount.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFinancialAccount(FinancialAccount account)
        {
            if (account == null) return;
            _context.FinancialAccount.Remove(account);
            await _context.SaveChangesAsync();
        }
    }
}
