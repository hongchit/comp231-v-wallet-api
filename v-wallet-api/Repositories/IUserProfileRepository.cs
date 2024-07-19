using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public interface IUserProfileRepository
    {
        Task<UserProfile?> GetUserProfile(Guid userProfileId);
        Task<UserProfile?> GetUserProfileByAccountID(Guid accountId);
        Task<UserProfile?> CreateUserProfile(UserProfile userProfile);
    }
}
