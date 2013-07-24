using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Api.Controllers;

namespace LinkMe.Web.Areas.Api.Routes
{
    public static class SearchRoutes
    {
        public static RouteReference SplitKeywords { get; private set; }
        public static RouteReference CombineKeywords { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            SplitKeywords = context.MapAreaRoute<SearchApiController, string>("api/search/splitkeywords", c => c.SplitKeywords);
            CombineKeywords = context.MapAreaRoute<SearchApiController, string, string, string, string>("api/search/combinekeywords", c => c.CombineKeywords);
        }
    }
}