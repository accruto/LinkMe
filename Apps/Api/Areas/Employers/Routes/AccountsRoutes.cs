using LinkMe.Apps.Api.Areas.Employers.Controllers.Accounts;
using LinkMe.Apps.Api.Areas.Employers.Models.Accounts;
using LinkMe.Apps.Asp.Routing;
using System.Web.Mvc;

namespace LinkMe.Apps.Api.Areas.Employers.Routes
{
    public class AccountsRoutes
    {
        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapAreaRoute<AccountsApiController, EmployerJoinModel>(1, "employers/join", c => c.Join);
            context.MapAreaRoute<AccountsApiController>(1, "employers/profile", c => c.Profile);
        }
    }
}