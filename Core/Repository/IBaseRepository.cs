using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Sakura.AspNetCore;

namespace Core.Repository
{
    public interface IBaseRepository<T, in TKey> where T : IBaselModel
    {
        #region Get

        /// <summary>
        ///     根据主键获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(TKey id);

        /// <summary>
        ///     根据主键获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> GetAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     获取对象集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     获取对象集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion

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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     插入文档
        /// </summary>
        /// <param name="entities"></param>
        int Insert(IEnumerable<T> entities);

        /// <summary>
        ///     异步插入文档
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
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
        int Update(T entity);

        /// <summary>
        ///     异步更新文档
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Delete

        /// <summary>
        ///     根据主键ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Delete(TKey id);

        /// <summary>
        ///     异步根据ID删除文档
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> DeleteAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     异步删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Other

        /// <summary>
        ///     计数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        long Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     计数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> CountAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        ///     是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken());

        #endregion

        #region Paged

        IPagedList<T> GetPaged<TProperty>(int pageIndex,int pageSize,Func<T, TProperty> keySelector, Expression<Func<T, bool>> predicate);


        Task<IPagedList<T>> GetPagedAsync<TProperty>(int pageIndex, int pageSize, Func<T, TProperty> keySelector, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Single

        T Single(TKey id);

        Task<T> SingleAsync(TKey id,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     获取对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Single(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     获取对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken));
        #endregion

    }
}