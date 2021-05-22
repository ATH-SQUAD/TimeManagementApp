using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using TimeManagementApp.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.DataProviders
{
    public class MongoDataProvider : IDataProvider
    {
        private readonly IMongoClient _mongoClient;
        private IMongoDatabase _mongoDatabase;

        public MongoDataProvider(string connectionString)
        {
            _mongoClient = new MongoClient(connectionString);
        }

        public void ConnectDB(string database)
        {
            _mongoDatabase = _mongoClient.GetDatabase(database);
        }

        public List<T> GetData<T>(string table)
        {
            var collection = _mongoDatabase.GetCollection<T>(table);

            return collection.Find(new BsonDocument()).ToList();
        }

        public List<string> GetTables()
        {
            List<string> _collections = new List<string>();
            foreach (BsonDocument collection in _mongoDatabase.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
            {
                string name = collection["name"].AsString;
                _collections.Add(name);
            }

            return _collections;
        }

        public List<string> GetDatabases()
        {
            List<string> _databases = new List<string>();
            IAsyncCursor<BsonDocument> _cursor = _mongoClient.ListDatabases();

            while (_cursor.MoveNext())
            {
                foreach (var doc in _cursor.Current)
                {
                    _databases.Add((string)doc["name"]);
                }
            }

            return _databases;
        }

        public bool IsConnected()
        {
            try
            {
                _mongoClient.ListDatabaseNames();
            }
            catch (Exception)
            {
                return false;
            }

            return _mongoClient.Cluster.Description.State == ClusterState.Connected;
        }
    }
}
