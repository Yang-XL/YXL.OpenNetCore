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
using Core.Repository.Ef;
using PermissionSystem.Models;
namespace IService
{
    public interface IUserService : IEfRepository<User>
    {

        bool ValidateCredentials(string loginName, string password);

        User FindByExternalProvider(string provider, Guid userId);
        
    }
}