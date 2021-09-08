using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.ViewModels
{
    public class UpdateUserDataViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Nieprawidłowy format maila")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string EditRole { get; set; }
        public bool ActiveAccount { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Hasła muszą być identyczne")]
        public string ConfirmPassword { get; set; }

    }
}
