using TimeManagementApp.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.DataProviders
{
    public class SqlServerDataProvider : IDataProvider
    {
        public void ConnectDB(string database)
        {
            throw new NotImplementedException();
        }

        public List<T> GetData<T>(string table)
        {
            throw new NotImplementedException();
        }

        public List<string> GetTables()
        {
            throw new NotImplementedException();
        }

        public List<string> GetDatabases()
        {
            throw new NotImplementedException();
        }

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }
    }
}
