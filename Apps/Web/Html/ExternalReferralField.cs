using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Domain.Roles.Registration;

namespace LinkMe.Web.Html
{
    public class ExternalReferralField<TModel>
        : OptionalDropDownListField<TModel, ExternalReferralSource, int>
    {
        internal ExternalReferralField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, int?>> expression, IEnumerable<ExternalReferralSource> sources)
            : base(htmlHelper, model, expression, s => s == null ? (int?)null : s.Id, sources)
        {
            SetItemText(s => s == null ? null : s.Name);
        }

        internal ExternalReferralField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, int?> expression, IEnumerable<ExternalReferralSource> sources)
            : base(htmlHelper, model, name, expression, s => s == null ? (int?)null : s.Id, sources)
        {
            SetItemText(s => s == null ? null : s.Name);
        }
    }

    public static class ExternalReferralFieldExtensions
    {
        public static ExternalReferralField<TModel> ExternalReferralField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, int?>> expression, IEnumerable<ExternalReferralSource> sources)
        {
            return new ExternalReferralField<TModel>(htmlHelper, model, expression, sources);
        }

        public static ExternalReferralField<TModel> ExternalReferralField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, int?> expression, IEnumerable<ExternalReferralSource> sources)
        {
            return new ExternalReferralField<TModel>(htmlHelper, model, name, expression, sources);
        }
    }
}
