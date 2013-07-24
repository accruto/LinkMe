using System.Web.Mvc;
using LinkMe.Web.Areas.Employers.Routes;

namespace LinkMe.Web.Areas.Employers
{
    public class EmployersAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Employers"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            AccountsRoutes.RegisterRoutes(context);
            LinkedInRoutes.RegisterRoutes(context);
            CandidatesRoutes.RegisterRoutes(context);
            JobAdsRoutes.RegisterRoutes(context);
            EnquiriesRoutes.RegisterRoutes(context);
            ProductsRoutes.RegisterRoutes(context);
            SearchRoutes.RegisterRoutes(context);
            ResourcesRoutes.RegisterRoutes(context);
            HomeRoutes.RegisterRoutes(context);
            SettingsRoutes.RegisterRoutes(context);
        }
    }
}
