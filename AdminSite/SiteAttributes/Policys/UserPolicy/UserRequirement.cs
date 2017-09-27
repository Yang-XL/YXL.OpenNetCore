using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AdminSite.SiteAttributes.Policys.UserPolicy
{
    public class UserRequirement : IAuthorizationRequirement
    {
        public UserRequirement()
        {
            
        }
    }
}
