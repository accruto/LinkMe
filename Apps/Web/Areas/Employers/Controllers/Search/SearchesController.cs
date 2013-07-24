using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.Search.Members.Queries;
using LinkMe.Web.Areas.Employers.Models.Search;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Employers.Controllers.Search
{
    [EnsureHttps, EnsureAuthorized(UserType.Employer)]
    public class SearchesController
        : EmployersController
    {
        private readonly IMemberSearchesQuery _memberSearchesQuery;

        public SearchesController(IMemberSearchesQuery memberSearchesQuery)
        {
            _memberSearchesQuery = memberSearchesQuery;
        }

        public ActionResult Searches()
        {
            return View(_memberSearchesQuery.GetMemberSearches(CurrentEmployer.Id, new Range()).RangeItems);
        }

        public ActionResult PartialSearches(Pagination pagination, int moreItems)
        {
            if (pagination.Page == null)
                pagination.Page = 1;
            var results = _memberSearchesQuery.GetMemberSearches(CurrentEmployer.Id, pagination.ToRange());
            return View(new SearchesModel {Searches = results.RangeItems, TotalItems = results.TotalItems, MoreItems = moreItems});
        }
    }
}