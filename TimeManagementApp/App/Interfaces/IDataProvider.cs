using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.Interfaces
{
    public interface IDataProvider
    {
        void ConnectDB(string database);
        List<T> GetData<T>(string table);
        List<string> GetTables();
        List<string> GetDatabases();
        bool IsConnected();
    }
}
