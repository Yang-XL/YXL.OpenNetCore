using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PermissionSystem.Models;
using ViewModels.AdminWeb.Nav;

namespace Controls.Layout
{
    public static class SideBarListExtensions
    {
        private static Menu ChoiceMenu;

        public static IHtmlContent MenuNavItem(this IHtmlHelper htmlHelper, NavViewModel NavViewModel)
        {
            if (NavViewModel == null || NavViewModel.Applications == null || NavViewModel.Menus == null) return null;
            ChoiceMenu = NavParent(htmlHelper, NavViewModel);

            var html = new DefaultTagHelperContent();
            foreach (var application in NavViewModel.Applications)
            {
                var tag = new TagBuilder("li");
                tag.MergeAttribute("class", "heading");
                var tagH3 = new TagBuilder("h3");
                tagH3.MergeAttribute("class", "uppercase");
                tagH3.InnerHtml.AppendHtml(application.Name);
                tag.InnerHtml.AppendHtml(tagH3);
                html.AppendHtml(tag);
                var appMenus = NavViewModel.Menus.Where(a => a.ApplicationID == application.ID && a.IsNav);

                foreach (var item in appMenus.Where(a => a.ParentID == default(Guid)))
                    html.AppendHtml(CreateMenuTag(appMenus, item));
            }
            return html;
        }

        #region private menuHeler

        /// <summary>
        ///     如果当前页面为权限菜单，则返回父级菜单，否则返回当前菜单
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="NavViewModel"></param>
        /// <returns></returns>
        private static Menu NavParent(IHtmlHelper htmlHelper, NavViewModel NavViewModel)
        {
            var controllerName = Convert.ToString(htmlHelper.ViewContext.RouteData.Values["controller"]);
            var actionName = Convert.ToString(htmlHelper.ViewContext.RouteData.Values["action"]);
            var areaName = Convert.ToString(htmlHelper.ViewContext.RouteData.Values["area"]);

            var currMenu = NavViewModel.Menus.FirstOrDefault(a =>
                string.Equals(a.ControllerName, controllerName, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(a.ActionName, actionName, StringComparison.CurrentCultureIgnoreCase));
            if (currMenu == null) return null;
            if (currMenu.IsNav) return currMenu;
            return NavViewModel.Menus.FirstOrDefault(a => a.ID == currMenu.ParentID);
        }

        #endregion

        #region private TagHelper

        private static TagBuilder CreateMenuTag(IEnumerable<Menu> menus, Menu menu)
        {
            var navItem = MenuItem(menu);
            if (menus.Any(a => a.ParentID == menu.ID))
            {
                var subMenuTag = new TagBuilder("ul");
                subMenuTag.MergeAttribute("class", "sub-menu");
                foreach (var item in menus.Where(a => a.ParentID == menu.ID))
                    subMenuTag.InnerHtml.AppendHtml(CreateMenuTag(menus, item));
                navItem.InnerHtml.AppendHtml(subMenuTag);
            }
            return navItem;
        }

        private static TagBuilder MenuItem(Menu menu)
        {
            var navLink = NavLink(menu);
            navLink.InnerHtml.AppendHtml(Icon(menu)).AppendHtml(Title(menu));
            if (string.IsNullOrEmpty(menu.ActionName))
                navLink.InnerHtml.AppendHtml(Arrow());
            var navItem = NavItem(menu);
            navItem.InnerHtml.AppendHtml(navLink);
            return navItem;
        }

        private static TagBuilder NavItem(Menu menu)
        {
            var liTag = new TagBuilder("li");

            if (ChoiceMenu != null && menu.ID == ChoiceMenu.ID)
                liTag.MergeAttribute("class", "nav-item active open");
            else
                liTag.MergeAttribute("class", "nav-item");

            return liTag;
        }

        private static TagBuilder NavLink(Menu menu)
        {
            var tag = new TagBuilder("a");
            if (string.IsNullOrEmpty(menu.ActionName))
            {
                tag.MergeAttribute("class", "nav-link nav-toggle");
                tag.MergeAttribute("href", "javascript:;");
            }
            else
            {
                var href = string.Format("{0}/{1}/{2}",
                    string.IsNullOrEmpty(menu.AreaName) ? "" : "/" + menu.AreaName, menu.ControllerName,
                    menu.ActionName);
                tag.MergeAttribute("class", "nav-link");
                tag.MergeAttribute("href", href);
            }
            return tag;
        }

        private static TagBuilder Icon(Menu menu)
        {
            var tag = new TagBuilder("i");
            if (!string.IsNullOrEmpty(menu.IconCss))
                tag.MergeAttribute("class", menu.IconCss);
            tag.InnerHtml.AppendHtml("&nbsp;&nbsp;");
            return tag;
        }

        private static TagBuilder Title(Menu menu)
        {
            var tag = new TagBuilder("span");
            tag.MergeAttribute("class", "title");
            tag.InnerHtml.Append(menu.Name);
            return tag;
        }

        private static TagBuilder Arrow()
        {
            var tag = new TagBuilder("span");
            tag.MergeAttribute("class", "arrow");
            return tag;
        }

        #endregion
    }
}