using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public class CheckBoxField<TModel>
        : ValueField<TModel, bool>
    {
        private bool _withLabelOnRight;

        public CheckBoxField(HtmlHelper htmlHelper, string name, bool value)
            : base(htmlHelper, name, value)
        {
            base.AddCssPrefix("checkbox");
        }

        public CheckBoxField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, bool>> getValue)
            : base(htmlHelper, model, getValue)
        {
            base.AddCssPrefix("checkbox");
        }

        public CheckBoxField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, bool> getValue)
            : base(htmlHelper, model, name, getValue)
        {
            base.AddCssPrefix("checkbox");
        }

        public CheckBoxField<TModel> WithLabelOnRight(string label)
        {
            _withLabelOnRight = true;
            return this.WithLabel(label);
        }

        public override MvcHtmlString InnerHtml
        {
            get
            {
                var htmlAttributes = IsReadOnly
                    ? (object)new { @class = GetCheckBoxCssClass(), disabled = "disabled" }
                    : new { @class = GetCheckBoxCssClass() };
                return CheckBox(Name, Value, htmlAttributes);
            }
        }

        internal override string Label
        {
            get { return _withLabelOnRight ? string.Empty : base.Label; }
        }

        protected override void BuildPostInnerHtml(StringBuilder sb)
        {
            if (_withLabelOnRight)
                BuildLabelHtml(sb, Name, base.Label);
            else
                base.BuildPostInnerHtml(sb);
        }

        protected virtual string GetCheckBoxCssClass()
        {
            var builder = new CssClassBuilder();
            AppendCheckBoxCssClass(builder);
            builder.Append("checkbox");
            foreach (var cssClass in CssClasses)
                builder.Append(cssClass);
            return builder.ToString();
        }

        protected virtual void AppendCheckBoxCssClass(CssClassBuilder builder)
        {
        }
    }

    public static class CheckBoxFieldExtensions
    {
        public static CheckBoxField<TModel> CheckBoxField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, bool>> getValue)
        {
            return new CheckBoxField<TModel>(htmlHelper, model, getValue);
        }

        public static CheckBoxField<TModel> CheckBoxField<TModel>(this HtmlHelper htmlHelper, string name, bool value)
        {
            return new CheckBoxField<TModel>(htmlHelper, name, value);
        }

        public static CheckBoxField<TModel> CheckBoxField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, bool> getValue)
        {
            return new CheckBoxField<TModel>(htmlHelper, model, name, getValue);
        }

        public static CheckBoxField<string> CheckBoxField(this HtmlHelper htmlHelper, string name, bool value)
        {
            return new CheckBoxField<string>(htmlHelper, name, value);
        }
    }
}