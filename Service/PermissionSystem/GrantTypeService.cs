// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： GrantTypeRespository.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================

using Core.Repository.Implementation;
using IService.PermissionSystem;
using PermissionSystem.Models;
using PermissionSystem;
namespace Service.PermissionSystem
{
    public class GrantTypeService  :EfRepository< GrantType>,IGrantTypeService
    {
      public GrantTypeService(PermissionSystemContext context):base(context)
        {
            
        }		
				
    }
}