using AdminSite.SiteAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdminSite.Controllers
{
    
    [Authorize(PolicysModels.PolicysUser)]
    public class BaseAdminController : Controller
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewData["Title"] = "后台管理系统";
            base.OnActionExecuted(context);
        }
    }
}