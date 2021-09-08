using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;

namespace TimeManagementApp.Pages.TimeManagement.Administracja
{
    [AllowAnonymous]
    public class NewUserModel : BasePageModel
    {
        private readonly ILogger<RejestracjaModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public NewUserModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RejestracjaModel> logger,
            AppDbContext context,
            RoleManager<ApplicationRole> roleManager) : base(signInManager)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
        }

        [BindProperty]
        public AddNewUserViewModel AddModel { get; set; }
        [BindProperty]
        public string SetRoles { get; set; }
        [BindProperty]
        public List<SelectListItem> RolesToChoose { get; set; }
        [BindProperty]
        public string RoleId { get; set; }
        [BindProperty]
        public RoleViewModel roleModel { get; set; }
        [TempData]
        public string SuccessCreationMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            //RolesToChoose = _context.Roles.Select(r =>
            //                     new SelectListItem
            //                     {
            //                         Value = r.Id,
            //                         Text = r.Name
            //                     }).ToList();

            var assignableRoles = _roleManager.Roles.ToList();
            if (User.IsInRole("Master"))
            {
                assignableRoles.RemoveAt(assignableRoles.IndexOf(await _roleManager.FindByNameAsync("Master")));
            }
            else
            {
                assignableRoles.RemoveAt(assignableRoles.IndexOf(await _roleManager.FindByNameAsync("Admin")));
                assignableRoles.RemoveAt(assignableRoles.IndexOf(await _roleManager.FindByNameAsync("Master")));
            }
            ViewData["Name"] = new SelectList(assignableRoles, "Name", "Name");
            ViewData["UserName"] = new SelectList(_userManager.Users, "UserName", "UserName");

            return Page();
        }


        public async Task<IActionResult> OnPost(IdentityResult createUser, string token, ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                user = new ApplicationUser
                {
                    UserName = AddModel.Email,
                    Email = AddModel.Email,
                    Firstname = AddModel.Firstname,
                    Lastname = AddModel.Lastname,
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.CurrentCulture)
                };

                createUser = await _userManager.CreateAsync(user, AddModel.Password);


                //var role = AddModel.SelectedRole;

                if (createUser.Succeeded)
                {
                    //if (User.IsInRole("Master"))
                    //{
                    //    await _userManager.AddToRoleAsync(user, roleModel.Name);
                    //}
                    //if (User.IsInRole("Admin"))
                    //{
                    //    await _userManager.AddToRoleAsync(user, "U¿ytkownik");
                    //}
                    var AddToRole = await _userManager.AddToRoleAsync(user, roleModel.Name);
                    if (AddToRole.Succeeded)
                    {
                        var role = await _roleManager.FindByNameAsync(roleModel.Name);
                        var RoleClaims = await _roleManager.GetClaimsAsync(role);
                        await _userManager.AddClaimsAsync(user, RoleClaims);
                    }
                    token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    await _userManager.ConfirmEmailAsync(user, token);
                    SuccessCreationMessage = "Utworzono nowego u¿ytkownika";
                    return RedirectToPage("/TimeManagement/Administracja/Uzytkownicy");

                }
                ModelState.AddModelError(string.Empty, "Podany Email jest ju¿ w u¿yciu");
            }

            SuccessCreationMessage = "Utworzono nowego u¿ytkownika";
            return Page();
        }


        private void AddErrors(IdentityResult result)
        {
            throw new NotImplementedException();
        }
    }
}
