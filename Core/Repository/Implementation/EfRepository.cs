using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Repository.Models;
using Core.Repository.Specification;
using Core.Repository.Unitity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.Connections;
using Sakura.AspNetCore;

namespace Core.Repository.Implementation
{
    public class EfRepository<TEntity> : IEfRepository<TEntity> where TEntity : class, IEfEntity
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see>
        ///         <cref>EfRepository{TEntity,Guid}</cref>
        ///     </see>
        ///     class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public EfRepository(DbContext dbContext)
        {
            _UnitOfWork = new UnitOfWork<TEntity>(dbContext);

        }

        public IUnitOfWork<TEntity> _UnitOfWork { get; }

        protected DbContext _dbContext => _UnitOfWork._dbContext;
        protected DbSet<TEntity> _dbSet => _UnitOfWork._dbSet;

        #region Query

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> Query()
        {
            return _dbSet.AsEnumerable();
        }

        /// <summary>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> QueryAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() => Query(), cancellationToken);
        }

        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsEnumerable();
        }

        public  async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            //return await _dbSet.AsQueryable().ToListAsync();
            return await Task.Run(() => Query(predicate), cancellationToken);
        }

        public IEnumerable<TEntity> Query(ISpecification<TEntity> specification)
        {
            return Query(specification.Predicate);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync(ISpecification<TEntity> specification,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await QueryAsync(specification.Predicate, cancellationToken);
        }

        #endregion

        #region Insert

        public TEntity Insert(TEntity entity, bool autoSaveChanges = true)
        {
            var result = _dbSet.Add(entity);
            if (autoSaveChanges) _UnitOfWork.SaveChange();
            return result.Entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity, bool autoSaveChanges = true,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _dbSet.AddAsync(entity, cancellationToken);
            if (autoSaveChanges) await _UnitOfWork.SaveAsyncChange(true, cancellationToken);
            return result.Entity;
        }

        public int Insert(IEnumerable<TEntity> entities, bool autoSaveChanges = true)
        {
            _dbSet.AddRange(entities);
            if (autoSaveChanges) return _UnitOfWork.SaveChange();
            return 0;
        }

        public async Task<int> InsertAsync(IEnumerable<TEntity> entities, bool autoSaveChanges = true,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            if (autoSaveChanges) return await _UnitOfWork.SaveAsyncChange(true, cancellationToken);
            return 0;
        }

        #endregion

        #region Update

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSaveChanges"></param>
        /// <returns></returns>
        public long Update(TEntity entity, bool autoSaveChanges = true)
        {
            _dbSet.Update(entity);
            if (autoSaveChanges) return _UnitOfWork.SaveChange();
            return 0;
        }

        public async Task<long> UpdateAsync(TEntity entity, bool autoSaveChanges = true,
            CancellationToken cancellationToken = new CancellationToken())
        {
            _dbSet.Update(entity);
            if (autoSaveChanges) return await _UnitOfWork.SaveAsyncChange(true, cancellationToken);
            return 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="autoSaveChanges"></param>
        /// <returns></returns>
        public long Update(IEnumerable<TEntity> entitys, bool autoSaveChanges = true)
        {
            _dbSet.UpdateRange(entitys);
            if (autoSaveChanges) return _UnitOfWork.SaveChange();
            return 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="autoSaveChanges"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        public async Task<long> UpdateAsync(IEnumerable<TEntity> entitys, bool autoSaveChanges = true,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            _dbSet.UpdateRange(entitys);
            if (autoSaveChanges) return await _UnitOfWork.SaveAsyncChange(true, cancellationToken);
            return 0;
        }

        #endregion

        #region Delete

        public long Delete(Guid id, bool autoSaveChanges = true)
        {
            var entity = _dbSet.Find(id);
            if (entity == null) return 0;
            _dbSet.Remove(entity);
            if (autoSaveChanges) return _UnitOfWork.SaveChange();
            return 0;
        }

        public async Task<long> DeleteAsync(Guid id, bool autoSaveChanges = true,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return 0;
            _dbSet.Remove(entity);
            if (autoSaveChanges) return await _UnitOfWork.SaveAsyncChange(true, cancellationToken);
            return 0;
        }

        public long Delete(Expression<Func<TEntity, bool>> predicate, bool autoSaveChanges = true)
        {
            var query = Query(predicate);
            if (query == null || !query.Any()) return 0;
            _dbSet.RemoveRange(query);
            if (autoSaveChanges) return _UnitOfWork.SaveChange();
            return 0;
        }

        public async Task<long> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSaveChanges = true,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var query = await QueryAsync(predicate, cancellationToken);
            if (query == null || !query.Any()) return 0;
            _dbSet.RemoveRange(query);
            if (autoSaveChanges) return await _UnitOfWork.SaveAsyncChange(true, cancellationToken);
            return 0;
        }

        public long Delete(ISpecification<TEntity> specification, bool autoSaveChanges = true)
        {
            return Delete(specification.Predicate, autoSaveChanges);
        }

        public async Task<long> DeleteAsync(ISpecification<TEntity> specification, bool autoSaveChanges = true,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await DeleteAsync(specification.Predicate, autoSaveChanges, cancellationToken);
        }

       

        #endregion

        #region Other

        public long Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.LongCount(predicate);
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbSet.LongCountAsync(predicate, cancellationToken);
        }

        public long Count(ISpecification<TEntity> specification)
        {
            return Count(specification.Predicate);
        }

        public async Task<long> CountAsync(ISpecification<TEntity> specification,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await CountAsync(specification.Predicate, cancellationToken);
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate) != null;
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
            return entity != null;
        }

        public bool Exists(ISpecification<TEntity> specification)
        {
            return Exists(specification.Predicate);
        }

        public async Task<bool> ExistsAsync(ISpecification<TEntity> specification,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await ExistsAsync(specification.Predicate, cancellationToken);
        }

        #endregion

        #region Single

        public TEntity Single(Guid id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> SingleAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public TEntity Single(ISpecification<TEntity> specification)
        {
            return Single(specification.Predicate);
        }

        public async Task<TEntity> SingleAsync(ISpecification<TEntity> specification,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await SingleAsync(specification.Predicate, cancellationToken);
        }

        #endregion

        #region Paged

        public IPagedList<TEntity> GetPaged<TProperty>(int pageIndex, int pageSize, Func<TEntity, TProperty> sortKey,
            Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).OrderBy(sortKey).ToPagedList(pageSize, pageIndex);
        }

        public Task<IPagedList<TEntity>> GetPagedAsync<TProperty>(int pageIndex, int pageSize,
            Func<TEntity, TProperty> sortKey, Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() => GetPaged(pageIndex, pageSize, sortKey, predicate), cancellationToken);
        }

        public IPagedList<TEntity> GetPaged<TProperty>(int pageIndex, int pageSize, Func<TEntity, TProperty> sortBy,
            ISpecification<TEntity> specification)
        {
            return GetPaged(pageIndex, pageSize, sortBy, specification.Predicate);
        }

        public Task<IPagedList<TEntity>> GetPagedAsync<TProperty>(int pageIndex, int pageSize,
            Func<TEntity, TProperty> sortBy, ISpecification<TEntity> specification,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return GetPagedAsync(pageIndex, pageSize, sortBy, specification.Predicate, cancellationToken);
        }

        #endregion

        #region Queryable

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet.AsQueryable();
        }
        #endregion
    }
}