
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeManagementApp.Database;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TimeManagementApp.Pages.TimeManagement.Administracja
{
    public class UstawieniaModel : BasePageModel
    {
        public SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UstawieniaModel(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            AppDbContext dbContext,
            RoleManager<ApplicationRole> roleManager)
            : base(signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = dbContext;
            _roleManager = roleManager;
        }
        [BindProperty]
        public IList<ApplicationRole> Roles { get; set; }

        public IList<string> Claims { get; set; }

        public async Task OnGet()
        {
            Roles = _context.Roles
             .Where(x => x.Name != "Master")
             .AsNoTracking()
             .ToList();


        }
    }
}