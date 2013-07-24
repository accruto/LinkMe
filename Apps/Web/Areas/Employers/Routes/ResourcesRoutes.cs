using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Employers.Controllers.Resources;

namespace LinkMe.Web.Areas.Employers.Routes
{
    public class ResourcesRoutes
    {
        public static RouteReference Resources { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Resources = context.MapAreaRoute<ResourcesController>("employers/resources", c => c.Index);
        }
    }
}
