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

            // Convert the string from userProfileViewModel.Birthdate to DateOnly format
            DateOnly birthdate;

            
            if (!DateOnly.TryParseExact(userProfileViewModel.Birthdate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthdate))
            {
                throw new Exception("Invalid date format for Birthdate");
            }

            // Validate the value
            var today = DateOnly.FromDateTime(DateTime.Today);
            if (birthdate > today)
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
                Birthdate = birthdate,
            };
            var profile = await _userProfileRepository.CreateUserProfile(userProfile);
            return profile;
        }
    }
}
