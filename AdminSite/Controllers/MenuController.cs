using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ViewModels.AdminWeb.Nav;
using ViewModels.Mapper;
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
        public async Task<IActionResult> Index(string queryString, Guid? ParentID = null, int page = 1)
        {
            var model = await _menuService.PageMenuViewModel(_setting.PageSize, page, queryString ?? "", ParentID);
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
        public  IActionResult Create(MenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();
                entity.ID = Guid.NewGuid();
                entity.CreateDate = DateTime.UtcNow;
                _menuService.Insert(entity);
                _menuService.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Modify(Guid id)
        {
            var model = await _menuService.GetMenuViewModel(id);
            model.ApplicationViewModels = from n in _applicationService._dbSet
                select new SelectListItem {Text = n.Name, Value = n.ID.ToString()};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Modify(MenuViewModel model)
        {
            model.CreateDate = DateTime.UtcNow;
            var entity = model.ToEntity(_menuService.Single(a => a.ID == model.ID));
            _menuService.Update(entity);
            _menuService.SaveChanges();
            return RedirectToAction("Index");
        }

        public  async Task< IActionResult> Detial(Guid id)
        {
            var model = await _menuService.GetMenuViewModel(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(Guid id)
        {
            var menu = await _menuService.SingleAsync(a => a.ID == id);
            if (menu == null)
            {
                return RedirectToAction("Index");
            }
            if (await _menuService.CountAsync(a => a.ParentID == id) > 0)
            {
                var model = await _menuService.GetMenuViewModel(id);
                ModelState.AddModelError(nameof(model.CustomizeErrorMessage),"拥有子模块的菜单不允许删除！");
                return View("Detial", model);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult QueryJson(Guid? applicationId = null,bool? isNav = true)
        {
            var query = _menuService._dbSet.AsQueryable();
            if (applicationId.HasValue)
            {
                query= query.Where(a => a.ApplicationID == applicationId);
            }
            if (isNav.HasValue)
            {
                query= query.Where(a => a.IsNav == isNav.Value);
            }
            var result = from n in query
                         select new {id = n.ID, pId = n.ParentID, name = n.Name, open =true};
            var model = result.ToList();
            model.Add(new {id = default(Guid), pId = default(Guid), name = "--根目录--", open = true});
            return  Json(model);
        }
       }
}