using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database;
using TimeManagementApp.Database.Data.EFCore;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;

namespace TimeManagementApp.Pages.TimeManagement.Time
{
    public class UrlopyModel : BasePageModel
    {
        [TempData]
        public string SuccessCreationMessage { get; set; }
        [BindProperty]
        public VacationTimeViewModel VacationTimeModel { get; set; }
        public IList<VacationTime> Times { get; set; }
        [Parameter]
        public DailyTimeViewModel TimeModel { get; set; }
        public DailyTime DailyTime { get; private set; }

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly EfCoreVacationRepo _efCoreVacation;
        private readonly UserManager<ApplicationUser> _userManager;
        public UrlopyModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, EfCoreVacationRepo efCoreVacation, AppDbContext context) : base(signInManager)
        {
            _signInManager = signInManager;
            _context = context;
            _efCoreVacation = efCoreVacation;
            _userManager = userManager;
        }

        public async Task OnGet()
        {
            Times = await _efCoreVacation.GetAll();
        }
        public async Task<IActionResult> OnPost()
        {
            var userName = User.Identity.Name;
            var toAdd = new VacationTime()
            {
                Id = Guid.NewGuid(),
                DateFrom = VacationTimeModel.DateFrom.ToString("yyyy-MM-dd"),
                DateTo = VacationTimeModel.DateTo.ToString("yyyy-MM-dd"),
                Reason = VacationTimeModel.Reason,
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.CurrentCulture),
                Person = userName,
                TotalDays = VacationTimeModel.CalculateDays(VacationTimeModel.DateFrom, VacationTimeModel.DateTo).ToString() + " dni"
            };

            await _efCoreVacation.Add(toAdd);
            SuccessCreationMessage = "Dodano urlop";
            return RedirectToPage("/TimeManagement/Time/Urlopy");
        }
    }
}
