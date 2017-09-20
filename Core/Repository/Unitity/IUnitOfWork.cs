using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository.Unitity
{
    public interface IUnitOfWork<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Create a object set for a type TEntity
        /// </summary>
        /// <typeparam name="TEntity">Type of elements in object set</typeparam>
        /// <returns>Object set of type {TEntity}</returns>
        DbSet<TEntity> _dbSet { get; }

        /// <summary>
        ///     DataSource
        /// </summary>
        DbContext _dbContext { get; }

        /// <summary>
        ///     保存更改
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        int SaveChange(bool acceptAllChangesOnSuccess = true);

        /// <summary>
        ///     异步保存更改
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        Task<int> SaveAsyncChange(bool acceptAllChangesOnSuccess = true,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}