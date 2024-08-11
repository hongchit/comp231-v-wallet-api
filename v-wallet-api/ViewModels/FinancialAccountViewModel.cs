using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using v_wallet_api.Models;

namespace v_wallet_api.ViewModels
{
    public class FinancialAccountViewModel
    {
        [SwaggerSchema(ReadOnly = true, Title = "ID of the record", Description = "example: e1cb23d0-6cbe-4a29-b586-bfa424bc93fd")]

        public string Id { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Currency { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public decimal Balance { get; set; }

        public decimal InitialBalance { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public FinancialAccountType? FinancialAccountType { get; set; } = new FinancialAccountType();

    }
}
