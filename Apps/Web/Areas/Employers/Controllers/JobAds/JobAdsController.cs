using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Presentation.Domain.Users.Employers.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Web.Areas.Employers.Models.Candidates;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Employers.Controllers.JobAds
{
    [EnsureHttps, EnsureAuthorized(UserType.Employer)]
    public class JobAdsController
        : CandidateListController
    {
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobAdApplicantsQuery _jobAdApplicantsQuery;

        public JobAdsController(IExecuteMemberSearchCommand executeMemberSearchCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IMemberStatusQuery memberStatusQuery, IJobAdsQuery jobAdsQuery, IJobAdApplicantsQuery jobAdApplicantsQuery)
            : base(executeMemberSearchCommand, employerMemberViewsQuery, memberStatusQuery)
        {
            _jobAdsQuery = jobAdsQuery;
            _jobAdApplicantsQuery = jobAdApplicantsQuery;
        }

        public ActionResult JobAds()
        {
            var employer = CurrentEmployer;

            if (employer == null)
                return View();

            // Get the jobAds and their counts.

            var jobAdIds = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open).Concat(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Closed));
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(jobAdIds);

            // Get the counts for each job ad through each applicant list.

            var applicantLists = (from a in jobAds select _jobAdApplicantsQuery.GetApplicantList(employer, a)).ToList();
            var counts = _jobAdApplicantsQuery.GetApplicantCounts(employer, from a in applicantLists where a != null select a);
            var applicantCounts = jobAds.ToDictionary(
                jobAd => jobAd.Id,
                jobAd => new ApplicantCountsModel
                {
                    New = jobAd.GetNewCount(applicantLists, counts),
                    Rejected = jobAd.GetRejectedCount(applicantLists, counts),
                    ShortListed = jobAd.GetShortlistedCount(applicantLists, counts)
                });

            // Convert counts to jobAdData.

            return View(new JobAdsModel
            {
                OpenJobAds = (from j in jobAds where j.Status == JobAdStatus.Open select j).ToList(),
                ClosedJobAds = (from j in jobAds where j.Status == JobAdStatus.Closed select j).ToList(),
                ApplicantCounts = applicantCounts
            });
        }
    }
}