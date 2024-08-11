using System.Diagnostics;
using v_wallet_api.Models;
using v_wallet_api.Repositories;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public class FinancialAccountService : IFinancialAccountService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IFinancialAccountRepository _financialAccountRepository;
        private readonly IFinancialAccountTypeRepository _financialAccountTypeRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUserProfileService _userProfileService;
        private readonly ICategoryService _categoryService;

        public FinancialAccountService(
            IUserProfileRepository userProfileRepository,
            IFinancialAccountRepository financialAccountRepository,
            IFinancialAccountTypeRepository financialAccountTypeRepository,
            ICurrencyRepository currencyRepository,
            IUserProfileService userProfileService, ICategoryService categoryService)
        {
            _userProfileRepository = userProfileRepository;
            _financialAccountRepository = financialAccountRepository;
            _financialAccountTypeRepository = financialAccountTypeRepository;
            _currencyRepository = currencyRepository;
            _userProfileService = userProfileService;
            _categoryService = categoryService;
        }

        public async Task<FinancialTransactionViewModel?> GetTransactionByTransactionId(string transactionId)
        {
            var transaction = await _financialAccountRepository.GetFinancialTransaction(Guid.Parse(transactionId));

            if (transaction == null)
            {
                return null;
            }

            var userProfile = await _userProfileService.GetUserProfileByAccountId(transaction.AccountId.ToString());
            var category =
                (await _categoryService.GetCategories()).FirstOrDefault(x =>
                    x.Id.Equals(transaction.CategoryId.ToString()));

            var transactionViewModel = new FinancialTransactionViewModel
            {
                Id = transaction.Id.ToString(),
                Amount = transaction.Amount,
                Description = transaction.Description,
                TransactionType = Enum.Parse<TransactionType>(transaction.TransactionType),
                TransactionInformation = transaction.TransactionType,
                TransactionDate = transaction.TransactionDate,
                AccountId = transaction.AccountId,
                AccountName = userProfile.FullName,
                CategoryId = transaction.CategoryId,
                CategoryName = category.Name
            };

            return transactionViewModel;
        }

        public async Task<List<FinancialTransactionViewModel>> GetTransactionsByFinancialAccountId(string accountId)
        {
            var transactions =
                await _financialAccountRepository.GetFinancialTransactions(new List<string> { accountId });

            var userProfile = await _userProfileService.GetUserProfileByAccountId(accountId);

            var categories = await _categoryService.GetCategories();

            var transactionViewModels = new List<FinancialTransactionViewModel>();

            foreach (var transaction in transactions)
            {
                var category = categories.FirstOrDefault(x => x.Id.Equals(transaction.CategoryId));

                var transactionViewModel = new FinancialTransactionViewModel
                {
                    Id = transaction.Id.ToString(),
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    TransactionType = Enum.Parse<TransactionType>(transaction.TransactionType),
                    TransactionInformation = transaction.TransactionType,
                    TransactionDate = transaction.TransactionDate,
                    AccountId = transaction.AccountId,
                    AccountName = userProfile.FullName,
                    CategoryId = transaction.CategoryId,
                    CategoryName = category.Name
                };

                transactionViewModels.Add(transactionViewModel);
            }

            return transactionViewModels;
        }

        public async Task<List<FinancialTransactionViewModel>> GetTransactionsByUserProfileId(string userId)
        {
            var financialAccounts = await _financialAccountRepository.GetFinancialAccountsByUserProfileId(userId);

            var accountIds = financialAccounts.Select(x => x.Id.ToString()).ToList();
            var financialTransactions = await _financialAccountRepository.GetFinancialTransactions(accountIds);

            var sevenDaysTransactions = financialTransactions.Where(x => x.TransactionDate >= DateTime.Now.AddDays(-7)).ToList();

            var categories = (await _categoryService.GetCategories());

            var transactionResults = new List<FinancialTransactionViewModel>();

            foreach (var transaction in sevenDaysTransactions)
            {
                var financialAccount = financialAccounts.First(x => x.Id == transaction.AccountId);

                var transactionViewModel = new FinancialTransactionViewModel
                {
                    Id = transaction.Id.ToString(),
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    TransactionType = Enum.Parse<TransactionType>(transaction.TransactionType),
                    TransactionInformation = transaction.TransactionType,
                    TransactionDate = transaction.TransactionDate,
                    AccountId = financialAccount.Id,
                    AccountName = financialAccount.AccountName,
                    CategoryId = transaction.CategoryId,
                    CategoryName = categories.FirstOrDefault(x => x.Id.Equals(transaction.CategoryId.ToString())).Name
                };

                transactionResults.Add(transactionViewModel);
            }

            return transactionResults;
        }

        public async Task<string> CreateTransaction(FinancialTransactionViewModel transaction)
        {
            var categories = await _categoryService.GetCategories();

            var selectedCategory = categories.FirstOrDefault(x => x.Name == transaction.CategoryName);

            var newTransaction = new FinancialTransaction
            {
                Amount = transaction.Amount,
                Description = transaction.Description,
                TransactionType = transaction.TransactionInformation,
                TransactionDate = transaction.TransactionDate,
                AccountId = transaction.AccountId,
                CategoryId = Guid.Parse(selectedCategory.Id)
            };

            var createdTransaction = await _financialAccountRepository.CreateFinancialTransaction(newTransaction);

            await UpdateFinancialAccountBalance(transaction.AccountId.ToString(), transaction.Amount,
                Enum.Parse<TransactionType>(transaction.TransactionInformation));

            return createdTransaction.Id.ToString();
        }

        public async Task<List<FinancialAccountViewModel>> GetFinancialAccountsByUserId(string userId)
        {
            try
            {
                var financialAccounts = await _financialAccountRepository.GetFinancialAccountsByUserProfileId(userId);
                var allCurrencies = await _currencyRepository.GetALlCurrencies();

                var financialAccountsViewModel = new List<FinancialAccountViewModel>();

                foreach (var financialAccount in financialAccounts)
                {
                    var currency = allCurrencies.FirstOrDefault(c => c.Id == financialAccount.CurrencyId);
                    financialAccountsViewModel.Add(new FinancialAccountViewModel
                    {
                        Id = financialAccount.Id.ToString(),
                        Name = financialAccount.AccountName,
                        Number = financialAccount.AccountNumber,
                        Type = financialAccount.AccountType.Name,
                        Currency = currency.Symbol,
                        Balance = financialAccount.CurrentValue,
                        FinancialAccountType = financialAccount.AccountType,
                    });
                }

                return financialAccountsViewModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        private async Task UpdateFinancialAccountBalance(string accountId, decimal price, TransactionType type)
        {
            var existingAccount =
                (await _financialAccountRepository.GetFinancialAccountsById(new List<string> { accountId }))
                .FirstOrDefault();

            if (existingAccount == null)
            {
                return;
            }

            switch (type)
            {
                case TransactionType.Income:
                    existingAccount.CurrentValue += price;
                    break;
                case TransactionType.Expense:
                default:
                    existingAccount.CurrentValue -= price;
                    break;
            }

            await _financialAccountRepository.UpdateFinancialAccount(existingAccount);
        }

        public async Task<FinancialAccountViewModel?> GetFinancialAccountByAccountId(string financialAccountId)
        {
            var financialAccount = await _financialAccountRepository.GetFinancialAccount(Guid.Parse(financialAccountId));
            if (financialAccount == null)
            {
                return null;
            }
            var allCurrencies = await _currencyRepository.GetALlCurrencies();
            var currency = allCurrencies.FirstOrDefault(c => c.Id == financialAccount.CurrencyId);
            var financialAccountViewModel = new FinancialAccountViewModel
            {
                Id = financialAccount.Id.ToString(),
                Name = financialAccount.AccountName,
                Number = financialAccount.AccountNumber,
                Type = financialAccount.AccountType.Name,
                Currency = currency.Symbol,
                Balance = financialAccount.CurrentValue,
                FinancialAccountType = financialAccount.AccountType,
            };
            return financialAccountViewModel;
        }
        public async Task<string> CreateFinancialAccount(string userProfileId, FinancialAccountViewModel financialAccountVM)
        {
            var userProfile = await _userProfileRepository.GetUserProfile(Guid.Parse(userProfileId));
            if (userProfile == null) {
                throw new KeyNotFoundException($"Unknown user profile ID: {userProfileId}");
            }
            var allCurrencies = await _currencyRepository.GetALlCurrencies();
            var currency = allCurrencies.FirstOrDefault(c => c.Symbol == financialAccountVM.Currency);
            if (currency == null)
            {
                throw new KeyNotFoundException($"Unsupported currency: {financialAccountVM.Currency}");
            }
            var accountType = await _financialAccountTypeRepository.GetFinancialAccountTypeByName(financialAccountVM.Type);
            if (accountType == null) {
                throw new KeyNotFoundException($"Unsupported Account Type: {financialAccountVM.Type}");
            }
            var financialAccount = new FinancialAccount
            {
                Id = Guid.NewGuid(),
                AccountName = financialAccountVM.Name,
                AccountNumber = financialAccountVM.Number,
                InitialValue = financialAccountVM.Balance,
                CurrentValue = financialAccountVM.Balance,
                AccountTypeId = accountType.Id,
                CurrencyId = currency.Id,
                UserProfileId = userProfile.Id,
                AccountType = accountType,
            };
            var created = await _financialAccountRepository.CreateFinancialAccount(financialAccount);
            return created.Id.ToString();
        }
        public async Task UpdateFinancialAccount(string userProfileId, FinancialAccountViewModel financialAccountVM)
        {
            var userProfile = await _userProfileRepository.GetUserProfile(Guid.Parse(userProfileId));
            if (userProfile == null || userProfile.Id != Guid.Parse(userProfileId))
            {
                throw new KeyNotFoundException($"Unknown user profile ID: {userProfileId}");
            }
            var financialAccount = await _financialAccountRepository.GetFinancialAccount(Guid.Parse(financialAccountVM.Id));
            if (financialAccount == null)
            {
                throw new KeyNotFoundException($"Unknown financial account: {financialAccountVM.Id}");
            }
            var allCurrencies = await _currencyRepository.GetALlCurrencies();
            var currency = allCurrencies.FirstOrDefault(c => c.Symbol == financialAccountVM.Currency);
            if (currency == null)
            {
                throw new KeyNotFoundException($"Unsupported currency: {financialAccountVM.Currency}");
            }
            var accountType = await _financialAccountTypeRepository.GetFinancialAccountTypeByName(financialAccountVM.Type);
            if (accountType == null)
            {
                throw new KeyNotFoundException($"Unsupported Account Type ID: {financialAccountVM.Type}");
            }
            financialAccount.AccountName = financialAccountVM.Name;
            financialAccount.AccountNumber = financialAccountVM.Number;
            financialAccount.InitialValue = financialAccountVM.Balance;
            financialAccount.CurrentValue = financialAccountVM.Balance;
            financialAccount.AccountTypeId = accountType.Id;
            financialAccount.CurrencyId = currency.Id;
            financialAccount.AccountType = accountType;
            await _financialAccountRepository.UpdateFinancialAccount(financialAccount);
        }
        public async Task DeleteFinancialAccount(string userProfileId, string id)
        {
            var userProfile = await _userProfileRepository.GetUserProfile(Guid.Parse(userProfileId));
            if (userProfile == null || userProfile.Id != Guid.Parse(userProfileId))
            {
                throw new KeyNotFoundException($"Unknown user profile ID: {userProfileId}");
            }
            var financialAccount = await _financialAccountRepository.GetFinancialAccount(Guid.Parse(id));
            if (financialAccount == null)
            {
                throw new KeyNotFoundException($"Unknown financial account: {id}");
            }
            await _financialAccountRepository.DeleteFinancialAccount(financialAccount);
        }
    }
}
