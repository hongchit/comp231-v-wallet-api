using System.ComponentModel.DataAnnotations;

namespace v_wallet_api.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
