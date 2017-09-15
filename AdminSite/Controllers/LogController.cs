using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    public class LogController : BaseAdminController
    {
        private readonly ILogService _logService;
        private readonly AdminSiteOption _setting;

        public LogController(ILogService logService, AdminSiteOption setting)
        {
            _logService = logService;
            _setting = setting;
        }


        public IActionResult Index(int page = 1)
        {
          var model =   _logService.GetPaged(page, _setting.PageSize, a => a.CreateDate, b => true);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string queryString, Guid? ParentID = null, int page = 1)
        {
            var model =  await _logService.GetPagedAsync(page, _setting.PageSize, a => a.CreateDate, b => b.KeyWord.StartsWith(queryString)||b.ShortMessage.StartsWith(queryString));
            return PartialView("AjaxMenuTable", model);
        }
    }
}