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
            var menus = await _menuService.GetAsync(a => true);
            var application = await _applicationService.GetAsync(a => true);
            return View("_LayoutSidebar", new NavViewModel {Applications = application, Menus = menus});
        }
    }
}