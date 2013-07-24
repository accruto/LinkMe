using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Domain.Users.Employers.JobAds;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Applicants.Commands;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Areas.Employers.Models.Candidates;

namespace LinkMe.Web.Areas.Employers.Controllers.JobAds
{
    public class JobAdsApiController
        : EmployersApiController
    {
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobAdApplicantsCommand _jobAdApplicantsCommand;
        private readonly IJobAdApplicantsQuery _jobAdApplicantsQuery;

        public JobAdsApiController(IJobAdsQuery jobAdsQuery, IJobAdApplicantsCommand jobAdApplicantsCommand, IJobAdApplicantsQuery jobAdApplicantsQuery)
        {
            _jobAdsQuery = jobAdsQuery;
            _jobAdApplicantsCommand = jobAdApplicantsCommand;
            _jobAdApplicantsQuery = jobAdApplicantsQuery;
        }

        [HttpPost]
        public ActionResult JobAds()
        {
            var employer = CurrentEmployer;

            if (employer == null)
                return Json(new JsonJobAdsModel {JobAds = new List<JobAdApplicantsModel>()});

            // Get the open and closed job ads.

            var jobAdIds = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open).Concat(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Closed));
            var jobAds = _jobAdsQuery.GetJobAds<JobAdEntry>(jobAdIds);
 
            // Get the counts for each job ad through each applicant list.

            var applicantLists = (from j in jobAds select _jobAdApplicantsQuery.GetApplicantList(employer, j)).ToList();
            var counts = _jobAdApplicantsQuery.GetApplicantCounts(employer, from a in applicantLists where a != null select a);

            return Json(new JsonJobAdsModel
            {
                JobAds = (from m in
                              (from j in jobAds
                               orderby j.Status, j.CreatedTime descending 
                               select new JobAdApplicantsModel
                               {
                                   Id = j.Id,
                                   Title = string.IsNullOrEmpty(j.Integration.ExternalReferenceId) ? j.Title : j.Integration.ExternalReferenceId + ": " + j.Title,
                                   Status = j.Status.ToString(),
                                   ApplicantCounts = new ApplicantCountsModel
                                   {
                                       ShortListed = j.GetShortlistedCount(applicantLists, counts),
                                       New = j.GetNewCount(applicantLists, counts),
                                       Rejected = j.GetRejectedCount(applicantLists, counts)
                                   }
                               })
                          select m).ToList(),
            });
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult ShortlistCandidates(Guid jobAdId, Guid[] candidateIds)
        {
            try
            {
                // Look for the jobAd.

                var employer = CurrentEmployer;

                var jobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(jobAdId);
                if (jobAd == null)
                    return JsonNotFound("job ad");

                // Add candidates.

                var statuses = _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, candidateIds);

                return BuildModelFromStatus(jobAd, statuses);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult RejectCandidates(Guid jobAdId, Guid[] candidateIds)
        {
            try
            {
                // Look for the jobAd.

                var employer = CurrentEmployer;

                var jobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(jobAdId);
                if (jobAd == null)
                    return JsonNotFound("job ad");

                // Remove candidates.

                var statuses = _jobAdApplicantsCommand.RejectApplicants(employer, jobAd, candidateIds);

                return BuildModelFromStatus(jobAd, statuses);

            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult RemoveCandidates(Guid jobAdId, Guid[] candidateIds)
        {
            try
            {
                // Look for the jobAd.

                var employer = CurrentEmployer;

                var jobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(jobAdId);
                if (jobAd == null)
                    return JsonNotFound("job ad");

                // Remove candidates.

                var statuses = _jobAdApplicantsCommand.RemoveApplicants(employer, jobAd, candidateIds);

                return BuildModelFromStatus(jobAd, statuses);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult UndoShortlistCandidates(Guid jobAdId, Guid[] candidateIds, ApplicantStatus? previousStatus)
        {
            try
            {
                // Look for the jobAd.

                var employer = CurrentEmployer;

                var jobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(jobAdId);
                if (jobAd == null)
                    return JsonNotFound("job ad");

                // Add candidates.

                var statuses = _jobAdApplicantsCommand.UndoShortlistApplicants(employer, jobAd, candidateIds, previousStatus);

                return BuildModelFromStatus(jobAd, statuses);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult UndoRejectCandidates(Guid jobAdId, Guid[] candidateIds, ApplicantStatus previousStatus)
        {
            try
            {
                // Look for the jobAd.

                var employer = CurrentEmployer;

                var jobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(jobAdId);
                if (jobAd == null)
                    return JsonNotFound("job ad");

                // Remove candidates.

                var statuses = _jobAdApplicantsCommand.UndoRejectApplicants(employer, jobAd, candidateIds, previousStatus);

                return BuildModelFromStatus(jobAd, statuses);

            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult UndoRemoveCandidates(Guid jobAdId, Guid[] candidateIds)
        {
            try
            {
                // Look for the jobAd.

                var employer = CurrentEmployer;

                var jobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(jobAdId);
                if (jobAd == null)
                    return JsonNotFound("job ad");

                // Remove candidates.

                var statuses = _jobAdApplicantsCommand.UndoRemoveApplicants(employer, jobAd, candidateIds);

                return BuildModelFromStatus(jobAd, statuses);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        private ActionResult BuildModelFromStatus(JobAdEntry jobAd, IDictionary<ApplicantStatus, int> statuses)
        {
            return Json(new JsonJobAdModel
            {
                JobAd = new JobAdApplicantsModel
                {
                    Id = jobAd.Id,
                    Status = jobAd.Status.ToString(),
                    Title = jobAd.Title,
                    ApplicantCounts = new ApplicantCountsModel
                    {
                        ShortListed = statuses[ApplicantStatus.Shortlisted],
                        New = statuses[ApplicantStatus.New],
                        Rejected = statuses[ApplicantStatus.Rejected],
                    }
                }
            });
        }
    }
}