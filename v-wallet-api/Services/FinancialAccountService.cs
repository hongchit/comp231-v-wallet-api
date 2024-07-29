using v_wallet_api.Models;
using v_wallet_api.Repositories;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Services
{
    public class FinancialAccountService : IFinancialAccountService
    {
        private readonly IFinancialAccountRepository _financialAccountRepository;
        private readonly IUserProfileService _userProfileService;
        private readonly ICategoryService _categoryService;

        public FinancialAccountService(IFinancialAccountRepository financialAccountRepository, IUserProfileService userProfileService, ICategoryService categoryService)
        {
            _financialAccountRepository = financialAccountRepository;
            _userProfileService = userProfileService;
            _categoryService = categoryService;
        }

        public async Task<FinancialTransactionViewModel?> GetTransactionByTransactionId(string transactionId)
        {
            var transaction = await _financialAccountRepository.GetFinancialTransaction(Guid.Parse(transactionId));

            if(transaction == null)
            {
                return null;
            }

            var userProfile = await _userProfileService.GetUserProfileByAccountId(transaction.AccountId.ToString());
            var category =
                (await _categoryService.GetCategories()).FirstOrDefault(x => x.Id.Equals(transaction.CategoryId.ToString()));

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

        public async Task<List<FinancialTransactionViewModel>> GetTransactionsByAccountId(string accountId)
        {
            var transactions = await _financialAccountRepository.GetFinancialTransactions(Guid.Parse(accountId));

            var sevenDayTransactions = transactions.Where(x => x.TransactionDate >= DateTime.Now.AddDays(-7)).ToList();

            var userProfile = await _userProfileService.GetUserProfileByAccountId(accountId);

            var categories = await _categoryService.GetCategories();

            var transactionViewModels = new List<FinancialTransactionViewModel>();

            foreach (var transaction in sevenDayTransactions)
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
    }
}
