using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database;
using TimeManagementApp.Database.Data.EFCore;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;

namespace TimeManagementApp.Pages.TimeManagement.Time
{
    [AllowAnonymous]
    public class DniowkiModel : BasePageModel
    {
        [TempData]
        public string SuccessCreationMessage { get; set; }
        [BindProperty]
        public DailyTimeViewModel DailyTimeModel { get; set; }
        public IList<DailyTime> Times { get; set; }
        [Parameter]
        public DailyTimeViewModel TimeModel { get; set; }
        public DailyTime DailyTime { get; private set; }

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly EfCoreDailyRepo _dailyRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        public DniowkiModel(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager, EfCoreDailyRepo dailyRepo, AppDbContext context) :base(signInManager)
        {
            _signInManager = signInManager;
            _context = context;
            _dailyRepo = dailyRepo;
            _userManager = userManager;
        }

        public async Task OnGet()
        {
            Times = await _dailyRepo.GetAll();
        }
        public async Task<IActionResult> OnPost() 
        {
            DailyTimeModel.WorkTime = DailyTimeModel.To.Hours - DailyTimeModel.From.Hours;
            var userName = User.Identity.Name;
            var toAdd = new DailyTime()
            {
                Id = Guid.NewGuid(),
                Person = userName,
                Date = DailyTimeModel.Date.ToString(),
                Job = DailyTimeModel.Job,
                From = DailyTimeModel.From.ToString(),
                To = DailyTimeModel.To.ToString(),
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.CurrentCulture),
                UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.CurrentCulture),
                WorkTime = DailyTimeModel.WorkTime.ToString() + "h"
            };

            await _dailyRepo.Add(toAdd);
            SuccessCreationMessage = "Dodano dniówke!";
            return RedirectToPage("/TimeManagement/Time/Dniowki");
        }
    }
}
