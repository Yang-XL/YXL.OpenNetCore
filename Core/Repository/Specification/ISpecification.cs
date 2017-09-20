// =================================================================== 
// 项目说明
//====================================================================
// 幸运草工作室 @ CopyRight 2014-2020
// 文件： ISpecification.cs
// 项目名称： 
// 创建时间：2014/6/4
// 创建人：丁富升
// ===================================================================

using System;
using System.Linq.Expressions;

namespace Core.Repository.Specification
{
    public  interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> Predicate { get; set; }
    }
}
