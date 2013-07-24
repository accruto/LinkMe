using System.Web.Mvc;
using LinkMe.Apps.Api.Areas.Employers.Controllers.Purchases;
using LinkMe.Apps.Api.Areas.Employers.Models.Purchases;
using LinkMe.Apps.Asp.Routing;

namespace LinkMe.Apps.Api.Areas.Employers.Routes
{
    public class PurchaseRoutes
    {
        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapAreaRoute<PurchasesController, VerifyModel>(1, "credits/purchase", c => c.Verify);
        }
    }
}
