using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Domain.Roles.Affiliations.Communities;

namespace LinkMe.Web.Html
{
    public class CommunityField<TModel>
        : OptionalDropDownListField<TModel, Community, Guid>
    {
        internal CommunityField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Guid?>> expression, IEnumerable<Community> communities)
            : base(htmlHelper, model, expression, c => c == null ? (Guid?)null : c.Id, communities)
        {
            SetItemText(c => c == null ? null : c.Name);
        }

        internal CommunityField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, Guid?> expression, IEnumerable<Community> communities)
            : base(htmlHelper, model, name, expression, c => c == null ? (Guid?)null : c.Id, communities)
        {
            SetItemText(c => c == null ? null : c.Name);
        }
    }

    public static class CommunityFieldExtensions
    {
        public static CommunityField<TModel> CommunityField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, Guid?>> expression, IEnumerable<Community> communities)
        {
            return new CommunityField<TModel>(htmlHelper, model, expression, communities);
        }

        public static CommunityField<TModel> CommunityField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, Guid?> expression, IEnumerable<Community> communities)
        {
            return new CommunityField<TModel>(htmlHelper, model, name, expression, communities);
        }
    }
}
