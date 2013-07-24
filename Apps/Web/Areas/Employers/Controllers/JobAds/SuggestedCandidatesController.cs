using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Apps.Services.JobAds.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Queries;
using LinkMe.Web.Areas.Employers.Models;
using LinkMe.Web.Areas.Employers.Models.Candidates;
using LinkMe.Web.Areas.Employers.Models.Search;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Employers.Controllers.JobAds
{
    [EnsureHttps]
    public class SuggestedCandidatesController
        : CandidateListController
    {
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IExternalJobAdsQuery _externalJobAdsQuery;
        private readonly ISuggestedMembersQuery _suggestedMembersQuery;
        private readonly ILocationQuery _locationQuery;
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IVerticalsQuery _verticalsQuery;
        private readonly IIndustriesQuery _industriesQuery;

        private static readonly MemberSortOrder[] SortOrders = new[] { MemberSortOrder.Relevance, MemberSortOrder.DateUpdated, MemberSortOrder.Distance, MemberSortOrder.Salary, MemberSortOrder.Flagged, MemberSortOrder.Availability, MemberSortOrder.FirstName }.OrderBy(o => o.ToString()).ToArray();

        public SuggestedCandidatesController(IExecuteMemberSearchCommand executeMemberSearchCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IMemberStatusQuery memberStatusQuery, IJobAdsQuery jobAdsQuery, IExternalJobAdsQuery externalJobAdsQuery, ISuggestedMembersQuery suggestedMembersQuery, ILocationQuery locationQuery, ICommunitiesQuery communitiesQuery, IVerticalsQuery verticalsQuery, IIndustriesQuery industriesQuery)
            : base(executeMemberSearchCommand, employerMemberViewsQuery, memberStatusQuery)
        {
            _jobAdsQuery = jobAdsQuery;
            _externalJobAdsQuery = externalJobAdsQuery;
            _suggestedMembersQuery = suggestedMembersQuery;
            _locationQuery = locationQuery;
            _communitiesQuery = communitiesQuery;
            _verticalsQuery = verticalsQuery;
            _industriesQuery = industriesQuery;
        }

        public ActionResult ExternalSuggestedCandidates(string externalReferenceId)
        {
            var jobAdId = _externalJobAdsQuery.GetJobAdId(externalReferenceId);
            if (jobAdId == null)
                return NotFound("job ad", "external reference id", externalReferenceId);

            return RedirectToRoute(JobAdsRoutes.SuggestedCandidates, new { jobAdId = jobAdId.Value });
        }

        public ActionResult SuggestedCandidates(Guid jobAdId, CandidatesPresentationModel presentation)
        {
            // Anyone can see any job ad.

            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            // Get the criteria for the search.

            var criteria = _suggestedMembersQuery.GetCriteria(jobAd);
            var criteriaIsEmpty = criteria.IsEmpty;
            criteria.PrepareCriteria();
            if (criteriaIsEmpty)
                SetDefaults(criteria);

            // Search.

            var model = Search(CurrentEmployer, jobAdId, criteria, presentation, jobAd.Title, jobAd.Status);

            // Save the basis for the search.

            EmployerContext.CurrentCandidates = new SuggestedCandidatesNavigation(jobAdId, jobAd.Title, jobAd.Status, criteria, presentation);
            return View(model);
        }

        public ActionResult PartialSuggestedCandidates(MemberSearchCriteria criteria, CandidatesPresentationModel presentation)
        {
            var currentSearch = (SuggestedCandidatesNavigation)EmployerContext.CurrentCandidates;
            var model = Search(CurrentEmployer, currentSearch.JobAdId, criteria, presentation, currentSearch.JobAdTitle, currentSearch.JobAdStatus);

            EmployerContext.CurrentCandidates = new SuggestedCandidatesNavigation(currentSearch.JobAdId, currentSearch.JobAdTitle, currentSearch.JobAdStatus, criteria, presentation);
            return PartialView("CandidateList", model);
        }

        private SuggestedCandidatesListModel Search(IEmployer employer, Guid jobAdId, MemberSearchCriteria criteria, CandidatesPresentationModel presentation, string jobAdTitle, JobAdStatus jobAdStatus)
        {
            var searchList = GetSearchList(Search(employer, jobAdId, criteria, presentation));
            searchList.JobAd = new JobAdDataModel
            {
                Id = jobAdId,
                Title = jobAdTitle,
                Status = jobAdStatus,
            };

            return searchList;
        }

        private SuggestedCandidatesListModel Search(IEmployer employer, Guid jobAdId, MemberSearchCriteria criteria, CandidatesPresentationModel presentation)
        {
            return Search<SuggestedCandidatesListModel>(employer, presentation, (e, r) => _executeMemberSearchCommand.SearchSuggested(e, jobAdId, criteria, r));
        }

        private void SetDefaults(MemberSearchCriteria criteria)
        {
            criteria.Location = _locationQuery.ResolveLocation(ActivityContext.Location.Country, null);
        }

        private SuggestedCandidatesListModel GetSearchList(SuggestedCandidatesListModel searchList)
        {
            searchList.SortOrders = SortOrders;
            searchList.Communities = _communitiesQuery.GetCommunities().ToDictionary(c => c.Id, c => c);
            searchList.Verticals = _verticalsQuery.GetVerticals().ToDictionary(v => v.Id, v => v);
            searchList.Countries = _locationQuery.GetCountries();
            searchList.Industries = _industriesQuery.GetIndustries();
            searchList.Distances = Reference.Distances;
            searchList.DefaultDistance = MemberSearchCriteria.DefaultDistance;
            searchList.DefaultCountry = ActivityContext.Location.Country;
            searchList.Recencies = Recencies;
            searchList.DefaultRecency = MemberSearchCriteria.DefaultRecency;
            searchList.MinSalary = MemberSearchCriteria.MinSalary;
            searchList.MaxSalary = MemberSearchCriteria.MaxSalary;
            searchList.StepSalary = MemberSearchCriteria.StepSalary;
            return searchList;
        }
    }
}
