// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： ClientGrantTypesRespository.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================

using Core.Repository.Ef;
using IService.PermissionSystem;
using PermissionSystem.Models;
using PermissionSystem;
namespace Service.PermissionSystem
{
    public class ClientGrantTypesService  :EfRepository< ClientGrantTypes>,IClientGrantTypesService
    {
      public ClientGrantTypesService(PermissionSystemContext context):base(context)
        {
            
        }		
				
    }
}