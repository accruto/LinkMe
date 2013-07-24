using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;

namespace LinkMe.Web.Html
{
    // Eventually it would be nice to put regular expression, client side checks here etc.

    public class NameTextBoxField<TModel>
        : TextBoxField<TModel, string>
    {
        public NameTextBoxField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
            : base(htmlHelper, model, getValue)
        {
        }
    }

    public class PhoneNumberTextBoxField<TModel>
        : TextBoxField<TModel, string>
    {
        public PhoneNumberTextBoxField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
            : base(htmlHelper, model, getValue)
        {
            base.AddCssPrefix("phone");
        }

        public PhoneNumberTextBoxField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, string> getValue)
            : base(htmlHelper, model, name, getValue)
        {
            base.AddCssPrefix("phone");
        }
    }

    public class EmailAddressTextBoxField<TModel>
        : TextBoxField<TModel, string>
    {
        public EmailAddressTextBoxField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
            : base(htmlHelper, model, getValue)
        {
            base.AddCssPrefix("email");
        }

        public EmailAddressTextBoxField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, string> getValue)
            : base(htmlHelper, model, name, getValue)
        {
            base.AddCssPrefix("email");
        }
    }

    public static class SpecificTextBoxFieldExtensions
    {
        public static NameTextBoxField<TModel> NameTextBoxField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
        {
            return new NameTextBoxField<TModel>(htmlHelper, model, getValue);
        }

        public static EmailAddressTextBoxField<TModel> EmailAddressTextBoxField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
        {
            return new EmailAddressTextBoxField<TModel>(htmlHelper, model, getValue);
        }

        public static EmailAddressTextBoxField<TModel> EmailAddressTextBoxField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, string> getValue)
        {
            return new EmailAddressTextBoxField<TModel>(htmlHelper, model, name, getValue);
        }

        public static PhoneNumberTextBoxField<TModel> PhoneNumberTextBoxField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, string>> getValue)
        {
            return new PhoneNumberTextBoxField<TModel>(htmlHelper, model, getValue);
        }

        public static PhoneNumberTextBoxField<TModel> PhoneNumberTextBoxField<TModel>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, string> getValue)
        {
            return new PhoneNumberTextBoxField<TModel>(htmlHelper, model, name, getValue);
        }
    }
}
