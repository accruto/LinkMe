using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Context;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers
{
    public abstract class MembersController
        : WebViewController
    {
        private MemberContext _context;

        protected MemberContext MemberContext
        {
            get { return _context ?? (_context = new MemberContext(HttpContext)); }
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
