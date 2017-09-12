using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Mvc;
using ViewModels.AdminWeb.Nav;

namespace AdminSite.ViewComponents
{
    public class SiteMapViewComponent : ViewComponent
    {
        private readonly IMenuService _menuService;

        public SiteMapViewComponent(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menus = await _menuService.GetAsync(a => true);
            return View(menus);
        }
    }
}
