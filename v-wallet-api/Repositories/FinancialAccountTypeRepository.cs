using Microsoft.EntityFrameworkCore;
using v_wallet_api.Data;
using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public class FinancialAccountTypeRepository : IFinancialAccountTypeRepository
    {
        private readonly DataContext _context;

        public FinancialAccountTypeRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<List<FinancialAccountType>> GetAllFinancialAccountTypes()
        {
            var allFinancialAccountTypes = await _context.FinancialAccountType.ToListAsync();
            return allFinancialAccountTypes;
        }

        public async Task<FinancialAccountType?> GetFinancialAccountTypeById(Guid id)
        {
            var financialAccountType = await _context.FinancialAccountType.FirstOrDefaultAsync(x => x.Id == id);
            return financialAccountType;
        }

        public async Task<FinancialAccountType?> GetFinancialAccountTypeByName(string name)
        {
            var financialAccountType = await _context.FinancialAccountType.FirstOrDefaultAsync(x => x.Name == name);
            return financialAccountType;
        }
    }
}
