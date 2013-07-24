using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Web.Areas.Controllers;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Accounts.Controllers
{
    public abstract class AccountsController
        : WebViewController
    {
        protected new ActionResult View()
        {
            ViewData.SetActiveUserType(UserType.Member);
            return base.View();
        }

        protected new ActionResult View(string viewName)
        {
            ViewData.SetActiveUserType(UserType.Member);
            return base.View(viewName);
        }

        protected new ActionResult View(object model)
        {
            ViewData.SetActiveUserType(UserType.Member);
            return base.View(model);
        }

        protected new ActionResult View(string viewName, object model)
        {
            ViewData.SetActiveUserType(UserType.Member);
            return base.View(viewName, model);
        }
    }

    public abstract class AccountsLoginJoinController
        : LoginJoinController
    {
        protected AccountsLoginJoinController(IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, IFaqsQuery faqsQuery)
            : base(accountsManager, loginCredentialsQuery, faqsQuery)
        {
        }

        protected new ActionResult View()
        {
            ViewData.SetActiveUserType(UserType.Member);
            return base.View();
        }

        protected new ActionResult View(string viewName)
        {
            ViewData.SetActiveUserType(UserType.Member);
            return base.View(viewName);
        }

        protected new ActionResult View(object model)
        {
            ViewData.SetActiveUserType(UserType.Member);
            return base.View(model);
        }

        protected new ActionResult View(string viewName, object model)
        {
            ViewData.SetActiveUserType(UserType.Member);
            return base.View(viewName, model);
        }
    }
}
