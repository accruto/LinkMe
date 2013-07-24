using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;

namespace LinkMe.Web.Html
{
    public class LocationField<TModel>
        : TextBoxField<TModel, string>
    {
        public LocationField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
            : base(htmlHelper, model, getValue)
        {
        }
    }

    public static class LocationFieldExtensions
    {
        public static LocationField<TModel> LocationField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
        {
            return new LocationField<TModel>(htmlHelper, model, getValue);
        }
    }
}
