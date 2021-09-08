using System.ComponentModel.DataAnnotations;

namespace TimeManagementApp.App.ViewModels
{
    public class RegisterViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy format maila")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [MinLength(8, ErrorMessage = "Hasło musi mieć con. 8 znaków")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Hasła muszą być identyczne")]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string Lastname { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Wymagana jest akceptacja regulaminu")]
        public bool AcceptRules { get; set; }
    }
}
