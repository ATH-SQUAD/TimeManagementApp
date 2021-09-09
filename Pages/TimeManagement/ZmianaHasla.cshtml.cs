using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database.Models;
using Microsoft.AspNetCore.Authorization;


namespace TimeManagementApp.Pages.TimeManagement
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {

        public ResetPasswordViewModel reset { get; set; }
        [TempData]
        public string ResetPassMessage { get; set; }

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnPostAsync(string userId, string token, ResetPasswordViewModel reset)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            //var user = await GetCurrentUser();
            //string userEmail = user.Email;
            //string userId = user.Id;
            var user = await _userManager.FindByEmailAsync(userId);
            if (user == null)
            {
                return Redirect("/ResetPassword");
            }
            //var result = await _userManager.ResetPasswordAsync(user, token, Reset.Password);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, token, reset.NewPassword);
            if (result.Succeeded)
            {
                TempData["ResetPassMessage"] = "Has³o zosta³o zmienione!";
                return RedirectToPage("./Logowanie");
            }

            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError(string.Empty, error.Description);
            //}
            return Page();
        }
    }
}
