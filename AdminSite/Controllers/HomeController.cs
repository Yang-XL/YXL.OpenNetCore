using System;
using System.Diagnostics;
using AdminSite.Models;
using AdminSite.SiteAttributes;
using Core.Plugins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdminSite.Controllers
{
    [Authorize(PolicysModels.PolicysRole)]
    public class HomeController : BaseAdminController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About(int id)
        {
            ViewData["Message"] = $"参数为：{id}";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}