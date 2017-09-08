using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViewModels.AdminWeb.Nav;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    public class MenuController : BaseAdminController
    {
        private readonly IMenuService _menuService;
        private readonly IApplicationService _applicationService;
        private readonly AdminSiteOption _setting;
        private readonly ILogger _logger;

        public MenuController(IApplicationService applicationService, IMenuService menuService, IOptions<AdminSiteOption> setting, ILoggerFactory loggerFactory)
        {
            _applicationService = applicationService;
            _menuService = menuService;
            _logger = loggerFactory.CreateLogger<MenuController>();
            _setting = setting.Value;
        }

        public IActionResult Index(int page = 1)
        {
            var model = _menuService.PageMenuViewModel(_setting.PageSize, page, "").Result;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string queryString, int page = 1)
        {
            var model = await _menuService.PageMenuViewModel(_setting.PageSize, page, queryString ?? "");
            return PartialView("AjaxMenuTable", model);
        }
        
        public IActionResult Create()
        {
            var model = new MenuViewModel()
            {
                ID =  Guid.NewGuid(),
                ApplicationViewModels = from n in _applicationService._dbSet select new SelectListItem { Text = n.Name,Value = n.ID.ToString()}
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MenuViewModel model)
        {
            return RedirectToAction("Index");
        }

        public IActionResult Modify(Guid id)
        {
            return View();
        }

        public IActionResult Detial(Guid id)
        {
            return View();
        }

        public IActionResult QueryJson(Guid? applicationId)
        {
            var result = from n in _menuService._dbSet
                where n.ApplicationID == applicationId
                select new {id = n.ID, pId = n.ParentID, name = n.Name, open =true};
            return  Json(result);
        }
    }
}