using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Management.Areas.Communications.Models;
using LinkMe.Apps.Management.Areas.Communications.Models.Members;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.Reports.Roles.JobAds.Queries;
using LinkMe.Query.Reports.Users.Employers.Queries;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Queries;

namespace LinkMe.Apps.Management.Areas.Communications.Controllers
{
    public class MembersController
        : CommunicationsController
    {
        private readonly IFilesQuery _filesQuery;
        private readonly IEmployerMemberAccessReportsQuery _employerMemberAccessReportsQuery;
        private readonly IMembersQuery _membersQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly IMemberStatusQuery _memberStatusQuery;
        private readonly IJobAdSearchesQuery _jobAdSearchesQuery;
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobAdReportsQuery _jobAdReportsQuery;
        private readonly IMemberApplicationsQuery _memberApplicationsQuery;
        private readonly IEmailVerificationsCommand _emailVerificationsCommand;
        private readonly IEmailVerificationsQuery _emailVerificationsQuery;
        private const int MaxSuggestedJobCount = 3;

        public MembersController(IFilesQuery filesQuery, IEmployerMemberAccessReportsQuery employerMemberAccessReportsQuery, IMembersQuery membersQuery, ICandidatesQuery candidatesQuery, IResumesQuery resumesQuery, IMemberStatusQuery memberStatusQuery, IJobAdSearchesQuery jobAdSearchesQuery, IExecuteJobAdSearchCommand executeJobAdSearchCommand, IJobAdsQuery jobAdsQuery, IJobAdReportsQuery jobAdReportsQuery, IMemberApplicationsQuery memberApplicationsQuery, IEmailVerificationsCommand emailVerificationsCommand, IEmailVerificationsQuery emailVerificationsQuery)
        {
            _filesQuery = filesQuery;
            _employerMemberAccessReportsQuery = employerMemberAccessReportsQuery;
            _membersQuery = membersQuery;
            _candidatesQuery = candidatesQuery;
            _resumesQuery = resumesQuery;
            _memberStatusQuery = memberStatusQuery;
            _jobAdSearchesQuery = jobAdSearchesQuery;
            _executeJobAdSearchCommand = executeJobAdSearchCommand;
            _jobAdsQuery = jobAdsQuery;
            _jobAdReportsQuery = jobAdReportsQuery;
            _memberApplicationsQuery = memberApplicationsQuery;
            _emailVerificationsCommand = emailVerificationsCommand;
            _emailVerificationsQuery = emailVerificationsQuery;
        }

        public ActionResult Photo(Guid id)
        {
            // Get the file reference.

            var file = _filesQuery.GetFileReference(id);
            if (file == null)
                return HttpNotFound();
            
            // Write the file out.

            return File(_filesQuery.OpenFile(file), file.MediaType);
        }

        public ActionResult Newsletter(CommunicationsContext context)
        {
            var member = _membersQuery.GetMember(context.UserId);
            if (member == null || !member.IsEnabled || !member.IsActivated)
                return HttpNotFound();

            var candidate = _candidatesQuery.GetCandidate(context.UserId);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

            var model = CreateModel<NewsletterModel>(context);
            model.Member = member;
            model.Candidate = candidate;
            model.Resume = resume;

            var lastMonth = new DateTimeRange(DateTime.Today.AddMonths(-1), DateTime.Today);

            model.TotalJobAds = _jobAdReportsQuery.GetCreatedJobAds(lastMonth);
            model.TotalViewed = _employerMemberAccessReportsQuery.GetMemberViewings(context.UserId, lastMonth);
            model.ProfilePercentComplete = _memberStatusQuery.GetPercentComplete(member, candidate, resume);
            var execution = _executeJobAdSearchCommand.SearchSuggested(member, null, new Range(0, MaxSuggestedJobCount));

            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(execution.Results.JobAdIds).ToDictionary(j => j.Id, j => j);
            model.SuggestedJobs = (from i in execution.Results.JobAdIds
                                   where jobAds.ContainsKey(i)
                                   select jobAds[i]).ToList();

            return View(model);
        }

        public ActionResult Reengagement(CommunicationsContext context)
        {
            var member = _membersQuery.GetMember(context.UserId);
            if (member == null)
                return HttpNotFound();

            var candidate = _candidatesQuery.GetCandidate(context.UserId); 
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

            var model = CreateModel<ReengagementModel>(context);
            model.Member = member;
            model.Candidate = candidate;

            if (!member.IsActivated)
            {
                // Create a new activation if needed.

                var emailAddress = member.GetBestEmailAddress().Address;
                var emailVerification = _emailVerificationsQuery.GetEmailVerification(member.Id, emailAddress);
                if (emailVerification == null)
                {
                    emailVerification = new EmailVerification { EmailAddress = emailAddress, UserId = member.Id };
                    _emailVerificationsCommand.CreateEmailVerification(emailVerification);
                }

                model.ActivationCode = emailVerification.VerificationCode;
            }

            model.ProfilePercentComplete = _memberStatusQuery.GetPercentComplete(member, candidate, resume);
            var execution = _executeJobAdSearchCommand.SearchSuggested(member, null, new Range(0, MaxSuggestedJobCount));

            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(execution.Results.JobAdIds).ToDictionary(j => j.Id, j => j);
            model.SuggestedJobs = (from i in execution.Results.JobAdIds
                                   where jobAds.ContainsKey(i)
                                   select jobAds[i]).ToList();

            model.JobSearch = GetJobSearchModel(member, candidate);

            var lastWeek = new DateTimeRange(DateTime.Today.AddDays(-7), DateTime.Today);
            model.TotalContactsLastWeek = _employerMemberAccessReportsQuery.GetMemberAccesses(lastWeek);

            var lastMonth = new DateTimeRange(DateTime.Today.AddMonths(-1), DateTime.Today);
            model.TotalContactsLastMonth = _employerMemberAccessReportsQuery.GetMemberAccesses(lastMonth);
            model.TotalViewed = _employerMemberAccessReportsQuery.GetMemberViewings(context.UserId, lastMonth);

            return View(model);
        }

        public ActionResult Edm(CommunicationsContext context)
        {
            var member = _membersQuery.GetMember(context.UserId);
            if (member == null)
                return HttpNotFound();

            var model = CreateModel<NewsletterModel>(context);
            model.Member = member;
            return View(model);
        }

        private JobSearchModel GetJobSearchModel(IMember member, ICandidate candidate)
        {
            // Find the most recent job they applied for.

            var applications = _memberApplicationsQuery.GetApplications(member.Id).OrderByDescending(a => a.CreatedTime).ToList();
            if (applications.Count != 0)
            {
                var model = GetSimilarJobSearchModel(member, applications[0].PositionId);
                if (model != null)
                    return model;
            }

            // Check their most recent search.

            var searches = _jobAdSearchesQuery.GetJobAdSearchExecutions(member.Id, 1);
            if (searches.Count != 0 && searches[0].Criteria != null && !searches[0].Criteria.IsEmpty)
            {
                var model = GetSearchJobSearchModel(member, searches[0].Criteria);
                if (model != null)
                    return model;
            }

            // Desired job title.

            if (!string.IsNullOrEmpty(candidate.DesiredJobTitle))
            {
                var criteria = new JobAdSearchCriteria { AdTitle = candidate.DesiredJobTitle };
                var model = GetSearchJobSearchModel(member, criteria);
                if (model != null)
                    return model;
            }

            return null;
        }

        private JobSearchModel GetSearchJobSearchModel(IMember member, JobAdSearchCriteria criteria)
        {
            var execution = _executeJobAdSearchCommand.Search(member, criteria, new Range(0, 1));
            if (execution.Results.TotalMatches < 2)
                return null;

            return new JobSearchModel { TotalMatches = execution.Results.TotalMatches, Description = criteria.GetDisplayText() };
        }

        private JobSearchModel GetSimilarJobSearchModel(IMember member, Guid jobAdId)
        {
            var execution = _executeJobAdSearchCommand.SearchSimilar(member, jobAdId, new Range(0, 1));
            if (execution.Results.TotalMatches < 2)
                return null;

            var jobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(jobAdId);
            if (jobAd == null)
                return null;

            return new JobSearchModel { TotalMatches = execution.Results.TotalMatches, Description = jobAd.Title };
        }
    }
}
