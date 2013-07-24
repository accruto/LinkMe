using System.Globalization;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Notifications;
using LinkMe.Apps.Asp.Urls;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public class NotificationActionFilter
        : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var url = filterContext.RequestContext.HttpContext.GetClientUrl();
            var notification = filterContext.HttpContext.Session.GetNotification(url);

            // Add the notification to the model state.

            if (notification != null && filterContext.Controller is ViewController)
            {
                switch (notification.Type)
                {
                    case NotificationType.Error:
                        ((ViewController)filterContext.Controller).ModelState.AddModelError("ErrorNotification", notification.Message);
                        break;

                    case NotificationType.Information:
                        ((ViewController)filterContext.Controller).ModelState.AddModelInformation(notification.Message);
                        break;

                    case NotificationType.Confirmation:
                        ((ViewController)filterContext.Controller).ModelState.AddModelConfirmation(notification.Message);
                        break;
                }
            }
        }
    }
}
