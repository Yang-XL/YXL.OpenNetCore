using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViewModels.AdminWeb;
using ViewModels.AdminWeb.Application;
using ViewModels.Mapper;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    public class ApplicationController : BaseAdminController
    {
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;
        private readonly AdminSiteOption _setting;
        private readonly ILogger _logger;

        public ApplicationController(IApplicationService applicationService, IOptions<AdminSiteOption> setting,
            IMapper mapper, ILoggerFactory loggerFactory)
        {
            _applicationService = applicationService;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<ApplicationController>();
            _setting = setting.Value;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var app = await _applicationService.GetPagedAsync(page, _setting.PageSize, a => a.ShowIndex, b => true);
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string queryString, int page = 1)
        {
            var app = await _applicationService.GetPagedAsync(page, _setting.PageSize, a => a.ShowIndex,
                b => b.Name.Contains(queryString) || b.PyCode.Contains(queryString));
            return View(app);
        }


        public async Task<IActionResult> Details(Guid id)
        {

            var enitty = await _applicationService.SingleAsync(a => a.ID == id);
            return View(enitty.ToModel());
        }

        public async Task<IActionResult> Modify(Guid id)
        {
            var enitty = await _applicationService.SingleAsync(a => a.ID == id);
            var model = enitty.ToModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(ApplicationViewModel model)
        {
            var entity = await _applicationService.SingleAsync(model.ID);
            entity = model.ToEntity(entity);
            await _applicationService.UpdateAsync(entity);
            return RedirectToAction("Details", new { id = entity.ID });
        }

        public IActionResult Create()
        {
            var model = new ApplicationViewModel
            {
                ID = Guid.NewGuid()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationViewModel model)
        {
            var entity = model.ToEntity();
            entity.CreateDate = DateTime.Now;
            await _applicationService.InsertAsync(entity);
            return RedirectToAction("Details", new { id = entity.ID });
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                await _applicationService.DeleteAsync(id);
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