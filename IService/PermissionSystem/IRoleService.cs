// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： IRoleRepository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using System.Threading.Tasks;
using Core.Repository;
using PermissionSystem.Models;
using ViewModels.AdminWeb.Roles;

namespace IService
{
    public interface IRoleService : IEfRepository<Role>
    {
        Task SaveRole(RoleViewModel model);
    }
}