using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdminSite.SiteAttributes.Policys.UserPolicy
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RoleRequirement requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext.ActionDescriptor.FilterDescriptors
                    .FirstOrDefault(a => a.Filter is IAllowAnonymousFilter) != null)
                context.Succeed(requirement);
            if (context.User.Identity.IsAuthenticated)
            {
                var action = filterContext.ActionDescriptor as ControllerActionDescriptor;
                var areaName = "";
                action.RouteValues.TryGetValue("area", out  areaName);
                var rolseClaims = context.User.Claims.Where(a => a.Type == ClaimTypes.Role);
                var roleList = from n in rolseClaims select new Guid(n.Value);

                if (await requirement.IsAuth(roleList, areaName, action.ControllerName, action.ActionName))
                    context.Succeed(requirement);
            }
        }
    }
}