// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： IApiRepository.cs
// 项目名称： 
// 创建时间：2017-08-25
// 负责人：YXL
// ===================================================================

using Core.Repository.Ef;
using PermissionSystem.Models;
namespace IService.PermissionSystem
{
    public interface IApiService : IEfRepository<Api>
    {
        
    }
}