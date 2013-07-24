using System.Web.Mvc;
using LinkMe.Web.Areas.Administrators.Routes;

namespace LinkMe.Web.Areas.Administrators
{
    public class AdministratorsAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Administrators"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            HomeRoutes.RegisterRoutes(context);
            CampaignsRoutes.RegisterRoutes(context);
            AdministratorsRoutes.RegisterRoutes(context);
            MembersRoutes.RegisterRoutes(context);
            EmployersRoutes.RegisterRoutes(context);
            OrganisationsRoutes.RegisterRoutes(context);
            CommunitiesRoutes.RegisterRoutes(context);
            CustodiansRoutes.RegisterRoutes(context);
            SearchRoutes.RegisterRoutes(context);
        }
    }
}
