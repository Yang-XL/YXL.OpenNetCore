using MongoDB.Bson.Serialization.Attributes;

namespace Core.Repository.MongoDB
{
    public interface IMongoDBEntity<TKey> 
    {
        /// <summary>
        ///     主键
        /// </summary>
        [BsonId]
        TKey Id { get; set; }
    }
}