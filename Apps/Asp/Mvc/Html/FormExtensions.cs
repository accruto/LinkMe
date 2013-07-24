using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Html
{
    public static class FormExtensions
    {
        public static MvcForm RenderForm(this HtmlHelper helper, ReadOnlyUrl url, string id, bool supportsFiles)
        {
            var tagBuilder = new TagBuilder("form");
            tagBuilder.MergeAttributes(new RouteValueDictionary(supportsFiles ? (object)new { id, enctype = "multipart/form-data" } : new { id }));
            tagBuilder.MergeAttribute("action", url.ToString());
            tagBuilder.MergeAttribute("method", HtmlHelper.GetFormMethodString(FormMethod.Post), true);

            helper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            return new MvcForm(helper.ViewContext);
        }

        public static MvcForm RenderForm(this HtmlHelper helper, ReadOnlyUrl url, bool supportsFiles)
        {
            return helper.RenderForm(url, "Form", supportsFiles);
        }

        public static MvcForm RenderForm(this HtmlHelper helper, ReadOnlyUrl url, string id)
        {
            return helper.RenderForm(url, id, false);
        }

        public static MvcForm RenderForm(this HtmlHelper helper, ReadOnlyUrl url)
        {
            return helper.RenderForm(url, "Form");
        }

        public static MvcForm RenderForm(this HtmlHelper helper, object routeValues)
        {
            return helper.BeginForm(null, null, routeValues, FormMethod.Post, new { id = "Form" });
        }

        public static MvcForm RenderForm(this HtmlHelper helper)
        {
            return helper.BeginForm(null, null, FormMethod.Post, new { id = "Form" });
        }

        public static MvcForm RenderForm(this HtmlHelper helper, string id)
        {
            return helper.BeginForm(null, null, FormMethod.Post, new { id });
        }

        public static MvcForm RenderRouteForm(this HtmlHelper helper, RouteReference route, object routeValues, string id, bool supportsFiles)
        {
            return helper.BeginRouteForm(route.Name, routeValues, FormMethod.Post, supportsFiles ? (object)new { id, enctype = "multipart/form-data" } : new { id });
        }

        public static MvcForm RenderRouteForm(this HtmlHelper helper, RouteReference route, string id, bool supportsFiles)
        {
            return helper.BeginRouteForm(route.Name, FormMethod.Post, supportsFiles ? (object)new { id, enctype = "multipart/form-data" } : new { id });
        }

        public static MvcForm RenderRouteForm(this HtmlHelper helper, RouteReference route, bool supportsFiles)
        {
            return helper.RenderRouteForm(route, "Form", supportsFiles);
        }

        public static MvcForm RenderRouteForm(this HtmlHelper helper, RouteReference route, object routeValues, string id)
        {
            return helper.RenderRouteForm(route, routeValues, id, false);
        }

        public static MvcForm RenderRouteForm(this HtmlHelper helper, RouteReference route, string id)
        {
            return helper.RenderRouteForm(route, id, false);
        }

        public static MvcForm RenderRouteForm(this HtmlHelper helper, RouteReference route)
        {
            return helper.RenderRouteForm(route, false);
        }
    }
}
