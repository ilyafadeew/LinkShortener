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
        public void Connect()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("myFirstDatabase");
        }
    }
}
