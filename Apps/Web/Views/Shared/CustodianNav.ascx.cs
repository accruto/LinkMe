using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Views.Shared
{
    public class CustodianNav
        : ViewUserControl
    {
        protected static ReadOnlyUrl ChangePasswordUrl { get { return Areas.Accounts.Routes.AccountsRoutes.ChangePassword.GenerateUrl(); } }
    }
}