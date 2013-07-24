using System;
using System.Text;
using System.Web;
using LinkMe.Apps.Asp.Urls;

namespace LinkMe.Web.Areas.Errors.Models.Errors
{
    public static class ErrorReportExtensions
    {
        public static ErrorReport GenerateErrorReport(this HttpContext context, Exception exception)
        {
            return new ErrorReport
            {
                Exception = exception,
                RequestUrl = GetRequestUrl(context),
                ReferrerUrl = GetReferrerUrl(context),
                UserAgent = GetUserAgent(context),
                UserHostAddress = GetUserHostAddress(context),
                RequestType = GetRequestType(context),
                IsAuthenticated = GetIsAuthenticated(context),
                RequestCookies = GetRequestCookies(context),
                ResponseCookies = GetResponseCookies(context),

            };
        }

        private static string GetRequestUrl(HttpContext context)
        {
            return context.GetClientUrl().ToString();
        }

        private static string GetReferrerUrl(HttpContext context)
        {
            return context == null
                ? null
                : context.Request.Headers["Referer"];
        }

        private static string GetUserAgent(HttpContext context)
        {
            return context == null
                ? null
                : context.Request.UserAgent;
        }

        private static string GetUserHostAddress(HttpContext context)
        {
            return context == null
                ? null
                : context.Request.UserHostAddress;
        }

        private static string GetRequestType(HttpContext context)
        {
            return context == null
                ? null
                : context.Request.RequestType;
        }

        private static bool GetIsAuthenticated(HttpContext context)
        {
            if (context == null || context.User == null)
                return false;
            return context.User.Identity.IsAuthenticated;
        }

        private static string GetRequestCookies(HttpContext context)
        {
            return context == null
                ? null
                : GetCookies(context.Request.Cookies);
        }

        private static string GetResponseCookies(HttpContext context)
        {
            return context == null
                ? null
                : GetCookies(context.Response.Cookies);
        }

        private static string GetCookies(HttpCookieCollection cookies)
        {
            if (cookies.Count == 0)
                return null;

            var sb = new StringBuilder();
            foreach (var key in cookies.AllKeys)
            {
                var cookie = cookies[key];
                if (cookie != null)
                {
                    sb.AppendFormat("{0} = \"{1}\"", cookie.Name, cookie.Value);
                    if (cookie.Expires != DateTime.MinValue)
                        sb.AppendFormat(" (expires {0:dd/MM/yyyy hh:mm:ss})", cookie.Expires);
                    sb.AppendLine();
                }
            }

            sb.Remove(sb.Length - System.Environment.NewLine.Length, System.Environment.NewLine.Length);
            return sb.ToString();
        }
    }
}
