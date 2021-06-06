using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeManagementApp.Database.Data.EFCore;
using TimeManagementApp.Database.Models;

namespace TimeManagementApp.Database.Data.EfCore
{
    public class EfCoreConnectionsRepo : EfCoreRepository<ConnectionModel, AppDbContext>
    {
        private readonly AppDbContext _context;

        public EfCoreConnectionsRepo(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<ConnectionModel>> GetAllConnections(string creatorId)
        {
            return await _context.Set<ConnectionModel>().Where(connection =>
            connection.CreatorId == creatorId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ConnectionModel> GetConnection(Guid connectionId)
        {
            var connectionEntity = await _context.Set<ConnectionModel>().Where(connection =>
            connection.ConnectionId == connectionId)
                .FirstOrDefaultAsync();

            return connectionEntity;
        }
    }
}
