using System.Web.Mvc;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Public.Controllers.Home
{
    [EnsureNotAuthorized]
    public class GuestsController
        : WebViewController
    {
        public new ActionResult Profile()
        {
            return View();
        }
    }
}
