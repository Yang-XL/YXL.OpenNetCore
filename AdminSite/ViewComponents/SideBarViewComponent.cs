using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PermissionSystem.Models;

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

        private Menu CurrentNavMenu { get; set; }
        private IEnumerable<Menu> AccessMenu { get; set; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var controllerName = Convert.ToString(ViewContext.RouteData.Values["controller"]);
            var actionName = Convert.ToString(ViewContext.RouteData.Values["action"]);
            var areaName = Convert.ToString(ViewContext.RouteData.Values["area"]);
            var currentMenu = await _menuService.SingleAsync(areaName, controllerName, actionName);
            CurrentNavMenu = currentMenu == null || currentMenu.IsNav
                ? currentMenu
                : await _menuService.SingleAsync(a => a.ID == currentMenu.ParentID);
            var userId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            AccessMenu = await _menuService.QueryMenuViewModel(userId);


            var applicationList = await _applicationService.QueryAsync(a => true);
            var html = new DefaultTagHelperContent();
            foreach (var application in applicationList)
            {
                var tag = new TagBuilder("li");
                tag.MergeAttribute("class", "heading");
                var tagH3 = new TagBuilder("h3");
                tagH3.MergeAttribute("class", "uppercase");
                tagH3.InnerHtml.AppendHtml(application.Name);
                tag.InnerHtml.AppendHtml(tagH3);
                html.AppendHtml(tag);
                var appMenus = AccessMenu.Where(a => a.ApplicationID == application.ID && a.IsNav);

                foreach (var item in appMenus.Where(a => a.ParentID == default(Guid)))
                    html.AppendHtml(CreateMenuTag(appMenus, item));
            }

            return View("_LayoutSidebar", html);
        }

        #region private TagHelper

        /// <summary>
        ///     递归创建所有的导航标签
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        private TagBuilder CreateMenuTag(IEnumerable<Menu> menus, Menu menu)
        {
            var navItem = NavItem(menu);
            if (menus.Any(a => a.ParentID == menu.ID))
            {
                var subMenuTag = new TagBuilder("ul");
                subMenuTag.AddCssClass("sub-menu");
                foreach (var item in menus.Where(a => a.ParentID == menu.ID))
                    subMenuTag.InnerHtml.AppendHtml(CreateMenuTag(menus, item));
                navItem.InnerHtml.AppendHtml(subMenuTag);
            }
            return navItem;
        }

        /// <summary>
        ///     导航菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        private TagBuilder NavItem(Menu menu)
        {
            var liTag = new TagBuilder("li");
            liTag.AddCssClass("nav-item");
            if (CurrentNavMenu != null && menu.ID == CurrentNavMenu.ID)
            {
                liTag.AddCssClass("active");
                liTag.AddCssClass("open");
            }
            var navLink = NavLink(menu);
            navLink.InnerHtml.AppendHtml(Icon(menu)).AppendHtml(Title(menu));
            if (string.IsNullOrEmpty(menu.ActionName))
                navLink.InnerHtml.AppendHtml(Arrow());
            liTag.InnerHtml.AppendHtml(navLink);
            return liTag;
        }

        /// <summary>
        ///     a 标签
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        private TagBuilder NavLink(Menu menu)
        {
            var tag = new TagBuilder("a");
            tag.MergeAttribute("class", "nav-link");
            if (string.IsNullOrEmpty(menu.ActionName))
            {
                tag.AddCssClass("nav-toggle");
                tag.MergeAttribute("href", "javascript:;");
            }
            else
            {
                var area = string.IsNullOrEmpty(menu.AreaName) ? "" : "/" + menu.AreaName;
                var href = $"{area}/{menu.ControllerName}/{menu.ActionName}";
                tag.MergeAttribute("href", href);
            }
            return tag;
        }

        /// <summary>
        ///     图标 icon 标签
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        private TagBuilder Icon(Menu menu)
        {
            var tag = new TagBuilder("i");
            tag.AddCssClass(menu.IconCss);
            tag.InnerHtml.AppendHtml("&nbsp;&nbsp;");
            return tag;
        }

        /// <summary>
        ///     title span 标签
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        private TagBuilder Title(Menu menu)
        {
            var tag = new TagBuilder("span");
            tag.AddCssClass("title");
            tag.InnerHtml.Append(menu.Name);
            return tag;
        }

        /// <summary>
        ///     是否可以点击展开 span 标签
        /// </summary>
        /// <returns></returns>
        private TagBuilder Arrow()
        {
            var tag = new TagBuilder("span");
            tag.AddCssClass("arrow");
            return tag;
        }

        #endregion
    }
}