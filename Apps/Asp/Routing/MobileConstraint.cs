using System.Web;
using System.Web.Routing;
using System.Web.WebPages;

namespace LinkMe.Apps.Asp.Routing
{
    public class MobileConstraint
        : IRouteConstraint
    {
        bool IRouteConstraint.Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return httpContext.GetOverriddenBrowser().IsMobileDevice;
        }
    }
}
