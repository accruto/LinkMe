using System.Web.Mvc;

namespace LinkMe.Web.Areas.Employers.Controllers.Resources
{
    public class ResourcesController
        : EmployersController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
