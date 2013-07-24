using System.Web.Mvc;
using LinkMe.Web.Areas.Technical.Routes;

namespace LinkMe.Web.Areas.Technical
{
    public class TechnicalAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Technical"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            TechnicalRoutes.RegisterRoutes(context);
        }
    }
}