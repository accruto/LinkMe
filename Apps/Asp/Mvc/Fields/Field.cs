using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public class CssClassBuilder
    {
        private readonly StringBuilder _sb = new StringBuilder();

        public void Append(string cssClass)
        {
            if (!string.IsNullOrEmpty(cssClass) && cssClass.Trim().Length != 0)
            {
                if (_sb.Length != 0)
                    _sb.Append(" ");
                _sb.Append(cssClass.Trim());
            }
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }

    public abstract class FieldHelp
    {
        public abstract string ToString(HtmlHelper htmlHelper);
    }

    public abstract class Field
    {
        private readonly HtmlHelper _htmlHelper;
        private bool? _isRequired;
        private bool _isReadOnly;
        private readonly IList<string> _cssPrefixes = new List<string>();

        protected Field(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        internal virtual string Label { get; set; }
        internal string Example { get; set; }
        internal FieldHelp Help { get; set; }

        internal bool IsRequired
        {
            set { _isRequired = value; }
            get { return _isRequired != null && _isRequired.Value; }
        }

        internal bool IsReadOnly
        {
            set
            {
                if (!IsAlwaysReadOnly)
                    _isReadOnly = value;
            }
            get
            {
                return IsAlwaysReadOnly || _isReadOnly;
            }
        }

        protected virtual bool IsAlwaysReadOnly
        {
            get { return false; }
        }

        internal bool IsHidden { get; set; }

        internal protected virtual void AddCssPrefix(string cssPrefix)
        {
            _cssPrefixes.Add(cssPrefix);
        }

        protected HtmlHelper Html
        {
            get { return _htmlHelper; }
        }

        protected IList<string> CssPrefixes
        {
            get { return _cssPrefixes; }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            BuildPreHtml(sb);

            // Label.

            if (Label != null)
                BuildLabelHtml(sb, For, Label);

            // Inner HTML.

            sb.Append(BuildDivHtml(GetControlCssClass(), GetInnerHtml(), false));

            // Help.

            if (Help != null)
                sb.AppendLine().Append(Help.ToString(Html));

            BuildPostHtml(sb);

            return BuildDivHtml(GetFieldCssClass(), sb.ToString(), IsHidden);
        }

        private string GetInnerHtml()
        {
            var sb = new StringBuilder();
            BuildPreInnerHtml(sb);
            sb.Append(InnerHtml);
            BuildPostInnerHtml(sb);
            return sb.ToString();
        }

        protected virtual void BuildPreHtml(StringBuilder sb)
        {
        }

        protected virtual void BuildPostHtml(StringBuilder sb)
        {
        }

        protected virtual void BuildPreInnerHtml(StringBuilder sb)
        {
        }

        protected virtual void BuildPostInnerHtml(StringBuilder sb)
        {
        }

        public abstract MvcHtmlString InnerHtml { get; }

        public virtual string For
        {
            get { return null; }
        }

        protected virtual IDictionary<string, object> GetHtmlAttributes(object htmlAttributes)
        {
            return new RouteValueDictionary(htmlAttributes);
        }

        protected static string GetMemberName<TModel, TMember>(Expression<Func<TModel, TMember>> getMember)
        {
            MemberExpression bodyMemberExpression;

            if (getMember.Body is UnaryExpression && getMember.Body.NodeType == ExpressionType.Convert)
                bodyMemberExpression = ((UnaryExpression)getMember.Body).Operand as MemberExpression;
            else
                bodyMemberExpression = getMember.Body as MemberExpression;

            return bodyMemberExpression != null
                ? bodyMemberExpression.Member.Name
                : string.Empty;
        }

        protected virtual void AppendFieldCssClass(CssClassBuilder builder)
        {
        }

        protected virtual void AppendControlCssClass(CssClassBuilder builder)
        {
        }

        protected string GetFieldCssClass()
        {
            var builder = new CssClassBuilder();

            foreach (var cssPrefix in _cssPrefixes)
                builder.Append(cssPrefix.EndsWith("_") ? cssPrefix + "field" : cssPrefix + "_field");

            // Add options.

            if (IsRequired)
                builder.Append("compulsory_field");
            if (IsReadOnly)
                builder.Append("read-only_field");

            AppendFieldCssClass(builder);
            builder.Append("field");
            return builder.ToString();
        }

        protected string GetControlCssClass()
        {
            var builder = new CssClassBuilder();

            foreach (var cssPrefix in _cssPrefixes)
                builder.Append(cssPrefix.EndsWith("_") ? cssPrefix + "control" : cssPrefix + "_control");

            AppendControlCssClass(builder);
            builder.Append("control");
            return builder.ToString();
        }

        protected static void BuildLabelHtml(StringBuilder sb, string forName, string label)
        {
            var builder = new TagBuilder("label");
            if (!string.IsNullOrEmpty(forName))
                builder.MergeAttribute("for", forName);
            builder.SetInnerText(label);
            sb.AppendLine(builder.ToString(TagRenderMode.Normal));
        }

        private static string BuildDivHtml(string cssClass, string innerHtml, bool isHidden)
        {
            var builder = new TagBuilder("div");
            builder.MergeAttribute("class", cssClass);
            if (isHidden)
                builder.MergeAttribute("style", "display: none;");
            builder.InnerHtml = innerHtml;
            return builder.ToString(TagRenderMode.Normal);
        }

        protected MvcHtmlString TextBox(string name, object value, object htmlAttributes)
        {
            var currentValue = PreHtml(name, value);
            var html = Html.TextBox(name, value, GetHtmlAttributes(htmlAttributes));
            PostHtml(name, currentValue);
            return html;
        }

        protected MvcHtmlString TextArea(string name, string value, int rows, int columns, object htmlAttributes)
        {
            var currentValue = PreHtml(name, value);
            var html = Html.TextArea(name, value, rows, columns, GetHtmlAttributes(htmlAttributes));
            PostHtml(name, currentValue);
            return html;
        }

        protected MvcHtmlString TextArea(string name, string value, object htmlAttributes)
        {
            var currentValue = PreHtml(name, value);
            var html = Html.TextArea(name, value, GetHtmlAttributes(htmlAttributes));
            PostHtml(name, currentValue);
            return html;
        }

        protected MvcHtmlString CheckBox(string name, bool isChecked, object htmlAttributes)
        {
            var currentValue = PreHtml(name, isChecked);
            var html = Html.CheckBox(name, isChecked, GetHtmlAttributes(htmlAttributes));
            PostHtml(name, currentValue);
            return html;
        }

        protected MvcHtmlString RadioButton(string name, object value, bool isChecked, object htmlAttributes)
        {
            var currentValue = PreHtml(name, isChecked);
            var html = Html.RadioButton(name, value, isChecked, GetHtmlAttributes(htmlAttributes));
            PostHtml(name, currentValue);
            return html;
        }

        protected MvcHtmlString Password(string name, string value, object htmlAttributes)
        {
            var currentValue = PreHtml(name, value);
            var html = Html.Password(name, value, GetHtmlAttributes(htmlAttributes));
            PostHtml(name, currentValue);
            return html;
        }

        private ValueProviderResult PreHtml(string name, object value)
        {
            if (value == null)
                return null;

            // Use the value explicitly, ie whatever may be in the model state
            // is not to be used so remove it.

            var modelState = Html.ViewData.ModelState[name];
            if (modelState == null || modelState.Value == null)
                return null;
            
            var currentValue = modelState.Value;
            modelState.Value = new ValueProviderResult(null, null, currentValue.Culture);
            return currentValue;
        }

        private void PostHtml(string name, ValueProviderResult currentValue)
        {
            if (currentValue != null)
                Html.ViewData.ModelState[name].Value = currentValue;
        }
    }

    public static class FieldExtensions
    {
        public static T WithCssPrefix<T>(this T field, string cssClass)
            where T : Field
        {
            field.AddCssPrefix(cssClass);
            return field;
        }

        public static T WithLabel<T>(this T field, string label)
            where T : Field
        {
            field.Label = label;
            return field;
        }

        public static T WithHelp<T>(this T field, FieldHelp help)
            where T : Field
        {
            field.Help = help;
            return field;
        }

        public static T WithIsRequired<T>(this T field)
            where T : Field
        {
            return field.WithIsRequired(true);
        }

        public static T WithIsRequired<T>(this T field, bool isRequired)
            where T : Field
        {
            field.IsRequired = isRequired;
            return field;
        }

        public static T WithIsReadOnly<T>(this T field)
            where T : Field
        {
            return field.WithIsReadOnly(true);
        }

        public static T WithIsReadOnly<T>(this T field, bool isReadOnly)
            where T : Field
        {
            field.IsReadOnly = isReadOnly;
            return field;
        }

        public static T WithIsHidden<T>(this T field)
            where T : Field
        {
            return field.WithIsHidden(true);
        }

        public static T WithIsHidden<T>(this T field, bool isHidden)
            where T : Field
        {
            field.IsHidden = isHidden;
            return field;
        }
    }
}