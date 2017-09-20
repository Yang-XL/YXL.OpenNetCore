// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： UserRoleRespository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using Core.Repository.Implementation;
using IService;
using PermissionSystem.Models;
using PermissionSystem;
namespace Service.PermissionSystem
{
    public class UserRoleService  :EfRepository< UserRole>,IUserRoleService
    {
      public UserRoleService(PermissionSystemContext context):base(context)
        {
            
        }		
				
    }
}