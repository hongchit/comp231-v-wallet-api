using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetCategories();
    }
}
