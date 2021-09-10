using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class UsunDniowkeModel : BasePageModel
    {
        [TempData]
        public string SuccessDeleteMessage { get; set; }
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly EfCoreDailyRepo _efCoreDaily;

        public DeleteViewModel DeleteModel { get; set; }

        public UsunDniowkeModel(SignInManager<ApplicationUser> signInManager,
            AppDbContext dbContext,
            EfCoreDailyRepo efCoreDaily) : base(signInManager)
        {
            _signInManager = signInManager;
            _context = dbContext;
            _efCoreDaily = efCoreDaily;
        }
        public async Task<IActionResult> OnGetAsync(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var daily = await _context.DailyTimes.FindAsync(Id);

            if (daily == null)
            {
                return NotFound();
            }
            return Page();

        }
        public async Task<IActionResult> OnPostAsync(Guid Id, DailyTime time)
        {
            if (Id == null)
            {
                return NotFound();
            }

            time = _context.DailyTimes.Find(Id);
            if (time != null)
            {
                _context.Remove(time);
                await _context.SaveChangesAsync();
            }
            SuccessDeleteMessage = "Dniówka została usunięta";
            return RedirectToPage("/TimeManagement/Time/Dniowki");
        }
    }
}
