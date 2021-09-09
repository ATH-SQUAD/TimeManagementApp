using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;

namespace TimeManagementApp.Pages
{
    public class PrivacyModel : BasePageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(SignInManager<ApplicationUser> signInManager, ILogger<PrivacyModel> logger)
            : base(signInManager)
        {
            _logger = logger;
        }
    }
}
