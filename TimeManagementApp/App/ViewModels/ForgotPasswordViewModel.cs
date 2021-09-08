using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace TimeManagementApp.App.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy format maila")]
        public string Email { get; set; }

        public string Name { get; set; }
    }
}