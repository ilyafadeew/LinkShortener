using LinkShortener.DAL.Interfaces;
using LinkShortener.DAL.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.DAL.Repository
{
    public class RequestCounterRepository : MongoGenericRepository<RequestCounter>, IRequestCounterRepository
    {
        public RequestCounterRepository(IMongoDbSettings settings)
        {
            _settings = settings;
            var database = new MongoClient(_settings.ConnectionString).GetDatabase(_settings.DatabaseName);
            _collection = database.GetCollection<RequestCounter>(GetCollectionName(typeof(RequestCounter)));
        }

        public RequestCounter GetMaxCountValue(ObjectId id)
        {
            return _collection.Find(filter => filter.LinkInfoId == id)
                .SortByDescending(d => d.Count).Limit(1).FirstOrDefault();
        }
    }
}
