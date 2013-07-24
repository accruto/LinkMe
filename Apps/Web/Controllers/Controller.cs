using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Apps.Pageflows;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Errors.Controllers;

namespace LinkMe.Web.Controllers
{
    [NoCache]
    public abstract class WebViewController
        : ViewController
    {
        protected ActionResult RedirectToReturnUrl()
        {
            return RedirectToUrl(HttpContext.GetReturnUrl());
        }

        protected ActionResult RedirectToReturnUrl(UserType userType)
        {
            return RedirectToUrl(HttpContext.GetReturnUrl(userType));
        }

        protected ActionResult RedirectToReturnUrl(ReadOnlyUrl defaultUrl)
        {
            return RedirectToUrl(HttpContext.GetReturnUrl(defaultUrl));
        }

        protected ActionResult RedirectToHomeUrl()
        {
            return RedirectToUrl(HttpContext.GetHomeUrl());
        }

        protected ActionResult RedirectToLoginUrl()
        {
            return RedirectToUrl(HttpContext.GetLoginUrl());
        }

        protected ActionResult RedirectToReturnUrlWithConfirmation(string message)
        {
            return RedirectToUrlWithConfirmation(HttpContext.GetReturnUrl(), message);
        }

        protected ActionResult NotFound(string type, string propertyName, object propertyValue)
        {
            HttpContext.ExecuteController<ErrorsController, string, string, string>(c => c.ObjectNotFound, type, propertyName, propertyValue == null ? null : propertyValue.ToString());
            return new EmptyResult();
        }

        protected ActionResult NotFound(string type)
        {
            HttpContext.ExecuteController<ErrorsController, string, string, string>(c => c.ObjectNotFound, type, null, null);
            return new EmptyResult();
        }

        protected override void HandleUnknownAction(string actionName)
        {
            if (GetType() != typeof(ErrorsController))
            {
                string requestedUrl = null;
                var clientUrl = HttpContext.GetClientUrl();
                if (clientUrl != null)
                    requestedUrl = clientUrl.PathAndQuery;
                var referrerUrl = Request.Headers["Referer"];
                HttpContext.ExecuteController<ErrorsController, string, string>(c => c.NotFound, requestedUrl, referrerUrl);
            }
        }
    }

    [NoCache]
    public abstract class PageflowController<TPageflow>
        : Apps.Asp.Mvc.Controllers.PageflowController<TPageflow>
        where TPageflow : Pageflow
    {
        protected PageflowController(PageflowRoutes routes, IPageflowEngine pageflowEngine)
            : base(routes, pageflowEngine)
        {
        }

        protected ActionResult RedirectToReturnUrl(ReadOnlyUrl defaultUrl)
        {
            return new RedirectToUrlResult(HttpContext.GetReturnUrl(defaultUrl));
        }

        protected ActionResult NotFound(string type)
        {
            HttpContext.ExecuteController<ErrorsController, string, string, string>(c => c.ObjectNotFound, type, null, null);
            return new EmptyResult();
        }

        protected ActionResult NotFound(string type, string propertyName, object propertyValue)
        {
            HttpContext.ExecuteController<ErrorsController, string, string, string>(c => c.ObjectNotFound, type, propertyName, propertyValue == null ? null : propertyValue.ToString());
            return new EmptyResult();
        }
    }
}
