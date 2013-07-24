using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Employers.Controllers.Accounts;

namespace LinkMe.Web.Areas.Employers.Routes
{
    public class AccountsRoutes
    {
        public static RouteReference LogIn { get; private set; }
        public static RouteReference Join { get; private set; }

        public static RouteReference HideCreditReminder { get; private set; }
        public static RouteReference HideBulkCreditReminder { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            LogIn = context.MapAreaRoute<LoginController>("employers/login", c => c.LogIn);
            Join = context.MapAreaRoute<LoginController>("employers/join", c => c.Join);

            HideCreditReminder = context.MapAreaRoute<StateApiController>("employers/state/hidecreditreminder", c => c.HideCreditReminder);
            HideBulkCreditReminder = context.MapAreaRoute<StateApiController>("employers/state/hidebulkcreditreminder", c => c.HideBulkCreditReminder);

            context.MapRedirectRoute("employers/Join.aspx", Join);
        }
    }
}
