using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Members.Controllers.Settings;

namespace LinkMe.Web.Areas.Members.Routes
{
    public static class SettingsRoutes
    {
        public static RouteReference Settings { get; private set; }
        public static RouteReference Communications { get; private set; }
        public static RouteReference Deactivate { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Settings = context.MapAreaRoute<SettingsController>("members/settings", c => c.Settings);
            context.MapRedirectRoute("ui/registered/networkers/CommunicationSettings.aspx", Settings);

            Communications = context.MapAreaRoute<SettingsController>("members/settings/communications", c => c.Communications);
            Deactivate = context.MapAreaRoute<SettingsController>("members/settings/deactivate", c => c.Deactivate);
        }
    }
}