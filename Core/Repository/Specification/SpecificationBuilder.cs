// =================================================================== 
// 项目说明
//====================================================================
// 幸运草工作室 @ CopyRight 2014-2020
// 文件： SpecificationBuilder.cs
// 项目名称： 
// 创建时间：2014/6/4
// 创建人：丁富升
// ===================================================================

using System;
using System.Linq.Expressions;

namespace Core.Repository.Specification
{
    public sealed class SpecificationBuilder
    {
        public static ISpecification<TEntity> Create<TEntity>()where TEntity:class 
        {
            return new SpecificationBuilder<TEntity>();
        }
    }
    public class SpecificationBuilder<TEntity>:Specification<TEntity> where TEntity:class 
    {
        private Expression<Func<TEntity, bool>> _predicate;
        public SpecificationBuilder()
        {
            _predicate = a => true;
        }
        public override System.Linq.Expressions.Expression<Func<TEntity, bool>> Predicate
        {
            get
            {
                return _predicate;
            }
            set
            {
                _predicate = value;
            }
        }
    }
}
