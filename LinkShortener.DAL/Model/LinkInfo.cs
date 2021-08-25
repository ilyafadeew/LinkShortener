using LinkShortener.DAL.Infrastructure;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.DAL.Model
{
    [BsonCollection("Links")]
    public class LinkInfo : Document
    {    
        [BsonElement("OriginalLink")]
        public string OriginalLink { get; set; }
        [BsonElement("ShortenedLink")]
        public string ShortenedLink { get; set; }
        [BsonElement("UserIdWhoAddThisLink")]
        public string UserIdWhoAddThisLink { get; set; }
        [BsonElement("NumberOfLinkRequests")]
        public int NumberOfLinkRequests { get; set; }
    }
}
