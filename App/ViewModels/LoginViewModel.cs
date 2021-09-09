using System.ComponentModel.DataAnnotations;

namespace TimeManagementApp.App.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email jest wymagany")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
