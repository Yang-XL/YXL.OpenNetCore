using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository.Ef
{
    public interface IEfRepository<TEntity, in TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class
    {
        DbContext _dbContext { get; }

        DbSet<TEntity> _dbSet { get; }

        #region Sql


        /// <summary>
        ///     Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An <see cref="IQueryable" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);
        
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChanges(CancellationToken cancellationToken = new CancellationToken());
    }

    public interface IEfRepository<TEntity> : IEfRepository<TEntity, Guid>
        where TEntity : class
    {
    }
}