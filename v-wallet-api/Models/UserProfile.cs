namespace v_wallet_api.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly? Birthdate { get; set; }
        public Guid UserAccountId { get; set; }
    }
}
