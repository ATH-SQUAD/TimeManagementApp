using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database.Models;


namespace TimeManagementApp.Pages.TimeManagement
{
    [AllowAnonymous]
    public class LogowanieModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogowanieModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(LoginViewModel.Email, LoginViewModel.Password, LoginViewModel.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToPage("./Start");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Attempt");
                }

                ModelState.AddModelError(string.Empty, "Email lub hasło są niepoprawne");
            }

            return Page();
        }
    }
}