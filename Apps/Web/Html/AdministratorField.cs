using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Domain.Contacts;

namespace LinkMe.Web.Html
{
    public class AdministratorField<TModel>
        : DropDownListField<TModel, Administrator, Guid>
    {
        internal AdministratorField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Guid>> expression, IEnumerable<Administrator> administrators)
            : base(htmlHelper, model, expression, a => a.Id, administrators)
        {
            SetItemText(a => a == null ? null : a.FullName + (a.IsEnabled ? "" : " (disabled)"));
        }
    }

    public class OptionalAdministratorField<TModel>
        : OptionalDropDownListField<TModel, Administrator, Guid>
    {
        internal OptionalAdministratorField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Guid?>> expression, IEnumerable<Administrator> administrators)
            : base(htmlHelper, model, expression, a => a == null ? (Guid?)null : a.Id, administrators)
        {
            SetItemText(a => a == null ? null : a.FullName + (a.IsEnabled ? "" : " (disabled)"));
        }
    }

    public static class AdministratorFieldExtensions
    {
        public static AdministratorField<TModel> AdministratorField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Guid>> expression, IEnumerable<Administrator> administrators)
        {
            return new AdministratorField<TModel>(htmlHelper, model, expression, administrators);
        }

        public static OptionalAdministratorField<TModel> AdministratorField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Guid?>> expression, IEnumerable<Administrator> administrators)
        {
            return new OptionalAdministratorField<TModel>(htmlHelper, model, expression, administrators);
        }
    }
}
