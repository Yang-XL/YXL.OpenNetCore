using System;
using System.Linq;
using System.Threading.Tasks;
using AdminSite.SiteAttributes;
using IdentityServer4.Extensions;
using IService;
using LoggerExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserRoleJurisdictionService _roleJurisdictionService;
        private readonly AdminSiteOption _setting;

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

        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Index(int page = 1)
        {
            var model = await _menuService.PageMenuViewModel(_setting.PageSize, page, "");
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Index(string queryString, Guid? ParentID = null, int page = 1)
        {
            var model = await _menuService.PageMenuViewModel(_setting.PageSize, page, queryString ?? "", ParentID);
            return PartialView("AjaxMenuTable", model);
        }

        public  IActionResult Create()
        {
            var model = new MenuViewModel
            {
                ID = Guid.NewGuid(),
                ApplicationViewModels = from n in _applicationService.Query()
                    select new SelectListItem {Text = n.Name, Value = n.ID.ToString()},
                MenuType = 1,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public IActionResult Create(MenuViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = model.ToEntity();
                    entity.ID = Guid.NewGuid();
                    entity.CreateDate = DateTime.Now;
                    _menuService.Insert(entity);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.TryAddModelError(nameof(model.CustomizeErrorMessage), e.Message);
                _logger.Error(e, "添加菜单");
            }
            model.ApplicationViewModels = from n in _applicationService.Query()
                select new SelectListItem {Text = n.Name, Value = n.ID.ToString()};
            return View(model);
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
        [Authorize(PolicysModels.PolicysRole)]
        public IActionResult Modify(MenuViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreateDate = DateTime.Now;
                    var entity = model.ToEntity(_menuService.Single(model.ID));
                    _menuService.Update(entity);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.TryAddModelError(nameof(model.CustomizeErrorMessage), e.Message);
                _logger.Error(e, "修改菜单");
            }
            return View(model);
        }

        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _menuService.GetMenuViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Remove(Guid id)
        {
            var menu = await _menuService.SingleAsync(id);
            if (menu == null)
                return RedirectToAction("Index");
            try
            {
                await _menuService.DeleteAsync(id);
            }
            catch (Exception e)
            {
                var entity = await _menuService.SingleAsync(id);
                var model = entity.ToModel();
                ModelState.AddModelError(nameof(model.CustomizeErrorMessage), e.Message);
                _logger.Error(e, "删除菜单");
                return View("Details", model);
            }
            return RedirectToAction("Index");
        }

        #region JsonResult

        [HttpPost]
        public async Task<IActionResult> QueryJson(Guid? applicationId = null, bool? isNav = true)
        {
            var query = _menuService.Queryable();
            if (applicationId.HasValue)
                query = query.Where(a => a.ApplicationID == applicationId);
            if (isNav.HasValue)
                query = query.Where(a => a.IsNav == isNav.Value);
            var result = from n in query
                select new {id = n.ID, pId = n.ParentID, name = n.Name, open = true};
            var model = await result.ToListAsync();
            model.Add(new {id = default(Guid), pId = default(Guid), name = "--根目录--", open = true});
            return Json(model);
        }



        [HttpPost]
        public async Task<IActionResult> RoleMenus(Guid roleID)
        {
            try
            {
                    var result = from n in _menuService.Queryable()
                    select new
                    {
                        id = $"{n.ID}|{n.ApplicationID}",
                        pId = $"{n.ParentID}|{n.ApplicationID}",
                        name = $"<span style='font-size: 13px; '>{n.Name}</span>",
                        @checked = _roleJurisdictionService.Queryable().Any(a=>a.MenuID ==n.ID&&a.RoleID == roleID),
                        Description = n.Description.IsNullOrEmpty() ? n.Name : n.Description,
                        open = true
                    };
                var model = await result.ToListAsync();
                return Json(model);
            }
            catch (Exception e)
            {
                _logger.Error(e, "角色菜单树Json错误");
            }
            return Json("");
        }


        #endregion
    }
}