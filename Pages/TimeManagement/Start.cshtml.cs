using Microsoft.AspNetCore.Identity;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;

namespace TimeManagementApp.Pages.TimeManagement
{
    public class StartModel : BasePageModel
    {
        public StartModel(SignInManager<ApplicationUser> signInManager)
            : base(signInManager)
        {

        }
    }
}