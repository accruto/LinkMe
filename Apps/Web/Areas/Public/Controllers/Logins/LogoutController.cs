using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Public.Controllers.Logins
{
    public class LogoutController
        : PublicLoginJoinController
    {
        public LogoutController(IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, IFaqsQuery faqsQuery)
            : base(accountsManager, loginCredentialsQuery, faqsQuery)
        {
        }

        [EnsureHttps]
        public ActionResult LogOut()
        {
            // Grab the current user type before logging out.

            var userType = GetCurrentUserType();

            // Log out.

            TryLogOut();

            new AnonymousUserContext(HttpContext).HasLoggedOut = true;
            return RedirectToReturnUrl(userType);
        }

        private UserType GetCurrentUserType()
        {
            var currentUser = CurrentRegisteredUser;
            return currentUser == null ? UserType.Member : currentUser.UserType;
        }
    }
}