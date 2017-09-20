using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViewModels.AdminWeb;
using ViewModels.AdminWeb.Application;
using ViewModels.AdminWeb.Roles;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    public class RoleController : BaseAdminController
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly AdminSiteOption _setting;
        private readonly ILogger _logger;

        public RoleController(IRoleService roleService, IOptions<AdminSiteOption> setting,
            IMapper mapper, ILoggerFactory loggerFactory)
        {
            _roleService = roleService;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<RoleController>();
            _setting = setting.Value;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var app = await _roleService.GetPagedAsync(page, _setting.PageSize, a => a.ShowIndex, b => true);
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string queryString, int page = 1)
        {
            var app = await _roleService.GetPagedAsync(page, _setting.PageSize, a => a.ShowIndex,b => b.Name.Contains(queryString) || b.PyCode.Contains(queryString));
            return View(app);
        }


        public async Task<IActionResult> Details(Guid id)
        {
            var enitty = await _roleService.SingleAsync(a => a.ID == id);
            var model = enitty.ToModel();
            return View(model);
        }

        public async Task<IActionResult> Modify(Guid id)
        {
            var enitty = await _roleService.SingleAsync(id);
            var model = enitty.ToModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(RoleViewModel model)
        {
            var entity = await _roleService.SingleAsync(model.ID);
            entity = model.ToEntity(entity);
            await _roleService.UpdateAsync(entity);
            return RedirectToAction("Details", new { id = entity.ID });
        }

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
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            var entity = model.ToEntity();
            entity.CreateDate = DateTime.UtcNow;
           await _roleService.InsertAsync(entity);
            return RedirectToAction("Details", new{ id= entity.ID });
        }

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
    }
}