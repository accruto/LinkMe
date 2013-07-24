using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Pageflows;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Web.Areas.Controllers;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Public.Controllers
{
    public abstract class PublicController
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

    public abstract class PublicLoginJoinController
        : LoginJoinController
    {
        protected PublicLoginJoinController(IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, IFaqsQuery faqsQuery)
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

    public abstract class PublicPageflowController<TPageflow>
        : Web.Controllers.PageflowController<TPageflow>
        where TPageflow : Pageflow
    {
        protected PublicPageflowController(PageflowRoutes routes, IPageflowEngine pageflowEngine)
            : base(routes, pageflowEngine)
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
