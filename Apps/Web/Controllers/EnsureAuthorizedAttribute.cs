using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Notifications;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Queries;
using LinkMe.Web.Areas.Accounts.Routes;
using Microsoft.Practices.Unity;

namespace LinkMe.Web.Controllers
{
    public class EnsureAuthorizedAttribute
        : AuthorizedAttribute
    {
        // This class is used instead of the System.Web.Mvc.AuthorizeAttribute because there
        // are multiple roles and different login pages for those roles.  Also, not going to worry
        // about caching of pages etc at the moment because all secure pages have caching turned off.

        private static readonly UserType[] AllUserTypes = new[] { UserType.Administrator, UserType.Member, UserType.Employer, UserType.Custodian };
        private UserType[] _userTypes = AllUserTypes;

        public EnsureAuthorizedAttribute(UserType userType)
        {
            _userTypes = AllUserTypes.Contains(userType) ? new[] {userType} : AllUserTypes;
        }

        public EnsureAuthorizedAttribute()
        {
            _userTypes = AllUserTypes;
        }

        public UserType[] UserTypes
        {
            get { return _userTypes; }
            set { _userTypes = (value == null || value.Length == 0) ? AllUserTypes : value; }
        }

        public string Reason { get; set; }
        public bool RequiresActivation { get; set; }

        [Dependency]
        public IAuthenticationManager AuthenticationManager { get; set; }
        [Dependency]
        public ILoginCredentialsQuery LoginCredentialsQuery { get; set; }
        [Dependency]
        public IUsersQuery UsersQuery { get; set; }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            // Check that the current user matches the role.

            var user = filterContext.Controller.ViewData.GetCurrentRegisteredUser();
            if (user == null)
            {
                filterContext.Result = GetRedirectResult(filterContext);
            }
            else
            {
                // If e.g. an employer is trying to access a member page, then redirect them home.

                if (!_userTypes.Contains(user.UserType))
                {
                    filterContext.Result = new RedirectResult(filterContext.HttpContext.GetHomeUrl().ToString());
                }
                else
                {
                    // If they need to be activated then redirect.

                    var result = CheckActivation(filterContext, user);
                    if (result != null)
                        filterContext.Result = result;

                    // If the user needs to change their password redirect them now.

                    result = CheckMustChangePassword(filterContext, user.Id);
                    if (result != null)
                        filterContext.Result = result;
                }
            }
        }

        private ActionResult CheckActivation(ControllerContext filterContext, IUserAccount user)
        {
            if (RequiresActivation && user.UserType == UserType.Member && !user.IsActivated)
            {
                // Check whether the status needs to be updated.

                if (!filterContext.HttpContext.User.NeedsReset())
                    return GetRedirectToNotActivatedResult(filterContext);

                // Reset.

                var updateUser = UsersQuery.GetUser(user.Id);
                AuthenticationManager.UpdateUser(filterContext.HttpContext, updateUser, false);

                // Retry.

                if (!updateUser.IsActivated)
                    return GetRedirectToNotActivatedResult(filterContext);
            }

            return null;
        }

        private ActionResult CheckMustChangePassword(ControllerContext filterContext, Guid userId)
        {
            // Only check on a home page.  Normally when a user logs in they are redirected.
            // This is here in case they log in through the API and then access a page.
            // Should really check on every page but don't want to access the database every time.

            if (!filterContext.HttpContext.IsHomeUrl())
                return null;

            var credentials = LoginCredentialsQuery.GetCredentials(userId);
            if (credentials != null && credentials.MustChangePassword)
                return new RedirectToRouteResult(AccountsRoutes.MustChangePassword.Name, new RouteValueDictionary());
            return null;
        }

        private RedirectResult GetRedirectResult(ControllerContext filterContext)
        {
            // Redirect to the appropriate log in page for the first role.

            var userType = _userTypes.Length == 0 ? UserType.Member : _userTypes[0];

            return userType == UserType.Employer
                ? new RedirectResult(filterContext.HttpContext.GetEmployerLoginUrl(Reason).ToString())
                : new RedirectResult(filterContext.HttpContext.GetLoginUrl(Reason).ToString());
        }

        private static ActionResult GetRedirectToNotActivatedResult(ControllerContext filterContext)
        {
            return new RedirectToRouteResult(
                AccountsRoutes.NotActivated.Name,
                new RouteValueDictionary(new Dictionary<string, object>
                {
                    {Apps.Asp.Constants.ReturnUrlParameter, filterContext.HttpContext.Request.Url.AbsoluteUri},
                    {NotificationsExtensions.NotificationIdParameter, filterContext.HttpContext.Request.QueryString[NotificationsExtensions.NotificationIdParameter]}
                }));
        }
    }
}