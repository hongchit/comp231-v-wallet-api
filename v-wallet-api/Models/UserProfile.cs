namespace v_wallet_api.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public DateOnly? Birthday { get; set; }
        public Guid AccountId { get; set; }
    }
}
