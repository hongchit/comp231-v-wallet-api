using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public interface IFinancialAccountService
    {
        Task<FinancialTransactionViewModel?> GetTransactionByTransactionId(string transactionId);
        Task<List<FinancialTransactionViewModel>> GetTransactionsByAccountId(string accountId);
    }
}
