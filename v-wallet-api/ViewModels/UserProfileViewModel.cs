using System.ComponentModel.DataAnnotations;

namespace v_wallet_api.ViewModels
{
    public class UserProfileViewModel
    {
        [Required]
        public required string Email { get; set; }

        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

        [Required]
        public required string Firstname { get; set; }

        [Required]
        public required string Lastname { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"\d{4}-\d{2}-\d{2}", ErrorMessage = "Invalid date format. Please use yyyy-MM-dd.")]
        public string? Birthdate { get; set; }
    }
}
