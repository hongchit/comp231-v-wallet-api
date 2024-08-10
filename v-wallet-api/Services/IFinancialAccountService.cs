using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public interface IFinancialAccountService
    {
        Task<FinancialTransactionViewModel?> GetTransactionByTransactionId(string transactionId);
        Task<List<FinancialTransactionViewModel>> GetTransactionsByFinancialAccountId(string accountId);
        Task<List<FinancialTransactionViewModel>> GetTransactionsByUserProfileId(string userId);
        Task<string> CreateTransaction(FinancialTransactionViewModel transaction);
        Task<List<FinancialAccountViewModel>> GetFinancialAccountsByUserId(string userId);
        Task<FinancialAccountViewModel?> GetFinancialAccountByAccountId(string financialAccountId);
    }
}
