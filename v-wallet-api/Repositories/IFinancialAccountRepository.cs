using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public interface IFinancialAccountRepository
    {
        Task<List<FinancialAccount>> GetFinancialAccountsByUserId(Guid userId);
        Task<FinancialAccount?> GetFinancialAccount(Guid id);
        Task<FinancialAccount> CreateFinancialAccount(FinancialAccount financialAccount);

        Task<bool> UpdateFinancialAccount(FinancialAccount financialAccount);

        Task<bool> DeleteFinancialAccount(Guid id);
    }
}
