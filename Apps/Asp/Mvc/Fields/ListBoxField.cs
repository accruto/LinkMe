using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using LinkMe.Apps.Asp.Mvc.Models;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public abstract class ListBoxField<TModel, TItem, TValue>
        : ValueField<TModel, IEnumerable<TValue>>
    {
        private int? _size;
        private readonly IEnumerable<TItem> _items;
        private readonly string _valueName;
        private readonly string _textName;

        protected ListBoxField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, IEnumerable<TValue>>> getValue, Expression<Func<TItem, TValue>> getItemValue, Expression<Func<TItem, string>> getItemText, IEnumerable<TItem> items)
            : base(htmlHelper, model, getValue)
        {
            base.AddCssPrefix("listbox");
            _items = items;
            _valueName = GetMemberName(getItemValue);
            _textName = GetMemberName(getItemText);
        }

        protected ListBoxField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, IEnumerable<TValue>> getValue, Expression<Func<TItem, TValue>> getItemValue, Expression<Func<TItem, string>> getItemText, IEnumerable<TItem> items)
            : base(htmlHelper, model, name, getValue)
        {
            base.AddCssPrefix("listbox");
            _items = items;
            _valueName = GetMemberName(getItemValue);
            _textName = GetMemberName(getItemText);
        }

        protected ListBoxField(HtmlHelper htmlHelper, TModel model, string name, Expression<Func<TModel, IEnumerable<TValue>>> getValue, Expression<Func<TItem, TValue>> getItemValue, Expression<Func<TItem, string>> getItemText, IEnumerable<TItem> items)
            : base(htmlHelper, model, name, getValue)
        {
            base.AddCssPrefix("listbox");
            _items = items;
            _valueName = GetMemberName(getItemValue);
            _textName = GetMemberName(getItemText);
        }

        public override MvcHtmlString InnerHtml
        {
            get
            {
                var list = new MultiSelectList<TItem, TValue>(_items, _valueName, _textName, Value);
                return _size != null
                    ? Html.ListBox(Name, list, GetHtmlAttributes(new { @class = GetListBoxCssClass(), size = _size.Value }))
                    : Html.ListBox(Name, list, GetHtmlAttributes(new { @class = GetListBoxCssClass() }));
            }
        }

        internal int Size
        {
            set { _size = value; }
        }

        protected virtual string GetListBoxCssClass()
        {
            var builder = new CssClassBuilder();
            AppendListBoxCssClass(builder);
            builder.Append("listbox");
            return builder.ToString();
        }

        protected virtual void AppendListBoxCssClass(CssClassBuilder builder)
        {
        }
    }

    public static class ListBoxFieldExtensions
    {
        public static TField WithSize<TField, TModel, TItem, TValue>(this TField field, int size)
            where TField : ListBoxField<TModel, TItem, TValue>
        {
            field.Size = size;
            return field;
        }
    }
}