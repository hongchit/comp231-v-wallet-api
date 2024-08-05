using System.ComponentModel.DataAnnotations;

namespace v_wallet_api.ViewModels
{
    public class UserProfileViewModel
    {
        private string _email;

        public string Id { get; set; }

       [Required]
        public string Email
        {
            get { return _email; }
            set { _email = value.Trim().ToLower(); }
        }
        
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

        [Required]
        public required string Firstname { get; set; }

        [Required]
        public required string Lastname { get; set; }

        public DateOnly? Birthdate { get; set; }

        public string FullName => $"{Firstname} {Lastname}";
    }
}
