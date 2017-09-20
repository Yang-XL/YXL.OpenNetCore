// =================================================================== 
// 项目说明
//====================================================================
// 幸运草工作室 @ CopyRight 2014-2020
// 文件： OrSpecification.cs
// 项目名称： 
// 创建时间：2014/6/4
// 创建人：丁富升
// ===================================================================

using System;
using System.Linq.Expressions;

namespace Core.Repository.Specification
{
    /// <summary>
    /// A Logic OR Specification
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that check this specification</typeparam>
    public sealed class OrSpecification<TEntity>
         : CompositeSpecification<TEntity>
         where TEntity : class
    {
        #region 变量定义
        private Expression<Func<TEntity, bool>> _predicate;
        private ISpecification<TEntity> _rightSideSpecification = null;
        private ISpecification<TEntity> _leftSideSpecification = null;
        #endregion

        #region Public Constructor

        /// <summary>
        /// Default constructor for AndSpecification
        /// </summary>
        /// <param name="leftSide">Left side specification</param>
        /// <param name="rightSide">Right side specification</param>
        public OrSpecification(ISpecification<TEntity> leftSide, ISpecification<TEntity> rightSide)
        {
            if (leftSide == (ISpecification<TEntity>)null)
                throw new ArgumentNullException("leftSide");

            if (rightSide == (ISpecification<TEntity>)null)
                throw new ArgumentNullException("rightSide");

            this._leftSideSpecification = leftSide;
            this._rightSideSpecification = rightSide;
        }

        #endregion

        #region Composite Specification overrides

        /// <summary>
        /// Left side specification
        /// </summary>
        public override ISpecification<TEntity> LeftSideSpecification
        {
            get { return _leftSideSpecification; }
        }

        /// <summary>
        /// Righ side specification
        /// </summary>
        public override ISpecification<TEntity> RightSideSpecification
        {
            get { return _rightSideSpecification; }
        }

        /// <summary>
        /// Gets the predicate.
        /// </summary>
        /// <value>The predicate.</value>
        public override Expression<Func<TEntity, bool>> Predicate
        {
            get
            {
                Expression<Func<TEntity, bool>> leftSite = _leftSideSpecification.Predicate;
                Expression<Func<TEntity, bool>> rightSite = _rightSideSpecification.Predicate;
                 _predicate=leftSite.Or(rightSite);
                return _predicate;
            }
            set { _predicate = value; }
        }

        #endregion
    }
}
