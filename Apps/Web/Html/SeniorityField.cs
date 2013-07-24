using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Domain;

namespace LinkMe.Web.Html
{
    public class SeniorityField<TModel>
        : OptionalEnumDropDownListField<TModel, Seniority>
    {
        internal SeniorityField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Seniority?>> expression)
            : base(htmlHelper, model, expression)
        {
            SetText(e => e.GetDisplayText());
            SetComparer((e1, e2) =>
            {
                // Empty selection comes first, Not relevant comes last.

                if (Equals(e1, e2))
                    return 0;
                if (e1 == null)
                    return -1;
                if (e2 == null)
                    return 1;
                if (e1 == Seniority.NotApplicable)
                    return 1;
                if (e2 == Seniority.NotApplicable)
                    return -1;
                return ((int) e1).CompareTo((int) e2);
            });
        }
    }

    public static class SeniorityFieldExtensions
    {
        public static SeniorityField<TModel> SeniorityField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Seniority?>> expression)
        {
            return new SeniorityField<TModel>(htmlHelper, model, expression);
        }
    }
}
