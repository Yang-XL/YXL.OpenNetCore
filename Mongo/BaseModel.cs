using System;
using System.Collections.Generic;
using System.Text;
using Core.Repository.MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mongo
{

    [BsonIgnoreExtraElements(Inherited = true)]
    public  class BaseModel :IMongoEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public  string Id { get; set; }
    }
}
