using System.Diagnostics;
using AdminSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ViewModels.Options;

namespace AdminSite.Controllers
{
    public class HomeController : BaseAdminController
    {
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        
    }
}