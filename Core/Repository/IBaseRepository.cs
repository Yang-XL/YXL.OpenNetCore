using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Repository.Specification;
using Sakura.AspNetCore;

namespace Core.Repository
{
    public interface IBaseRepository<T, in TKey> where T:class
    {

        #region Query

        /// <summary>
        ///     获取对象集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Query();

        /// <summary>
        ///     获取对象集合
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     获取对象集合
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        IEnumerable<T> Query(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     获取对象集合
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     获取对象集合
        /// </summary>
        /// <param name="specification">组合查询条件</param>
        /// <returns></returns>
        IEnumerable<T> Query(ISpecification<T> specification);

        /// <summary>
        ///     获取对象集合
        /// </summary>
        /// <param name="specification">组合查询条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync(ISpecification<T> specification,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Other

        /// <summary>
        ///     计数
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        long Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     计数
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> CountAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        ///     计数
        /// </summary>
        /// <param name="specification">组合查询条件</param>
        /// <returns></returns>
        long Count(ISpecification<T> specification);

        /// <summary>
        ///     计数
        /// </summary>
        /// <param name="specification">组合查询条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> CountAsync(ISpecification<T> specification,
            CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        ///     是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        bool Exists(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        ///     是否存在
        /// </summary>
        /// <param name="specification">组合查询条件</param>
        /// <returns></returns>
        bool Exists(ISpecification<T> specification);

        /// <summary>
        ///     是否存在
        /// </summary>
        /// <param name="specification">组合查询条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(ISpecification<T> specification,
            CancellationToken cancellationToken = new CancellationToken());

        #endregion

        #region Single
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        T Single(TKey id);
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<T> SingleAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     获取对象
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        T Single(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     获取对象
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Gets single entity using specification
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="specification">The criteria.</param>
        /// <returns></returns>
        T Single(ISpecification<T> specification);

        /// <summary>
        ///     Gets single entity using specification
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="specification">The criteria.</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<T> SingleAsync(ISpecification<T> specification,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Paged

        /// <summary>
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="sortBy">排序</param>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        IPagedList<T> GetPaged<TProperty>(int pageIndex, int pageSize, Func<T, TProperty> sortBy,
            Expression<Func<T, bool>> predicate);

        /// <summary>
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="sortBy">排序</param>
        /// <param name="predicate">条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<IPagedList<T>> GetPagedAsync<TProperty>(int pageIndex, int pageSize, Func<T, TProperty> sortBy,
            Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="sortBy">排序</param>
        /// <param name="specification">组合查询条件</param>
        /// <returns></returns>
        IPagedList<T> GetPaged<TProperty>(int pageIndex, int pageSize, Func<T, TProperty> sortBy,
            ISpecification<T> specification);

        /// <summary>
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="sortBy">排序</param>
        /// <param name="specification">组合查询条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<IPagedList<T>> GetPagedAsync<TProperty>(int pageIndex, int pageSize, Func<T, TProperty> sortBy,
            ISpecification<T> specification, CancellationToken cancellationToken = default(CancellationToken));

        #endregion
    }

    public interface IBaseRepository<T> : IBaseRepository<T, Guid> where T:class { }
}