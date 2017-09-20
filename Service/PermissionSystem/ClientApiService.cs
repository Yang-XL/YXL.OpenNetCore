// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： ClientApiRespository.cs
// 项目名称： 
// 创建时间：2017-08-25
// 负责人：YXL
// ===================================================================

using Core.Repository.Implementation;
using PermissionSystem.Models;
using PermissionSystem;
using IService.PermissionSystem;
namespace Service.PermissionSystem
{
    public class ClientApiService  :EfRepository< ClientApi>,IClientApiService
    {
      public ClientApiService(PermissionSystemContext context):base(context)
        {
            
        }		
				
    }
}