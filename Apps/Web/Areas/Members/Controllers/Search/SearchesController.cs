using System;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Query.Search.JobAds;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Queries;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.Search;

namespace LinkMe.Web.Areas.Members.Controllers.Search
{
    public abstract class SearchesController
        : MembersController
    {
        private readonly IJobAdSearchesQuery _jobAdSearchesQuery;
        private readonly IJobAdSearchAlertsQuery _jobAdSearchAlertsQuery;

        protected SearchesController(IJobAdSearchesQuery jobAdSearchesQuery, IJobAdSearchAlertsQuery jobAdSearchAlertsQuery)
        {
            _jobAdSearchesQuery = jobAdSearchesQuery;
            _jobAdSearchAlertsQuery = jobAdSearchAlertsQuery;
        }

        protected SearchesModel GetRecentSearchesModel(Pagination pagination)
        {
            var member = CurrentMember;

            // Get saved searches, past searches, and alerts.

            var executions = _jobAdSearchesQuery.GetRecentSearchExecutions(member.Id);
            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            var alerts = _jobAdSearchAlertsQuery.GetJobAdSearchAlerts(from s in searches select s.Id);

            // Now attempt to match them up.

            return new SearchesModel
            {
                TotalItems = executions.Count,
                Pagination = pagination,
                Type = SearchesType.Recent,
                Searches = (from e in executions.SelectRange(pagination.ToRange())
                            let s = (from s in searches
                                     where s.Criteria.Equals(e.Criteria)
                                     select s).FirstOrDefault()
                            select new JobAdSearchModel
                            {
                                ExecutionId = e.Id,
                                Criteria = e.Criteria,
                                SearchId = s == null ? (Guid?)null : s.Id,
                                HasAlert = s != null && (from a in alerts where a.JobAdSearchId == s.Id select a).Any(),
                            }).ToList(),
            };
        }

        protected SearchesModel GetSavedSearchesModel(Pagination pagination)
        {
            var member = CurrentMember;

            // Get searches and alerts.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id).ToList();
            var count = searches.Count;
            searches = searches.SelectRange(pagination.ToRange()).ToList();
            var alerts = _jobAdSearchAlertsQuery.GetJobAdSearchAlerts(from s in searches select s.Id);

            return new SearchesModel
            {
                TotalItems = count,
                Pagination = pagination,
                Type = SearchesType.Saved,
                Searches = (from s in searches
                            select new JobAdSearchModel
                            {
                                SearchId = s.Id,
                                Criteria = s.Criteria,
                                Name = s.Name,
                                HasAlert = (from a in alerts where a.JobAdSearchId == s.Id select a).Any(),
                            }).ToList(),
            };
        }

        protected JobAdSearch GetSearch(Guid searchId)
        {
            var search = _jobAdSearchAlertsQuery.GetJobAdSearch(searchId);
            if (search == null)
                throw new ValidationErrorsException(new NotFoundValidationError("alert id", searchId));
            return search;
        }

        protected static void PreparePaginationModel(Pagination pagination)
        {
            // Ensure that the pagination values are always set.

            if (pagination.Page == null)
                pagination.Page = 1;
            if (pagination.Items == null)
                pagination.Items = Reference.DefaultItemsPerPage;
        }
    }
}
