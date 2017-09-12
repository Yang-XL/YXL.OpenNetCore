using System;
using System.Collections.Generic;
using System.Text;
using PermissionSystem.Models;

namespace ViewModels.AdminWeb.Nav
{
    public class NavViewModel
    {
        public  IEnumerable<PermissionSystem.Models.Application> Applications { get; set; }

        public  IEnumerable<Menu> Menus { get; set; }
    }
}
