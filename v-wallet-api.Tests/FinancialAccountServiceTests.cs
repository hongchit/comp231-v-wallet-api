using Moq;
using v_wallet_api.Models;
using v_wallet_api.Repositories;
using v_wallet_api.Services;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Tests
{
    [TestClass]
    public class FinancialAccountServiceTests
    {
        private readonly Mock<IUserProfileRepository> _userProfileRepositoryMock;
        private readonly Mock<IFinancialAccountRepository> _financialAccountRepositoryMock;
        private readonly Mock<IFinancialAccountTypeRepository> _financialAccountTypeRepositoryMock;
        private readonly Mock<ICurrencyRepository> _currencyRepositoryMock;
        private readonly Mock<IUserProfileService> _userProfileServiceMock;
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly FinancialAccountService _financialAccountService;

        public FinancialAccountServiceTests()
        {
            // Create mock objects for dependencies
            _userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            _financialAccountRepositoryMock = new Mock<IFinancialAccountRepository>();
            _financialAccountTypeRepositoryMock = new Mock<IFinancialAccountTypeRepository>();
            _currencyRepositoryMock = new Mock<ICurrencyRepository>();
            _userProfileServiceMock = new Mock<IUserProfileService>();
            _categoryServiceMock = new Mock<ICategoryService>();

            // Create an instance of the FinancialAccountService class with the mock dependencies
            _financialAccountService = new FinancialAccountService(
                _userProfileRepositoryMock.Object,
                _financialAccountRepositoryMock.Object,
                _financialAccountTypeRepositoryMock.Object,
                _currencyRepositoryMock.Object,
                _userProfileServiceMock.Object,
                _categoryServiceMock.Object
            );
        }

        [TestMethod]
        public async Task GetFinancialAccountsById_ShouldReturnAccounts()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var financialAccount = new FinancialAccount
            {
                Id = Guid.NewGuid(),
                AccountName = "Account1",
                AccountNumber = "987654321",
                AccountType = new FinancialAccountType { Id = Guid.NewGuid(), Name = "Savings" },
                CurrencyId = new Guid("09cba794-33bd-4c78-a167-93bc4b593783"),
                InitialValue = 1000,
                CurrentValue = 1000,
            };

            var currencies = new List<Currency>
                    {
                        new Currency { Id = new Guid("09cba794-33bd-4c78-a167-93bc4b593783"), Symbol = "CAD", Country = "Canada", IsActive = true }
                    };

            // Setup mock repository methods
            _financialAccountRepositoryMock.Setup(repo =>
                repo.GetFinancialAccount(It.IsAny<Guid>())).ReturnsAsync(financialAccount);

            _currencyRepositoryMock.Setup(repo =>
                repo.GetAllCurrencies()).ReturnsAsync(currencies);

            _financialAccountRepositoryMock.Setup(repo =>
                repo.GetFinancialAccount(It.IsAny<Guid>())).ReturnsAsync(financialAccount);

            // Act
            var result = await _financialAccountService.GetFinancialAccountByAccountId(accountId.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Account1", result.Name);
            Assert.AreEqual("987654321", result.Number);
            Assert.AreEqual("Savings", result.Type);
            Assert.AreEqual("CAD", result.Currency);
            Assert.AreEqual(1000, result.InitialBalance);
            Assert.AreEqual(1000, result.Balance);
        }
        [TestMethod]
        public async Task GetFinancialTransactionsByAccountId_ShouldReturnTransactions()
        {

            // Arrange
            var accountId = Guid.NewGuid();
            var transactions = new List<FinancialTransaction>
            {
                new FinancialTransaction
                {
                    Id = new Guid("C87F4FD5-57BE-4867-B596-B6AC8254E8F9"),
                    TransactionType = "Expense",
                    AccountId = accountId,
                    CategoryId = Guid.NewGuid(),
                    Amount = 500,
                    Description = "Transaction 1",
                },
                new FinancialTransaction
                {
                    Id = new Guid("F4459E32-7C50-4945-94CE-0A39251E03F2"),
                    TransactionType = "Income",
                    AccountId = accountId,
                    CategoryId = Guid.NewGuid(),
                    Amount = 1000,
                    Description = "Transaction 2",
                }
            };

            // Mock the Category repository and assign valid categories in the FinancialTransaction CategoryId
            var categories = new List<Category>
            {
                new Category
                {
                    Id = transactions[0].CategoryId,
                    Name = "Food"
                },
                new Category
                {
                    Id = transactions[1].CategoryId,
                    Name = "Income"
                }
            };

            var categoryViewModels = categories.Select(c => new CategoryViewModel
            {
               Id = c.Id.ToString(),
               Type = Enum.Parse<CategoryType>(c.Name),
            }).ToList();

            _categoryServiceMock.Setup(repo =>
                    repo.GetCategories())
                .ReturnsAsync(categoryViewModels);


            _categoryServiceMock.Setup(repo =>
                    repo.GetCategories())
                .ReturnsAsync(categoryViewModels);


            // Setup mock repository method
            _financialAccountRepositoryMock.Setup(repo =>
                    repo.GetFinancialTransactions(It.IsAny<List<string>>()))
                .ReturnsAsync(transactions);


            // Act
            var result = await _financialAccountService.GetTransactionsByFinancialAccountId(accountId.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            Assert.AreEqual("Expense", result[0].TransactionInformation);
            Assert.AreEqual(accountId, result[0].AccountId);
            Assert.AreEqual("Food", result[0].CategoryName);

            Assert.AreEqual("Income", result[1].TransactionInformation);
            Assert.AreEqual(accountId, result[1].AccountId);
            Assert.AreEqual("Income", result[1].CategoryName);
        }
    }

}
