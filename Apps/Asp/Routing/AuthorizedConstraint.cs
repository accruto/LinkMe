using System.Web;
using System.Web.Routing;
using LinkMe.Apps.Asp.Security;

namespace LinkMe.Apps.Asp.Routing
{
    public class AuthorizedConstraint
        : IRouteConstraint
    {
        bool IRouteConstraint.Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return httpContext.HasRegisteredUserIdentity();
        }
    }
}
