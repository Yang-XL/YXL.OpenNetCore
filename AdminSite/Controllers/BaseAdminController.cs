using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    public class BaseAdminController : Controller
    {
        
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewData["Title"] = "后台管理系统";
            base.OnActionExecuted(context);
        }
    }
}
