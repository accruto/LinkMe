using System.Web.Mvc;
using LinkMe.Web.Areas.Landing.Routes;

namespace LinkMe.Web.Areas.Landing
{
    public class LandingAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Landing"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            JobAdsRoutes.RegisterRoutes(context);
        }
    }
}