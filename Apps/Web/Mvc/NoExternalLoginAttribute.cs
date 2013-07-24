using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Web.Areas.Members.Routes;
using Microsoft.Practices.Unity;

namespace LinkMe.Web.Mvc
{
    public class NoExternalLoginAttribute
        : ActionFilterAttribute
    {
        [Dependency]
        public IVerticalsQuery VerticalsQuery { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var activityContext = filterContext.Controller.ViewData.GetActivityContext();
            var currentUser = filterContext.Controller.ViewData.GetCurrentRegisteredUser();
            if (activityContext == null || (currentUser is Member && !VerticalsQuery.CanEditContactDetails(activityContext.Vertical.Id)))
                filterContext.Result = new RedirectToRouteResult(SettingsRoutes.Settings.Name, null);
        }
    }
}