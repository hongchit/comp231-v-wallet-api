using v_wallet_api.Data;
using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly DataContext _context;

        public UserProfileRepository(DataContext context) {
            _context = context;
        }

        public async Task<UserProfile?> GetUserProfile(Guid accountId) {
            // TODO
            return null;
//            var userProfile = await _context.UserProfile.FirstOrDefaultAsync(x => x.AccountId == accountId);

//            return userProfile;
        }

        public async Task<UserProfile?> CreateUserProfile(UserProfile userProfile) {
            await _context.UserProfile.AddAsync(userProfile);
            await _context.SaveChangesAsync();

            return userProfile;
        }
    }
}
