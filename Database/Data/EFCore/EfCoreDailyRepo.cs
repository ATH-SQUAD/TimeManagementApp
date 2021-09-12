using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeManagementApp.Database.Models;

namespace TimeManagementApp.Database.Data.EFCore
{
    public class EfCoreDailyRepo : EfCoreRepository<DailyTime, AppDbContext>

    {
        private readonly AppDbContext _context;

        public EfCoreDailyRepo(AppDbContext context)
            : base(context)
        {
            _context = context;
        }


    }
}
