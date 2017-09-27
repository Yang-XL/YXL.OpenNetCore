using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    [Authorize("Role")]
    public class BaseAdminController : Controller
    {
        
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewData["Title"] = "后台管理系统";
            base.OnActionExecuted(context);
        }
    }
}
