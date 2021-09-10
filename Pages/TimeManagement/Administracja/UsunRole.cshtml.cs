using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;

namespace TimeManagementApp.Pages.TimeManagement.Administracja
{
    public class DeleteRoleModel : BasePageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public EditDeleteViewModel DeleteModel { get; set; }

        public DeleteRoleModel(SignInManager<ApplicationUser> signInManager,
            AppDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager) : base(signInManager)
        {
            _signInManager = signInManager;
            _context = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [TempData]
        public string SuccessRoleDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(string? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var user = await _context.Roles.FindAsync(Id);

            if (user == null)
            {
                return NotFound();
            }
            return Page();

        }

        public async Task<IActionResult> OnPostAsync(string Id, ApplicationRole role)
        {
            if (Id == null)
            {
                return NotFound();
            }

            role = await _roleManager.FindByIdAsync(Id);
            if (role != null)
            {
                _context.Remove(role);
                await _context.SaveChangesAsync();
            }
            SuccessRoleDelete = "Grupa zosta³a usuniêta";
            return RedirectToPage("./Ustawienia");
        }
    }
}
