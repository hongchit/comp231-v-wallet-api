using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public interface ICurrencyRepository
    {
        Task<List<Currency>> GetAllCurrencies();
    }
}
