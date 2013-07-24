using System.Web.Mvc;
using LinkMe.Apps.Api.Areas.Accounts.Routes;

namespace LinkMe.Apps.Api.Areas.Accounts
{
    public class AccountsAreaRegistration
        : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Accounts"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            AccountsRoutes.RegisterRoutes(context);
        }
    }
}