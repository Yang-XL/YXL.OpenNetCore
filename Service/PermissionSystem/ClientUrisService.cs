// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： ClientUrisRespository.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================

using Core.Repository.Implementation;
using IService.PermissionSystem;
using PermissionSystem.Models;
using PermissionSystem;
namespace Service.PermissionSystem
{
    public class ClientUrisService  :EfRepository< ClientUris>,IClientUrisService
    {
      public ClientUrisService(PermissionSystemContext context):base(context)
        {
            
        }		
				
    }
}