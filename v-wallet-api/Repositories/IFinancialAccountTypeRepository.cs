using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public interface IFinancialAccountTypeRepository
    {
        Task<List<FinancialAccountType>> GetAllFinancialAccountTypes();

        Task<FinancialAccountType?> GetFinancialAccountTypeById(Guid id);

        Task<FinancialAccountType?> GetFinancialAccountTypeByName(string name);
    }
}
