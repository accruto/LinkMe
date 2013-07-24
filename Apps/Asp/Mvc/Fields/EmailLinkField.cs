using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Html;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public class EmailLinkField
        : Field
    {
        private readonly string _name;
        private readonly string _emailAddress;

        public EmailLinkField(HtmlHelper helper, string name, string emailAddress)
            : base(helper)
        {
            _name = name;
            _emailAddress = emailAddress;
        }

        public override MvcHtmlString InnerHtml
        {
            get { return Html.EmailLink(_name, _emailAddress); }
        }
    }

    public static class EmailLinkFieldExtensions
    {
        public static EmailLinkField EmailLinkField(this HtmlHelper htmlHelper, string name, string emailAddress)
        {
            return new EmailLinkField(htmlHelper, name, emailAddress);
        }
    }
}
