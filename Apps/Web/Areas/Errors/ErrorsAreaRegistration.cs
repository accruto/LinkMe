using System.Web.Mvc;
using LinkMe.Web.Areas.Errors.Routes;

namespace LinkMe.Web.Areas.Errors
{
    public class ErrorsAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Errors"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            ErrorsRoutes.RegisterRoutes(context);
        }
    }
}