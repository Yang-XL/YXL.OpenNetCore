// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： MenuRespository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using Sakura.AspNetCore;
using Core;
using Core.Repository.Implementation;
using Core.Repository.Specification;
using IService;
using PermissionSystem.Models;
using PermissionSystem;
using ViewModels.AdminWeb;
using ViewModels.AdminWeb.Nav;

namespace Service.PermissionSystem
{
    public class MenuService : EfRepository<Menu>, IMenuService
    {
        private readonly IApplicationService _applicationService;
        private readonly IUserRoleService _userRoleService;
        private readonly IUserRoleJurisdictionService _userRoleJurisdictionService;
        private readonly ILogger _logger;

        public MenuService(PermissionSystemContext context, IApplicationService applicationService,
            ILoggerFactory loggerFactory, IUserRoleService userRoleService, IUserRoleJurisdictionService userRoleJurisdictionService) : base(context)
        {
            _applicationService = applicationService;
            _userRoleService = userRoleService;
            _userRoleJurisdictionService = userRoleJurisdictionService;
            _logger = loggerFactory.CreateLogger<MenuService>();
        }

        public async Task<IPagedList<MenuViewModel>> PageMenuViewModel(int pageSize, int pageIndex, string queryString,Guid? parentID = null)
        {
            return await Task.Run<IPagedList<MenuViewModel>>(() =>
            {
                IQueryable<Menu> queryable = _dbSet;
                if (!string.IsNullOrEmpty(queryString))
                {
                    queryable = queryable.Where(n => n.Name.Contains(queryString) || n.PyCode.Contains(queryString));
                }
                if (parentID.HasValue)
                {
                    queryable = queryable.Where(a => a.ParentID == parentID);
                }

                var query = from n in queryable
                            orderby n.ShowIndex
                    select new MenuViewModel
                    {
                        ID = n.ID,
                        ApplicationName = n.ApplicationID_Model.Name,
                        ActionName = n.ActionName,
                        ApplicationID = n.ApplicationID,
                        PyCode = n.PyCode,
                        Code = n.Code,
                        ControllerName = n.ControllerName,
                        CreateDate = n.CreateDate,
                        Description = n.Description,
                        IconCss = n.IconCss,
                        IsNav = n.IsNav,
                        Name = n.Name,
                        ShowIndex = n.ShowIndex,
                        ParentID = n.ParentID,
                        ParentMenuName = n.ParentID == default(Guid) ? "--根目录--" : _dbSet.FirstOrDefault(a => a.ID == n.ParentID).Name
                    };

                return query.ToPagedList(pageSize, pageIndex);
            });
        }

        public Task<MenuViewModel> GetMenuViewModel(Guid rid)
        {
            var query = from n in _dbSet
                where n.ID == rid
                select new MenuViewModel
                {
                    ID = n.ID,
                    ApplicationName = n.ApplicationID_Model.Name,
                    ActionName = n.ActionName,
                    ApplicationID = n.ApplicationID,
                    Code = n.Code,
                    ControllerName = n.ControllerName,
                    CreateDate = n.CreateDate,
                    Description = n.Description,
                    IconCss = n.IconCss,
                    IsNav = n.IsNav,
                    Name = n.Name,
                    ShowIndex = n.ShowIndex,
                    ParentID = n.ParentID,
                    ParentMenuName = n.ParentID == default(Guid)
                        ? "--根目录--"
                        : _dbSet.FirstOrDefault(a => a.ID == n.ParentID).Name,
                    MenuType = n.IsNav ? (string.IsNullOrEmpty(n.ActionName) ? 1 : 2) : 3
                };
            return query.FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Menu>> QueryMenuViewModel(IEnumerable<Guid> roleList)
        {

            throw new NotImplementedException();
        }

        public  async Task<IEnumerable<Menu>> QueryMenuViewModel(Guid userID)
        {
            var result = await (from m in Queryable()
                join urj in _userRoleJurisdictionService.Queryable() on m.ID equals urj.MenuID
                join ur in _userRoleService.Queryable() on urj.RoleID equals ur.RoleID
                where ur.UserID.Equals(userID)
                select m).Distinct().ToListAsync();
            return result;
        }

        public async Task<Menu> SingleAsync(string areaName, string controllerName, string actionName)
        {
            var query = SpecificationBuilder.Create<Menu>();
            if (!string.IsNullOrEmpty(areaName))
                query.Equals(a => a.AreaName.ToUpper(), areaName.ToUpper());
            query.Equals(a => a.ActionName.ToUpper(), actionName.ToUpper());
            query.Equals(a => a.ControllerName.ToUpper(), controllerName.ToUpper());
            return  await SingleAsync(query);
        }
    }


}