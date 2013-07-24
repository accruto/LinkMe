using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Routing
{
    public class RouteReference
    {
        private readonly string _name;
        private readonly bool? _ensureHttps;

        public RouteReference(string name, bool? ensureHttps)
        {
            _name = name;
            _ensureHttps = ensureHttps;
        }

        public string Name
        {
            get { return _name; }
        }

        public bool? EnsureHttps
        {
            get { return _ensureHttps; }
        }
    }

    public class AreaRoute
        : Route
    {
        private readonly RouteReference _reference;

        public AreaRoute(string name, bool? ensureHttps, string url, IRouteHandler routeHandler)
            : base(GetUrl(url), routeHandler)
        {
            _reference = new RouteReference(name, ensureHttps);
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            var data = base.GetVirtualPath(requestContext, values);

            // Need to remove the default.aspx if present.

            if (data != null && data.VirtualPath != null)
                data.VirtualPath = data.VirtualPath.Replace("/default.aspx", "");

            return data;
        }

        public RouteReference Reference
        {
            get { return _reference; }
        }

        private static string GetUrl(string url)
        {
            // This is for IIS6 which we are still stuck on, and depends on ISAPI_Rewrite etc.

            return url.EndsWith(".aspx")
                ? url
                : url.AddUrlSegments("default.aspx");
        }
    }

    public static class AreaRouteExtensions
    {
        public static ReadOnlyApplicationUrl GenerateUrl(this RouteReference route)
        {
            return route.GenerateUrl(new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()), RouteTable.Routes, null);
        }

        public static ReadOnlyApplicationUrl GenerateUrl(this RouteReference route, object routeValues)
        {
            return route.GenerateUrl(new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()), RouteTable.Routes, new RouteValueDictionary(routeValues));
        }

        public static ReadOnlyApplicationUrl GenerateUrl(this RouteReference route, RouteValueDictionary routeValues)
        {
            return route.GenerateUrl(new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()), RouteTable.Routes, routeValues);
        }

        private static ReadOnlyApplicationUrl GenerateUrl(this RouteReference route, RequestContext requestContext, RouteCollection routeCollection, RouteValueDictionary routeValues)
        {
            var url = UrlHelper.GenerateUrl(route.Name, null, null, routeValues, routeCollection, requestContext, false);
            return new ReadOnlyApplicationUrl(route.EnsureHttps, RemoveDefaultAspx(url));
        }

        private static string RemoveDefaultAspx(string url)
        {
            return url.Replace("default.aspx", "");
        }
    }
}
