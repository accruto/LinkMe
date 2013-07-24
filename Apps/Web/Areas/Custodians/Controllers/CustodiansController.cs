using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Custodians.Controllers
{
    public abstract class CustodiansController
        : WebViewController
    {
        protected new ActionResult View()
        {
            ViewData.SetActiveUserType(UserType.Custodian);
            return base.View();
        }

        protected new ActionResult View(string viewName)
        {
            ViewData.SetActiveUserType(UserType.Custodian);
            return base.View(viewName);
        }

        protected new ActionResult View(object model)
        {
            ViewData.SetActiveUserType(UserType.Custodian);
            return base.View(model);
        }

        protected new ActionResult View(string viewName, object model)
        {
            ViewData.SetActiveUserType(UserType.Custodian);
            return base.View(viewName, model);
        }
    }
}
