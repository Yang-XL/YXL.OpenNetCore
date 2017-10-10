using System;
using System.Linq;
using System.Threading.Tasks;
using AdminSite.SiteAttributes;
using Core.Repository.Specification;
using IService;
using LoggerExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PermissionSystem.Models;
using Sakura.AspNetCore;
using ViewModels.AdminWeb;
using ViewModels.AdminWeb.User;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IOrganizationService _organizationService;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;
        private readonly AdminSiteOption _setting;
        private readonly ILogger _logger;


        public UserController(IUserService userService, IOrganizationService organizationService, IOptions<AdminSiteOption> setting, ILogger<UserController> logger, IRoleService roleService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _organizationService = organizationService;
            _logger = logger;
            _roleService = roleService;
            _userRoleService = userRoleService;
            _setting = setting.Value;
        }
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Index(int page = 1)
        {
            var entityList = await _userService.GetPagedAsync( page, _setting.PageSize, a=>a.CreateDate,true);
            return View(entityList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Index(string queryString, Guid? ParentID = null, int page = 1)
        {
            var query = SpecificationBuilder.Create<User>();
            if (!string.IsNullOrEmpty(queryString))
            {
                query.StartWith(a => a.Name, queryString);
                query.Predicate.Or(a => a.LoginName.StartsWith(queryString));
            }
            var entityList = await _userService.GetPagedAsync(page, _setting.PageSize, a => a.CreateDate, query, true);
            return PartialView("AjaxTable", entityList);
        }


        public async  Task<IActionResult> Create()
        {
            var roles = await (from n in  _roleService.Queryable() select new SelectListItem{Value = n.ID.ToString(),Text = n.Name}).ToListAsync();
            var mode = new UserViewModel
            {
                ID = Guid.NewGuid(),
                Roles = roles
            };
            return View(mode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<ActionResult> Create(UserViewModel model, IFormCollection form)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();
                model.RoleID = form["RoleID"];
                await _userService.SaveUser(model);
            }
            catch (Exception e)
            {
                _logger.Error(e,"新建用户失败");
                ModelState.TryAddModelError(nameof(model.CustomizeErrorMessage), e.Message);
                return View(model);
            }
            return RedirectToAction(nameof(Details),new {id=model.ID});
        }


        public async Task<IActionResult> Modify(Guid id)
        {
            var entity = await _userService.SingleAsync(a => a.ID == id);
            var model = entity.ToModel();
            var userRoles = await (from n in _userRoleService.Queryable() where n.UserID == model.ID select n.RoleID).ToListAsync();
            var roles = await (from n in _roleService.Queryable()
                               select new SelectListItem
                               {
                                   Value = n.ID.ToString(),
                                   Text = n.Name,
                                   Selected = userRoles.Any(a=>a == n.ID)
                               }).ToListAsync();
            model.Roles = roles;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Modify(UserViewModel model, IFormCollection form)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                model.RoleID = form["RoleID"];
                await _userService.SaveUser(model);
            }
            catch (Exception e)
            {
                _logger.Error(e, "修改用户失败");
                ModelState.TryAddModelError(nameof(model.CustomizeErrorMessage), e.Message);
            }
            return RedirectToAction(nameof(Details),new{id = model.ID});
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var entity = await _userService.SingleAsync(a => a.ID == id);
            var model = entity.ToModel();
            var dep = await _organizationService.SingleAsync(a => a.ID == entity.OrganizationID);
            if (dep != null)
                model.OrganizationName = dep.Name;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicysModels.PolicysRole)]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                var result = await _userService.DeleteAsync(a => a.ID == id);
                if(result!=1)
                    throw new Exception("删除用户数据库操作失败");
            }
            catch (Exception e)
            {
                _logger.Error(e, "删除用户失败");
                ModelState.TryAddModelError("CustomizeErrorMessage", e.Message);
            }
            return RedirectToAction(nameof(Details),id);
        }

        #region JsonResult
        /// <summary>
        ///     按照部门组织树形用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OrganizationZTreeUsers()
        {
            var orgnizationList =  _organizationService.Queryable();
            var userList =  _userService.Queryable();

            var orgnizations = await (from n in orgnizationList
                select new
                {
                    id = n.ID,
                    pId = n.ParentOrganizationID.Value,
                    name = n.Name,
                    open = true,
                    chkDisabled = true
                }).ToListAsync();

            var users =  await (from n in userList
                select new
                {
                    id = n.ID,
                    pId = n.OrganizationID,
                    name = n.LockoutEnabled
                        ? $"< span style = 'margin-right:0px;' >{n.Name}</ span >< span style = 'color:red;margin-right:0px;' > (被锁定) </ span >"
                        : n.Name,
                    open = true,
                    chkDisabled = false
                }).ToListAsync(); ;
            return Json(orgnizations.Union(users));
        }

        public async Task<IActionResult> AllRoles()
        {
            var query =  _roleService.Queryable();
            var modle = await (from n in query
                select new
                {
                    Text = n.Name,
                    Value = n.ID.ToString()
                }).ToListAsync();
            return Json(modle);
        }
        
        #endregion
    }
}