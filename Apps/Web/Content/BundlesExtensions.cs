using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Environment;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Content
{
    public static class BundlesExtensions
    {
        private static readonly Version Version = StaticEnvironment.GetFileVersion(Assembly.GetExecutingAssembly());

        private class BundleVersionConstraint
            : IRouteConstraint
        {
            private readonly Version _version;

            public BundleVersionConstraint(Version version)
            {
                _version = version;
            }

            bool IRouteConstraint.Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                return values.ContainsKey("version") && !Equals(values["version"], _version.ToString());
            }
        }

        private class RedirectBundleUrlHandler
            : IRouteHandler
        {
            private readonly Version _version;

            public RedirectBundleUrlHandler(Version version)
            {
                _version = version;
            }

            public IHttpHandler GetHttpHandler(RequestContext requestContext)
            {
                return new RedirectUrlHttpHandler(GetApplicationUrl(requestContext.HttpContext.Request.RawUrl), true);
            }

            private ReadOnlyApplicationUrl GetApplicationUrl(string redirectUrl)
            {
                // Need to replace the old version with the current version.

                var startPos = redirectUrl.IndexOf("bundle/") + "bundle/".Length;
                if (startPos == -1)
                    return new ReadOnlyApplicationUrl(redirectUrl);
                var endPos = redirectUrl.IndexOf("/", startPos + 1, StringComparison.InvariantCultureIgnoreCase);
                if (endPos == -1)
                    return new ReadOnlyApplicationUrl(redirectUrl);

                redirectUrl = redirectUrl.Substring(0, startPos) + _version + redirectUrl.Substring(endPos);
                return new ReadOnlyApplicationUrl(redirectUrl);
            }
        }

        public static void MapBundleVersions(this AreaRegistrationContext context)
        {
            // Between releases the versions in the bundle urls change so need to map older versions to the current version.

            const string url = "bundle/{version}/{*catchall}";
            var constraints = new RouteValueDictionary {{"bundle", new BundleVersionConstraint(Version)}};

            var route = new Route(url, new RedirectBundleUrlHandler(Version))
            {
                Constraints = constraints
            };
            context.Routes.Add(route);
        }
    }
}