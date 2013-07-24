using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Web.Areas.Landing.Models.JobAds;
using LinkMe.Web.Areas.Members.Routes;
using JobAdsRoutes=LinkMe.Web.Areas.Landing.Routes.JobAdsRoutes;

namespace LinkMe.Web.Areas.Landing.Controllers
{
    public class JobAdsController
        : ViewController
    {
        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Sample()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchModel search)
        {
            if (string.IsNullOrEmpty(search.Keywords) && string.IsNullOrEmpty(search.Location))
                return RedirectToRoute(JobAdsRoutes.Search);

            return RedirectToRoute(SearchRoutes.Search, new { search.Keywords, search.Location });
        }
    }
}
