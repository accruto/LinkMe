using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public abstract class FormattedValueField<TValue, TFormattedValue>
        : NamedField
    {
        private readonly TValue _value;

        protected FormattedValueField(HtmlHelper htmlHelper, string name, TValue value)
            : base(htmlHelper, name)
        {
            _value = value;
        }

        protected FormattedValueField(HtmlHelper htmlHelper, string name)
            : base(htmlHelper, name)
        {
            _value = default(TValue);
        }

        protected TFormattedValue Value
        {
            get { return GetFormattedValue(_value); }
        }

        protected abstract TFormattedValue GetFormattedValue(TValue value);
    }

    public abstract class FormattedValueField<TModel, TValue, TFormattedValue>
        : FormattedValueField<TValue, TFormattedValue>
    {
        protected FormattedValueField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue>> getValue)
            : base(htmlHelper, GetMemberName(getValue), typeof(TModel).IsClass && Equals(model, null) ? default(TValue) : getValue.Compile()(model))
        {
        }

        protected FormattedValueField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue> getValue)
            : base(htmlHelper, name, getValue(model))
        {
        }

        protected FormattedValueField(HtmlHelper htmlHelper, string name, TValue value)
            : base(htmlHelper, name, value)
        {
        }
    }

    public abstract class ValueField<TValue>
        : NamedField
    {
        private readonly TValue _value;

        protected ValueField(HtmlHelper htmlHelper, string name, TValue value)
            : base(htmlHelper, name)
        {
            _value = value;
        }

        protected ValueField(HtmlHelper htmlHelper, string name)
            : base(htmlHelper, name)
        {
            _value = default(TValue);
        }

        protected TValue Value
        {
            get { return _value; }
        }
    }

    public abstract class ValueField<TModel, TValue>
        : ValueField<TValue>
    {
        protected ValueField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue>> getValue)
            : base(htmlHelper, GetMemberName(getValue), typeof(TModel).IsClass && Equals(model, null) ? default(TValue) : getValue.Compile()(model))
        {
        }

        protected ValueField(HtmlHelper htmlHelper, TModel model, string name, Expression<Func<TModel, TValue>> getValue)
            : base(htmlHelper, name, getValue.Compile()(model))
        {
        }

        protected ValueField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue> getValue)
            : base(htmlHelper, name, getValue(model))
        {
        }

        protected ValueField(HtmlHelper htmlHelper, string name, TValue value)
            : base(htmlHelper, name, value)
        {
        }
    }
}