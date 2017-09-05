using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Repository.MongoDB
{
    [BsonIgnoreExtraElements(Inherited = true)]
    public class MongoDBEntity : IMongoDBEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
