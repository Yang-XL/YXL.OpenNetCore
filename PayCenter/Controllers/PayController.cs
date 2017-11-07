using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PayCenter.Controllers
{
    public class PayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
