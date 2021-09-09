using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeManagementApp.App.Models;
using TimeManagementApp.App.Services;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;
using Xamarin.Forms;

namespace TimeManagementApp.Pages.TimeManagement.Administracja
{
    public class EdycjaUprawnienModel : BasePageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AppDbContext _context;

        public EdycjaUprawnienModel(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            AppDbContext context) : base(signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [BindProperty]
        public UserClaimsViewModel ClaimView { get; set; }
        [TempData]
        public string SuccessClaimAdd { get; set; }

        public async Task<IActionResult> OnGet(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            var roleName = await _roleManager.GetRoleNameAsync(role);
            var userInRole = await _userManager.GetUsersInRoleAsync(roleName);



            // UserManager service GetClaimsAsync method gets all the current claims of the user
            var existingUserClaims = await _roleManager.GetClaimsAsync(role);

            ClaimView =  new UserClaimsViewModel
            {
                RoleId = Id
            };

            // Loop through each claim we have in our application
            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                // If the user has the claim, set IsSelected property to true, so the checkbox
                // next to the claim is checked on the UI
                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    userClaim.IsSelected = true;
                }

                ClaimView.Claims.Add(userClaim);
            }

            return Page();

        }


        public async Task<IActionResult> OnPost(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            var roleName = await _roleManager.GetRoleNameAsync(role);
            var userInRole = await _userManager.GetUsersInRoleAsync(roleName);


            var RoleClaims = await _roleManager.GetClaimsAsync(role);
            if (RoleClaims != null)
            {
                foreach (var ClaimInRole in RoleClaims)
                {
                    var result = await _roleManager.RemoveClaimAsync(role, ClaimInRole);
                }
            }
            foreach (var user in userInRole)
            {
                var UserClaims = await _userManager.GetClaimsAsync(user);
                var deleteClaims = await _userManager.RemoveClaimsAsync(user, UserClaims);
            }

            foreach (var SelectedClaim in ClaimView.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType)))
            {
                var AddClaimToRole = await _roleManager.AddClaimAsync(role, SelectedClaim);
                foreach (var user in userInRole)
                {
                    var result = await _userManager.AddClaimAsync(user, SelectedClaim);
                }
            }

            SuccessClaimAdd = "Uprawnienia zosta³y zmienione";
            return RedirectToPage("/TimeManagement/Administracja/Ustawienia");

        }
    }
}
