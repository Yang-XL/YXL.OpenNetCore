// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： IUserRepository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Repository;
using PermissionSystem.Models;
using ViewModels.AdminWeb.User;

namespace IService
{
    public interface IUserService : IEfRepository<User>
    {

        Task SaveUser(UserViewModel model);
        bool ValidateCredentials(string loginName, string password);

        User FindByExternalProvider(string provider, Guid userId);
        
    }
}