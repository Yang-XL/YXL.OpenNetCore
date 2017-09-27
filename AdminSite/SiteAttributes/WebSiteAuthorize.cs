using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Repository.Specification;
using IdentityServer4.Extensions;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Org.BouncyCastle.Asn1.X509.Qualified;
using PermissionSystem.Models;

namespace AdminSite.SiteAttributes
{
    public class AdminSiteAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IMenuService _menuService;

        public AdminSiteAuthorizationFilter(IMenuService menuService, IUserRoleService userRoleService)
        {
            _menuService = menuService;
            _userRoleService = userRoleService;
        }

        public  void OnAuthorization(AuthorizationFilterContext context)
        {
           if (context.ActionDescriptor.FilterDescriptors.FirstOrDefault(a => a.Filter is IAllowAnonymousFilter) != null)
            {
                return;
            }
            var b = context.HttpContext.User.IsAuthenticated();
            var c = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
           if (!context.HttpContext.User.IsAuthenticated() || context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            if (string.IsNullOrEmpty(action.RouteValues["area"]) && action.ActionName.ToUpper() == "INDEX" &&
                action.ControllerName.ToUpper() == "HOME")
            {
                return;
            }
            var query = SpecificationBuilder.Create<Menu>();
            query.Equals(a => a.AreaName.ToUpper(), action.RouteValues["area"].ToUpper());
            query.Equals(a => a.ActionName.ToUpper(), action.ActionName.ToUpper());
            query.Equals(a => a.ControllerName.ToUpper(), action.ControllerName.ToUpper());
            var menu = _menuService.Single(query);
            if (menu == null)
            {
                context.Result = new NotFoundResult();
                return;
            }
            var rid = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var roleMenu =  _userRoleService.IsPermissionsMenu(new Guid(rid.Value), menu.ID);
            if (!roleMenu.IsFaulted && roleMenu.Result)
            {
                context.Result = new ForbidResult();
            }
            
        }
    }
}
