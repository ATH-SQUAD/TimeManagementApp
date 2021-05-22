using TimeManagementApp.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.Database.Data.EFCore
{
    public class EfCoreSqlServerConnectionsRepo : EfCoreRepository<SqlServerConnectionModel, AppDbContext>
    {
        public EfCoreSqlServerConnectionsRepo(AppDbContext context)
            : base(context)
        {

        }
    }
}
