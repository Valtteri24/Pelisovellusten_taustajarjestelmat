using MongoDB.Driver;

namespace gameapi.mongodb
{
    public class MongoDBClient
    {
        private readonly MongoClient _mongoClient;

        public MongoDBClient()
        {
            //Mongo client holds the connection to the database
            _mongoClient = new MongoClient("mongodb://localhost:27017");
        }

        public IMongoDatabase GetDatabase(string name)
        {
            return _mongoClient.GetDatabase(name);
        }
    }
}