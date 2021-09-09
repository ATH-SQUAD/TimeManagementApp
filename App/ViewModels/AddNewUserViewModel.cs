using TimeManagementApp.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.ViewModels
{
    public class AddNewUserViewModel
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
        [Display(Name = "Selected Role")]
        public string SelectedRole { get; set; }
        public IEnumerable<ApplicationRole> Roles { get; set; }
    }
}
