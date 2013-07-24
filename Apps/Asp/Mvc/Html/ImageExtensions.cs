using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Html
{
    public static class ImageExtensions
    {
        public static string Image(this HtmlHelper helper, ReadOnlyUrl url, object htmlAttributes)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", url.ToString());
            if (htmlAttributes != null)
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return builder.ToString(TagRenderMode.SelfClosing);
        }

        public static string Image(this HtmlHelper helper, ReadOnlyUrl url)
        {
            return helper.Image(url, null);
        }

        public static string Image(this HtmlHelper helper, RouteReference route, object routeValues)
        {
            return helper.Image(route.GenerateUrl(routeValues));
        }

        public static string Image(this HtmlHelper helper, RouteReference route, object routeValues, object htmlAttributes)
        {
            return helper.Image(route.GenerateUrl(routeValues), htmlAttributes);
        }
    }
}
