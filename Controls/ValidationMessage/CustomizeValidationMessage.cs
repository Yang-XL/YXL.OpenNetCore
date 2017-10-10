using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;


namespace Controls.ValidationMessage
{
    public static class CustomizeValidationMessage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="type">
        ///     1：success
        ///     2：info
        ///     3：warning
        ///     4：danger
        /// </param>
        /// <returns></returns>
        private static IHtmlContent ValidationMessageFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,Expression<Func<TModel, TProperty>> expression, int type)
        {
            var validationMessageTag = htmlHelper.ValidationMessageFor(expression) as TagBuilder;
            if (validationMessageTag == null || !validationMessageTag.HasInnerHtml)
            {
                return validationMessageTag;
            }
            var tag = new TagBuilder("div");
            tag.AddCssClass("alert");
            tag.AddCssClass("alert-dismissible");
            tag.MergeAttribute("role", "alert");
            var titleMessge = "";
            switch (type)
            {
                case 1:
                    tag.AddCssClass("alert-success");
                    titleMessge = "恭喜！";
                    break;
                case 3:
                    tag.AddCssClass("alert-warning");
                    titleMessge = "警告！";
                    break;
                case 4:
                    tag.AddCssClass("alert-danger");
                    titleMessge = "错误！";
                    break;
                default:
                    tag.AddCssClass("alert-info");
                    titleMessge = "消息！";
                    break;
            }
            var btnTag = new TagBuilder("button");
            btnTag.MergeAttribute("type", "button");
            btnTag.MergeAttribute("data-dismiss", "alert");
            btnTag.MergeAttribute("aria-label", "Close");
            btnTag.AddCssClass("close");
            btnTag.InnerHtml.AppendHtml("<span aria-hidden=\"true\">&times;</span>");
            tag.InnerHtml.AppendHtml(btnTag);
            tag.InnerHtml.AppendHtml($"<strong>{titleMessge}</strong>&nbsp;&nbsp;&nbsp;&nbsp;");
            tag.InnerHtml.AppendHtml(validationMessageTag);
            return tag;
        }

        public static IHtmlContent ValidationMessageDangerFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.ValidationMessageFor(expression, 4);
        }
        public static IHtmlContent ValidationMessageWarningFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.ValidationMessageFor(expression, 3);
        }
        public static IHtmlContent ValidationMessageSuccessFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.ValidationMessageFor(expression, 1);
        }
        public static IHtmlContent ValidationMessageinfoFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.ValidationMessageFor(expression, 2);
        }
    }
}
