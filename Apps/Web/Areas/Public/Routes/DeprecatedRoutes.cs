using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Public.Controllers.Deprecated;

namespace LinkMe.Web.Areas.Public.Routes
{
    public class DeprecatedRoutes
    {
        public static RouteReference Blogs { get; private set; }
        public static RouteReference Groups { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Blogs = context.MapAreaRoute<DeprecatedController>("blogs", c => c.Blogs);
            context.MapRedirectRoute("ui/registered/blogs/{*catchall}", Blogs);
            context.MapRedirectRoute("ui/registered/blog/{*catchall}", Blogs);
            context.MapRedirectRoute("ui/unregistered/blogs/{*catchall}", Blogs);
            context.MapRedirectRoute("ui/unregistered/blog/{*catchall}", Blogs);

            Groups = context.MapAreaRoute<DeprecatedController>("groups", c => c.Groups);
            context.MapRedirectRoute("groups/{*catchall}", Groups);
            context.MapRedirectRoute("ui/unregistered/groups/{*catchall}", Groups);
            context.MapRedirectRoute("ui/registered/networkers/groups/{*catchall}", Groups);
        }
    }
}
