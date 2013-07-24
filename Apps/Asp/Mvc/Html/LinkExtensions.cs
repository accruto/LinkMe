using System.Web.Mvc;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Html
{
    public static class LinkExtensions
    {
        public static MvcHtmlString Link<THtmlHelper>(this THtmlHelper helper, Link link)
        {
            var builder = new TagBuilder("a");
            if (!string.IsNullOrEmpty(link.Id))
                builder.MergeAttribute("id", link.Id);
            if (!string.IsNullOrEmpty(link.CssClass))
                builder.AddCssClass(link.CssClass);
            if (link.Url != null)
                builder.MergeAttribute("href", link.Url);
            if (!string.IsNullOrEmpty(link.Title))
                builder.MergeAttribute("title", link.Title);
            if (!string.IsNullOrEmpty(link.Target))
                builder.MergeAttribute("target", link.Target);
            builder.SetInnerText(link.Text);
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString Link(this HtmlHelper helper, ReadOnlyUrl url, string text, string title, string cssClass, string target, string id)
        {
            return Link(helper, new Link(url, text, title, cssClass, target, id));
        }

        public static MvcHtmlString Link(this HtmlHelper helper, ReadOnlyUrl url, string text, string title, string cssClass, string target)
        {
            return Link(helper, new Link(url, text, title, cssClass, target, null));
        }

        public static MvcHtmlString Link(this HtmlHelper helper, string url, string text, string title, string cssClass, string target)
        {
            return Link(helper, new Link(url, text, title, cssClass, target, null));
        }

        public static MvcHtmlString Link(this HtmlHelper helper, ReadOnlyUrl url, string text, string title, string cssClass)
        {
            return Link(helper, new Link(url, text, title, cssClass, null, null));
        }

        public static MvcHtmlString Link(this HtmlHelper helper, ReadOnlyUrl url, string text, string title)
        {
            return Link(helper, new Link(url, text, title, null, null, null));
        }

        public static MvcHtmlString Link(this HtmlHelper helper, ReadOnlyUrl url, string text)
        {
            return Link(helper, new Link(url, text, null, null, null, null));
        }
    }
}
