using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Domain;

namespace LinkMe.Web.Html
{
    public class EducationLevelField<TModel>
        : OptionalEnumDropDownListField<TModel, EducationLevel>
    {
        internal EducationLevelField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, EducationLevel?>> expression)
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
                if (e1 == EducationLevel.NotRelevant)
                    return 1;
                if (e2 == EducationLevel.NotRelevant)
                    return -1;
                return ((int) e1).CompareTo((int) e2);
            });
        }
    }

    public static class EducationLevelFieldExtensions
    {
        public static EducationLevelField<TModel> EducationLevelField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, EducationLevel?>> expression)
        {
            return new EducationLevelField<TModel>(htmlHelper, model, expression);
        }
    }
}
