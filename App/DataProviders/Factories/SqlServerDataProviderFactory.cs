using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using TimeManagementApp.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.DataProviders.Factories
{
    public class SqlServerDataProviderFactory : IDataProviderFactory
    {
        public bool ConnectionIsAlive => throw new NotImplementedException();

        public IDataProvider CreateDataProvider(string dbName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetDatabasesNames()
        {
            throw new NotImplementedException();
        }
    }
}
