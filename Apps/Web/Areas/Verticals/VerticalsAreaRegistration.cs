using System.Web.Mvc;
using LinkMe.Web.Areas.Verticals.Routes;

namespace LinkMe.Web.Areas.Verticals
{
    public class VerticalsAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Verticals"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            VerticalsRoutes.RegisterRoutes(context);
        }
    }
}
