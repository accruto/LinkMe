using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Domain.Location;

namespace LinkMe.Web.Html
{
    public class CountriesField<TModel>
        : ListBoxField<TModel, Country, int>
    {
        public CountriesField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, IEnumerable<int>>> getValue, IEnumerable<Country> countries)
            : base(htmlHelper, model, getValue, c => c.Id, c => c.Name, countries)
        {
        }
    }

    public static class CountriesFieldExtensions
    {
        public static CountriesField<TModel> CountriesField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, IEnumerable<int>>> getValue, IEnumerable<Country> countries)
        {
            return new CountriesField<TModel>(htmlHelper, model, getValue, countries);
        }
    }
}