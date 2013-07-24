using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public enum TextBoxWidth
    {
        Shorter,
        Standard,
        Larger,
        Largest
    }

    public class TextBoxField<TModel, TValue>
        : FormattedValueField<TModel, TValue, string>
    {
        private TextBoxWidth _width = TextBoxWidth.Standard;

        internal protected TextBoxField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue>> getValue)
            : base(htmlHelper, model, getValue)
        {
            base.AddCssPrefix("textbox");
        }

        internal protected TextBoxField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue> getValue)
            : base(htmlHelper, model, name, getValue)
        {
            base.AddCssPrefix("textbox");
        }

        internal protected TextBoxField(HtmlHelper htmlHelper, string name, TValue value)
            : base(htmlHelper, name, value)
        {
            base.AddCssPrefix("textbox");
        }

        public override MvcHtmlString InnerHtml
        {
            get
            {
                var htmlAttributes = IsReadOnly
                    ? (object)new { @class = GetTextBoxCssClass(), @readonly = "readonly" }
                    : new { @class = GetTextBoxCssClass() };
                return TextBox(Name, Value, htmlAttributes);
            }
        }

        public TextBoxField<TModel, TValue> WithShorterWidth()
        {
            _width = TextBoxWidth.Shorter;
            return this;
        }

        public TextBoxField<TModel, TValue> WithLargerWidth()
        {
            _width = TextBoxWidth.Larger;
            return this;
        }

        public TextBoxField<TModel, TValue> WithLargestWidth()
        {
            _width = TextBoxWidth.Largest;
            return this;
        }

        protected internal override void AddCssPrefix(string cssPrefix)
        {
            base.AddCssPrefix(cssPrefix + "_textbox");
        }

        protected virtual string GetTextBoxCssClass()
        {
            var builder = new CssClassBuilder();
            foreach (var cssPrefix in CssPrefixes)
                builder.Append(cssPrefix);
            AppendTextBoxCssClass(builder);
            foreach (var cssClass in CssClasses)
                builder.Append(cssClass);
            return builder.ToString();
        }

        protected virtual void AppendTextBoxCssClass(CssClassBuilder builder)
        {
            switch (_width)
            {
                case TextBoxWidth.Shorter:
                    builder.Append("shorter_textbox");
                    break;

                case TextBoxWidth.Larger:
                    builder.Append("wider_textbox");
                    break;

                case TextBoxWidth.Largest:
                    builder.Append("widest_textbox");
                    break;
            }
        }

        protected override string GetFormattedValue(TValue value)
        {
            return typeof(TValue).IsClass && Equals(value, null) ? null : value.ToString();
        }
    }

    public class TextBoxField<TValue>
        : TextBoxField<TValue, TValue>
    {
        internal protected TextBoxField(HtmlHelper htmlHelper, string name, TValue value)
            : base(htmlHelper, name, value)
        {
        }

        internal protected TextBoxField(HtmlHelper htmlHelper, string name)
            : base(htmlHelper, name, default(TValue))
        {
        }
    }

    public class TextBoxField
        : TextBoxField<string>
    {
        internal protected TextBoxField(HtmlHelper htmlHelper, string name, string value)
            : base(htmlHelper, name, value)
        {
        }

        internal protected TextBoxField(HtmlHelper htmlHelper, string name)
            : base(htmlHelper, name, null)
        {
        }
    }

    public class MultilineTextBoxField
        : TextBoxField<string>
    {
        public MultilineTextBoxField(HtmlHelper htmlHelper, string name, string value)
            : base(htmlHelper, name, value)
        {
            base.AddCssPrefix("multiline");
        }

        public MultilineTextBoxField(HtmlHelper htmlHelper, string name)
            : base(htmlHelper, name)
        {
            base.AddCssPrefix("multiline");
        }

        public override MvcHtmlString InnerHtml
        {
            get
            {
                var htmlAttributes = IsReadOnly
                    ? (object)new { @class = GetTextBoxCssClass(), @readonly = "readonly" }
                    : new { @class = GetTextBoxCssClass() };
                return TextArea(Name, Value, htmlAttributes);
            }
        }
    }

    public class MultilineTextBoxField<TModel>
        : TextBoxField<TModel, string>
    {
        private bool _htmlEditable;
        private int _rows;
        private int _columns;

        public MultilineTextBoxField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
            : base(htmlHelper, model, getValue)
        {
            base.AddCssPrefix("multiline");
        }

        public MultilineTextBoxField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, string> getValue)
            : base(htmlHelper, model, name, getValue)
        {
            base.AddCssPrefix("multiline");
        }

        public MultilineTextBoxField<TModel> WithSize(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            return this;
        }

        public MultilineTextBoxField<TModel> WithHtmlEditable()
        {
            _htmlEditable = true;
            return this;
        }

        public override MvcHtmlString InnerHtml
        {
            get
            {
                if (_rows > 0 && _columns > 0)
                    return TextArea(Name, Value, _rows, _columns, new { @class = GetTextBoxCssClass() });
                return TextArea(Name, Value, new { @class = GetTextBoxCssClass() });
            }
        }

        protected override void AppendTextBoxCssClass(CssClassBuilder builder)
        {
            base.AppendTextBoxCssClass(builder);
            if (_htmlEditable)
                builder.Append("html-editable");
        }
    }

    public class MessageTextBoxField<TModel>
        : MultilineTextBoxField<TModel>
    {
        public MessageTextBoxField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
            : base(htmlHelper, model, getValue)
        {
            base.AddCssPrefix("message_multiline");
        }

        public MessageTextBoxField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, string> getValue)
            : base(htmlHelper, model, name, getValue)
        {
            base.AddCssPrefix("message_multiline");
        }
    }

    public class YesNoTextBoxField<TModel>
        : TextBoxField<TModel, bool>
    {
        public YesNoTextBoxField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, bool>> getValue)
            : base(htmlHelper, model, getValue)
        {
        }

        protected override bool IsAlwaysReadOnly
        {
            get { return true; }
        }

        protected override string GetFormattedValue(bool value)
        {
            return value ? "Yes" : "No";
        }
    }

    public static class TextBoxFieldExtensions
    {
        public static TextBoxField<TValue> TextBoxField<TValue>(this HtmlHelper htmlHelper, string name, TValue value)
        {
            return new TextBoxField<TValue>(htmlHelper, name, value);
        }

        public static MultilineTextBoxField MultilineTextBoxField(this HtmlHelper htmlHelper, string name, string value)
        {
            return new MultilineTextBoxField(htmlHelper, name, value);
        }

        public static TextBoxField<TModel, TValue> TextBoxField<TModel, TValue>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue>> getValue)
        {
            return new TextBoxField<TModel, TValue>(htmlHelper, model, getValue);
        }

        public static TextBoxField<TModel, TValue> TextBoxField<TModel, TValue>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue> getValue)
        {
            return new TextBoxField<TModel, TValue>(htmlHelper, model, name, getValue);
        }

        public static MultilineTextBoxField<TModel> MultilineTextBoxField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
        {
            return new MultilineTextBoxField<TModel>(htmlHelper, model, getValue);
        }

        public static MultilineTextBoxField<TModel> MultilineTextBoxField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, string> getValue)
        {
            return new MultilineTextBoxField<TModel>(htmlHelper, model, name, getValue);
        }

        public static MessageTextBoxField<TModel> MessageTextBoxField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
        {
            return new MessageTextBoxField<TModel>(htmlHelper, model, getValue);
        }

        public static MessageTextBoxField<TModel> MessageTextBoxField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, string> getValue)
        {
            return new MessageTextBoxField<TModel>(htmlHelper, model, name, getValue);
        }

        public static YesNoTextBoxField<TModel> YesNoTextBoxField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, bool>> getValue)
        {
            return new YesNoTextBoxField<TModel>(htmlHelper, model, getValue);
        }
    }
}