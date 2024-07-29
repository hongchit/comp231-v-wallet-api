using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
    }
}
