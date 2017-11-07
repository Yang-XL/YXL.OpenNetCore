using System;
using System.Threading.Tasks;
using AdminSite.SiteAttributes;
using IService.PermissionSystem;
using LoggerExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViewModels.OpenPlatform;
using ViewModels.OpenPlatform.ClientModel;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    [Authorize(PolicysModels.PolicysRole)]
    public class ClientController : BaseAdminController
    {
        private readonly IClientService _clientService;
        private readonly ILogger _logger;
        private readonly AdminSiteOption _setting;


        public ClientController(IOptions<AdminSiteOption> setting, ILogger<ClientController> logger,
            IClientService clientService)
        {
            _clientService = clientService;
            _logger = logger;
            _setting = setting.Value;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var app = await _clientService.GetPagedAsync(page, _setting.PageSize, a => a.CreateDate, b => true);
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string queryString, int page = 1)
        {
            var app = await _clientService.GetPagedAsync(page, _setting.PageSize, a => a.CreateDate,
                b => b.Name.Contains(queryString));
            return PartialView("AjaxTable", app);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var enitty = await _clientService.SingleAsync(a => a.ID == id);
            return View(enitty.ToModel());
        }

        public async Task<IActionResult> Modify(Guid id)
        {
            var enitty = await _clientService.SingleAsync(a => a.ID == id);
            var model = enitty.ToModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(ClientViewModel model)
        {
            var entity = await _clientService.SingleAsync(model.ID);
            entity = model.ToEntity(entity);
            await _clientService.UpdateAsync(entity);
            return RedirectToAction("Details", new {id = entity.ID});
        }

        public IActionResult Create()
        {
            var model = new ClientViewModel
            {
                ID = Guid.NewGuid()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel model)
        {
            try
            {
                if(!ModelState.IsValid) return View(model);
                var entity = model.ToEntity();
                entity.CreateDate = entity.UpdateDate = DateTime.Now;
                await _clientService.InsertAsync(entity);
                return RedirectToAction("Details", new {id = entity.ID});
            }
            catch (Exception e)
            {
                ModelState.TryAddModelError(nameof(model.CustomizeErrorMessage), e.Message);
                _logger.Error(e, "添加商户");
                return View(model);
            }
        }
    }
}