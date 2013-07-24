using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Html
{
    public static class RouteExtensions
    {
        public static ReadOnlyApplicationUrl RouteRefUrl(this HtmlHelper htmlHelper, RouteReference route)
        {
            return route.GenerateUrl();
        }

        public static ReadOnlyApplicationUrl RouteRefUrl(this HtmlHelper htmlHelper, RouteReference route, object routeValues)
        {
            return route.GenerateUrl(routeValues);
        }

        public static ReadOnlyApplicationUrl RouteRefUrl(this HtmlHelper htmlHelper, RouteReference route, RouteValueDictionary routeValues)
        {
            return route.GenerateUrl(routeValues);
        }

        public static MvcHtmlString RouteRefLink(this HtmlHelper htmlHelper, string linkText, RouteReference route, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            var url = htmlHelper.RouteRefUrl(route, routeValues);
            var tagBuilder = new TagBuilder("a") { InnerHtml = !string.IsNullOrEmpty(linkText) ? HttpUtility.HtmlEncode(linkText) : string.Empty };
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("href", url.ToString());
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString RouteRefLink(this HtmlHelper htmlHelper, string linkText, RouteReference route, object routeValues, object htmlAttributes)
        {
            return htmlHelper.RouteRefLink(linkText, route, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString RouteRefLink(this HtmlHelper htmlHelper, string linkText, RouteReference route, RouteValueDictionary routeValues)
        {
            return htmlHelper.RouteRefLink(linkText, route, routeValues, null);
        }

        public static MvcHtmlString RouteRefLink(this HtmlHelper htmlHelper, string linkText, RouteReference route, RouteValueDictionary routeValues, object htmlAttributes)
        {
            return htmlHelper.RouteRefLink(linkText, route, routeValues, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString RouteRefLink(this HtmlHelper htmlHelper, string linkText, RouteReference route, object routeValues)
        {
            return htmlHelper.RouteRefLink(linkText, route, new RouteValueDictionary(routeValues));
        }

        public static MvcHtmlString RouteRefLink(this HtmlHelper htmlHelper, string linkText, RouteReference route)
        {
            return htmlHelper.RouteRefLink(linkText, route, null);
        }

        public static string MungeUrl(this HtmlHelper htmlHelper, ReadOnlyApplicationUrl url)
        {
            return url.ToString().Replace("/", "~@~");
        }

        public static void TransferTracking(this HttpContextBase context, Action<string, string> add)
        {
            if (context.Request.HttpMethod != "GET")
                return;

            var clientUrl = context.GetClientUrl();
            var utmSource = clientUrl.QueryString["utm_source"];
            var utmMedium = clientUrl.QueryString["utm_medium"];
            var utmCampaign = clientUrl.QueryString["utm_campaign"];
            var gclid = clientUrl.QueryString["gclid"];

            if (!string.IsNullOrEmpty(utmSource))
                add("utm_source", utmSource);
            if (!string.IsNullOrEmpty(utmMedium))
                add("utm_medium", utmMedium);
            if (!string.IsNullOrEmpty(utmCampaign))
                add("utm_campaign", utmCampaign);
            if (!string.IsNullOrEmpty(gclid))
                add("gclid", gclid);
        }
    }
}
