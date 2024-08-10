using Microsoft.EntityFrameworkCore;
using v_wallet_api.Data;
using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly DataContext _context;

        public CurrencyRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<List<Currency>> GetALlCurrencies()
        {
            var curriencies = await _context.Currency.ToListAsync();
            return curriencies;
        }
    }
}
