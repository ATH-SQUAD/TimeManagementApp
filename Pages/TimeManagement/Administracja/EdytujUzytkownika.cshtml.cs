using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeManagementApp.App.Models;
using TimeManagementApp.App.Services;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;

namespace TimeManagementApp.Pages.TimeManagement.Administracja
{
    public class EditModel : BasePageModel
    {
        public SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public EditModel(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            AppDbContext dbContext,
            RoleManager<ApplicationRole> roleManager) : base(signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = dbContext;
            _roleManager = roleManager;
        }
        [BindProperty]
        public UpdateUserDataViewModel edit { get; set; }
        [BindProperty]
        public List<SelectListItem> RolesToChoose { get; set; }
        [BindProperty]
        public string SetRole { get; set; }
        [BindProperty]
        public string RoleId { get; set; }
        [BindProperty]
        public RoleViewModel roleModel { get; set; }
        [BindProperty]
        public IList<ApplicationUser> Users { get; set; }
        [TempData]
        public string SuccessEditMessage { get; set; }

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


            //RolesToChoose = new SelectList(sRoles, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnEditAccountAsync(string id, List<ApplicationUser> user, ApplicationUser userToUpdate)
        {
            userToUpdate = await _context.Users.FindAsync(id);

            if (userToUpdate == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //userToUpdate.Email = edit.Email;
                //userToUpdate.UserName = edit.Email;
                //userToUpdate.NormalizedUserName = edit.Email;
                //userToUpdate.NormalizedEmail = edit.Email;
                if (edit.Email == null)
                {
                    edit.Email = userToUpdate.Email;
                    edit.Email = userToUpdate.UserName;
                    edit.Email = userToUpdate.NormalizedEmail;
                    edit.Email = userToUpdate.NormalizedUserName;
                }
                else
                {
                    userToUpdate.Email = edit.Email;
                    userToUpdate.UserName = edit.Email;
                    userToUpdate.NormalizedUserName = edit.Email;
                    userToUpdate.NormalizedEmail = edit.Email;
                }

                if (edit.FirstName == null)
                {
                    edit.FirstName = userToUpdate.Firstname;
                }
                else
                {
                    userToUpdate.Firstname = edit.FirstName;
                }

                if (edit.LastName == null)
                {
                    edit.LastName = userToUpdate.Lastname;
                }
                else
                {
                    userToUpdate.Lastname = edit.LastName;
                }

                if (edit.PhoneNumber == null)
                {
                    edit.PhoneNumber = userToUpdate.PhoneNumber;
                }
                else
                {
                    userToUpdate.PhoneNumber = edit.PhoneNumber;
                }
                await _userManager.UpdateAsync(userToUpdate);
                await _context.SaveChangesAsync();
            }
                return RedirectToPage("/TimeManagement/Administracja/Uzytkownicy");
        }
        public async Task<IActionResult> OnPostAsync(string id, List<ApplicationUser> user, ApplicationUser userToUpdate)
        {
            userToUpdate = await _context.Users.FindAsync(id);

            if (userToUpdate == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //userToUpdate.Email = edit.Email;
                //userToUpdate.UserName = edit.Email;
                //userToUpdate.NormalizedUserName = edit.Email;
                //userToUpdate.NormalizedEmail = edit.Email;
                if (edit.Email == null)
                {
                    edit.Email = userToUpdate.Email;
                    edit.Email = userToUpdate.UserName;
                    edit.Email = userToUpdate.NormalizedEmail;
                    edit.Email = userToUpdate.NormalizedUserName;
                }
                else
                {
                    userToUpdate.Email = edit.Email;
                    userToUpdate.UserName = edit.Email;
                    userToUpdate.NormalizedUserName = edit.Email;
                    userToUpdate.NormalizedEmail = edit.Email;
                }

                if (edit.FirstName == null)
                {
                    edit.FirstName = userToUpdate.Firstname;
                }
                else
                {
                    userToUpdate.Firstname = edit.FirstName;
                }

                if (edit.LastName == null)
                {
                    edit.LastName = userToUpdate.Lastname;
                }
                else
                {
                    userToUpdate.Lastname = edit.LastName;
                }

                if (edit.PhoneNumber == null)
                {
                    edit.PhoneNumber = userToUpdate.PhoneNumber;
                }
                else
                {
                    userToUpdate.PhoneNumber = edit.PhoneNumber;
                }

                if (edit.NewPassword != null)
                {
                    if(edit.NewPassword == edit.ConfirmPassword)
                    {
                        userToUpdate.PasswordHash = _userManager.PasswordHasher.HashPassword(userToUpdate, edit.NewPassword);
                    }
                }
                else
                {
                    edit.NewPassword = userToUpdate.PasswordHash;
                }


                if (roleModel.Name != null)
                {
                    if (await _roleManager.RoleExistsAsync(roleModel.Name))
                    {
                        var CurrentRole = await _userManager.GetRolesAsync(userToUpdate);
                        await _userManager.RemoveFromRolesAsync(userToUpdate, CurrentRole);
                        await _userManager.UpdateSecurityStampAsync(userToUpdate);
                        var result = await _userManager.AddToRoleAsync(userToUpdate, roleModel.Name);
                        if (result.Succeeded)
                        {
                            var UserClaims = await _userManager.GetClaimsAsync(userToUpdate);

                            var role = await _roleManager.FindByNameAsync(roleModel.Name);
                            var RoleClaims = await _roleManager.GetClaimsAsync(role);

                            await _userManager.RemoveClaimsAsync(userToUpdate, UserClaims);

                            await _userManager.AddClaimsAsync(userToUpdate, RoleClaims);

                            ViewData["Message"] = $@"User {roleModel.UserName} was assigned the {roleModel.Name} role.";
                        }
                    }
                }

                if (userToUpdate.EmailConfirmed == false)
                {
                    if (edit.ActiveAccount == true)
                    {
                        userToUpdate.EmailConfirmed = true;
                    }
                    else
                    {
                        userToUpdate.EmailConfirmed = false;
                    }

                }
                await _userManager.UpdateAsync(userToUpdate);
                await _context.SaveChangesAsync();
            }
            SuccessEditMessage = "Dane zostały zmienione!";
            return RedirectToPage("/TimeManagement/Administracja/Uzytkownicy");


            //if (await TryUpdateModelAsync<EditDeleteViewModel>(userToUpdate, "user", edit.Firstname = model.FirstName, edit.Lastname = model.LastName, edit.Email = model.Email).ToString())
            //{
            //    await _context.SaveChangesAsync();
            //    return RedirectToPage("./U�ytkownicy");
            //}
        }
    }
}
