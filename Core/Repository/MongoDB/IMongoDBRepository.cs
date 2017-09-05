using System.Linq;
using MongoDB.Driver;

namespace Core.Repository.MongoDB
{
    public interface IMongoDBRepository<TEntity> : IMongoDBRepository<TEntity, string>
        where TEntity : class, IMongoDBEntity<string>
    {
    }

    public interface IMongoDBRepository<TEntity, in TKey> :IBaseRepository<TEntity,TKey>
        where TEntity : class, IMongoDBEntity<TKey>
    {
        /// <summary>
        ///     MongoDB表
        /// </summary>
        IMongoCollection<TEntity> _collection { get; }

        /// <summary>
        ///     MongoDB库
        /// </summary>
        IMongoDatabase _mongoDB { get; }
    }
}