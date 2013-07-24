using System.Web.Mvc;

namespace LinkMe.Web.Areas.Public.Controllers.Deprecated
{
    public class DeprecatedController
        : PublicController
    {
        public ActionResult Blogs()
        {
            return View();
        }

        public ActionResult Groups()
        {
            return View();
        }
    }
}
