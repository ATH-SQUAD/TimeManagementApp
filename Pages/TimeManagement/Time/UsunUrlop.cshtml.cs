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
    public class UsunUrlopModel : BasePageModel
    {
        [TempData]
        public string SuccessDeleteMessage { get; set; }
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly EfCoreVacationRepo _efCoreVacation;

        public DeleteViewModel DeleteModel { get; set; }

        public UsunUrlopModel(SignInManager<ApplicationUser> signInManager,
            AppDbContext dbContext,
            EfCoreVacationRepo efCoreVacation) : base(signInManager)
        {
            _signInManager = signInManager;
            _context = dbContext;
            _efCoreVacation = efCoreVacation;
        }
        public async Task<IActionResult> OnGetAsync(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var daily = await _context.VacationTimes.FindAsync(Id);

            if (daily == null)
            {
                return NotFound();
            }
            return Page();

        }
        public async Task<IActionResult> OnPostAsync(Guid Id, VacationTime time)
        {
            if (Id == null)
            {
                return NotFound();
            }

            time = _context.VacationTimes.Find(Id);
            if (time != null)
            {
                _context.Remove(time);
                await _context.SaveChangesAsync();
            }
            SuccessDeleteMessage = "Urlop zosta³ usuniêty";
            return RedirectToPage("/TimeManagement/Time/Dniowki");
        }
    }
}
