using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Employers.Controllers.LinkedIn;

namespace LinkMe.Web.Areas.Employers.Routes
{
    public class LinkedInRoutes
    {
        public static RouteReference Account { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Account = context.MapAreaRoute<LinkedInController>("employers/linkedin", c => c.Account);
        }
    }
}
