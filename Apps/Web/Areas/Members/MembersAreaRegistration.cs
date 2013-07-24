using System.Web.Mvc;
using LinkMe.Web.Areas.Members.Routes;

namespace LinkMe.Web.Areas.Members
{
    public class MembersAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Members"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            SettingsRoutes.RegisterRoutes(context);
            ProfilesRoutes.RegisterRoutes(context);
            HomeRoutes.RegisterRoutes(context);
            SearchRoutes.RegisterRoutes(context);
            JobAdsRoutes.RegisterRoutes(context);
            FriendsRoutes.RegisterRoutes(context);
            ResourcesRoutes.RegisterRoutes(context);
        }
    }
}
