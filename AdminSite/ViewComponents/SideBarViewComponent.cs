using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Mvc;
using ViewModels.AdminWeb.Nav;

namespace AdminSite.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly IApplicationService _applicationService;
        private readonly IMenuService _menuService;

        public SideBarViewComponent(IMenuService menuService, IApplicationService applicationService)
        {
            _menuService = menuService;
            _applicationService = applicationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menus = await _menuService.QueryAsync(a => true);
            var application = await _applicationService.QueryAsync(a => true);

            var model = new NavViewModel {Applications = application.ToList(), Menus = menus.ToList()};

            return View("_LayoutSidebar", model);
        }
    }
}