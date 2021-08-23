using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.DAL.Model
{
    public class LinkInfo
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("OriginalLink")]
        public string OriginalLink { get; set; }
        [BsonElement("ShortenedLink")]
        public string ShortenedLink { get; set; }
    }
}
