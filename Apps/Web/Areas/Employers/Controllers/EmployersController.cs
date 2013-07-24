using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Pageflows;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Web.Areas.Controllers;
using LinkMe.Web.Context;
using LinkMe.Web.Controllers;
using LinkMe.Web.Mvc;

namespace LinkMe.Web.Areas.Employers.Controllers
{
    public abstract class EmployersController
        : WebViewController
    {
        private EmployerContext _context;

        protected EmployerContext EmployerContext
        {
            get { return _context ?? (_context = new EmployerContext(HttpContext)); }
        }

        protected new ActionResult View()
        {
            ViewData.SetActiveUserType(UserType.Employer);
            ViewData.SetCreditAllocations(CurrentEmployer);
            return base.View();
        }

        protected new ActionResult View(string viewName)
        {
            ViewData.SetActiveUserType(UserType.Employer);
            ViewData.SetCreditAllocations(CurrentEmployer);
            return base.View(viewName);
        }

        protected new ActionResult View(object model)
        {
            ViewData.SetActiveUserType(UserType.Employer);
            ViewData.SetCreditAllocations(CurrentEmployer);
            return base.View(model);
        }

        protected new ActionResult View(string viewName, object model)
        {
            ViewData.SetActiveUserType(UserType.Employer);
            ViewData.SetCreditAllocations(CurrentEmployer);
            return base.View(viewName, model);
        }
    }

    public abstract class EmployersLoginJoinController
        : LoginJoinController
    {
        protected EmployersLoginJoinController(IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, IFaqsQuery faqsQuery)
            : base(accountsManager, loginCredentialsQuery, faqsQuery)
        {
        }

        protected new ActionResult View()
        {
            ViewData.SetActiveUserType(UserType.Employer);
            ViewData.SetCreditAllocations(CurrentEmployer);
            return base.View();
        }

        protected new ActionResult View(string viewName)
        {
            ViewData.SetActiveUserType(UserType.Employer);
            ViewData.SetCreditAllocations(CurrentEmployer);
            return base.View(viewName);
        }

        protected new ActionResult View(object model)
        {
            ViewData.SetActiveUserType(UserType.Employer);
            ViewData.SetCreditAllocations(CurrentEmployer);
            return base.View(model);
        }

        protected new ActionResult View(string viewName, object model)
        {
            ViewData.SetActiveUserType(UserType.Employer);
            ViewData.SetCreditAllocations(CurrentEmployer);
            return base.View(viewName, model);
        }
    }

    public abstract class EmployersPageflowController<TPageflow>
        : Web.Controllers.PageflowController<TPageflow>
        where TPageflow : Pageflow
    {
        protected EmployersPageflowController(PageflowRoutes routes, IPageflowEngine pageflowEngine)
            : base(routes, pageflowEngine)
        {
        }

        protected new ActionResult View()
        {
            ViewData.SetActiveUserType(UserType.Employer);
            ViewData.SetCreditAllocations(CurrentEmployer);
            return base.View();
        }

        protected new ActionResult View(string viewName)
        {
            ViewData.SetActiveUserType(UserType.Employer);
            ViewData.SetCreditAllocations(CurrentEmployer);
            return base.View(viewName);
        }

        protected new ActionResult View(object model)
        {
            ViewData.SetActiveUserType(UserType.Employer);
            ViewData.SetCreditAllocations(CurrentEmployer);
            return base.View(model);
        }

        protected new ActionResult View(string viewName, object model)
        {
            ViewData.SetActiveUserType(UserType.Employer);
            ViewData.SetCreditAllocations(CurrentEmployer);
            return base.View(viewName, model);
        }
    }
}