using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Repository.Dapper;
using Core.Repository.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository.Unitity
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
    {
        public DbSet<TEntity> _dbSet => _dbContext.Set<TEntity>();

        public DbContext _dbContext { get; }

        public IDapperCommand _dapperCommand { get; }

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dapperCommand = new DapperCommand<TEntity>(dbContext.Database.GetDbConnection());
        }

        public int SaveChange(bool acceptAllChangesOnSuccess = true)
        {
           return _dbContext.SaveChanges(acceptAllChangesOnSuccess);
        }
        public async Task<int> SaveAsyncChange(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default(CancellationToken))
        {
          return await _dbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
            GC.SuppressFinalize(this);
        }

     
    }
}
