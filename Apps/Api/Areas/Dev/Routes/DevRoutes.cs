using System.Web.Mvc;
using LinkMe.Apps.Api.Areas.Dev.Controllers;
using LinkMe.Apps.Asp.Routing;

namespace LinkMe.Apps.Api.Areas.Dev.Routes
{
    public class DevRoutes
    {
        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapAreaRoute<DevApiController>(1, "dev/error", c => c.Error);
        }
    }
}
