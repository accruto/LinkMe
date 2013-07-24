using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Custodians.Controllers
{
    [EnsureHttps, EnsureAuthorized(UserType.Custodian)]
    public class HomeController
        : CustodiansController
    {
        public ActionResult Home()
        {
            return View();
        }
    }
}