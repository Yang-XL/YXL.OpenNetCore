using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Controls.SelectZtree
{
    public static class ZTreeSelectHtmlExtensions
    {
        public static IHtmlContent ZtreeTextBoxFor<TModel>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, string>> expression, object htmlAttributes = null)
        {
            var expressionName = (expression.Body as MemberExpression).Member.Name;
            return ZtreeTextBox(helper, expressionName, htmlAttributes);
        }

        public static IHtmlContent ZtreeTextBox(this IHtmlHelper helper, string name, object htmlAttributes = null)
        {
            
            var zTreeDivId = name + "_ZTreeContent";
            var zTreeUlId = name + "_ZTree";
            var tagContent = new TagBuilder("div");
            tagContent.MergeAttribute("id", zTreeDivId);
            tagContent.MergeAttribute("style", "display: none; position: absolute; background-color:darkgrey;z-index: 10000");
            var tagUl = new TagBuilder("ul");
            tagUl.MergeAttribute("id", zTreeUlId);
            tagUl.MergeAttribute("class", "ztree");
            tagContent.InnerHtml.AppendHtml(tagUl);

            var HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var input = new TagBuilder("input");
            input.MergeAttribute("type", "text");
            input.MergeAttribute("id", name);
            input.MergeAttribute("name", name);
            input.MergeAttributes(HtmlAttributes);
            return new HtmlContentBuilder().AppendHtml(input).AppendHtml(tagContent);
        }
    }

    [HtmlTargetElement("input", Attributes = ForAttributeName)]
    public class ZTreeSelectTag : TagHelper
    {
        public override int Order => 100;
        private const string ForAttributeName = "ztree-select";

        [HtmlAttributeName(ForAttributeName)]
        public bool IsZtreeSelect { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            if (IsZtreeSelect)
            {
                var zTreeDivId = context.UniqueId + "ZTreeContent";
                var zTreeUlId = context.UniqueId + "ZTree";

                var tagContent = new TagBuilder("div");
                tagContent.Attributes.Add("id", zTreeDivId);
                tagContent.Attributes.Add("style","display: none; position: absolute; background-color:darkgrey;z-index: 10000");
                var tagUl = new TagBuilder("div");
                tagUl.Attributes.Add("id", zTreeUlId);
                tagUl.Attributes.Add("class", "ztree");
                tagContent.InnerHtml.AppendHtml(tagUl);

                output.PostContent.AppendHtml(tagContent.ToString());
            }
        }

    }
}
