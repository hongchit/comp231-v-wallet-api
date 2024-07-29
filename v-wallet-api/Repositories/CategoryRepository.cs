using Microsoft.EntityFrameworkCore;
using v_wallet_api.Data;
using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _context.Category.ToListAsync();
            return categories;
        }
    }
}
