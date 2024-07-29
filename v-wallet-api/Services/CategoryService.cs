using v_wallet_api.Models;
using v_wallet_api.Repositories;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryViewModel>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();

            var categoriesListViewModel = new List<CategoryViewModel>();

            foreach(var category in categories)
            {
                var categoryViewModel = new CategoryViewModel
                {
                    Id = category.Id,
                    Type = Enum.Parse<CategoryType>(category.Name),
                };

                categoriesListViewModel.Add(categoryViewModel);
            }

            return categoriesListViewModel;
        }
    }
}
