using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AdminSite.Controllers
{
    public class RoleController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}