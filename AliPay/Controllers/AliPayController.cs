using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AliPay.Controllers
{
    public class AliPayController : Controller
    {
        public IActionResult Index()
        {
            return View("/Plugins/AliPay/Views/AliPay/Index.cshtml");
        }
    }
}
