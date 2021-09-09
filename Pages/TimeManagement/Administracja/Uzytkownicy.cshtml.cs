using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeManagementApp.Database;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TimeManagementApp.App.ViewModels;
using System.Web.Http;

namespace TimeManagementApp.Pages.TimeManagement.Administracja
{
    [AllowAnonymous]
    public class UzytkownicyModel : BasePageModel
    {

        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IdentityDbContext<IdentityUser> identity;
        private readonly IServiceProvider serviceProvider;
        private readonly RoleManager<ApplicationRole> role;

        public UzytkownicyModel(
            SignInManager<ApplicationUser> signInManager,
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            IServiceProvider serviceProvider,
            RoleManager<ApplicationRole> role) : base(signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.serviceProvider = serviceProvider;
            this.role = role;
        }


        [TempData]
        public string SuccessChangePwMessage { get; set; }
        public UserListViewModel userList { get; set; }
        [BindProperty]
        public IList<ApplicationUser> Users { get; set; }
        public IList<ApplicationUserRole> userRoles { get; set; }
        public IList<ApplicationRole> Roles { get; set; }
        public string ErrorMessage { get; private set; }


        public async Task OnGet()
        {
            if (User.IsInRole("Master"))
            {
                Users = userManager.Users
                .Include(ur => ur.UserRoles)
                .ThenInclude(r => r.Role).Where(x => x.UserRoles.Any(s => s.Role.Name != "Master"))
                .AsNoTracking()
                .ToList();
            }
            else
            {
                Users = userManager.Users
                .Include(ur => ur.UserRoles)
                .ThenInclude(r => r.Role).Where(x => x.UserRoles.Any(s => s.Role.Name != "Master" && s.Role.Name != "Admin"))
                .AsNoTracking()
                .ToList();
            }
        }

        public async Task OnPostAsync(string searchString)
        {

        }

        public async Task<JsonResult> OnPostActivate(bool isChecked, string userId, ApplicationUser user, string token)
        {
            isChecked = true;
            user = await _context.Users.FindAsync(userId);
            token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            if (isChecked == true)
            {
                user.EmailConfirmed = true;
            }
            else
            {
                user.EmailConfirmed = false;
            }
            await userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();
            SuccessChangePwMessage = "Dane zostały zmienione!";
            return new JsonResult(new { ActivateRaw = true });
        }
    }
}