using System;
using System.Linq;
using System.Threading.Tasks;
using AdminSite.SiteAttributes;
using AutoMapper;
using IService;
using LoggerExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViewModels.AdminWeb;
using ViewModels.AdminWeb.Roles;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    public class RoleController : BaseAdminController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly IUserRoleJurisdictionService _userRoleJurisdictionService;
        private readonly AdminSiteOption _setting;

        public RoleController(IRoleService roleService, IOptions<AdminSiteOption> setting,
            IMapper mapper, ILoggerFactory loggerFactory, IUserRoleJurisdictionService userRoleJurisdictionService)
        {
            _roleService = roleService;
            _mapper = mapper;
            _userRoleJurisdictionService = userRoleJurisdictionService;
            _logger = loggerFactory.CreateLogger<RoleController>();
            _setting = setting.Value;
        }
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Index(int page = 1)
        {
            var app = await _roleService.GetPagedAsync(page, _setting.PageSize, a => a.ShowIndex, b => true);
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Index(string queryString, int page = 1)
        {
            var app = await _roleService.GetPagedAsync(page, _setting.PageSize, a => a.ShowIndex,
                b => b.Name.Contains(queryString) || b.PyCode.Contains(queryString));
            return View(app);
        }

        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Details(Guid id)
        {
            var enitty = await _roleService.SingleAsync(a => a.ID == id);
            var model = enitty.ToModel();
            return View(model);
        }
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Modify(Guid id)
        {
            var enitty = await _roleService.SingleAsync(id);
            var model = enitty.ToModel();
            var roleMenu = (from n in _userRoleJurisdictionService.Query()
                select n.ID + "|" + n.ApplicationID).ToList();
            model.RoleMenus = roleMenu;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Modify(RoleViewModel model)
        {
            try
            {
                model.CreateDate = DateTime.Now;
                await _roleService.SaveRole(model);
            }
            catch (Exception e)
            {
                _logger.Error(e, "修改角色");
                ModelState.TryAddModelError(nameof(model.CustomizeErrorMessage), e.Message);
                return View(model);
            }
            return RedirectToAction("Details", new {id = model.ID});
        }
        [Authorize(PolicysModels.PolicysRole)]
        public IActionResult Create()
        {
            var model = new RoleViewModel
            {
                ID = Guid.NewGuid()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            try
            {
                model.CreateDate = DateTime.Now;
                await _roleService.SaveRole(model);
            }
            catch (Exception e)
            {
                _logger.Error(e, "添加角色");
                ModelState.TryAddModelError(nameof(model.CustomizeErrorMessage), e.Message);
                return View(model);
            }
            return RedirectToAction("Details", new {id = model.ID});
        }
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                await _roleService.DeleteAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogCritical("删除应用程序错误：{e}", e);
                RedirectToAction("Details", id);
            }
            return RedirectToAction("Index");
        }

        #region JsonData
        [Authorize("AuthorizedUser")]
        [HttpPost]
        public async Task<IActionResult> QueryJson()
        {
            var list = _roleService.Queryable();

            var models = await (from n in list select new {id = n.ID, text = n.Name}).ToListAsync();
            return Json(models);
        }

        #endregion
    }
}