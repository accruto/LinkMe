using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Areas.Public.Routes;

namespace LinkMe.Web.Views.Shared
{
    public class MemberHeader
        : ViewUserControl
    {
        protected static ReadOnlyUrl HomeUrl { get { return ProfilesRoutes.Profile.GenerateUrl(); } }
        protected static ReadOnlyUrl AnonymousHomeUrl { get { return Areas.Public.Routes.HomeRoutes.Home.GenerateUrl(); } }
        protected static ReadOnlyUrl EmployerHomeUrl { get { return Areas.Employers.Routes.HomeRoutes.Home.GenerateUrl(new { ignorePreferred = true }); } }

        protected static ReadOnlyUrl AccountUrl { get { return SettingsRoutes.Settings.GenerateUrl(); } }
        protected static ReadOnlyUrl LogOutUrl { get { return LoginsRoutes.LogOut.GenerateUrl(); } }
        protected static ReadOnlyUrl JoinUrl { get { return JoinRoutes.Join.GenerateUrl(); } }
    }
}
