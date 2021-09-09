using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using TimeManagementApp.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeManagementApp.App.DataProviders
{
    public class MongoDataProvider : IDataProvider
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDataProvider(IMongoClient mongoClient, string dbName)
        {
            _mongoDatabase = mongoClient.GetDatabase(dbName);
        }

        public bool ConnectionWithDbIsAlive
        {
            get
            {
                try
                {
                    if (GetTablesNames().Count() > 0)
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
                
                return false;
            }
        }

        public List<string> GetTablesNames()
        {
            List<string> collectionsNames = new List<string>();
            foreach (var collection in _mongoDatabase.ListCollectionsAsync().Result.ToListAsync().Result)
            {
                collectionsNames.Add(collection["name"].AsString);
            }

            return collectionsNames;
        }

        public HashSet<string> GetColumnsNames(string dbTable)
        {
            HashSet<string> keys = new HashSet<string>();
            var collection = GetCollection<BsonDocument>(dbTable).Find(new BsonDocument()).ToList();
            foreach (var document in collection)
            {
                foreach (var field in document)
                {
                    keys.Add(field.Name);
                }
            }

            return keys;
        }

        public IMongoCollection<T> GetCollection<T>(string dbTable)
        {
            return _mongoDatabase.GetCollection<T>(dbTable);
        }
    }
}
