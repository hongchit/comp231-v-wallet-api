namespace v_wallet_api.Models
{
    public class FinancialAccount
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public decimal InitialValue { get; set; }
        public decimal CurrentValue { get; set; }
        public FinancialAccountType AccountType { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid UserProfileId { get; set; }
    }
}
