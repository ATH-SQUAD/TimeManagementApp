using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.Interfaces
{
    interface IDataProviderFactory
    {
        bool ConnectionIsAlive { get; }

        IDataProvider CreateDataProvider(string dbName);

        List<string> GetDatabasesNames();
    }
}
