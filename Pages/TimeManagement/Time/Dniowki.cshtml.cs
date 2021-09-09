using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;

namespace TimeManagementApp.Pages.TimeManagement.Time
{
    [AllowAnonymous]
    public class DniowkiModel : BasePageModel
    {

        [BindProperty]
        public DailyTimeViewModel DailyTimeModel { get; set; }

        [Parameter]
        public DailyTimeViewModel TimeModel { get; set; }
        private readonly SignInManager<ApplicationUser> _signInManager;
        public DniowkiModel(SignInManager<ApplicationUser> signInManager):base(signInManager)
        {
            _signInManager = signInManager;
        }
    }
}
