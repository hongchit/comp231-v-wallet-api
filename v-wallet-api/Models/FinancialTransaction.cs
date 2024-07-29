namespace v_wallet_api.Models
{
    public class FinancialTransaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid AccountId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
