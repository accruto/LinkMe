using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Routing
{
    public class RedirectQueryString
    {
        private readonly string _key;
        private readonly bool _isRequired;

        public RedirectQueryString(string key, bool isRequired)
        {
            _key = key;
            _isRequired = isRequired;
        }

        public RedirectQueryString(bool isRequired)
            : this(null, isRequired)
        {
        }

        public RedirectQueryString()
            : this(null, true)
        {
        }

        public string Key
        {
            get { return _key; }
        }

        public bool IsRequired
        {
            get { return _isRequired; }
        }
    }

    public class RedirectRouteValue
    {
        private readonly string _key;

        public RedirectRouteValue(string key)
        {
            _key = key;
        }

        public RedirectRouteValue()
        {
        }

        public string Key
        {
            get { return _key; }
        }
    }

    public class RedirectRouteHandler
        : IRouteHandler
    {
        private readonly RouteReference _redirectRoute;
        private readonly RouteValueDictionary _routeValues;
        private readonly bool _permanently;

        public RedirectRouteHandler(RouteReference redirectRoute, RouteValueDictionary routeValues, bool permanently)
        {
            _redirectRoute = redirectRoute;
            _routeValues = routeValues;
            _permanently = permanently;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            // Construct route values.

            var queryString = new QueryString(requestContext.HttpContext.Request.QueryString);
            var routeValues = GetRouteValues(requestContext.RouteData.Values, queryString);

            // Construct the url and redirect.

            var url = _redirectRoute.GenerateUrl(routeValues).AsNonReadOnly();
            url.QueryString.Add(queryString);
            return new RedirectUrlHttpHandler(url, _permanently);
        }

        private RouteValueDictionary GetRouteValues(IDictionary<string, object> requestRouteValues, QueryString requestQueryString)
        {
            if (_routeValues == null)
                return null;

            // Check first whether anything has to be done.

            if (!_routeValues.Values.Any(v => v is RedirectQueryString) && !_routeValues.Values.Any(v => v is RedirectRouteValue))
                return _routeValues;

            // Create a new collection.

            var routeValues = new RouteValueDictionary();
            foreach (var key in _routeValues.Keys)
            {
                var value = _routeValues[key];
                var redirectQueryString = value as RedirectQueryString;
                if (redirectQueryString != null)
                {
                    var requestKey = redirectQueryString.Key ?? key;
                    var requestValue = requestQueryString[requestKey];

                    // Add the extracted value into route values and remove it from the query string.

                    routeValues[key] = requestValue;
                    requestQueryString.Remove(requestKey);
                }
                else
                {
                    var redirectRouteValue = value as RedirectRouteValue;
                    if (redirectRouteValue != null)
                    {
                        var requestKey = redirectRouteValue.Key ?? key;
                        var requestValue = requestRouteValues[requestKey];

                        // Add the extracted value into route values.

                        routeValues[key] = requestValue;
                    }
                    else
                    {
                        // Transfer the raw value.

                        routeValues[key] = value;
                    }
                }
            }

            return routeValues;
        }
    }
}
