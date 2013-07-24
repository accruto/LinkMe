using System;
using System.Net;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public class RedirectToUrlResult
        : ActionResult
    {
        private readonly ReadOnlyUrl _url;
        private readonly bool _permanently;

        public RedirectToUrlResult(ReadOnlyUrl url, bool permanently)
        {
            _url = url;
            _permanently = permanently;
        }

        public RedirectToUrlResult(ReadOnlyUrl url)
        {
            _url = url;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (_permanently)
            {
                // - Redirect with 301 instead of 302
                // - Use an absolute URI
                // - Ensure any tracking parameters are passed across
                // - End the response now

                var response = context.HttpContext.Response;
                response.StatusCode = (int) HttpStatusCode.MovedPermanently;
                response.AddHeader("Location", GetUrl(context).AbsoluteUri);
                response.End();
            }
            else
            {
                context.HttpContext.Response.Redirect(GetUrl(context).ToString());
            }
        }

        private ReadOnlyUrl GetUrl(ControllerContext context)
        {
            var url = _url.AsNonReadOnly();
            context.HttpContext.TransferTracking((name, value) => url.QueryString[name] = value);
            return url;
        }
    }
}
