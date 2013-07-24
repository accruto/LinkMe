using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Employers.Controllers.Settings;

namespace LinkMe.Web.Areas.Employers.Routes
{
    public static class SettingsRoutes
    {
        public static RouteReference Settings { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Settings = context.MapAreaRoute<SettingsController>("employers/settings", c => c.Settings);
            context.MapRedirectRoute("ui/registered/employers/EmployerEditUserProfileForm.aspx", Settings);
        }
    }
}