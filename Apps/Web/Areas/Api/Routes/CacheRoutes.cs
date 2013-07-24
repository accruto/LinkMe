using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Api.Controllers;

namespace LinkMe.Web.Areas.Api.Routes
{
    public class CacheRoutes
    {
        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapAreaRoute<CacheApiController>("api/cache/clear", c => c.Clear);
        }
    }
}