using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PermissionSystem.Models;

namespace Controls.Layout
{
    public static class SiteMapExtensions
    {
        private static Menu CurrMenu;

        public static IHtmlContent SiteMap(this IHtmlHelper htmlHelper, IEnumerable<Menu> menus)
        {
            if (menus == null) return null;
            var html = new DefaultTagHelperContent();
            var controllerName = Convert.ToString(htmlHelper.ViewContext.RouteData.Values["controller"]);
            var actionName = Convert.ToString(htmlHelper.ViewContext.RouteData.Values["action"]);
            CurrMenu = menus.FirstOrDefault(a =>
                string.Equals(a.ControllerName, controllerName, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(a.ActionName, actionName, StringComparison.CurrentCultureIgnoreCase));

            var tag = CreateTag(new Menu {IsNav = true, ActionName = "Index", ControllerName = "Home", Name = "首页"},CurrMenu != null);
            var builder = Create(menus, CurrMenu);
            return html.AppendHtml(tag).AppendHtml(builder);
        }

        private static IHtmlContentBuilder Create(IEnumerable<Menu> menus, Menu menu)
        {
            if (menus == null || menu == null) return null;
            var tag = CreateTag(menu, menu.ID != CurrMenu.ID);
            var parentMenu = menus.FirstOrDefault(a => a.ID == menu.ParentID);
            if (parentMenu != null)
            {
                var beforTag = Create(menus, parentMenu);
                return beforTag.AppendHtml(tag);
            }
            return new DefaultTagHelperContent().AppendHtml(tag);
        }

        private static TagBuilder CreateTag(Menu menu, bool isParent)
        {
            var tag = new TagBuilder("li");
            if (menu.IsNav && !string.IsNullOrEmpty(menu.ActionName))
            {
                var innerTag = new TagBuilder("a");
                var href = string.Format("{0}/{1}/{2}",
                    string.IsNullOrEmpty(menu.AreaName) ? "" : "/" + menu.AreaName, menu.ControllerName,
                    menu.ActionName);
                innerTag.MergeAttribute("href", href);
                innerTag.InnerHtml.Append(menu.Name);
                tag.InnerHtml.AppendHtml(innerTag);
            }
            else
            {
                var innerTag = new TagBuilder("span");
                innerTag.InnerHtml.Append(menu.Name);
                tag.InnerHtml.AppendHtml(innerTag);
            }
            if (isParent)
            {
                var iconTag = new TagBuilder("i");
                iconTag.AddCssClass("fa fa-angle-double-right");
                tag.InnerHtml.AppendHtml(iconTag);
            }
            return tag;
        }
    }
}