using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Public.Routes;

namespace LinkMe.Web.UI.Controls.Common.Navs
{
    public partial class CustodianHeader
        : LinkMeUserControl
    {
        protected static ReadOnlyUrl AboutUrl { get { return SupportRoutes.AboutUs.GenerateUrl(); } }
        protected static ReadOnlyUrl PrivacyUrl { get { return SupportRoutes.Privacy.GenerateUrl(); } }

        protected static ReadOnlyUrl HomeUrl { get { return Areas.Custodians.Routes.HomeRoutes.Home.GenerateUrl(); } }
        protected static ReadOnlyUrl LogOutUrl { get { return LoginsRoutes.LogOut.GenerateUrl(); } }

        protected static ReadOnlyUrl ChangePasswordUrl { get { return Areas.Accounts.Routes.AccountsRoutes.ChangePassword.GenerateUrl(); } }
    }
}