using v_wallet_api.Repositories;
using v_wallet_api.ViewModels;
using v_wallet_api.Models;
using System.Globalization;

namespace v_wallet_api.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IAccountRepository accountRepository, IUserProfileRepository userProfileRepository)
        {
            _accountRepository = accountRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<UserProfileViewModel> GetUserProfile(string userProfileId)
        {
            var user = await _userProfileRepository.GetUserProfile(Guid.Parse(userProfileId));

            var userProfile = new UserProfileViewModel
            {
                Id = user.Id.ToString(),
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Birthdate = user.Birthdate
            };

            return userProfile;
        }

        public async Task<UserProfile?> Register(UserProfileViewModel userProfileViewModel)
        {
            var account = await _accountRepository.GetUser(userProfileViewModel.Email);
            if (account is not null)
            {
                throw new Exception($"Account already exist for email \"{userProfileViewModel.Email}\"");
            }

            if (userProfileViewModel.Password is null || userProfileViewModel.Password.Length < 8)
            {
                throw new Exception("Password must be at least 8 characters long");
            }
            if (userProfileViewModel.Password != userProfileViewModel.ConfirmPassword)
            {
                throw new Exception("Password and confirm password do not match");
            }

            // Validate the value
            var today = DateTime.Now;
            if (userProfileViewModel.Birthdate > today)
            {
                throw new Exception("Invalid date for Birthdate");
            }


            account = new Account
            {
                Email = userProfileViewModel.Email,
                Password = userProfileViewModel.Password,
                AccountType = AccountType.User,
            };
            var createdAccount = await _accountRepository.CreateAccount(account);
            if (createdAccount is null)
            {
                throw new Exception("Failed to create account");
            }

            var userProfile = new UserProfile
            {
                UserAccountId = createdAccount.Id,
                Firstname = userProfileViewModel.Firstname,
                Lastname = userProfileViewModel.Lastname,
                Birthdate = userProfileViewModel.Birthdate,
            };
            var profile = await _userProfileRepository.CreateUserProfile(userProfile);
            return profile;
        }

        public async Task<UserProfileViewModel?> GetUserProfileByAccountId(string accountId)
        {
            try
            {
                var userProfile = await _userProfileRepository.GetUserProfileByAccountID(Guid.Parse(accountId));
                if (userProfile is null)
                {
                    return null;
                }
                var userProfileViewModel = new UserProfileViewModel
                {
                    Id = userProfile.Id.ToString(),
                    Firstname = userProfile.Firstname,
                    Lastname = userProfile.Lastname,
                    Birthdate = userProfile.Birthdate
                };

                return userProfileViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get user profile", ex);
            }
        }
    }
}
