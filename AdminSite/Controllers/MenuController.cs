using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IService;
using LoggerExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViewModels.AdminWeb;
using ViewModels.AdminWeb.Nav;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    public class MenuController : BaseAdminController
    {
        private readonly IApplicationService _applicationService;
        private readonly ILogger _logger;
        private readonly IMenuService _menuService;
        private readonly AdminSiteOption _setting;
        private readonly IUserRoleJurisdictionService _roleJurisdictionService;

        public MenuController(IApplicationService applicationService, IMenuService menuService,
            IOptions<AdminSiteOption> setting, ILogger<MenuController> logger,
            IUserRoleJurisdictionService roleJurisdictionService)
        {
            _applicationService = applicationService;
            _menuService = menuService;
            _logger = logger;
            _roleJurisdictionService = roleJurisdictionService;
            _setting = setting.Value;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = await _menuService.PageMenuViewModel(_setting.PageSize, page, "");
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
            var model = new MenuViewModel
            {
                ID = Guid.NewGuid(),
                ApplicationViewModels = from n in _applicationService.Query()
                    select new SelectListItem {Text = n.Name, Value = n.ID.ToString()}
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();
                entity.ID = Guid.NewGuid();
                entity.CreateDate = DateTime.Now;
                _menuService.Insert(entity);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Modify(Guid id)
        {
            var model = await _menuService.GetMenuViewModel(id);
            model.ApplicationViewModels = from n in _applicationService.Query()
                select new SelectListItem {Text = n.Name, Value = n.ID.ToString()};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Modify(MenuViewModel model)
        {
            model.CreateDate = DateTime.Now;
            var entity = model.ToEntity(_menuService.Single(model.ID));
            _menuService.Update(entity);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _menuService.GetMenuViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(Guid id)
        {
            var menu = await _menuService.SingleAsync(id);
            if (menu == null)
                return RedirectToAction("Index");
            if (await _menuService.CountAsync(a => a.ParentID == id) > 0)
            {
                var model = await _menuService.GetMenuViewModel(id);
                ModelState.AddModelError(nameof(model.CustomizeErrorMessage), "拥有子模块的菜单不允许删除！");
                return View("Details", model);
            }
            return RedirectToAction("Index");
        }





        [HttpPost]
        public IActionResult QueryJson(Guid? applicationId = null, bool? isNav = true)
        {
            var query = _menuService.Query();
            if (applicationId.HasValue)
                query = query.Where(a => a.ApplicationID == applicationId);
            if (isNav.HasValue)
                query = query.Where(a => a.IsNav == isNav.Value);
            var result = from n in query
                select new {id = n.ID, pId = n.ParentID, name = n.Name, open = true};
            var model = result.ToList();
            model.Add(new {id = default(Guid), pId = default(Guid), name = "--根目录--", open = true});
            return Json(model);
        }


        [HttpPost]
        public async Task<IActionResult> RoleMenus(Guid roleID)
        {
            try
            {
                var menus = await _menuService.QueryAsync();
                var roleMenus = await _roleJurisdictionService.QueryAsync(a => a.RoleID == roleID);
                var result = from n in menus
                    select new
                    {
                        id = n.ID+"|"+n.ApplicationID,
                        pId = n.ParentID+"|"+n.ApplicationID,
                        name = $"<span style='font-size: 13px; '>{n.Name}</span>",
                        @checked = roleMenus.FirstOrDefault(a => a.MenuID == n.ID) != null,
                        Description = n.Description.IsNullOrEmpty() ? n.Name : n.Description,
                        open = true
                    };
                var model = result.ToList();
                return Json(model);
            }
            catch (Exception e)
            {
                _logger.Error(e, "角色菜单树Json错误");
            }
            return Json("");
        }


    }
}