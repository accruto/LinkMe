using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public class LinkField
        : Field, IHaveId
    {
        private readonly string _linkText;
        private readonly ReadOnlyUrl _url;
        private string _id;

        public LinkField(HtmlHelper helper, ReadOnlyUrl url, string linkText)
            : base(helper)
        {
            _url = url;
            _linkText = linkText;
            base.AddCssPrefix("link");
        }

        public override MvcHtmlString InnerHtml
        {
            get { return Html.Link(_url, _linkText, null, null, null, _id); }
        }

        string IHaveId.Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }

    public static class LinkFieldExtensions
    {
        public static LinkField LinkField(this HtmlHelper htmlHelper, ReadOnlyUrl url, string linkText)
        {
            return new LinkField(htmlHelper, url, linkText);
        }
    }
}
