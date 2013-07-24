using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Administrators.Controllers.Home;

namespace LinkMe.Web.Areas.Administrators.Routes
{
    public static class HomeRoutes
    {
        public static RouteReference Home { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Home = context.MapAreaRoute<HomeController>("administrators/home", c => c.Home);
        }
    }
}