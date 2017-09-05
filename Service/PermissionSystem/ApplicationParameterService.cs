// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： ApplicationParameterRespository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using Core.Repository.Ef;
using IService;
using PermissionSystem;
using PermissionSystem.Models;

namespace Service.PermissionSystem
{
    public class ApplicationParameterService : EfRepository<ApplicationParameter>, IApplicationParameterService
    {
        public ApplicationParameterService(PermissionSystemContext context) : base(context)
        {
        }
    }
}