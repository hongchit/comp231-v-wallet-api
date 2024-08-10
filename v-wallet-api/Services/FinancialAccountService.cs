using System.Diagnostics;
using v_wallet_api.Models;
using v_wallet_api.Repositories;
using v_wallet_api.ViewModels;
using FinancialAccountType = v_wallet_api.ViewModels.FinancialAccountType;

namespace v_wallet_api.Services
{
    public class FinancialAccountService : IFinancialAccountService
    {
        private readonly IFinancialAccountRepository _financialAccountRepository;
        private readonly IUserProfileService _userProfileService;
        private readonly ICategoryService _categoryService;

        public FinancialAccountService(IFinancialAccountRepository financialAccountRepository,
            IUserProfileService userProfileService, ICategoryService categoryService)
        {
            _financialAccountRepository = financialAccountRepository;
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

            var categories = await _categoryService.GetCategories();

            var transactionViewModels = new List<FinancialTransactionViewModel>();

            foreach (var transaction in transactions)
            {
                var category = categories.FirstOrDefault(x => x.Id.Equals(transaction.CategoryId.ToString()));

                var transactionViewModel = new FinancialTransactionViewModel
                {
                    Id = transaction.Id.ToString(),
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    TransactionType = Enum.Parse<TransactionType>(transaction.TransactionType),
                    TransactionInformation = transaction.TransactionType,
                    TransactionDate = transaction.TransactionDate,
                    AccountId = transaction.AccountId,
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

                var financialAccountsViewModel = new List<FinancialAccountViewModel>();

                foreach (var financialAccount in financialAccounts)
                {

                    financialAccountsViewModel.Add(new FinancialAccountViewModel
                    {
                        Id = financialAccount.Id.ToString(),
                        Name = financialAccount.AccountName,
                        Number = financialAccount.AccountNumber,
                        Type = Enum.Parse<FinancialAccountType>(financialAccount.AccountType.Name, true),
                        FinancialAccountType = financialAccount.AccountType.Name,
                        Balance = financialAccount.CurrentValue
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

        public async Task<FinancialAccountViewModel?> GetFinancialAccountByAccountId(string financialAccountId)
        {
            var existingAccount =
                (await _financialAccountRepository.GetFinancialAccountsById(new List<string> { financialAccountId }))
                .FirstOrDefault();

            if (existingAccount == null) {
                return null;
            }

            return new FinancialAccountViewModel
            {
                Id = existingAccount.Id.ToString(),
                Name = existingAccount.AccountName,
                Number = existingAccount.AccountNumber,
                Type = Enum.Parse<FinancialAccountType>(existingAccount.AccountType.Name, true),
                FinancialAccountType = existingAccount.AccountType.Name,
                Balance = existingAccount.CurrentValue,
                InitialBalance = existingAccount.InitialValue
            };
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
    }
}
