using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Sakura.AspNetCore;

namespace Core.Repository.MongoDB
{
    public class MongoDBRepository<TEntity> : IMongoDBRepository<TEntity> where TEntity : class, IMongoDBEntity<string>
    {
        protected MongoDBRepository(IMongoCollection<TEntity> collection)
        {
            _collection = collection;
            _mongoDB = collection.Database;
        }


        public IMongoCollection<TEntity> _collection { get; }
        public IMongoDatabase _mongoDB { get; }


        public TEntity Get(string id)
        {
            return Get(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public async Task<TEntity> GetAsync(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            var item = await _collection.FindAsync(m => m.Id == id, cancellationToken: cancellationToken);

            return item.Current.FirstOrDefault();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.Find(predicate).ToEnumerable();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _collection.FindAsync(predicate, cancellationToken: cancellationToken);
            return result.ToEnumerable();
        }

        public TEntity Insert(TEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() => Insert(entity), cancellationToken);
        }

        public int Insert(IEnumerable<TEntity> entities)
        {
            _collection.InsertMany(entities);
            return entities.Count();
        }

        public async Task<int> InsertAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await _collection.InsertManyAsync(entities, null, cancellationToken);
            return entities.Count();
        }

        public int Update(TEntity entity)
        {
            var doc = entity.ToBsonDocument();
            _collection.UpdateOne(Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id),
                new BsonDocumentUpdateDefinition<TEntity>(doc));
            return 1;
        }

        public async Task<int> UpdateAsync(TEntity entity,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var doc = entity.ToBsonDocument();
            await _collection.UpdateOneAsync(Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id),
                new BsonDocumentUpdateDefinition<TEntity>(doc), cancellationToken: cancellationToken);
            return 1;
        }

        public TEntity Delete(string id)
        {
            return _collection.FindOneAndDelete(a => a.Id.Equals(id));
        }

        public async Task<TEntity> DeleteAsync(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            return await _collection.FindOneAndDeleteAsync(a => a.Id.Equals(id), null, cancellationToken);
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var result = _collection.DeleteMany(predicate);

            return (int) result.DeletedCount;
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _collection.DeleteManyAsync(predicate, cancellationToken);

            return (int) result.DeletedCount;
        }

        public long Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.Count(predicate);
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await _collection.CountAsync(predicate, null, cancellationToken);
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.Find(predicate).Any();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await _collection.Find(predicate).AnyAsync(cancellationToken);
        }

        public IPagedList<TEntity> GetPaged<TProperty>(int pageIndex, int pageSize, Func<TEntity, TProperty> keySelector, Expression<Func<TEntity, bool>> predicate)
        {
           return _collection.AsQueryable().Where(predicate).OrderBy(keySelector).ToPagedList(pageSize, pageIndex);
        }

        public Task<IPagedList<TEntity>> GetPagedAsync<TProperty>(int pageIndex, int pageSize, Func<TEntity, TProperty> keySelector, Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(()=>GetPaged(pageIndex, pageSize, keySelector, predicate), cancellationToken);
        }

        public TEntity Single(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> SingleAsync(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}