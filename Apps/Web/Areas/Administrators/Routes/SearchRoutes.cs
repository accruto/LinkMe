using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Administrators.Controllers.Search;

namespace LinkMe.Web.Areas.Administrators.Routes
{
    public static class SearchRoutes
    {
        public static RouteReference SearchEngines { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            SearchEngines = context.MapAreaRoute<SearchEnginesController>("administrators/search/engines", c => c.Search);
        }
    }
}
