using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Repository.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using PermissionSystem.Models;

namespace AdminSite.SiteAttributes.Policys.UserPolicy
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RoleRequirement requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            var actionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;
            var areaName = "";
            actionDescriptor.RouteValues.TryGetValue("area", out areaName);
            var rolseClaims = context.User.Claims.Where(a => a.Type == ClaimTypes.Role);
            var roleList = from n in rolseClaims select new Guid(n.Value);
            if (await requirement.IsAuth(roleList, areaName, actionDescriptor.ControllerName,
                actionDescriptor.ActionName))
                context.Succeed(requirement);

        }


    }
}