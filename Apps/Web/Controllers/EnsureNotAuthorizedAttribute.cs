using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;

namespace LinkMe.Web.Controllers
{
    public class EnsureNotAuthorizedAttribute
        : AuthorizedAttribute
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            // Check that the there is not current user.

            var user = filterContext.Controller.ViewData.GetCurrentRegisteredUser();
            if (user != null)
                filterContext.Result = new RedirectResult(filterContext.HttpContext.GetReturnUrl().ToString());
        }
    }
}