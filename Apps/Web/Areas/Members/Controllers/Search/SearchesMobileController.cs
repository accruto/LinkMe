using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Search.JobAds.Queries;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.Search
{
    [EnsureAuthorized(UserType.Member)]
    public class SearchesMobileController
        : SearchesController
    {
        public SearchesMobileController(IJobAdSearchesQuery jobAdSearchesQuery, IJobAdSearchAlertsQuery jobAdSearchAlertsQuery)
            : base(jobAdSearchesQuery, jobAdSearchAlertsQuery)
        {
        }

        public ActionResult Searches()
        {
            return View();
        }

        public ActionResult RecentSearches()
        {
            var pagination = new Pagination();
            PreparePaginationModel(pagination);
            return View(GetRecentSearchesModel(pagination));
        }

        public ActionResult SavedSearches()
        {
            var pagination = new Pagination();
            PreparePaginationModel(pagination);
            return View(GetSavedSearchesModel(pagination));
        }
    }
}
