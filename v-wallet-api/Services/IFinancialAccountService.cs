using v_wallet_api.Models;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public interface IFinancialAccountService
    {
        Task<List<FinancialAccountViewModel>> GetFinancialAccountsByUserId(string userId);
        Task<FinancialAccountViewModel?> GetFinancialAccount(string id);
        Task<string> CreateFinancialAccount(FinancialAccountViewModel financialAccount);

        Task<bool> UpdateFinancialAccount(FinancialAccountViewModel financialAccount);

        Task<bool> DeleteFinancialAccount(string id);
    }
}
