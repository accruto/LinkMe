using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Landing.Controllers;

namespace LinkMe.Web.Areas.Landing.Routes
{
    public class JobAdsRoutes
    {
        public static RouteReference Search { get; private set; }
        public static RouteReference Sample { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Search = context.MapAreaRoute<JobAdsController>("landing/search/jobs", c => c.Search);
            Sample = context.MapAreaRoute<JobAdsController>("landing/search/jobs/sample", c => c.Sample);

            context.MapRedirectRoute("landing/search/jobs/SimpleSearch.aspx", Search);
            context.MapRedirectRoute("landing/search/jobs/Sample.aspx", Sample);
        }
    }
}
