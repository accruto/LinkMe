using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Areas.Public.Routes;

namespace LinkMe.Web.Areas.Shared.Routes
{
    public static class LoginsRoutesExtensions
    {
        public static MvcHtmlString LoginLink(this HtmlHelper htmlHelper, string linkText, HttpContext context, object htmlAttributes)
        {
            return htmlHelper.RouteRefLink(linkText, LoginsRoutes.LogIn, new { returnUrl = context.GetClientUrl().PathAndQuery }, htmlAttributes);
        }

        public static MvcHtmlString LoginLink(this HtmlHelper htmlHelper, string linkText, RouteReference returnRoute, object htmlAttributes)
        {
            return htmlHelper.RouteRefLink(linkText, LoginsRoutes.LogIn, new { returnUrl = htmlHelper.RouteRefUrl(returnRoute).PathAndQuery }, htmlAttributes);
        }

        public static MvcHtmlString LoginLink(this HtmlHelper htmlHelper, string linkText, RouteReference returnRoute)
        {
            return htmlHelper.RouteRefLink(linkText, LoginsRoutes.LogIn, new { returnUrl = htmlHelper.RouteRefUrl(returnRoute).PathAndQuery });
        }

        public static MvcHtmlString EmployerLoginLink(this HtmlHelper htmlHelper, string linkText, HttpContext context, object htmlAttributes)
        {
            return htmlHelper.RouteRefLink(linkText, AccountsRoutes.LogIn, new { returnUrl = context.GetClientUrl().PathAndQuery }, htmlAttributes);
        }
    }
}