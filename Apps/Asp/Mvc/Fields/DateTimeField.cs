using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public abstract class DateTimeBaseField<TModel, TValue>
        : TextBoxField<TModel, TValue>
    {
        private string _format;

        protected DateTimeBaseField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue>> getValue)
            : base(htmlHelper, model, getValue)
        {
            base.AddCssPrefix("datetime");
        }

        protected DateTimeBaseField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue> getValue)
            : base(htmlHelper, model, name, getValue)
        {
            base.AddCssPrefix("datetime");
        }

        public DateTimeBaseField<TModel, TValue> WithFormat(string format)
        {
            _format = format;
            return this;
        }

        protected string Format
        {
            get { return _format; }
        }

        protected string FormatValue(DateTime dt)
        {
            return string.IsNullOrEmpty(_format) ? dt.ToString() : dt.ToString(_format);
        }

        protected string FormatValue(DateTime dt, string defaultFormat)
        {
            return string.IsNullOrEmpty(_format) ? dt.ToString(defaultFormat) : dt.ToString(_format);
        }
    }

    public class DateTimeField<TModel>
        : DateTimeBaseField<TModel, DateTime>
    {
        internal DateTimeField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, DateTime>> getValue)
            : base(htmlHelper, model, getValue)
        {
        }

        protected override string GetFormattedValue(DateTime value)
        {
            return FormatValue(value);
        }
    }

    public class OptionalDateTimeField<TModel>
        : DateTimeBaseField<TModel, DateTime?>
    {
        internal OptionalDateTimeField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, DateTime?>> getValue)
            : base(htmlHelper, model, getValue)
        {
        }

        protected override string GetFormattedValue(DateTime? value)
        {
            return value == null ? string.Empty : FormatValue(value.Value);
        }
    }

    public abstract class DateBaseField<TModel, TValue>
        : DateTimeBaseField<TModel, TValue>
    {
        private ReadOnlyUrl _buttonUrl;

        protected DateBaseField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TValue>> getValue)
            : base(htmlHelper, model, getValue)
        {
        }

        protected DateBaseField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TValue> getValue)
            : base(htmlHelper, model, name, getValue)
        {
        }

        public DateBaseField<TModel, TValue> WithButton(ReadOnlyUrl buttonUrl)
        {
            _buttonUrl = buttonUrl;
            return this;
        }

        protected override void BuildPostHtml(System.Text.StringBuilder sb)
        {
            base.BuildPostHtml(sb);

            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("\tjQuery(document).ready(function() {");
            sb.Append("\t\tjQuery(\"#" + Name + "\").datepicker(");

            var options = new SortedList<string, string>();
            if (_buttonUrl != null)
            {
                options["showOn"] = "both";
                options["buttonImage"] = _buttonUrl.ToString();
            }
            if (!string.IsNullOrEmpty(Format))
                options["dateFormat"] = Format.Replace("M", "m").Replace("yyyy", "yy");

            if (options.Count > 0)
            {
                sb.Append("{");
                sb.Append(" ").Append(options.Keys[0]).Append(": '").Append(options.Values[0]).Append("'");
                for (var index = 1; index < options.Count; ++index)
                    sb.Append(", ").Append(options.Keys[index]).Append(": '").Append(options.Values[index]).Append("'");
                sb.Append(" }");
            }

            sb.AppendLine(");");
            sb.AppendLine("});");
            sb.AppendLine("</script>");
        }
    }

    public class DateField<TModel>
        : DateBaseField<TModel, DateTime>
    {
        internal DateField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, DateTime>> getValue)
            : base(htmlHelper, model, getValue)
        {
        }

        internal DateField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, DateTime> getValue)
            : base(htmlHelper, model, name, getValue)
        {
        }

        protected override string GetFormattedValue(DateTime value)
        {
            return FormatValue(value.Date, "d");
        }
    }

    public class OptionalDateField<TModel>
        : DateBaseField<TModel, DateTime?>
    {
        internal OptionalDateField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, DateTime?>> getValue)
            : base(htmlHelper, model, getValue)
        {
        }

        internal OptionalDateField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, DateTime?> getValue)
            : base(htmlHelper, model, name, getValue)
        {
        }

        protected override string GetFormattedValue(DateTime? value)
        {
            return value == null ? string.Empty : FormatValue(value.Value.Date, "d");
        }
    }

    public static class DateTimeFieldExtensions
    {
        public static DateTimeField<TModel> DateTimeField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, DateTime>> getValue)
        {
            return new DateTimeField<TModel>(htmlHelper, model, getValue);
        }

        public static OptionalDateTimeField<TModel> DateTimeField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, DateTime?>> getValue)
        {
            return new OptionalDateTimeField<TModel>(htmlHelper, model, getValue);
        }

        public static DateField<TModel> DateField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, DateTime>> getValue)
        {
            return new DateField<TModel>(htmlHelper, model, getValue);
        }

        public static DateField<TModel> DateField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, DateTime> getValue)
        {
            return new DateField<TModel>(htmlHelper, model, name, getValue);
        }

        public static OptionalDateField<TModel> DateField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, DateTime?>> getValue)
        {
            return new OptionalDateField<TModel>(htmlHelper, model, getValue);
        }

        public static OptionalDateField<TModel> DateField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, DateTime?> getValue)
        {
            return new OptionalDateField<TModel>(htmlHelper, model, name, getValue);
        }
    }
}
