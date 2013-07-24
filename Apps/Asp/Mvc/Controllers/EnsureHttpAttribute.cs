using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Urls;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class EnsureHttpAttribute
        : FilterAttribute, IAuthorizationFilter
    {
        public EnsureHttpAttribute()
        {
            Order = 0;
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsSecureConnection)
                HandleNonHttpRequest(filterContext);
        }

        private static void HandleNonHttpRequest(AuthorizationContext filterContext)
        {
            // Only redirect for GET requests, otherwise the browser might not propagate the verb and request
            // body correctly.

            if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            // Redirect to HTTP version of page.

            var url = filterContext.HttpContext.GetClientUrl().AsNonReadOnly();
            url.Scheme = Uri.UriSchemeHttp;
            filterContext.Result = new RedirectToUrlResult(url);
        }
    }
}
