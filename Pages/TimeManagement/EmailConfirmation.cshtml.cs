using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database.Models;

namespace TimeManagementApp.Pages.TimeManagement
{
    public class EmailConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EmailConfirmationModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public EmailConfirmationViewModel Confirm { get; set; }

        public IActionResult OnGet(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}