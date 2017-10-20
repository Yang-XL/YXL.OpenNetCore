using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WxPay.Controllers
{
    public class WxPayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
