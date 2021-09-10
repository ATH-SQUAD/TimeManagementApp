using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeManagementApp.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.Pages.Shared
{
    public class BasePageModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public BasePageModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToPage("/TimeManagement/Logowanie");
        }
    }
}
