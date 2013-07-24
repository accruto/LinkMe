using System.Web.Mvc;
using LinkMe.Web.Areas.Api.Routes;

namespace LinkMe.Web.Areas.Api
{
    public class ApiAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Api"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            LocationRoutes.RegisterRoutes(context);
            SearchRoutes.RegisterRoutes(context);
            CacheRoutes.RegisterRoutes(context);
            SeoRoutes.RegisterRoutes(context);
            DevRoutes.RegisterRoutes(context);
            ResumesRoutes.RegisterRoutes(context);
            CouponsRoutes.RegisterRoutes(context);
        }
    }
}