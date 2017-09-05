// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： LogRespository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using Core.Repository.Ef;
using IService;
using PermissionSystem.Models;
using PermissionSystem;
namespace Service.PermissionSystem
{
    public class LogService  : EfRepository< Log>,ILogService
    {
      public LogService(PermissionSystemContext context):base(context)
        {
            
        }		
				
    }
}