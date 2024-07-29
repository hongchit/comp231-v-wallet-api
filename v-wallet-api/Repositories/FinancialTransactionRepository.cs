using Microsoft.EntityFrameworkCore;
using v_wallet_api.Data;
using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public class FinancialTransactionRepository : IFinancialAccountRepository
    {
        private readonly DataContext _context;

        public FinancialTransactionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<FinancialAccount>> GetFinancialAccountsByUserId(string userId)
        {
            var accounts = _context.FinancialAccount.Where(x => x.UserProfileId == Guid.Parse(userId)).ToList();

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
    }
}
