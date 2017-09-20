// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： ApplicationRespository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Repository.Implementation;
using IService;
using PermissionSystem.Models;
using PermissionSystem;
using Sakura.AspNetCore;
using ViewModels.AdminWeb.Application;
using ViewModels.AdminWeb.Nav;

namespace Service.PermissionSystem
{
    public class ApplicationService  : EfRepository<Application>,IApplicationService
    {
      public ApplicationService(PermissionSystemContext context):base(context)
        {
            
        }
      

    }
}