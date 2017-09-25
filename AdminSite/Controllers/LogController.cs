using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Repository.Specification;
using IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mongo.Models;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    public class LogController : BaseAdminController
    {
        private readonly ILogService _logService;
        private readonly AdminSiteOption _setting;

        public LogController(ILogService logService, IOptions<AdminSiteOption> setting)
        {
            _logService = logService;
            _setting = setting.Value;
        }


        public async Task<IActionResult> Index(int page = 1)
        {
          var model = await   _logService.GetPagedAsync(page, _setting.PageSize, a => a.CreateDate,true);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index( string queryString, int queryLogLevel, int page = 1)
        {
            var query = SpecificationBuilder.Create<PermissionSystemLogs>();
         
            if (!string.IsNullOrEmpty(queryString))
            {
                query.StartWith(a => a.KeyWord, queryString);
                query.Predicate.Or(a => a.ShortMessage.StartsWith(queryString));
            }
            if (queryLogLevel != -1)
            {
                query.Equals(a => a.LogLeve, queryLogLevel);
            }
            var model =  await _logService.GetPagedAsync(page, _setting.PageSize, a => a.CreateDate, query,true);
            return PartialView("AjaxTable", model);
        }


        public async Task<IActionResult> Detials(string id)
        {
            return Json(await _logService.SingleAsync(id));
        }
    }
}