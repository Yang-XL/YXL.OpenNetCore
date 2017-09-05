// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： IRoleClaimRepository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using Core.Repository.Ef;
using PermissionSystem.Models;
namespace IService
{
    public interface IRoleClaimService : IEfRepository<RoleClaim>
    {
        
    }
}