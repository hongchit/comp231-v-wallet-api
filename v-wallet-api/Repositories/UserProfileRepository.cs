using Microsoft.EntityFrameworkCore;
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

        public async Task<UserProfile?> GetUserProfile(Guid userProfileId)
        {
            var userProfile = await _context.UserProfile.FirstOrDefaultAsync(x => x.Id == userProfileId);

            return userProfile;
        }

        public async Task<UserProfile?> GetUserProfileByAccountID(Guid accountId) 
        {
            var userProfile = await _context.UserProfile
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserAccountId == accountId);


            return userProfile;
        }

        public async Task<UserProfile?> CreateUserProfile(UserProfile userProfile) 
        {
            await _context.UserProfile.AddAsync(userProfile);
            await _context.SaveChangesAsync();

            return userProfile;
        }
    }
}
