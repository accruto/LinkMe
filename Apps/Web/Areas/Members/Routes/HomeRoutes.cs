using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Members.Controllers.Home;

namespace LinkMe.Web.Areas.Members.Routes
{
    public static class HomeRoutes
    {
        public static RouteReference Home { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Home = context.MapAreaRoute<HomeMobileController>("members/home", c => c.Home);
            context.MapRedirectRoute("members/home", ProfilesRoutes.Profile);
            context.MapRedirectRoute("ui/registered/networkers/NetworkerHome.aspx", ProfilesRoutes.Profile);
        }
    }
}