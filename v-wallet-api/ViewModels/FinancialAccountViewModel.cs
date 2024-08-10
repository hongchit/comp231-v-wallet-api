using System.Security;
using v_wallet_api.Models;

namespace v_wallet_api.ViewModels
{
    public class FinancialAccountViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public FinancialAccountType Type { get; set; }
        public string FinancialAccountType { get; set; }
        public decimal Balance { get; set; }
        public decimal InitialBalance { get; set; }
    }
}
