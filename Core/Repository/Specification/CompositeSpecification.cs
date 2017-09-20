// =================================================================== 
// 项目说明
//====================================================================
// 幸运草工作室 @ CopyRight 2014-2020
// 文件： CompositeSpecification.cs
// 项目名称： 
// 创建时间：2014/6/4
// 创建人：丁富升
// ===================================================================

namespace Core.Repository.Specification
{
    public abstract class CompositeSpecification<TEntity>:Specification<TEntity> where TEntity:class 
    {
        /// <summary>
        /// Left side specification for this composite element
        /// </summary>
        public abstract ISpecification<TEntity> LeftSideSpecification { get; }

        /// <summary>
        /// Right side specification for this composite element
        /// </summary>
        public abstract ISpecification<TEntity> RightSideSpecification { get; }
    }
}
