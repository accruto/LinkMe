using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Api.Controllers;

namespace LinkMe.Web.Areas.Api.Routes
{
    public class LocationRoutes
    {
        public static RouteReference PartialMatches { get; private set; }
        public static RouteReference PartialPostalMatches { get; private set; }
        public static RouteReference ResolveLocation { get; private set; }
        public static RouteReference ClosestLocation { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            PartialMatches = context.MapAreaRoute<LocationApiController, int?, string, int?>("api/location/partialmatches", c => c.FindPartialMatchedLocations);
            PartialPostalMatches = context.MapAreaRoute<LocationApiController, int?, string, int?>("api/location/partialpostalmatches", c => c.FindPartialMatchedPostalSuburbs);
            ResolveLocation = context.MapAreaRoute<LocationApiController, int?, string>("api/location/resolve", c => c.ResolveLocation);
            ClosestLocation = context.MapAreaRoute<LocationApiController, int?, float, float>("api/location/closest", c => c.ClosestLocation);
        }
    }
}