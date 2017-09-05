using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sakura.AspNetCore;

namespace Core.Repository.Ef
{
    public class EfRepository<TEntity> : IEfRepository<TEntity> where TEntity : class, IBaselModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EfRepository{TEntity,Guid}" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public EfRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbContext.Set<TEntity>();
        }

        public DbContext _dbContext { get; }
        public DbSet<TEntity> _dbSet { get; }

        public long Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.LongCount(predicate);
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbSet.LongCountAsync(predicate, cancellationToken);
        }


        public TEntity Delete(Guid id)
        {
            var entity = Get(id);
            _dbSet.Remove(entity);
            return entity;
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var list = Get(predicate);
            _dbSet.RemoveRange(list);
            return 0;
        }

        public async Task<TEntity> DeleteAsync(Guid id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await GetAsync(id, cancellationToken);
            _dbSet.Remove(entity);
            return entity;
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var list = await GetAsync(predicate, cancellationToken);
            _dbSet.RemoveRange(list);
            return 0;
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

        public IPagedList<TEntity> GetPaged<TProperty>(int pageIndex, int pageSize, Func<TEntity, TProperty> keySelector, Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).OrderBy(keySelector).ToPagedList(pageSize,pageIndex);
        }

        public Task<IPagedList<TEntity>> GetPagedAsync<TProperty>(int pageIndex, int pageSize, Func<TEntity, TProperty> keySelector, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() => GetPaged(pageIndex, pageSize, keySelector, predicate), cancellationToken);
        }

        public TEntity Single(Guid id)
        {
            return Get(id);
        }

        public async Task<TEntity> SingleAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            return await GetAsync(id, cancellationToken);
        }
        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return  _dbSet.SingleOrDefault(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = new CancellationToken())
        {
            return await _dbSet.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public IQueryable<TEntity> FromSql(string sql, params object[] parameters)
        {
            return _dbSet.FromSql(sql, parameters);
        }

        public TEntity Get(Guid id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbSet.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public TEntity Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public int Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            return SaveChanges();
        }

        public async Task<TEntity> InsertAsync(TEntity entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<int> InsertAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            return 0;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChanges(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public int Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return 0;
        }

        public Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() => Update(entity), cancellationToken);
        }
    }
}

    