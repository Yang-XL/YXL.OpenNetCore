// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： UserClaimRespository.cs
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
    public class UserClaimService  :EfRepository< UserClaim>,IUserClaimService
    {
      public UserClaimService(PermissionSystemContext context):base(context)
        {
            
        }		
				
    }
}