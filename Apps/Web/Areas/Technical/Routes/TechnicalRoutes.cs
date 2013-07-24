using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Content;

namespace LinkMe.Web.Areas.Technical.Routes
{
    public class TechnicalRoutes
    {
        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapBundleVersions();

            // Old combres.

            context.MapRedirectRoute("combres.axd/{*catchall}", HomeRoutes.Home);
        }
    }
}