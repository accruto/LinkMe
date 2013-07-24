using System.Web.Mvc;
using LinkMe.Apps.Api.Areas.Employers.Routes;

namespace LinkMe.Apps.Api.Areas.Employers
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
            SearchRoutes.RegisterRoutes(context);
            CandidatesRoutes.RegisterRoutes(context);
            PurchaseRoutes.RegisterRoutes(context);
        }
    }
}