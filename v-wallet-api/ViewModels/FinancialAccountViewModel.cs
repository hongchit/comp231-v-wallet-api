using System.ComponentModel.DataAnnotations;

namespace v_wallet_api.ViewModels
{
    public class FinancialAccountViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string AccountName { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public decimal InitialValue { get; set; }

        public decimal CurrentValue { get; set; }

        [Required]
        public Guid AccountTypeId { get; set; }

        [Required]
        public Guid CurrencyId { get; set; }

        [Required]
        public Guid UserProfileId { get; set; }
    }
}
