using System.Web.Mvc;
using LinkMe.Apps.Api.Areas.Dev.Routes;

namespace LinkMe.Apps.Api.Areas.Dev
{
    public class DevAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Dev"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            DevRoutes.RegisterRoutes(context);
        }
    }
}