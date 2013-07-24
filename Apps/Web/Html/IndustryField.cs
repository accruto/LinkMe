using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Domain.Industries;

namespace LinkMe.Web.Html
{
    public class IndustryField<TModel>
        : OptionalDropDownListField<TModel, Industry, Guid>
    {
        internal IndustryField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Guid?>> expression, IEnumerable<Industry> industries)
            : base(htmlHelper, model, expression, i => i == null ? (Guid?)null : i.Id, (from i in industries where i.Name != "Other" select i).Concat(from i in industries where i.Name == "Other" select i))
        {
            SetItemText(i => i == null ? null : i.Name);
        }
    }

    public static class IndustryFieldExtensions
    {
        public static IndustryField<TModel> IndustryField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Guid?>> expression, IEnumerable<Industry> industries)
        {
            return new IndustryField<TModel>(htmlHelper, model, expression, industries);
        }
    }
}
