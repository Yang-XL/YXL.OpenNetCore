using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Repository.Specification;
using MongoDB.Bson;
using MongoDB.Driver;
using Sakura.AspNetCore;

namespace Core.Repository.MongoDB
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : class, IMongoEntity<string>
    {
        protected MongoRepository(IMongoCollection<TEntity> collection)
        {
            _collection = collection;
            _mongoDB = collection.Database;
        }

        public IMongoCollection<TEntity> _collection { get; }
        public IMongoDatabase _mongoDB { get; }


        #region Query

        /// <summary>
        /// 全部结果
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> Query()
        {
            return _collection.AsQueryable();
        }

        /// <summary>
        /// </summary>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> QueryAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() => Query(), cancellationToken);
        }

        /// <summary>
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.FindSync(predicate).ToEnumerable();
        }

        /// <summary>
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var reulst = await _collection.FindAsync(predicate, cancellationToken: cancellationToken);
            return reulst.ToEnumerable();
        }

        /// <summary>
        /// </summary>
        /// <param name="specification">过滤条件组合</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Query(ISpecification<TEntity> specification)
        {
            return Query(specification.Predicate);
        }

        /// <summary>
        /// </summary>
        /// <param name="specification">过滤条件组合</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> QueryAsync(ISpecification<TEntity> specification,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await QueryAsync(specification.Predicate, cancellationToken);
        }

        #endregion

        #region Insert

        /// <summary>
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity Insert(TEntity entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        /// <summary>
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<TEntity> InsertAsync(TEntity entity,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            return entity;
        }

        /// <summary>
        /// </summary>
        /// <param name="entities">实体</param>
        /// <returns></returns>
        public int Insert(IEnumerable<TEntity> entities)
        {
            _collection.InsertMany(entities);
            return entities.Count();
        }

        /// <summary>
        /// </summary>
        /// <param name="entities">实体</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<int> InsertAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await _collection.InsertManyAsync(entities, cancellationToken: cancellationToken);
            return entities.Count();
        }

        #endregion

        #region Update

        /// <summary>
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public long Update(TEntity entity)
        {
            var doc = entity.ToBsonDocument();
            var result = _collection.UpdateOne(Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id),
                new BsonDocumentUpdateDefinition<TEntity>(doc));
            return result.ModifiedCount;
        }

        /// <summary>
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<long> UpdateAsync(TEntity entity,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var doc = entity.ToBsonDocument();
            var result = await _collection.UpdateOneAsync(Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id),
                new BsonDocumentUpdateDefinition<TEntity>(doc), cancellationToken: cancellationToken);
            return result.ModifiedCount;
        }

        /// <summary>
        /// </summary>
        /// <param name="entitys">实体集合</param>
        /// <returns></returns>
        public long Update(IEnumerable<TEntity> entitys)
        {
            long reulst = 0;
            foreach (var item in entitys)
                reulst += Update(item);
            return reulst;
        }

        /// <summary>
        /// </summary>
        /// <param name="entitys">实体集合</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<long> UpdateAsync(IEnumerable<TEntity> entitys,
            CancellationToken cancellationToken = new CancellationToken())
        {
            long reulst = 0;
            foreach (var item in entitys)
                reulst += await UpdateAsync(item, cancellationToken);
            return reulst;
        }

        #endregion

        #region Delete

        /// <summary>
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public long Delete(string id)
        {
            return _collection.DeleteOne(a => a.Id == id).DeletedCount;
        }

        /// <summary>
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<long> DeleteAsync(string id,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _collection.DeleteOneAsync(a => a.Id == id, cancellationToken);

            return result.DeletedCount;
        }

        /// <summary>
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns></returns>
        public long Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.DeleteOne(predicate).DeletedCount;
        }

        /// <summary>
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<long> DeleteAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _collection.DeleteOneAsync(predicate, cancellationToken);
            return result.DeletedCount;
        }

        /// <summary>
        /// </summary>
        /// <param name="specification">过滤条件组合</param>
        /// <returns></returns>
        public long Delete(ISpecification<TEntity> specification)
        {
            return Delete(specification.Predicate);
        }

        /// <summary>
        /// </summary>
        /// <param name="specification">过滤条件组合</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public Task<long> DeleteAsync(ISpecification<TEntity> specification,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return DeleteAsync(specification.Predicate, cancellationToken);
        }

        #endregion

        #region Other

        /// <summary>
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns></returns>
        public long Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.Count(predicate);
        }

        /// <summary>
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await _collection.CountAsync(predicate, null, cancellationToken);
        }

        /// <summary>
        /// </summary>
        /// <param name="specification">过滤条件组合</param>
        /// <returns></returns>
        public long Count(ISpecification<TEntity> specification)
        {
            return Count(specification.Predicate);
        }

        /// <summary>
        /// </summary>
        /// <param name="specification">过滤条件组合</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public Task<long> CountAsync(ISpecification<TEntity> specification,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return CountAsync(specification.Predicate, cancellationToken);
        }

        /// <summary>
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.Find(predicate).Any();
        }

        /// <summary>
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await _collection.Find(predicate).AnyAsync(cancellationToken);
        }

        /// <summary>
        /// </summary>
        /// <param name="specification">过滤条件组合</param>
        /// <returns></returns>
        public bool Exists(ISpecification<TEntity> specification)
        {
            return Exists(specification.Predicate);
        }

        /// <summary>
        /// </summary>
        /// <param name="specification">过滤条件组合</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public Task<bool> ExistsAsync(ISpecification<TEntity> specification,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return ExistsAsync(specification.Predicate, cancellationToken);
        }

        #endregion

        #region Single

        /// <summary>
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public TEntity Single(string id)
        {
            return Single(a => a.Id == id);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<TEntity> SingleAsync(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            return await SingleAsync(a => a.Id == id, cancellationToken);
        }

        /// <summary>
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns></returns>
        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            var result = _collection.FindSync(predicate);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _collection.FindAsync(predicate, cancellationToken: cancellationToken);
            return result.FirstOrDefault(cancellationToken);
        }

        /// <summary>
        /// </summary>
        /// <param name="specification">过滤条件组合</param>
        /// <returns></returns>
        public TEntity Single(ISpecification<TEntity> specification)
        {
            return Single(specification.Predicate);
        }

        /// <summary>
        /// </summary>
        /// <param name="specification">过滤条件组合</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<TEntity> SingleAsync(ISpecification<TEntity> specification,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await SingleAsync(specification.Predicate, cancellationToken);
        }



        #endregion

        #region Paged

        public IPagedList<TEntity> GetPaged<TProperty>(int pageIndex, int pageSize, Func<TEntity, TProperty> sortBy, bool isDesc = false)
        {
            if(isDesc)
                return _collection.AsQueryable().OrderByDescending(sortBy).ToPagedList(pageSize, pageIndex);
            return _collection.AsQueryable().OrderBy(sortBy).ToPagedList(pageSize, pageIndex);
        }

        public Task<IPagedList<TEntity>> GetPagedAsync<TProperty>(int pageIndex, int pageSize, Func<TEntity, TProperty> sortBy, bool isDesc = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() => GetPaged(pageIndex, pageSize, sortBy, isDesc), cancellationToken);
        }


        /// <summary>
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="keySelector">排序</param>
        /// <param name="predicate">过滤条件</param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        public IPagedList<TEntity> GetPaged<TProperty>(int pageIndex, int pageSize, Func<TEntity, TProperty> keySelector, Expression<Func<TEntity, bool>> predicate, bool isDesc = false)
        {
            if(isDesc)
                return _collection.AsQueryable().Where(predicate).OrderByDescending(keySelector).ToPagedList(pageSize, pageIndex);
            return _collection.AsQueryable().Where(predicate).OrderBy(keySelector).ToPagedList(pageSize, pageIndex);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="keySelector">排序</param>
        /// <param name="predicate">过滤条件</param>
        /// <param name="isDesc"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public Task<IPagedList<TEntity>> GetPagedAsync<TProperty>(int pageIndex, int pageSize, Func<TEntity, TProperty> keySelector, Expression<Func<TEntity, bool>> predicate, bool isDesc = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() => GetPaged(pageIndex, pageSize, keySelector, predicate, isDesc), cancellationToken);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="sortBy"></param>
        /// <param name="specification">过滤条件组合</param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        public IPagedList<TEntity> GetPaged<TProperty>(int pageIndex, int pageSize, Func<TEntity, TProperty> sortBy,
            ISpecification<TEntity> specification, bool isDesc = false)
        {
            return GetPaged(pageIndex, pageSize, sortBy, specification.Predicate, isDesc);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="sortBy"></param>
        /// <param name="specification">过滤条件组合</param>
        /// <param name="isDesc"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public Task<IPagedList<TEntity>> GetPagedAsync<TProperty>(int pageIndex, int pageSize,
            Func<TEntity, TProperty> sortBy, ISpecification<TEntity> specification, bool isDesc = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return GetPagedAsync(pageIndex, pageSize, sortBy, specification.Predicate, isDesc,cancellationToken);
        }

        #endregion
    }
}