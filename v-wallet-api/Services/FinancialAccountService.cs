using v_wallet_api.Models;
using v_wallet_api.Repositories;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public class FinancialAccountService : IFinancialAccountService
    {
        private readonly IFinancialAccountRepository _financialAccountRepository;
        private readonly IUserProfileService _userProfileService;

        public FinancialAccountService(IFinancialAccountRepository financialAccountRepository, IUserProfileService userProfileService)
        {
            this._financialAccountRepository = financialAccountRepository;
            this._userProfileService = userProfileService;
        }

        public async Task<List<FinancialAccountViewModel>> GetFinancialAccountsByUserId(string userId)
        {
            var accounts = await _financialAccountRepository.GetFinancialAccountsByUserId(Guid.Parse(userId));
            var accountViewModels = new List<FinancialAccountViewModel>();
            foreach (var account in accounts)
            {
                var accountViewModel = new FinancialAccountViewModel
                {
                    Id = account.Id,
                    AccountName = account.AccountName,
                    AccountNumber = account.AccountNumber,
                    InitialValue = account.InitialValue,
                    CurrentValue = account.CurrentValue,
                    AccountType = Enum.GetName<FinancialAccountType>(account.AccountType),
                    Currency = account.Currency,
                    UserAccountId = account.UserAccountId,
                };
                accountViewModels.Add(accountViewModel);
            }
            return accountViewModels;
        }

        public async Task<FinancialAccountViewModel?> GetFinancialAccount(string id)
        {
            var account = await _financialAccountRepository.GetFinancialAccount(Guid.Parse(id));
            if (account == null)
            {
                return null;
            }
            var accountViewModel = new FinancialAccountViewModel
            {
                Id = account.Id,
                AccountName = account.AccountName,
                AccountNumber = account.AccountNumber,
                InitialValue = account.InitialValue,
                CurrentValue = account.CurrentValue,
                AccountType = Enum.GetName<FinancialAccountType>(account.AccountType),
                Currency = account.Currency,
                UserAccountId = account.UserAccountId,
            };
            return accountViewModel;
        }

        public async Task<string> CreateFinancialAccount(FinancialAccountViewModel financialAccount)
        {
            var account = new FinancialAccount
            {
                Id = Guid.NewGuid(),
                AccountName = financialAccount.AccountName,
                AccountNumber = financialAccount.AccountNumber,
                InitialValue = financialAccount.CurrentValue, // InitialValue = CurrentValue
                CurrentValue = financialAccount.CurrentValue,
                AccountType = Enum.Parse<FinancialAccountType>(financialAccount.AccountType),
                Currency = financialAccount.Currency,
                UserAccountId = financialAccount.UserAccountId,
            };
            var created = await _financialAccountRepository.CreateFinancialAccount(account);
            return created.Id.ToString();
        }

        public async Task<bool> UpdateFinancialAccount(FinancialAccountViewModel financialAccount)
        {
            var account = new FinancialAccount
            {
                Id = financialAccount.Id,
                AccountName = financialAccount.AccountName,
                AccountNumber = financialAccount.AccountNumber,
                InitialValue = financialAccount.InitialValue,
                CurrentValue = financialAccount.CurrentValue,
                AccountType = Enum.Parse<FinancialAccountType>(financialAccount.AccountType),
                Currency = financialAccount.Currency,
                UserAccountId = financialAccount.UserAccountId,
            };
            return await _financialAccountRepository.UpdateFinancialAccount(account);
        }

        public async Task<bool> DeleteFinancialAccount(string id)
        {
            return await _financialAccountRepository.DeleteFinancialAccount(Guid.Parse(id));
        }
    }
}
