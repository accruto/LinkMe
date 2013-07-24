using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Routes;

namespace LinkMe.Web.Areas.Employers.Views.Shared
{
    public class LinkedInAuth
        : ViewUserControl
    {
        protected ReadOnlyUrl GetAuthenticatedUrl()
        {
            return CurrentRegisteredUser == null
                ? Context.GetReturnUrl(UserType.Employer)
                : Context.GetClientUrl();
        }

        protected ReadOnlyUrl GetAccountUrl()
        {
            if (CurrentRegisteredUser == null)
            {
                // Look for return url.

                var returnUrl = Context.Request.GetReturnUrl();
                return returnUrl == null
                    ? LinkedInRoutes.Account.GenerateUrl()
                    : LinkedInRoutes.Account.GenerateUrl(new {returnUrl});
            }

            return Context.GetClientUrl();
        }
    }
}