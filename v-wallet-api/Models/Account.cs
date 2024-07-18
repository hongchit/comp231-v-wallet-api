namespace v_wallet_api.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AccountType AccountType { get; set; }
    }
}
