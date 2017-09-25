using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AdminSite.SiteAttributes
{
    public class WebSiteAuthorize : AuthorizeAttribute
    {
        public WebSiteAuthorize()
        {
            
        }
    }
}
