using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeManagementApp.App.Auth;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;
using Microsoft.Extensions.Logging;
using TimeManagementApp.App.ViewModels;

namespace TimeManagementApp.Pages.TimeManagement.UserPanel
{
    [AllowAnonymous]
    public class ZmienHasloModel : BasePageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private DbContext context;
        private readonly IWebHostEnvironment _env;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly ILogger<ApplicationUser> _logger;

        public ZmienHasloModel(
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment env,
            IPasswordHasher<ApplicationUser> passwordHasher,
            ILogger<ApplicationUser> logger) : base(signInManager)
        {
            _signInManager = signInManager;
            _emailSender = emailSender;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public ChangePasswordViewModel PasswordModel { get; set; }
        public object Store { get; private set; }
        public PasswordValidator<ApplicationUser> PasswordValidator { get; }
        [TempData]
        public string SuccessChangePwMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(ChangePasswordViewModel PasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, PasswordModel.OldPassword, PasswordModel.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            SuccessChangePwMessage = "Hasło zostało pomyślnie zmienione!";
            return RedirectToPage("/TimeManagement/Panel/TwojProfil");
        }
    }
}