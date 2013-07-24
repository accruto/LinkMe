using System.Web.Mvc;
using LinkMe.Web.Areas.Public.Routes;

namespace LinkMe.Web.Areas.Public
{
    public class PublicAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Public"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            LoginsRoutes.RegisterRoutes(context);
            HomeRoutes.RegisterRoutes(context);
            JoinRoutes.RegisterRoutes(context);
            DeprecatedRoutes.RegisterRoutes(context);
            SupportRoutes.RegisterRoutes(context);
            FaqsRoutes.RegisterRoutes(context);
        }
    }
}
