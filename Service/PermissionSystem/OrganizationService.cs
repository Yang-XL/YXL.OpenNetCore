// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： OrganizationRespository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Repository.Ef;
using IService;
using PermissionSystem.Models;
using PermissionSystem;
using Sakura.AspNetCore;
using ViewModels.AdminWeb;
using ViewModels.AdminWeb.Nav;
using ViewModels.AdminWeb.Organization;

namespace Service.PermissionSystem
{
    public class OrganizationService  : EfRepository< Organization>,IOrganizationService
    {
        private readonly IUserService _userService;
      public OrganizationService(PermissionSystemContext context, IUserService userService):base(context)
      {
          _userService = userService;
      }
        public Task<IPagedList<OrganizationViewModel>> GetPaged(int pageSize, int pageIndex, string queryString, Guid? parentID = null)
        {
            return Task.Run<IPagedList<OrganizationViewModel>>(() =>
            {
                var query = _dbSet.AsQueryable();
                if (!string.IsNullOrEmpty(queryString))
                    query= query.Where(a => a.Name.Contains(queryString) || a.PyCode.Contains(queryString));
                if (parentID.HasValue)
                    query = query.Where(a => a.ParentOrganizationID == parentID.Value);
                var result = from n in query
                    orderby n.ShowIndex
                    select new OrganizationViewModel
                    {
                        ID = n.ID,
                        Code =  n.Code,
                        CreateDate = n.CreateDate,
                        Description = n.Description,
                        Leader =  n.Leader,
                        Name = n.Name,
                        PyCode = n.PyCode,
                        ShowIndex = n.ShowIndex,
                        LeaderName = n.Leader.HasValue? _userService.Single(a=>a.ID==n.Leader).Name:"",
                        ParentOrganizationName = n.ParentOrganizationID.HasValue?_dbSet.FirstOrDefault(a=>a.ID==n.ParentOrganizationID).Name:""
                    };
                return result.ToPagedList(pageSize, pageIndex);
            });
        }
    }
}