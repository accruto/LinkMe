using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Html
{
    public static class EmailLinkExtensions
    {
        public static MvcHtmlString EmailLink(this HtmlHelper htmlHelper, string name, string emailAddress)
        {
            var tagBuilder = new TagBuilder("a");
            tagBuilder.MergeAttribute("href", "mailto:" + emailAddress);
            tagBuilder.InnerHtml = htmlHelper.Encode(name);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}
