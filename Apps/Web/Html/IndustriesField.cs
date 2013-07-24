using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Domain.Industries;

namespace LinkMe.Web.Html
{
    public class IndustriesField<TModel>
        : ListBoxField<TModel, Industry, Guid>
    {
        internal IndustriesField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, IEnumerable<Guid>>> getValue, IEnumerable<Industry> industries)
            : base(htmlHelper, model, getValue, i => i.Id, i => i.Name, (from i in industries where i.Name != "Other" select i).Concat(from i in industries where i.Name == "Other" select i))
        {
            base.AddCssPrefix("industry_listbox");
        }

        internal IndustriesField(HtmlHelper htmlHelper, TModel model, string name, Expression<Func<TModel, IEnumerable<Guid>>> getValue, IEnumerable<Industry> industries)
            : base(htmlHelper, model, name, getValue, i => i.Id, i => i.Name, (from i in industries where i.Name != "Other" select i).Concat(from i in industries where i.Name == "Other" select i))
        {
            base.AddCssPrefix("industry_listbox");
        }

        public IndustriesField<TModel> WithSize(int size)
        {
            return this.WithSize<IndustriesField<TModel>, TModel, Industry, Guid>(size);
        }
    }

    public static class IndustriesFieldExtensions
    {
        public static IndustriesField<TModel> IndustriesField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, IEnumerable<Guid>>> getValue, IEnumerable<Industry> industries)
        {
            return new IndustriesField<TModel>(htmlHelper, model, getValue, industries);
        }

        public static IndustriesField<TModel> IndustriesField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Expression<Func<TModel, IEnumerable<Guid>>> getValue, IEnumerable<Industry> industries)
        {
            return new IndustriesField<TModel>(htmlHelper, model, name, getValue, industries);
        }
    }
}
