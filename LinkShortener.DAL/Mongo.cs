using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.DAL
{
    public class Mongo
    {
        public MongoClient Client;
        public IMongoDatabase Database;

        public Mongo()
        {
            Client = new MongoClient("mongodb://localhost:27017");
            Database = Client.GetDatabase("myFirstDatabase");
        }
    }
}
