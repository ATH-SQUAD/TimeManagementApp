using MongoDB.Bson;
using MongoDB.Driver;
using TimeManagementApp.App.Interfaces;
using System;
using System.Collections.Generic;

namespace TimeManagementApp.App.DataProviders
{
    public class SqlServerDataProvider : IDataProvider
    {
        public bool ConnectionWithDbIsAlive => throw new NotImplementedException();

        public HashSet<string> GetColumnsNames(string table)
        {
            throw new NotImplementedException();
        }

        public List<string> GetTablesNames()
        {
            throw new NotImplementedException();
        }
    }
}
