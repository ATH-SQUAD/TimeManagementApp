using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace TimeManagementApp.App.Interfaces
{
    public interface IDataProvider
    {
        bool ConnectionWithDbIsAlive { get; }

        List<string> GetTablesNames();

        HashSet<string> GetColumnsNames(string table);
    }
}
