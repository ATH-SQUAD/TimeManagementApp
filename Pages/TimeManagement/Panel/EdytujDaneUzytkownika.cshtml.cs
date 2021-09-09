using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimeManagementApp.App.Auth;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using TimeManagementApp.App.ViewModels;

namespace TimeManagementApp.Pages.TimeManagement.UserPanel
{
    public class EdytujDaneUzytkownikaModel : BasePageModel
    {
        [BindProperty]
        public UpdateUserDataViewModel UpdateModel { get; set; }
        public object Store { get; private set; }
        public PasswordValidator<ApplicationUser> PasswordValidator { get; }
        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public string SuccessChangePwMessage { get; set; }


        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private DbContext context;
        private readonly IWebHostEnvironment _env;
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;
        private readonly ILogger<ApplicationUser> _logger;

        public EdytujDaneUzytkownikaModel(
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment env,
            IPasswordHasher<ApplicationUser> passwordHasher,
            ILogger<ApplicationUser> logger) : base(signInManager)
        {
            this._signInManager = signInManager;
            this._emailSender = emailSender;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            this.passwordHasher = passwordHasher;
            _logger = logger;
        }

        public ApplicationUser userToUpdate { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            userToUpdate = await _userManager.GetUserAsync(User);
            if (userToUpdate == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            userToUpdate = await _userManager.GetUserAsync(User);

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
                if (UpdateModel.Email == null)
                {
                    UpdateModel.Email = userToUpdate.Email;
                    UpdateModel.Email = userToUpdate.UserName;
                    UpdateModel.Email = userToUpdate.NormalizedEmail;
                    UpdateModel.Email = userToUpdate.NormalizedUserName;
                }
                else
                {
                    userToUpdate.Email = UpdateModel.Email;
                    userToUpdate.UserName = UpdateModel.Email;
                    userToUpdate.NormalizedUserName = UpdateModel.Email;
                    userToUpdate.NormalizedEmail = UpdateModel.Email;
                }

                if (UpdateModel.FirstName == null)
                {
                    UpdateModel.FirstName = userToUpdate.Firstname;
                }
                else
                {
                    userToUpdate.Firstname = UpdateModel.FirstName;
                }

                if (UpdateModel.LastName == null)
                {
                    UpdateModel.LastName = userToUpdate.Lastname;
                }
                else
                {
                    userToUpdate.Lastname = UpdateModel.LastName;
                }

                if (UpdateModel.PhoneNumber == null)
                {
                    UpdateModel.PhoneNumber = userToUpdate.PhoneNumber;
                }
                else
                {
                    userToUpdate.PhoneNumber = UpdateModel.PhoneNumber;
                }

                //ApUser.PhoneNumber = model.PhoneNumber;
                await _userManager.UpdateAsync(userToUpdate);
                _logger.LogInformation("User changed their password successfully.");
                SuccessChangePwMessage = "Dane zostały zmienione!";
                return RedirectToPage("/TimeManagement/Panel/TwojProfil");
            }
            return Page();
        }
    }
}