using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Web.Areas.Employers.Models;
using LinkMe.Web.Areas.Employers.Models.Candidates;
using LinkMe.Web.Areas.Employers.Models.Search;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Context;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Employers.Controllers.JobAds
{
    [EnsureHttps, EnsureAuthorized(UserType.Employer)]
    public class ManageCandidatesController
        : CandidateListController
    {
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobAdApplicantsQuery _jobAdApplicantsQuery;
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IVerticalsQuery _verticalsQuery;

        private const ApplicantStatus DefaultStatus = ApplicantStatus.New;
        private const ApplicantStatus FallbackStatus = ApplicantStatus.Shortlisted;
        private const string ManageCandidatesViewName = "ManageCandidates";

        public ManageCandidatesController(IExecuteMemberSearchCommand executeMemberSearchCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IMemberStatusQuery memberStatusQuery, IJobAdsQuery jobAdsQuery, IJobAdApplicantsQuery jobAdApplicantsQuery, ICommunitiesQuery communitiesQuery, IVerticalsQuery verticalsQuery)
            : base(executeMemberSearchCommand, employerMemberViewsQuery, memberStatusQuery)
        {
            _jobAdsQuery = jobAdsQuery;
            _jobAdApplicantsQuery = jobAdApplicantsQuery;

            _communitiesQuery = communitiesQuery;
            _verticalsQuery = verticalsQuery;

        }

        public ActionResult Manage(Guid jobAdId, ApplicantStatus? status, MemberSearchSortCriteria sortOrder, CandidatesPresentationModel presentation)
        {
            var model = JobAdResults(jobAdId, status, sortOrder, presentation);
            if (model == null)
                return NotFound("job ad", "id", jobAdId);

            return View(ManageCandidatesViewName, model);
        }

        public ActionResult Partial(ApplicantStatus status, MemberSearchSortCriteria sortOrder, CandidatesPresentationModel presentation)
        {
            var navigation = (ManageCandidatesNavigation)EmployerContext.CurrentCandidates;

            var model = JobAdResults(navigation.JobAdId, status, sortOrder, presentation); 
            if (model == null)
                return NotFound("job ad", "id", navigation.JobAdId);
            
            return PartialView("CandidateList", model);
        }

        /// <summary>
        /// This route exists only to be built client-side into the ManageJobAdCandidates route
        /// </summary>
        /// <returns>Should this route ever be hit redirect to the Manage JobAds page</returns>
        public ActionResult ManageCandidates()
        {
            return RedirectToRoute(JobAdsRoutes.JobAds);
        }

        private ManageCandidatesListModel JobAdResults(Guid jobAdId, ApplicantStatus? status, MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            var employer = CurrentEmployer;
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
            if (jobAd == null)
                return null;

            var searchStatus = status ?? DefaultStatus;
            var model = Search(employer, jobAdId, searchStatus, sortCriteria, presentation);

            if (model.Results.TotalCandidates == 0 && status == null)
            {
                searchStatus = FallbackStatus;
                model = Search(employer, jobAdId, searchStatus, sortCriteria, presentation);
            }

            // Indicate those who are rejected.

            model.ApplicantStatus = searchStatus;
            model.Results.RejectedCandidateIds = searchStatus == ApplicantStatus.Rejected ? model.Results.CandidateIds : new List<Guid>();

            var applicantList = _jobAdApplicantsQuery.GetApplicantList(employer, jobAd);
            var counts = _jobAdApplicantsQuery.GetApplicantCounts(employer, applicantList);

            model.JobAd = new JobAdDataModel
            {
                Id = jobAdId,
                Title = jobAd.Title,
                Status = jobAd.Status,
                ApplicantCounts = new ApplicantCountsModel
                {
                    New = counts[ApplicantStatus.New],
                    Rejected = counts[ApplicantStatus.Rejected],
                    ShortListed = counts[ApplicantStatus.Shortlisted],
                },
                Applications = _jobAdApplicantsQuery.GetApplicationsByPositionId(jobAd.Id).ToDictionary(a => a.ApplicantId, a => a),
            };
            model.SortOrders = new[] { MemberSortOrder.DateUpdated, MemberSortOrder.Flagged, MemberSortOrder.FirstName }.OrderBy(o => o.ToString()).ToArray();
            model.Communities = _communitiesQuery.GetCommunities().ToDictionary(c => c.Id, c => c);
            model.Verticals = _verticalsQuery.GetVerticals().ToDictionary(v => v.Id, v => v);

            EmployerContext.CurrentSearch = null;
            EmployerContext.CurrentCandidates = new ManageCandidatesNavigation(jobAdId, model.JobAd.Status, searchStatus, presentation);

            return model;
        }

        private ManageCandidatesListModel Search(IEmployer employer, Guid jobAdId, ApplicantStatus status, MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            return Search<ManageCandidatesListModel>(employer, presentation, (e, r) => _executeMemberSearchCommand.SearchManaged(e, jobAdId, status, sortCriteria, r));
        }
    }
}
