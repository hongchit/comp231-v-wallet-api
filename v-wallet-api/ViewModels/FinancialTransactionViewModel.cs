using v_wallet_api.Models;

namespace v_wallet_api.ViewModels
{
    public class FinancialTransactionViewModel
    {
        public string? Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public string? TransactionInformation { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
