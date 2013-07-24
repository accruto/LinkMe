using System;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace LinkMe.Apps.Asp.Routing
{
    public class IntConstraint
        : IRouteConstraint
    {
        bool IRouteConstraint.Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(parameterName))
                return true;

            var value = values[parameterName];

            if (value is int || value is int?)
                return true;

            if (value is string)
            {
                int iValue;
                return int.TryParse((string) value, out iValue);
            }

            return false;
        }
    }

    public class EnumConstraint
        : IRouteConstraint
    {
        private readonly Type _type;
        private readonly string[] _values;

        public EnumConstraint(Type type)
        {
            _type = type;
            var values = Enum.GetValues(_type);
            _values = new string[values.Length];
            for (var index = 0; index < values.Length; ++ index)
                _values[index] = values.GetValue(index).ToString();
        }

        bool IRouteConstraint.Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(parameterName))
                return true;

            var value = values[parameterName];
            if (value == null)
                return true;

            if (value.GetType() == _type)
                return true;

            if (value is string)
                return _values.Any(v => string.Compare(v, (string)value, StringComparison.InvariantCultureIgnoreCase) == 0);

            return false;
        }
    }

    public class GuidConstraint
        : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(parameterName))
                return true;

            var value = values[parameterName];
            if (value is Guid)
                return true;

            if (value is string)
            {
                try
                {
                    new Guid((string) value);
                    return true;
                }
                catch (Exception)
                {
                }
            }

            return false;
        }
    }
}
