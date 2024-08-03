using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using v_wallet_api.Models;

namespace v_wallet_api.ViewModels
{
    public class FinancialAccountViewModel
    {
        [SwaggerSchema(ReadOnly = true, Title = "ID of the record", Description = "example: e1cb23d0-6cbe-4a29-b586-bfa424bc93fd")]
        public Guid Id { get; set; }

        [Required]
        public string AccountName { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public decimal InitialValue { get; set; }

        [Required]
        public decimal CurrentValue { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public Guid UserProfileId { get; set; }
    }
}
