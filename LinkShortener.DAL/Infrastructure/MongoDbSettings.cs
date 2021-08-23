
using LinkShortener.DAL.Interfaces;

namespace LinkShortener.DAL.Infrastructure;
public class MongoDbSettings : IMongoDbSettings
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
}
