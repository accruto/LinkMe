using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Public.Controllers.Logins;
using LinkMe.Web.Areas.Public.Models.Logins;

namespace LinkMe.Web.Areas.Public.Routes
{
    public static class LoginsRoutes
    {
        public static RouteReference LogIn { get; private set; }
        public static RouteReference LogOut { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            LogIn = context.MapAreaRoute<LoginController, LoginReason?>("login", c => c.LogIn);
            LogOut = context.MapAreaRoute<LogoutController>("logout", c => c.LogOut);
        }
    }
}