using v_wallet_api.Models;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public interface IUserProfileService
    {
        Task<UserProfile?> Register(UserProfileViewModel userProfileViewModel);
        Task<UserProfileViewModel?> GetUserProfileByAccountId(string userProfileId);
    }
}
