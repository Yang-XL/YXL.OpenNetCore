using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using ViewModels.AdminWeb.Application;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    public class ApplicationController : BaseAdminController
    {
        private readonly IApplicationService _applicationService;
        private readonly AdminSiteOption _setting;
        public ApplicationController(IApplicationService applicationService, IOptions<AdminSiteOption>  setting)
        {
            _applicationService = applicationService;
            _setting = setting.Value;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var app = await _applicationService.GetPagedAsync(page, _setting.PageSize, a => a.ShowIndex, b => true);
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string queryString,  int page = 1)
        {
            var app = await _applicationService.GetPagedAsync(page, _setting.PageSize, a => a.ShowIndex, b => b.Name.Contains(queryString)||b.PyCode.Contains(queryString));
            return View(app);
        }


        public async Task<IActionResult> Detial(Guid id)
        {
            return View();
        }
        public async Task<IActionResult> Modify(Guid id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(ApplicationViewModel model)
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationViewModel model)
        {
            return View();
        }
        public async Task<IActionResult> Remove( Guid id)
        {
            return View();
        }
    }
}