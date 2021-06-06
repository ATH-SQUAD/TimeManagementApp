using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeManagementApp.Database.Models;

namespace TimeManagementApp.Database.Data.EFCore
{
    public class EfCoreMongoConnectionsRepo : EfCoreRepository<MongoConnectionModel, AppDbContext>
    {
        public EfCoreMongoConnectionsRepo(AppDbContext context)
            : base(context)
        {

        }
    }
}
