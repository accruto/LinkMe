using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Areas.Public.Controllers.Home;

namespace LinkMe.Web.Areas.Public.Routes
{
    public static class HomeRoutes
    {
        public static RouteReference Home { get; private set; }
        public static RouteReference GuestsProfile { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Home = context.MapAreaRoute<HomeController, bool?>("", c => c.Home);
            context.MapAreaRoute<HomeController, string>("partners/{pcode}", c => c.Partners);

            context.MapRedirectRoute("guests/profile", ProfilesRoutes.Profile, new AuthorizedConstraint());
            GuestsProfile = context.MapAreaRoute<GuestsController>("guests/profile", c => c.Profile);
            context.MapRedirectRoute("guests/profile.aspx", GuestsProfile);
        }
    }
}