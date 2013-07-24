using System.Linq;
using System.Web;
using System.Web.Routing;

namespace LinkMe.Apps.Asp.Routing
{
    public class QueryStringConstraint
        : IRouteConstraint
    {
        private readonly string _key;

        public QueryStringConstraint(string key)
        {
            _key = key;
        }

        bool IRouteConstraint.Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return httpContext.Request.QueryString.AllKeys.Contains(_key);
        }
    }
}
