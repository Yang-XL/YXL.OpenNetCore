using MongoDB.Bson.Serialization.Attributes;

namespace Core.Repository.MongoDB
{
    public interface IMongoDBEntity<TKey> : IBaselModel
    {
        /// <summary>
        ///     主键
        /// </summary>
        [BsonId]
        TKey Id { get; set; }
    }
}