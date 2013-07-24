using System.Web;
using System.Web.Routing;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Routing
{
    public class RedirectUrlHttpHandler
        : IHttpHandler
    {
        private readonly ReadOnlyUrl _redirectUrl;
        private readonly bool _permanently;

        public RedirectUrlHttpHandler(ReadOnlyUrl redirectUrl, bool permanently)
        {
            _redirectUrl = redirectUrl;
            _permanently = permanently;
        }

        public void ProcessRequest(HttpContext context)
        {
            if (_permanently)
            {
                context.Response.Status = "301 Moved Permanently";
                context.Response.StatusCode = 301;
                context.Response.AddHeader("Location", _redirectUrl.AbsoluteUri);
            }
            else
            {
                context.Response.Redirect(_redirectUrl.ToString(), false);
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }

    public class RedirectUrlHandler
        : IRouteHandler
    {
        private readonly string _redirectUrl;
        private readonly bool _permanently;

        public RedirectUrlHandler(string redirectUrl, bool permanently)
        {
            _redirectUrl = redirectUrl;
            _permanently = permanently;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new RedirectUrlHttpHandler(GetApplicationUrl(_redirectUrl), _permanently);
        }

        private static ReadOnlyApplicationUrl GetApplicationUrl(string redirectUrl)
        {
            if (!redirectUrl.StartsWith("~/") && !redirectUrl.StartsWith("/"))
                return new ReadOnlyApplicationUrl("~/" + redirectUrl);
            return new ReadOnlyApplicationUrl(redirectUrl);
        }
    }
}
