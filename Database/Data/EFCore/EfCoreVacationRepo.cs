using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeManagementApp.Database.Models;

namespace TimeManagementApp.Database.Data.EFCore
{
    public class EfCoreVacationRepo : EfCoreRepository<VacationTime, AppDbContext>
    {
        private readonly AppDbContext _context;

        public EfCoreVacationRepo(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
