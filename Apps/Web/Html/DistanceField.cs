using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;

namespace LinkMe.Web.Html
{
    public class DistanceField<TModel>
        : ValuesDropDownListField<TModel, int>
    {
        internal DistanceField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, int> getValue, IEnumerable<int> distances)
            : base(htmlHelper, model, name, getValue, distances)
        {
            SetText(d => d + " km");
        }

        internal DistanceField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, int>> getValue, IEnumerable<int> distances)
            : base(htmlHelper, model, getValue, distances)
        {
            SetText(d => d + " km");
        }

        protected override void BuildPreInnerHtml(System.Text.StringBuilder sb)
        {
            sb.Append("Within a ");
            base.BuildPreInnerHtml(sb);
        }

        protected override void BuildPostInnerHtml(System.Text.StringBuilder sb)
        {
            base.BuildPostInnerHtml(sb);
            sb.Append(" radius of <span class='loc_label'> Melbourne, 3000</span>");
        }
    }

    public static class DistanceFieldExtensions
    {
        public static DistanceField<TModel> DistanceField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, int> getValue, IEnumerable<int> distances)
        {
            return new DistanceField<TModel>(htmlHelper, model, name, getValue, distances);
        }

        public static DistanceField<TModel> DistanceField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, int>> getValue, IEnumerable<int> distances)
        {
            return new DistanceField<TModel>(htmlHelper, model, getValue, distances);
        }
    }
}
