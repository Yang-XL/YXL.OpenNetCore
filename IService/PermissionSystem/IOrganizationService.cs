// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： IOrganizationRepository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using System.Threading.Tasks;
using Core.Repository.Ef;
using PermissionSystem.Models;
using Sakura.AspNetCore;
using ViewModels.AdminWeb.Organization;

namespace IService
{
    public interface IOrganizationService : IEfRepository<Organization>
    {
        Task<IPagedList<OrganizationViewModel>> GetPaged(int pageSize, int pageIndex, string queryString,
            Guid? parentID = null);
    }
}