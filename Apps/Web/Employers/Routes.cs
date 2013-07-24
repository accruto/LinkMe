using System.Web.Routing;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Employers
{
    public static class Routes
    {
        public static void RegisterRoutes(string areaPath, RouteCollection routes)
        {
            Enquiries.Routes.RegisterRoutes(areaPath.AddUrlSegments("Enquiries"), routes);
            Products.Routes.RegisterRoutes(areaPath.AddUrlSegments("Products"), routes);
        }
    }
}
