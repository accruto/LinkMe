using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Employers.Controllers.Home;

namespace LinkMe.Web.Areas.Employers.Routes
{
    public class HomeRoutes
    {
        public static RouteReference Home { get; private set; }
        public static RouteReference Features { get; private set; }
        public static RouteReference CandidateConnect { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Home = context.MapAreaRoute<HomeController, bool?>("employers", c => c.Home);
            Features = context.MapAreaRoute<HomeController>("employers/features", c => c.Features);
            CandidateConnect = context.MapAreaRoute<HomeController>("employers/candidateconnect", c => c.CandidateConnect);
        }
    }
}
