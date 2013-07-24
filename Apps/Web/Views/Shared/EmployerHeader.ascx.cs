using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Areas.Public.Routes;
using HomeRoutes=LinkMe.Web.Areas.Employers.Routes.HomeRoutes;

namespace LinkMe.Web.Views.Shared
{
    public class EmployerHeader
        : ViewUserControl
    {
        protected static ReadOnlyUrl HomeUrl { get { return SearchRoutes.Search.GenerateUrl(); } }
        protected static ReadOnlyUrl AnonymousHomeUrl { get { return HomeRoutes.Home.GenerateUrl(); } }
        protected static ReadOnlyUrl MemberHomeUrl { get { return Areas.Public.Routes.HomeRoutes.Home.GenerateUrl(new { ignorePreferred = true }); } }
        
        protected static ReadOnlyUrl AccountUrl { get { return SettingsRoutes.Settings.GenerateUrl(); } }

        protected static ReadOnlyUrl LogOutUrl { get { return LoginsRoutes.LogOut.GenerateUrl(); } }
        protected static ReadOnlyUrl JoinUrl { get { return AccountsRoutes.Join.GenerateUrl(); } }
    }
}