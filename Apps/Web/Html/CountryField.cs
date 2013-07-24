using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Domain.Location;

namespace LinkMe.Web.Html
{
    public class CountryField<TModel>
        : DropDownListField<TModel, Country, int>
    {
        internal CountryField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, int>> getValue, IEnumerable<Country> countries)
            : base(htmlHelper, model, getValue, c => c.Id, countries)
        {
            SetItemText(c => c.Name);
        }

        internal CountryField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, int> getValue, IEnumerable<Country> countries)
            : base(htmlHelper, model, name, getValue, c => c.Id, countries)
        {
            SetItemText(c => c.Name);
        }
    }

    public class OptionalCountryField<TModel>
        : OptionalDropDownListField<TModel, Country, int>
    {
        internal OptionalCountryField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, int?>> getValue, IEnumerable<Country> countries)
            : base(htmlHelper, model, getValue, c => c == null ? (int?)null : c.Id, countries)
        {
            SetItemText(c => c == null ? null : c.Name);
        }

        internal OptionalCountryField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, int?> getValue, IEnumerable<Country> countries)
            : base(htmlHelper, model, name, getValue, c => c == null ? (int?)null : c.Id, countries)
        {
            SetItemText(c => c == null ? null : c.Name);
        }
    }

    public static class CountryFieldExtensions
    {
        public static CountryField<TModel> CountryField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, int>> getValue, IEnumerable<Country> countries)
        {
            return new CountryField<TModel>(htmlHelper, model, getValue, countries);
        }

        public static CountryField<TModel> CountryField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, int> getValue, IEnumerable<Country> countries)
        {
            return new CountryField<TModel>(htmlHelper, model, name, getValue, countries);
        }

        public static OptionalCountryField<TModel> CountryField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, int?>> getValue, IEnumerable<Country> countries)
        {
            return new OptionalCountryField<TModel>(htmlHelper, model, getValue, countries);
        }

        public static OptionalCountryField<TModel> CountryField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, int?> getValue, IEnumerable<Country> countries)
        {
            return new OptionalCountryField<TModel>(htmlHelper, model, name, getValue, countries);
        }
    }
}
