using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Home
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class HomeController
        : AdministratorsController
    {
        public ActionResult Home()
        {
            return View();
        }
    }
}