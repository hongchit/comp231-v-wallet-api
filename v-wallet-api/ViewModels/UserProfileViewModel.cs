using System.ComponentModel.DataAnnotations;

namespace v_wallet_api.ViewModels
{
    public class UserProfileViewModel
    {
        public string? Id { get; set; }
        public string Email { get; set; }

        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public DateTime? Birthdate { get; set; }

        public string FullName => $"{Firstname} {Lastname}";
    }
}
