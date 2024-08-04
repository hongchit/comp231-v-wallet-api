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

        public async Task<List<FinancialAccount>> GetFinancialAccountsByUserId(string userId)
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

        public async Task<List<FinancialTransaction>> GetFinancialTransactions(Guid accountId)
        {
            var transactions = _context.FinancialTransaction.Where(x => x.AccountId == accountId).ToList();

            return await Task.FromResult(transactions);
        }

        public async Task<FinancialTransaction> CreateFinancialTransaction(FinancialTransaction transaction)
        {
            await _context.FinancialTransaction.AddAsync(transaction);

            await _context.SaveChangesAsync();

            return transaction;
        }
    }
}
