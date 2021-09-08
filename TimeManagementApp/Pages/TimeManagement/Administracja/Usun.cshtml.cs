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
    public class UsunModel : BasePageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditDeleteViewModel DeleteModel { get; set; }

        public UsunModel(SignInManager<ApplicationUser> signInManager,
            AppDbContext dbContext,
            UserManager<ApplicationUser> userManager) : base(signInManager)
        {
            _signInManager = signInManager;
            _context = dbContext;
            _userManager = userManager;
        }
        [BindProperty]
        public IList<ApplicationUser> Users { get; set; }
        [TempData]
        public string SuccessDeleteMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(Id);

            if (user == null)
            {
                return NotFound();
            }
            return Page();

        }
        public async Task<IActionResult> OnPostAsync(string Id, ApplicationUser user)
        {
            if (Id == null)
            {
                return NotFound();
            }

            user = await  _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
            }
            SuccessDeleteMessage = "U¿ytkownik zosta³ usuniêty";
            return RedirectToPage("./Uzytkownicy");
        }

    }
}
