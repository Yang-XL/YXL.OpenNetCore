// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： UserRespository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using Core.Repository.Implementation;
using IService;
using PermissionSystem.Models;
using PermissionSystem;
namespace Service.PermissionSystem
{
    public class UserService  :EfRepository< User>,IUserService
    {
      public UserService(PermissionSystemContext context):base(context)
        {
            
        }

        public bool ValidateCredentials(string loginName, string password)
        {
            return true;
        }

        public User FindByExternalProvider(string provider, Guid userId)
        {
            return new User {Name = "ZZ", ID = userId};
        }
    }
}