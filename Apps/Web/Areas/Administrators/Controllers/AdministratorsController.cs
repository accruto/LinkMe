using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers
{
    public abstract class AdministratorsController
        : WebViewController
    {
        protected new ActionResult View()
        {
            ViewData.SetActiveUserType(UserType.Administrator);
            return base.View();
        }

        protected new ActionResult View(string viewName)
        {
            ViewData.SetActiveUserType(UserType.Administrator);
            return base.View(viewName);
        }

        protected new ActionResult View(object model)
        {
            ViewData.SetActiveUserType(UserType.Administrator);
            return base.View(model);
        }

        protected new ActionResult View(string viewName, object model)
        {
            ViewData.SetActiveUserType(UserType.Administrator);
            return base.View(viewName, model);
        }
    }
}
