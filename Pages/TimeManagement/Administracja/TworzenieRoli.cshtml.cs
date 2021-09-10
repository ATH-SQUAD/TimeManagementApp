using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimeManagementApp.App.Models;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;

namespace TimeManagementApp.Pages.TimeManagement.Administracja
{
    public class CreateRoleModel : BasePageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AppDbContext _context;

        public CreateRoleModel(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            AppDbContext context) : base(signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        [BindProperty]
        public CreateRoleViewModel CreateRole { get; set; }
        [TempData]
        public string SuccessRoleCreation { get; set; }
        [BindProperty]
        public UserClaimsViewModel ClaimView { get; set; }

        public IActionResult OnGet()
        {
            ClaimView = new UserClaimsViewModel();

            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                ClaimView.Claims.Add(userClaim);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(string roleName)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole applicationRole = new ApplicationRole
                {
                    Name = CreateRole.Name
                };
                var roleExist = _roleManager.RoleExistsAsync(applicationRole.ToString());

                IdentityResult result = await _roleManager.CreateAsync(applicationRole);
                if (result.Succeeded) {
                    foreach (var SelectedClaim in ClaimView.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType)))
                    {
                        var AddClaimToRole = await _roleManager.AddClaimAsync(applicationRole, SelectedClaim);
                    }
                }
            }
            SuccessRoleCreation = "Grupa zosta³a utworzona";
            return RedirectToPage("./Ustawienia");
        }
    }
}
