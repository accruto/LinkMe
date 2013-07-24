using System.Web.Mvc;
using LinkMe.Web.Areas.Integration.Routes;

namespace LinkMe.Web.Areas.Integration
{
    public class IntegrationAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Integration"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            JobAdsRoutes.RegisterRoutes(context);
        }
    }
}