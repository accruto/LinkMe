using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using LinkMe.Apps.Asp.Mvc.Models;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public abstract class DropDownListField<TModel, TValue>
        : ValueField<TModel, TValue>
    {
        private string _preText;

        protected DropDownListField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue>> getValue)
            : base(htmlHelper, model, getValue)
        {
            base.AddCssPrefix("dropdown");
        }

        protected DropDownListField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue> getValue)
            : base(htmlHelper, model, name, getValue)
        {
            base.AddCssPrefix("dropdown");
        }

        internal protected override void AddCssPrefix(string cssPrefix)
        {
            base.AddCssPrefix(cssPrefix + "_dropdown");
        }

        internal void AddPreText(string preText)
        {
            _preText = preText;
        }

        protected virtual string GetDropDownListCssClass()
        {
            var builder = new CssClassBuilder();
            foreach (var cssPrefix in CssPrefixes)
                builder.Append(cssPrefix);
            foreach (var classValue in CssClasses)
                builder.Append(classValue);
            return builder.ToString();
        }

        protected override void BuildPreInnerHtml(StringBuilder sb)
        {
            if (!string.IsNullOrEmpty(_preText))
                sb.Append(_preText);
            base.BuildPreInnerHtml(sb);
        }

        public DropDownListField<TModel, TValue> WithPreText(string preText)
        {
            AddPreText(preText);
            return this;
        }
    }

    public class DropDownListField<TModel, TItem, TValue>
        : DropDownListField<TModel, TValue>
        where TItem : class
        where TValue : struct
    {
        private readonly IEnumerable<TItem> _items;
        private readonly Func<TItem, TValue> _getItemValue;
        private Func<TItem, string> _getItemText;

        public DropDownListField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue> getValue, Func<TItem, TValue> getItemValue, IEnumerable<TItem> items)
            : base(htmlHelper, model, name, getValue)
        {
            _items = items;
            _getItemValue = getItemValue;
        }

        public DropDownListField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue>> getValue, Func<TItem, TValue> getItemValue, IEnumerable<TItem> items)
            : base(htmlHelper, model, getValue)
        {
            _items = items;
            _getItemValue = getItemValue;
        }

        public override MvcHtmlString InnerHtml
        {
            get
            {
                var list = new SelectList<TItem, TValue>(_items, GetItemValue, _getItemText ?? (i => i.ToString()), Value);
                return Html.DropDownList(Name, list, GetHtmlAttributes(new { @class = GetDropDownListCssClass() }));
            }
        }

        private TValue? GetItemValue(TItem item)
        {
            return _getItemValue(item);
        }

        protected void SetItemText(Func<TItem, string> getItemText)
        {
            _getItemText = getItemText;
        }
    }

    public class OptionalDropDownListField<TModel, TItem, TValue>
        : DropDownListField<TModel, TValue?>
        where TItem : class
        where TValue : struct
    {
        private readonly IEnumerable<TItem> _items;
        private readonly Func<TItem, TValue?> _getItemValue;
        private Func<TItem, string> _getItemText;

        public OptionalDropDownListField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue?> getValue, Func<TItem, TValue?> getItemValue, IEnumerable<TItem> items)
            : base(htmlHelper, model, name, getValue)
        {
            // Add an extra empty item to make it optional.

            _items = new TItem[1].Concat(items);
            _getItemValue = getItemValue;
        }

        public OptionalDropDownListField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue?>> getValue, Func<TItem, TValue?> getItemValue, IEnumerable<TItem> items)
            : base(htmlHelper, model, getValue)
        {
            // Add an extra empty item to make it optional.

            _items = new TItem[1].Concat(items);
            _getItemValue = getItemValue;
        }

        public override MvcHtmlString InnerHtml
        {
            get
            {
                var list = new SelectList<TItem, TValue>(_items, _getItemValue, _getItemText ?? (i => i == null ? null : i.ToString()), Value);
                return Html.DropDownList(Name, list, GetHtmlAttributes(new { @class = GetDropDownListCssClass() }));
            }
        }

        public OptionalDropDownListField<TModel, TItem, TValue> WithText(Func<TItem, string> getItemText)
        {
            _getItemText = getItemText;
            return this;
        }

        protected void SetItemText(Func<TItem, string> getItemText)
        {
            _getItemText = getItemText;
        }
    }

    public class EnumDropDownListField<TModel, TValue>
        : DropDownListField<TModel, TValue>
        where TValue : struct
    {
        private Func<TValue, string> _getText;
        private IList<TValue> _values;
        private IList<TValue> _except;

        public EnumDropDownListField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue> getValue)
            : base(htmlHelper, model, name, getValue)
        {
        }

        public EnumDropDownListField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue>> getValue)
            : base(htmlHelper, model, getValue)
        {
        }

        public override MvcHtmlString InnerHtml
        {
            get { return Html.DropDownList(Name, new EnumSelectList<TValue>(Value, _values, _except, _getText ?? (v => v.ToString())), GetHtmlAttributes(new { @class = GetDropDownListCssClass() })); }
        }

        public EnumDropDownListField<TModel, TValue> With(IEnumerable<TValue> values)
        {
            if (values == null || values.Count() == 0)
                return this;

            if (_values == null)
                _values = new List<TValue>();
            foreach (var value in values)
                _values.Add(value);
            return this;
        }

        public EnumDropDownListField<TModel, TValue> Without(TValue value)
        {
            if (_except == null)
                _except = new List<TValue>();
            _except.Add(value);
            return this;
        }

        protected void SetText(Func<TValue, string> getText)
        {
            _getText = getText;
        }
    }

    public class OptionalEnumDropDownListField<TModel, TValue>
        : DropDownListField<TModel, TValue?>
        where TValue : struct
    {
        private Func<TValue?, string> _getText;
        private Func<TValue?, TValue?, int> _comparer;
        private IList<TValue> _values;
        private IList<TValue> _except;

        public OptionalEnumDropDownListField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue?> getValue)
            : base(htmlHelper, model, name, getValue)
        {
        }

        public OptionalEnumDropDownListField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue?>> getValue)
            : base(htmlHelper, model, getValue)
        {
        }

        public override MvcHtmlString InnerHtml
        {
            get { return Html.DropDownList(Name, new OptionalEnumSelectList<TValue>(Value, _values, _except, _comparer, _getText), GetHtmlAttributes(new { @class = GetDropDownListCssClass() })); }
        }

        public OptionalEnumDropDownListField<TModel, TValue> With(IEnumerable<TValue> values)
        {
            if (values == null || values.Count() == 0)
                return this;

            if (_values == null)
                _values = new List<TValue>();
            foreach (var value in values)
                _values.Add(value);
            return this;
        }

        public OptionalEnumDropDownListField<TModel, TValue> Without(TValue value)
        {
            if (_except == null)
                _except = new List<TValue>();
            _except.Add(value);
            return this;
        }

        protected void SetText(Func<TValue?, string> getText)
        {
            _getText = getText;
        }

        protected void SetComparer(Func<TValue?, TValue?, int> comparer)
        {
            _comparer = comparer;
        }

        public OptionalEnumDropDownListField<TModel, TValue> WithText(Func<TValue?, string> getText)
        {
            SetText(getText);
            return this;
        }
    }

    public class ValuesDropDownListField<TModel, TValue>
        : DropDownListField<TModel, TValue>
    {
        private readonly IEnumerable<TValue> _values;
        private Func<TValue, string> _getText;

        public ValuesDropDownListField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue> getValue, IEnumerable<TValue> values)
            : base(htmlHelper, model, name, getValue)
        {
            _values = values;
        }

        public ValuesDropDownListField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue>> getValue, IEnumerable<TValue> values)
            : base(htmlHelper, model, getValue)
        {
            _values = values;
        }

        public override MvcHtmlString InnerHtml
        {
            get { return Html.DropDownList(Name, new SelectList<TValue>(_values, Value, _getText), GetHtmlAttributes(new { @class = GetDropDownListCssClass() })); }
        }

        public ValuesDropDownListField<TModel, TValue> WithText(Func<TValue, string> getText)
        {
            SetText(getText);
            return this;
        }

        protected void SetText(Func<TValue, string> getText)
        {
            _getText = getText;
        }
    }

    public static class DropDownListFieldExtensions
    {
        public static ValuesDropDownListField<TModel, TValue> DropDownListField<TModel, TValue>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue> getValue, IEnumerable<TValue> values)
        {
            return new ValuesDropDownListField<TModel, TValue>(htmlHelper, model, name, getValue, values);
        }

        public static ValuesDropDownListField<TModel, TValue> DropDownListField<TModel, TValue>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue>> getValue, IEnumerable<TValue> values)
        {
            return new ValuesDropDownListField<TModel, TValue>(htmlHelper, model, getValue, values);
        }

        public static EnumDropDownListField<TModel, TValue> DropDownListField<TModel, TValue>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue>> getValue)
            where TValue : struct
        {
            return new EnumDropDownListField<TModel, TValue>(htmlHelper, model, getValue);
        }

        public static OptionalEnumDropDownListField<TModel, TValue> DropDownListField<TModel, TValue>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue?> getValue)
            where TValue : struct
        {
            return new OptionalEnumDropDownListField<TModel, TValue>(htmlHelper, model, name, getValue);
        }

        public static OptionalEnumDropDownListField<TModel, TValue> DropDownListField<TModel, TValue>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue?>> getValue)
            where TValue : struct
        {
            return new OptionalEnumDropDownListField<TModel, TValue>(htmlHelper, model, getValue);
        }
    }
}