using System;
using System.Linq;
using System.Threading.Tasks;
using AdminSite.SiteAttributes;
using IService;
using LoggerExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViewModels.AdminWeb;
using ViewModels.AdminWeb.Organization;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly ILogger _logger;
        private readonly IOrganizationService _organizationService;
        private readonly AdminSiteOption _setting;
        private readonly IUserService _userService;

        public OrganizationController(IOptions<AdminSiteOption> setting, IUserService userService,
            IOrganizationService organizationService, ILogger<OrganizationController> logger)
        {
            _userService = userService;
            _organizationService = organizationService;
            _logger = logger;
            _setting = setting.Value;
        }

        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Index(int page = 1)
        {
            var model = await _organizationService.GetPaged(_setting.PageSize, page, "");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Index(string queryString, Guid? ParentID = null, int page = 1)
        {
            var model = await _organizationService.GetPaged(_setting.PageSize, page, queryString ?? "", ParentID);
            return PartialView("AjaxTable", model);
        }

        [Authorize(PolicysModels.PolicysRole)]
        public async Task<ActionResult> Details(Guid id)
        {
            var entity = await _organizationService.SingleAsync(a => a.ID == id);
            var model = entity.ToModel();
            if (entity.Leader.HasValue)
            {
                var leader = await _userService.SingleAsync(a => a.ID == entity.Leader);
                model.LeaderName = leader == null ? "" : leader.Name;
            }
            if (entity.Leader.HasValue)
            {
                var parnet = await _organizationService.SingleAsync(a => a.ID == entity.ParentOrganizationID);
                model.ParentOrganizationName = parnet == null ? "顶级" : parnet.Name;
            }
            return View(model);
        }

        [Authorize(PolicysModels.PolicysRole)]
        public ActionResult Create()
        {
            var model = new OrganizationViewModel
            {
                ID = Guid.NewGuid()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<ActionResult> Create(OrganizationViewModel model)
        {
            try
            {
                model.CreateDate = DateTime.Now;
                if (!ModelState.IsValid)
                    return View(model);
                var entity = model.ToEntity();
                await _organizationService.InsertAsync(entity);
                return RedirectToAction(nameof(Details), new {id = model.ID});
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString(), "创建组织架构", e.Message);
                ModelState.TryAddModelError(nameof(model.CustomizeErrorMessage), e.ToString());
                return View(model);
            }
        }

        [Authorize(PolicysModels.PolicysRole)]
        public async Task<ActionResult> Modify(Guid id)
        {
            var entity = await _organizationService.SingleAsync(a => a.ID == id);
            var model = entity.ToModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<ActionResult> Modify(OrganizationViewModel model)
        {
            try
            {
                var entity = await _organizationService.SingleAsync(a => a.ID == model.ID);

                entity = model.ToEntity(entity);
                await _organizationService.UpdateAsync(entity);

                return RedirectToAction(nameof(Details), new {id = model.ID});
            }
            catch (Exception e)
            {
                _logger.Error(e, "修改组织架构错误");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<ActionResult> Remove(Guid id)
        {
            try
            {
                var result = await _organizationService.DeleteAsync(id);

                if (result != 1)
                    throw new Exception("删除组织架构失败");
                return RedirectToAction(nameof(Details), id);
            }
            catch (Exception e)
            {
                _logger.Error(e, "删除组织架构失败");
                return RedirectToAction(nameof(Details), id);
            }
        }

        #region JsonResult

        [HttpPost]
        public async Task<IActionResult> ZtreeOrganization()
        {
            var list = _organizationService.Queryable();
            var result = await (from n in list
                select new {id = n.ID, pId = n.ParentOrganizationID, name = n.Name, open = true}).ToListAsync();
            return Json(result);
        }

        #endregion
    }
}