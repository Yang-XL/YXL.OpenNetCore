// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： ClientRespository.cs
// 项目名称： 
// 创建时间：2017-08-25
// 负责人：YXL
// ===================================================================

using Core.Repository.Ef;
using IService.PermissionSystem;
using PermissionSystem.Models;
using PermissionSystem;
namespace Service.PermissionSystem
{
    public class ClientService  :EfRepository< Client>,IClientService
    {
      public ClientService(PermissionSystemContext context):base(context)
        {
            
        }		
				
    }
}