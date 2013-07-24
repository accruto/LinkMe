using System.Web.Mvc;
using LinkMe.Web.Areas.Accounts.Routes;

namespace LinkMe.Web.Areas.Accounts
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