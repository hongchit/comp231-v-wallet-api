namespace v_wallet_api.ViewModels
{
    public class AuthenticateViewModel
    {
        public string PrimaryId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(PrimaryId) || !string.IsNullOrEmpty(Username) || !string.IsNullOrEmpty(Name) ||
                   !string.IsNullOrEmpty(Role);
        }
    }
}