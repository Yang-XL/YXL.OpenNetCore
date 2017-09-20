using MongoDB.Bson.Serialization.Attributes;

namespace Core.Repository.MongoDB
{
    public interface IMongoEntity<TKey> 
    {
        /// <summary>
        ///     主键
        /// </summary>
        [BsonId]
        TKey Id { get; set; }
    }
}