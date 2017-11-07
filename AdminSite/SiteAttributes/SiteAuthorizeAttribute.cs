using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AdminSite.SiteAttributes
{
    public class SiteAuthorizeAttribute : AuthorizeAttribute
    {

        public SiteAuthorizeAttribute(string policy) : base(policy)
        {
        }
    }
}
