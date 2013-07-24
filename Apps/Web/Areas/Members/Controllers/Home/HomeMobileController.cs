using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Presentation.Query.Search.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Queries;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.Search;
using LinkMe.Web.Areas.Public.Models.Home;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.Home
{
    [EnsureHttps, EnsureAuthorized(UserType.Member, RequiresActivation = true)]
    public class HomeMobileController
        : MembersController
    {
        private readonly ILocationQuery _locationQuery;
        private readonly IJobAdSearchesQuery _jobAdSearchesQuery;
        private readonly IJobAdSearchAlertsQuery _jobAdSearchAlertsQuery;

        public HomeMobileController(ILocationQuery locationQuery, IJobAdSearchesQuery jobAdSearchesQuery, IJobAdSearchAlertsQuery jobAdSearchAlertsQuery)
        {
            _locationQuery = locationQuery;
            _jobAdSearchesQuery = jobAdSearchesQuery;
            _jobAdSearchAlertsQuery = jobAdSearchAlertsQuery;
        }

        public ActionResult Home()
        {
            var country = ActivityContext.Location.Country;
            return View(new HomeModel
            {
                Reference = new ReferenceModel
                {
                    MinSalary = JobAdSearchCriteria.MinSalary,
                    MaxSalary = JobAdSearchCriteria.MaxSalary,
                    StepSalary = JobAdSearchCriteria.StepSalary,
                    MinHourlySalary = JobAdSearchCriteria.MinHourlySalary,
                    MaxHourlySalary = JobAdSearchCriteria.MaxHourlySalary,
                    StepHourlySalary = JobAdSearchCriteria.StepHourlySalary,
                    Countries = _locationQuery.GetCountries(),
                    CountrySubdivisions = (from s in _locationQuery.GetCountrySubdivisions(country) where !s.IsCountry select s).ToList(),
                    Regions = _locationQuery.GetRegions(country),
                    DefaultCountry = country,
                },
                RecentSearches = GetRecentSearchesModel()
            });
        }

        private IList<JobAdSearchModel> GetRecentSearchesModel()
        {
            var member = CurrentMember;

            // Get saved searches, past searches, and alerts.

            var executions = _jobAdSearchesQuery.GetRecentSearchExecutions(member.Id);
            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            var alerts = _jobAdSearchAlertsQuery.GetJobAdSearchAlerts(from s in searches select s.Id);

            // Now attempt to match them up.

            return (from e in executions.SelectRange(new Range(0, Reference.DefaultItemsPerPage))
                    let s = (from s in searches
                             where s.Criteria.Equals(e.Criteria)
                             select s).FirstOrDefault()
                    select new JobAdSearchModel
                    {
                        ExecutionId = e.Id,
                        Criteria = e.Criteria,
                        SearchId = s == null ? (Guid?)null : s.Id,
                        HasAlert = s != null && (from a in alerts where a.JobAdSearchId == s.Id select a).Any(),
                    }).ToList();
        }
    }
}