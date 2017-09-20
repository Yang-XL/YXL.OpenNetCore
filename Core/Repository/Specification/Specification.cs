// =================================================================== 
// 项目说明
//====================================================================
// 幸运草工作室 @ CopyRight 2014-2020
// 文件： Specification.cs
// 项目名称： 
// 创建时间：2014/6/4
// 创建人：丁富升
// ===================================================================

using System;
using System.Linq.Expressions;

namespace Core.Repository.Specification
{
    public abstract class Specification<TEntity> : ISpecification<TEntity> where TEntity:class 
    {
        #region ISpecification<TEntity> 成员

        public abstract Expression<Func<TEntity, bool>> Predicate
        {
            get; set;
        }

        #endregion

        /// <summary>
        /// Implements the operator &amp;.
        /// </summary>
        /// <param name="leftSideSpecification">The left side specification.</param>
        /// <param name="rightSideSpecification">The right side specification.</param>
        /// <returns>The result of the operator.</returns>
        public static Specification<TEntity> operator &(Specification<TEntity> leftSideSpecification, Specification<TEntity> rightSideSpecification)
        {
            return new AndSpecification<TEntity>(leftSideSpecification, rightSideSpecification);
        }

        /// <summary>
        /// Implements the operator |.
        /// </summary>
        /// <param name="leftSideSpecification">The left side specification.</param>
        /// <param name="rightSideSpecification">The right side specification.</param>
        /// <returns>The result of the operator.</returns>
        public static Specification<TEntity> operator |(Specification<TEntity> leftSideSpecification, Specification<TEntity> rightSideSpecification)
        {
            return new OrSpecification<TEntity>(leftSideSpecification, rightSideSpecification);
        }
        /// <summary>
        /// Not specification
        /// </summary>
        /// <param name="specification">Specification to negate</param>
        /// <returns>New specification</returns>
        public static Specification<TEntity> operator !(Specification<TEntity> specification)
        {
            return new NotSpecification<TEntity>(specification);
        }
        /// <summary>
        /// Override operator false, only for support AND OR operators
        /// </summary>
        /// <param name="specification">Specification instance</param>
        /// <returns>See False operator in C#</returns>
        public static bool operator false(Specification<TEntity> specification)
        {
            return false;
        }

        /// <summary>
        /// Override operator True, only for support AND OR operators
        /// </summary>
        /// <param name="specification">Specification instance</param>
        /// <returns>See True operator in C#</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "specification")]
        public static bool operator true(Specification<TEntity> specification)
        {
            return true;
        }
    }
}
