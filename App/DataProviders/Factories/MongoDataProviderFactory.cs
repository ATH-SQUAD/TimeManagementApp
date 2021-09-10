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
    public class MongoDataProviderFactory : IDataProviderFactory
    {
        private readonly IMongoClient _mongoClient;

        public MongoDataProviderFactory(string connectionString)
        {
            _mongoClient = new MongoClient(connectionString);
        }

        public bool ConnectionIsAlive
        {
            get
            {
                try
                {
                    _mongoClient.ListDatabaseNames();
                }
                catch 
                {
                    return false;
                }

                return _mongoClient.Cluster.Description.State == ClusterState.Connected;
            }
        }

        public IDataProvider CreateDataProvider(string dbName)
        {
            return new MongoDataProvider(_mongoClient, dbName);
        }

        public List<string> GetDatabasesNames()
        {
            List<string> dbNames = new List<string>();
            IAsyncCursor<BsonDocument> dbsCursor = _mongoClient.ListDatabases();
            while (dbsCursor.MoveNext())
            {
                foreach (var db in dbsCursor.Current)
                {
                    dbNames.Add(db["name"].AsString);
                }
            }

            return dbNames;
        }
    }
}
