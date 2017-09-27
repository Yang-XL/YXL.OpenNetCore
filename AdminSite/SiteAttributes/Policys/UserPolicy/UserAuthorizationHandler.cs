using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdminSite.SiteAttributes.Policys.UserPolicy
{
    public class UserAuthorizationHandler : AuthorizationHandler<UserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;

           

            throw new NotImplementedException();
        }
    }
}
