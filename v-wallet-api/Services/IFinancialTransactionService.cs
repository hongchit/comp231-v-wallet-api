using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public interface IFinancialTransactionService
    {

        Task<FinancialTransactionViewModel?> GetTransactionByTransactionId(string transactionId);
        Task<List<FinancialTransactionViewModel>> GetTransactionsByFinancialAccountId(string accountId);
        Task<List<FinancialTransactionViewModel>> GetTransactionsByUserId(string userId);
        Task<string> CreateTransaction(FinancialTransactionViewModel transaction);
    }
}
