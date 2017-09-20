// =================================================================== 
// 项目说明
//====================================================================
// 幸运草工作室 @ CopyRight 2014-2020
// 文件： DirectSpecification.cs
// 项目名称： 
// 创建时间：2014/6/4
// 创建人：丁富升
// ===================================================================

using System;
using System.Linq.Expressions;

namespace Core.Repository.Specification
{
    public sealed class DirectSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        public DirectSpecification(Expression<Func<TEntity, bool>> expression)
        {
            _expression = expression;
        }
        Expression<Func<TEntity, bool>> _expression;
        public override Expression<Func<TEntity, bool>> Predicate
        {
            get { return _expression; }
            set { _expression = value; }
        }
    }
}
