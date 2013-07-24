using System;
using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using Microsoft.Practices.Unity;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public abstract class Controller
        : System.Web.Mvc.Controller
    {
        [Dependency]
        public IAuthenticationManager AuthenticationManager { get; set; }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var viewData = filterContext.Controller.ViewData;
            viewData.SetClientUrl(filterContext.HttpContext);
            viewData.SetActivityContext();
            SetCurrentUser(viewData);
        }

        protected ActivityContext ActivityContext
        {
            get { return ViewData.GetActivityContext(); }
        }

        protected RegisteredUser CurrentRegisteredUser
        {
            get { return ViewData.GetCurrentRegisteredUser(); }
        }

        protected Member CurrentMember
        {
            get { return CurrentRegisteredUser as Member; }
        }

        protected Employer CurrentEmployer
        {
            get { return CurrentRegisteredUser as Employer; }
        }

        protected Administrator CurrentAdministrator
        {
            get { return CurrentRegisteredUser as Administrator; }
        }

        protected AnonymousUser CurrentAnonymousUser
        {
            get { return ViewData.GetCurrentAnonymousUser(); }
        }

        protected IUser CurrentUser
        {
            get { return (IUser)CurrentRegisteredUser ?? CurrentAnonymousUser; }
        }

        protected RedirectToRouteResult RedirectToRoute(RouteReference route)
        {
            return RedirectToRoute(route.Name, GetRouteValues(new RouteValueDictionary()));
        }

        protected RedirectToRouteResult RedirectToRoute(RouteReference route, RouteValueDictionary routeValues)
        {
            return RedirectToRoute(route.Name, GetRouteValues(routeValues));
        }

        protected RedirectToRouteResult RedirectToRoute(RouteReference route, object routeValues)
        {
            return RedirectToRoute(route.Name, GetRouteValues(new RouteValueDictionary(routeValues)));
        }

        protected ActionResult EnsureUrl(ReadOnlyUrl url)
        {
            // Ensure redirect is permanent if needed.

            return string.Equals(url.Path, ViewData.GetClientUrl().Path, StringComparison.InvariantCultureIgnoreCase)
                ? null
                : new RedirectToUrlResult(url, true);
        }

        private RouteValueDictionary GetRouteValues(RouteValueDictionary routeValues)
        {
            HttpContext.TransferTracking((name, value) => routeValues[name] = value);
            return routeValues;
        }

        private void SetCurrentUser(ViewDataDictionary viewData)
        {
            var registeredUser = AuthenticationManager.GetUser(HttpContext);
            if (registeredUser != null)
            {
                viewData.SetCurrentRegisteredUser(registeredUser);
            }
            else
            {
                var anonymousUser = GetAnonymousUser();
                if (anonymousUser != null)
                    viewData.SetCurrentAnonymousUser(anonymousUser);
            }
        }

        private AnonymousUser GetAnonymousUser()
        {
            if (string.IsNullOrEmpty(Request.AnonymousID))
                return null;

            var anonymousId = GetAnonymousId(Request.AnonymousID);
            return anonymousId == null ? null : new AnonymousUser { Id = anonymousId.Value };
        }

        private static Guid? GetAnonymousId(string anonymousId)
        {
            try
            {
                return new Guid(anonymousId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
