using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Repository.Models;
using Core.Repository.Specification;

namespace Core.Repository
{
    public interface IEfRepository<T, in TKey> : IBaseRepository<T, TKey>
        where T : class, IEfEntity
    {
        #region Insert

        /// <summary>
        ///     插入文档
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSaveChanges"></param>
        /// <returns></returns>
        T Insert(T entity, bool autoSaveChanges = true);

        /// <summary>
        ///     异步插入文档
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSaveChanges"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<T> InsertAsync(T entity, bool autoSaveChanges = true,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     插入文档
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="autoSaveChanges"></param>
        int Insert(IEnumerable<T> entities, bool autoSaveChanges = true);

        /// <summary>
        ///     异步插入文档
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="autoSaveChanges"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<int> InsertAsync(IEnumerable<T> entities, bool autoSaveChanges = true,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Update

        /// <summary>
        ///     更新文档
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSaveChanges"></param>
        /// <returns></returns>
        long Update(T entity, bool autoSaveChanges = true);

        /// <summary>
        ///     异步更新文档
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSaveChanges"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> UpdateAsync(T entity, bool autoSaveChanges = true,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     更新文档
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="autoSaveChanges"></param>
        /// <returns></returns>
        long Update(IEnumerable<T> entitys, bool autoSaveChanges = true);

        /// <summary>
        ///     异步更新文档
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="autoSaveChanges"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> UpdateAsync(IEnumerable<T> entitys, bool autoSaveChanges = true,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Delete

        /// <summary>
        ///     根据主键ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSaveChanges"></param>
        /// <returns></returns>
        long Delete(TKey id, bool autoSaveChanges = true);

        /// <summary>
        ///     异步根据ID删除文档
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSaveChanges"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> DeleteAsync(TKey id, bool autoSaveChanges = true,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="autoSaveChanges"></param>
        /// <returns></returns>
        long Delete(Expression<Func<T, bool>> predicate, bool autoSaveChanges = true);

        /// <summary>
        ///     异步删除
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="autoSaveChanges"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> DeleteAsync(Expression<Func<T, bool>> predicate, bool autoSaveChanges = true,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="specification">组合查询条件</param>
        /// <param name="autoSaveChanges"></param>
        /// <returns></returns>
        long Delete(ISpecification<T> specification, bool autoSaveChanges = true);

        /// <summary>
        ///     异步删除
        /// </summary>
        /// <param name="specification">组合查询条件</param>
        /// <param name="autoSaveChanges"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> DeleteAsync(ISpecification<T> specification, bool autoSaveChanges = true,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion


        #region Query
        /// <summary>
        /// Queryable
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Queryable();
        

        #endregion
    }

    public interface IEfRepository<TEntity> : IEfRepository<TEntity, Guid>
        where TEntity : class, IEfEntity
    {
       
    }
}