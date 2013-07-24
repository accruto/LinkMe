using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Domain;

namespace LinkMe.Web.Html
{
    public class ProfessionField<TModel>
        : OptionalEnumDropDownListField<TModel, Profession>
    {
        internal ProfessionField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Profession?>> expression)
            : base(htmlHelper, model, expression)
        {
            SetText(e => e.GetDisplayText());
        }
    }

    public static class ProfessionFieldExtensions
    {
        public static ProfessionField<TModel> ProfessionField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Profession?>> expression)
        {
            return new ProfessionField<TModel>(htmlHelper, model, expression);
        }
    }
}
