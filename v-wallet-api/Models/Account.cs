namespace v_wallet_api.Models
{
    public class Account
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public AccountType AccountType { get; set; }
    }
}
