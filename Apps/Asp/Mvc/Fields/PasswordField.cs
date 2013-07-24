using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public class PasswordField
        : TextBoxField
    {
        public PasswordField(HtmlHelper htmlHelper, string name, string value)
            : base(htmlHelper, name, value)
        {
        }

        public PasswordField(HtmlHelper htmlHelper, string name)
            : base(htmlHelper, name)
        {
        }

        public override MvcHtmlString InnerHtml
        {
            get { return Password(Name, Value, new { @class = GetTextBoxCssClass() }); }
        }
    }

    public class PasswordField<TModel>
        : TextBoxField<TModel, string>
    {
        internal PasswordField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
            : base(htmlHelper, model, getValue)
        {
            base.AddCssPrefix("password");
        }

        public override MvcHtmlString InnerHtml
        {
            get { return Password(Name, Value, new { @class = GetTextBoxCssClass() }); }
        }
    }

    public static class PasswordFieldExtensions
    {
        public static PasswordField PasswordField(this HtmlHelper htmlHelper, string name, string value)
        {
            return new PasswordField(htmlHelper, name, value);
        }

        public static PasswordField PasswordField(this HtmlHelper htmlHelper, string name)
        {
            return new PasswordField(htmlHelper, name);
        }

        public static PasswordField<TModel> PasswordField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
        {
            return new PasswordField<TModel>(htmlHelper, model, getValue);
        }
    }
}