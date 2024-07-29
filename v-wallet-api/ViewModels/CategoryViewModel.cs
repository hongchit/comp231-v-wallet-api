using v_wallet_api.Models;

namespace v_wallet_api.ViewModels
{
    public class CategoryViewModel
    {
        public string Id { get; set; }
        public CategoryType Type { get; set; }
        public string Name => Enum.GetName(typeof(CategoryType), Type);
    }
}
