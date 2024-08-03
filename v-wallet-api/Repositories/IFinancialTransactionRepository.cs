using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public interface IFinancialTransactionRepository
    {
        Task<FinancialTransaction?> GetFinancialTransaction(Guid id);
        Task<List<FinancialTransaction>> GetFinancialTransactions(Guid accountId);
        Task<FinancialTransaction> CreateFinancialTransaction(FinancialTransaction transaction);

    }
}
