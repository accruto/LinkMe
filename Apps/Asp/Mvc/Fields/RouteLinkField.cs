using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Apps.Asp.Routing;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public class RouteLinkField
        : Field, IHaveId
    {
        private readonly string _linkText;
        private readonly RouteReference _route;
        private readonly object _routeValues;
        private string _id;

        public RouteLinkField(HtmlHelper helper, string linkText, RouteReference route, object routeValues)
            : base(helper)
        {
            _linkText = linkText;
            _route = route;
            _routeValues = routeValues;
        }

        public RouteLinkField(HtmlHelper helper, string linkText, RouteReference route)
            : this(helper, linkText, route, null)
        {
        }

        public override MvcHtmlString InnerHtml
        {
            get
            {
                return string.IsNullOrEmpty(_id)
                    ? Html.RouteRefLink(_linkText, _route, _routeValues)
                    : Html.RouteRefLink(_linkText, _route, _routeValues, new { id = _id });
            }
        }

        string IHaveId.Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }

    public static class RouteLinkFieldExtensions
    {
        public static RouteLinkField RouteLinkField(this HtmlHelper htmlHelper, string linkText, RouteReference route, object routeValues)
        {
            return new RouteLinkField(htmlHelper, linkText, route, routeValues);
        }

        public static RouteLinkField RouteLinkField(this HtmlHelper htmlHelper, string linkText, RouteReference route)
        {
            return new RouteLinkField(htmlHelper, linkText, route);
        }
    }
}
