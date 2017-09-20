using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace Controls
{
    public static class RadioButtonListExtensions
    {
        public static IHtmlContent RadioButtonListFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel,
            object htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            string name = ExpressionHelper.GetExpressionText(expression);

            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }

            bool usedViewData = false;

            // If we got a null selectList, try to use ViewData to get the list of items.
            if (selectList == null)
            {
                selectList = htmlHelper.GetSelectData(fullName);
                usedViewData = true;
            }

            object defaultValue = htmlHelper.GetModelStateValue(fullName, typeof(string));

            // If we haven't already used ViewData to get the entire list of items then we need to
            // use the ViewData-supplied value before using the parameter-supplied value.
            if (!usedViewData)
            {
                if (defaultValue == null)
                {
                    defaultValue = htmlHelper.ViewData.Eval(fullName);
                }
            }
            if (defaultValue != null)
            {
                IEnumerable defaultValues = new[] { defaultValue };
                IEnumerable<string> values = from object value in defaultValues select Convert.ToString(value, CultureInfo.CurrentCulture);
                HashSet<string> selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
                List<SelectListItem> newSelectList = new List<SelectListItem>();

                foreach (SelectListItem item in selectList)
                {
                    item.Selected = (item.Value != null) ? selectedValues.Contains(item.Value) : selectedValues.Contains(item.Text);
                    newSelectList.Add(item);
                }
                selectList = newSelectList;
            }
            #region

            IHtmlContentBuilder htm = new HtmlContentBuilder();
            int i = 0;

            foreach (SelectListItem item in selectList)
            {
                i++;
                string id = string.Format("{0}{1}", name, i);
                htm.AppendHtml(GenerateRadioTag(name, id, item.Text, item.Value, item.Selected, htmlAttributes));
            }

            #endregion

            return htm;
        }

        private static IEnumerable<SelectListItem> GetSelectData(this IHtmlHelper htmlHelper, string name)
        {
            object o = null;
            if (htmlHelper.ViewData != null)
                o = htmlHelper.ViewData.Eval(name);
            if (o == null)
                throw new InvalidOperationException(
                    "IEnumerable<SelectListItem>");
            var selectList = o as IEnumerable<SelectListItem>;
            if (selectList == null)
                throw new InvalidOperationException(
                    "IEnumerable<SelectListItem>");
            return selectList;
        }

        private static object GetModelStateValue(this IHtmlHelper htmlHelper, string key, Type destinationType)
        {
            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out ModelStateEntry modelState))
            {

                if (modelState.AttemptedValue != null)
                {
                    return modelState.AttemptedValue;
                }
            }
            return null;
        }

        private static TagBuilder GenerateRadioTag(string name,
            string id, string labelText, string value, bool isChecked,
            object htmlAttributes)
        {

            var _htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var input = new TagBuilder("input");
            input.MergeAttribute("id", id);
            input.MergeAttribute("name", name);
            input.MergeAttribute("type", "radio");
            input.MergeAttribute("value", value);
            if (isChecked)
                input.MergeAttribute("checked", "checked");

            var label = new TagBuilder("label");
            label.MergeAttribute("for", id);
            label.MergeAttributes(_htmlAttributes);
            label.InnerHtml.AppendHtml(input);
            label.InnerHtml.Append(labelText);
            return label;
        }
    }
}