using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Domain.Location;

namespace LinkMe.Web.Html
{
    public class LocationsField<TModel>
        : ListBoxField<TModel, NamedLocation, int>
    {
        public LocationsField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, IEnumerable<int>>> getValue, IEnumerable<NamedLocation> locations)
            : base(htmlHelper, model, getValue, c => c.Id, c => c.Name, locations)
        {
        }
    }

    public static class LocationsFieldExtensions
    {
        public static LocationsField<TModel> LocationsField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, IEnumerable<int>>> getValue, IEnumerable<NamedLocation> locations)
        {
            return new LocationsField<TModel>(htmlHelper, model, getValue, locations);
        }
    }
}
