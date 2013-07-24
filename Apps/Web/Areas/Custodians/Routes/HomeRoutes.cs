using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Custodians.Controllers;

namespace LinkMe.Web.Areas.Custodians.Routes
{
    public static class HomeRoutes
    {
        public static RouteReference Home { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Home = context.MapAreaRoute<HomeController>("custodians/home", c => c.Home);
            context.MapRedirectRoute("communities/administrators/home.aspx", Home);
        }
    }
}