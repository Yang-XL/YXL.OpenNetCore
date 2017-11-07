using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Repository.Specification;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PermissionSystem.Models;

namespace AdminSite.SiteAttributes.Policys.UserPolicy
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        private readonly IMenuService _menuService;
        private readonly IUserRoleJurisdictionService _userRoleJurisdictionService;

        public RoleRequirement(IServiceProvider serviceProvider)
        {
            _menuService = serviceProvider.GetService<IMenuService>();
            _userRoleJurisdictionService = serviceProvider.GetService<IUserRoleJurisdictionService>();
        }


        public async Task<bool> IsAuth(IEnumerable<Guid> roleList, string areaName, string controllerName,
            string actionName)
        {
            if (roleList.Any(a=>a.Equals(new Guid("24F8290B-029D-4976-B171-D4DA02925CCB"))))
            {
                return true;
            }
            if (string.IsNullOrEmpty(areaName) && controllerName.ToUpper() == "HOME" && actionName.ToUpper() == "INDEX")
                return true;
            var query = SpecificationBuilder.Create<Menu>();
            if (!string.IsNullOrEmpty(areaName))
                query.Equals(a => a.AreaName.ToUpper(), areaName.ToUpper());
            query.Equals(a => a.ActionName.ToUpper(), actionName.ToUpper());
            query.Equals(a => a.ControllerName.ToUpper(), controllerName.ToUpper());
            var menu = await _menuService.SingleAsync(query);
            if (menu == null) return false;
            return await _userRoleJurisdictionService.ExistsAsync(
                a => a.MenuID == menu.ID && roleList.Contains(a.RoleID));
        }
    }
}