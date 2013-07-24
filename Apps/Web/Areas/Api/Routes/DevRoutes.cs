using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Api.Controllers;

namespace LinkMe.Web.Areas.Api.Routes
{
    public class DevRoutes
    {
        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapAreaRoute<DevApiController>("api/dev/error", c => c.Error);
            context.MapAreaRoute<DevApiController>("api/dev/anonymousid", c => c.AnonymousId);
        }
    }
}
