using LinkShortener.DAL.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.DAL.Interfaces
{
    public interface IRequestCounterRepository : IMongoRepository<RequestCounter>
    {
        public RequestCounter GetMaxCountValue(ObjectId id);
    }
}
