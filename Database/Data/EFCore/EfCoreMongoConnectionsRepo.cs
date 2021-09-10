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
