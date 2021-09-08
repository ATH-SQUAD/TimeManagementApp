using TimeManagementApp.Database.Models;

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
