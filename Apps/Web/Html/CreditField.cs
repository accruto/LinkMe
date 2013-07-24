using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Domain.Credits;

namespace LinkMe.Web.Html
{
    public class CreditField<TModel>
        : DropDownListField<TModel, Credit, Guid>
    {
        internal CreditField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Guid>> expression, IEnumerable<Credit> credits)
            : base(htmlHelper, model, expression, a => a.Id, credits)
        {
            SetItemText(c => c.ShortDescription);
        }
    }

    public static class CreditFieldExtensions
    {
        public static CreditField<TModel> CreditField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Guid>> expression, IEnumerable<Credit> credits)
        {
            return new CreditField<TModel>(htmlHelper, model, expression, credits);
        }
    }
}
