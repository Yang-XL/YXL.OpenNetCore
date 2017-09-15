using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViewModels.AdminWeb.Organization;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly AdminSiteOption _setting;

        public OrganizationController(IOptions<AdminSiteOption> setting, ILoggerFactory loggerFactory, IUserService userService, IOrganizationService organizationService)
        {
            _userService = userService;
            _organizationService = organizationService;
            _logger = loggerFactory.CreateLogger<MenuController>();
            _setting = setting.Value;
        }

        public IActionResult Index(int page = 1)
        {
            var model = _organizationService.GetPaged(_setting.PageSize, page, "").Result;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string queryString, Guid? ParentID = null, int page = 1)
        {
            var model = await _organizationService.GetPaged(_setting.PageSize, page, queryString ?? "", ParentID);
            return PartialView("AjaxTable", model);
        }

        // GET: Organization/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Organization/Create
        public ActionResult Create()
        {
            var model = new OrganizationViewModel
            {
                ID = Guid.NewGuid(),
            };
            return View(model);
        }

        // POST: Organization/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Organization/Edit/5
        public ActionResult Modify(int id)
        {
            return View();
        }

        // POST: Organization/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Organization/Delete/5
        public ActionResult Remove(int id)
        {
            return View();
        }

        // POST: Organization/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

      
        public IActionResult All()
        {
            var result = from n in _organizationService._dbSet
                select new {id = n.ID, pId = n.ParentOrganizationID, name = n.Name, open = true};
            var model = result.ToList();
            return Json(model);
        }
    }
}