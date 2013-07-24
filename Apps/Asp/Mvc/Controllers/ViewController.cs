using System.Web.Mvc;
using System.Web.Routing;
using Ionic.Zip;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Mvc.Models.Binders;
using LinkMe.Apps.Asp.Mvc.Results;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Notifications;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    [NotificationActionFilter]
    [CompressActionFilter(Order = 5)]
    public abstract class ViewController
        : Controller
    {
        protected ActionResult RedirectToUrl(ReadOnlyUrl url)
        {
            return new RedirectToUrlResult(url);
        }

        protected ActionResult RedirectToUrl(ReadOnlyUrl url, bool permanently)
        {
            return new RedirectToUrlResult(url, permanently);
        }

        protected ActionResult RedirectToRouteWithConfirmation(RouteReference route, object routeValues, string message)
        {
            var id = Session.SetNotification(NotificationType.Confirmation, message);

            // Add in the notification id.

            var dictionary = new RouteValueDictionary(routeValues) {{NotificationsExtensions.NotificationIdParameter, id.ToString("n")}};
            return RedirectToRoute(route, dictionary);
        }

        protected ActionResult RedirectToUrlWithConfirmation(ReadOnlyUrl url, string message)
        {
            return RedirectToUrlWithNotification(url, NotificationType.Confirmation, message);
        }

        protected ActionResult RedirectToUrlWithError(ReadOnlyUrl url, string message)
        {
            return RedirectToUrlWithNotification(url, NotificationType.Error, message);
        }

        private ActionResult RedirectToUrlWithNotification(ReadOnlyUrl url, NotificationType type, string message)
        {
            var id = Session.SetNotification(type, message);

            // Add in the notification id.

            var newUrl = url.AsNonReadOnly();
            newUrl.QueryString[NotificationsExtensions.NotificationIdParameter] = id.ToString("n");
            return RedirectToUrl(newUrl);
        }

        protected ZipFileResult ZipFile(ZipFile zipFile)
        {
            return new ZipFileResult(zipFile);
        }

        protected DocFileResult DocFile(DocFile docFile, string mediaType)
        {
            return new DocFileResult(docFile, mediaType);
        }

        protected DocFileResult DocFile(DocFile docFile)
        {
            return new DocFileResult(docFile);
        }

        protected ActionResult Redirect<T>()
        {
            return Redirect(NavigationManager.GetUrlForPage<T>().ToString());
        }

        protected bool IsChecked(string modelName)
        {
            var value = new CheckBoxBinder().BindModel(ControllerContext, new ModelBindingContext { FallbackToEmptyPrefix = true, ValueProvider = ValueProvider, ModelName = modelName }) as CheckBoxValue;
            return value != null && value.IsChecked;
        }

        protected TEnum GetValue<TEnum>(string modelName)
            where TEnum : struct
        {
            return (TEnum)new EnumBinder<TEnum>().BindModel(ControllerContext, new ModelBindingContext { FallbackToEmptyPrefix = true, ValueProvider = ValueProvider, ModelName = modelName });
        }
    }
}
