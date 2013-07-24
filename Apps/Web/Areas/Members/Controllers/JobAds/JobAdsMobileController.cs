using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Caches;
using LinkMe.Apps.Services.JobAds.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    public class JobAdsMobileController
        : JobAdsController
    {
        private readonly IMemberJobAdViewsQuery _memberJobAdViewsQuery;
        private readonly IEmployersQuery _employersQuery;

        public JobAdsMobileController(IJobAdViewsQuery jobAdViewsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IExecuteJobAdSearchCommand executeJobAdSearchCommand, IEmployersQuery employersQuery, ICandidatesQuery candidatesQuery, ICandidateResumeFilesQuery candidateResumeFilesQuery, IFilesQuery filesQuery, ICacheManager cacheManager, IMemberStatusQuery memberStatusQuery, IResumesQuery resumesQuery, IExternalJobAdsQuery externalJobAdsQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IJobAdFoldersQuery jobAdFoldersQuery, IJobAdFoldersCommand jobAdFoldersCommand)
            : base(jobAdViewsQuery, memberJobAdViewsQuery, executeJobAdSearchCommand, candidatesQuery, candidateResumeFilesQuery, filesQuery, cacheManager, memberStatusQuery, resumesQuery, externalJobAdsQuery, jobAdFlagListsQuery, jobAdFoldersQuery, jobAdFoldersCommand)
        {
            _memberJobAdViewsQuery = memberJobAdViewsQuery;
            _employersQuery = employersQuery;
        }

        [EnsureAuthorized(UserType.Member)]
        public ActionResult Jobs()
        {
            return View();
        }

        public ActionResult Email(Guid jobAdId)
        {
            return GetJobAdView(jobAdId);
        }

        public ActionResult EmailSent(Guid jobAdId)
        {
            return GetJobAdView(jobAdId);
        }

        [EnsureAuthorized(UserType.Member, Reason = "Apply")]
        public ActionResult Apply(Guid jobAdId)
        {
            var member = CurrentMember;

            var jobAd = _memberJobAdViewsQuery.GetMemberJobAdView(member, jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            // Stop direct browsing to this page for old jobs.

            if (jobAd.Status == JobAdStatus.Closed)
                return RedirectToUrl(jobAd.GenerateJobAdUrl(), true);

            var jobPoster = _employersQuery.GetEmployer(jobAd.PosterId);
            if (jobPoster == null)
                return NotFound("job poster", "id", jobAd.PosterId);

            return View(GetJobAdModel(CurrentMember, jobAd, jobPoster));
        }

        private ActionResult GetJobAdView(Guid jobAdId)
        {
            var member = CurrentMember;

            var jobAd = _memberJobAdViewsQuery.GetMemberJobAdView(member, jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            var jobPoster = _employersQuery.GetEmployer(jobAd.PosterId);
            if (jobPoster == null)
                return NotFound("job poster", "id", jobAd.PosterId);

            return View(GetJobAdModel(member, jobAd, jobPoster));
        }
    }
}