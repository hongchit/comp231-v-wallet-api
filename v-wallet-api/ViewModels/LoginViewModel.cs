using System.ComponentModel.DataAnnotations;

namespace v_wallet_api.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
