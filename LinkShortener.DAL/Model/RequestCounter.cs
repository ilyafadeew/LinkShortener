using LinkShortener.DAL.Infrastructure;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.DAL.Model
{
    [BsonCollection("RequestCounter")]
    public class RequestCounter : Document
    {
       public int Count {  get; set;}
       public ObjectId LinkInfoId {  get; set;}
    }
}
