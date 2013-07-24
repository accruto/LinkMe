using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;

namespace LinkMe.Web.Html
{
    public class ItemsPerPageField<TModel>
        : ValuesDropDownListField<TModel, int>
    {
        internal ItemsPerPageField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, int> getValue, IEnumerable<int> itemsPerPage)
            : base(htmlHelper, model, name, getValue, itemsPerPage)
        {
            SetText(i => i.ToString());
        }

        internal ItemsPerPageField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, int>> getValue, IEnumerable<int> itemsPerPage)
            : base(htmlHelper, model, getValue, itemsPerPage)
        {
            SetText(i => i.ToString());
        }

        protected override void BuildPreInnerHtml(System.Text.StringBuilder sb)
        {
            sb.Append("Show ");
            base.BuildPreInnerHtml(sb);
        }

        protected override void BuildPostInnerHtml(System.Text.StringBuilder sb)
        {
            base.BuildPostInnerHtml(sb);
            sb.Append(" results per page");
        }
    }

    public static class ItemsPerPageFieldExtensions
    {
        public static ItemsPerPageField<TModel> ItemsPerPageField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, int> getValue, IEnumerable<int> itemsPerPage)
        {
            return new ItemsPerPageField<TModel>(htmlHelper, model, name, getValue, itemsPerPage);
        }

        public static ItemsPerPageField<TModel> ItemsPerPageField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, int>> getValue, IEnumerable<int> itemsPerPage)
        {
            return new ItemsPerPageField<TModel>(htmlHelper, model, getValue, itemsPerPage);
        }
    }
}
