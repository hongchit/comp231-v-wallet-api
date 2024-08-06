using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public interface IFinancialAccountRepository
    {
        Task<List<FinancialAccount>> GetFinancialAccountsById(List<string> accountIds);
        Task<List<FinancialAccount>> GetFinancialAccountsByUserProfileId(string userId);
        Task<FinancialTransaction?> GetFinancialTransaction(Guid id);
        Task<List<FinancialTransaction>> GetFinancialTransactions(List<string> accountIds);
        Task<FinancialTransaction> CreateFinancialTransaction(FinancialTransaction transaction);
        Task UpdateFinancialAccount(FinancialAccount account);
    }
}
