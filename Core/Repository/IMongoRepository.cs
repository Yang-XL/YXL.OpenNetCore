using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Repository.Specification;
using MongoDB.Driver;

namespace Core.Repository.MongoDB
{
    public interface IMongoRepository<TEntity> : IMongoRepository<TEntity, string>
        where TEntity : class, IMongoEntity<string>
    {
    }

    public interface IMongoRepository<T, in TKey> : IBaseRepository<T, TKey>
        where T : class, IMongoEntity<TKey>
    {

        #region Insert

        /// <summary>
        ///     插入文档
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Insert(T entity);

        /// <summary>
        ///     异步插入文档
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<T> InsertAsync(T entity,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     插入文档
        /// </summary>
        /// <param name="entities"></param>
        int Insert(IEnumerable<T> entities);

        /// <summary>
        ///     异步插入文档
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<int> InsertAsync(IEnumerable<T> entities,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Update

        /// <summary>
        ///     更新文档
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        long Update(T entity);

        /// <summary>
        ///     异步更新文档
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> UpdateAsync(T entity,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     更新文档
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        long Update(IEnumerable<T> entitys);

        /// <summary>
        ///     异步更新文档
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> UpdateAsync(IEnumerable<T> entitys,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Delete

        /// <summary>
        ///     根据主键ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        long Delete(TKey id);

        /// <summary>
        ///     异步根据ID删除文档
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> DeleteAsync(TKey id,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        long Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     异步删除
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> DeleteAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="specification">组合查询条件</param>
        /// <returns></returns>
        long Delete(ISpecification<T> specification);

        /// <summary>
        ///     异步删除
        /// </summary>
        /// <param name="specification">组合查询条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> DeleteAsync(ISpecification<T> specification,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion
    }
}