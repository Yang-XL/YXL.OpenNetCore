// =================================================================== 
// 项目说明
//====================================================================
// 幸运草工作室 @ CopyRight 2014-2020
// 文件： AndSpecification.cs
// 项目名称： 
// 创建时间：2014/6/4
// 创建人：丁富升
// ===================================================================

using System;
using System.Linq.Expressions;

namespace Core.Repository.Specification
{
    public class AndSpecification<TEntity>:CompositeSpecification<TEntity>where TEntity:class
    {
        #region 变量定义

        private Expression<Func<TEntity, bool>> _predicate;
        private ISpecification<TEntity> _rightSideSpecification = null;
        private ISpecification<TEntity> _leftSideSpecification = null;
        #endregion

        #region
        /// <summary>
        /// Default constructor for AndSpecification
        /// </summary>
        /// <param name="leftSide">Left side specification</param>
        /// <param name="rightSide">Right side specification</param>
        public AndSpecification(ISpecification<TEntity> leftSide, ISpecification<TEntity> rightSide)
        {
            if (leftSide == (ISpecification<TEntity>)null)
                throw new ArgumentNullException("leftSide");

            if (rightSide == (ISpecification<TEntity>)null)
                throw new ArgumentNullException("rightSide");

            this._leftSideSpecification = leftSide;
            this._rightSideSpecification = rightSide;
        } 
        #endregion
        public override ISpecification<TEntity> LeftSideSpecification
        {
            get { return _leftSideSpecification; }
        }

        public override ISpecification<TEntity> RightSideSpecification
        {
            get { return _rightSideSpecification; }
        }
        public override Expression<Func<TEntity, bool>> Predicate
        {
            get
            {
                Expression<Func<TEntity, bool>> leftSite = _leftSideSpecification.Predicate;
                Expression<Func<TEntity, bool>> rightSite = _rightSideSpecification.Predicate;
                _predicate= leftSite.And(rightSite);
                return _predicate;
            }
            set { _predicate = value; }
        }
    }
}
