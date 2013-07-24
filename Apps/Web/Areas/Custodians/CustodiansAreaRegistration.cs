using System.Web.Mvc;
using LinkMe.Web.Areas.Custodians.Routes;

namespace LinkMe.Web.Areas.Custodians
{
    public class CustodiansAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Custodians"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            HomeRoutes.RegisterRoutes(context);
        }
    }
}
