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
                id = transaction.Id,
                Amount = transaction.Amount,
                Description = transaction.Description,
                TransactionType = transaction.TransactionType,
                TransactionInformation = Enum.GetName(typeof(TransactionType), transaction.TransactionType),
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
            var transactions = await _financialAccountRepository.GetFinancialTransactions(Guid.Parse(accountId));

            var userProfile = await _userProfileService.GetUserProfileByAccountId(accountId);

            var categories = await _categoryService.GetCategories();

            var transactionViewModels = new List<FinancialTransactionViewModel>();

            foreach (var transaction in transactions)
            {
                var category = categories.FirstOrDefault(x => x.Id.Equals(transaction.CategoryId));

                var transactionViewModel = new FinancialTransactionViewModel
                {
                    id = transaction.Id,
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    TransactionType = transaction.TransactionType,
                    TransactionInformation = Enum.GetName(typeof(TransactionType), transaction.TransactionType),
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

        public async Task<List<FinancialTransactionViewModel>> GetTransactionsByUserId(string userId)
        {
            var transactions = await _financialAccountRepository.GetFinancialTransactions(Guid.Parse(userId));

            var sevenDaysTransactions = transactions.Where(x => x.TransactionDate >= DateTime.Now.AddDays(-7)).ToList();

            var userProfile = await _userProfileService.GetUserProfileByAccountId(userId);
            var categories = (await _categoryService.GetCategories());

            var transactionResults = new List<FinancialTransactionViewModel>();

            foreach (var transaction in sevenDaysTransactions)
            {
                var transactionViewModel = new FinancialTransactionViewModel
                {
                    id = transaction.Id,
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    TransactionType = transaction.TransactionType,
                    TransactionInformation = Enum.GetName(typeof(TransactionType), transaction.TransactionType),
                    TransactionDate = transaction.TransactionDate,
                    AccountId = transaction.AccountId,
                    AccountName = userProfile.FullName,
                    CategoryId = transaction.CategoryId,
                    CategoryName = categories.FirstOrDefault(x => x.Id.Equals(transaction.CategoryId)).Name
                };

                transactionResults.Add(transactionViewModel);
            }

            return transactionResults;
        }

        public async Task<string> CreateTransaction(FinancialTransactionViewModel transaction)
        {
            var newTransaction = new FinancialTransaction
            {
                Amount = transaction.Amount,
                Description = transaction.Description,
                TransactionType = Enum.Parse<TransactionType>(transaction.TransactionInformation),
                TransactionDate = transaction.TransactionDate,
                AccountId = transaction.AccountId,
                CategoryId = transaction.CategoryId
            };

            var createdTransaction = await _financialAccountRepository.CreateFinancialTransaction(newTransaction);

            return createdTransaction.Id.ToString();
        }

        public async Task<List<FinancialAccountViewModel>> GetFinancialAccountsByUserId(string userId)
        {
            try
            {
                var financialAccounts = await _financialAccountRepository.GetFinancialAccountsByUserId(userId);

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
    }
}
