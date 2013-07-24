using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Employers.Controllers.Enquiries;

namespace LinkMe.Web.Areas.Employers.Routes
{
    public static class EnquiriesRoutes
    {
        public static RouteReference Apply { get; private set; }
        public static RouteReference Confirm { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapAreaRoute<EnquiriesController>("employers/enquiries/", c => c.Home);
            Apply = context.MapAreaRoute<EnquiriesController>("employers/enquiries/apply", c => c.Apply);
            Confirm = context.MapAreaRoute<EnquiriesController>("employers/enquiries/confirm", c => c.Confirm);
        }
    }
}