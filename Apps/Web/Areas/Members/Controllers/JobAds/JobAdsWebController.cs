using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Users.Members.Queries;
using LinkMe.Apps.Asp.Caches;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Mvc.Models.Binders;
using LinkMe.Apps.Services.External.Commands;
using LinkMe.Apps.Services.JobAds.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Anonymous;
using LinkMe.Domain.Users.Anonymous.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Areas.Errors.Routes;
using LinkMe.Web.Areas.Members.Models.JobAds;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    public class JobAdsWebController
        : JobAdsController
    {
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IMemberJobAdViewsQuery _memberJobAdViewsQuery;
        private readonly IAnonymousUsersQuery _anonymousUsersQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IVisitorStatusQuery _visitorStatusQuery;
        private readonly IInternalApplicationsCommand _internalApplicationsCommand;
        private readonly IMemberApplicationsQuery _memberApplicationsQuery;
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand;
        private readonly ISendApplicationsCommand _sendApplicationsCommand;
        private Prompts _prompts;

        public JobAdsWebController(IJobAdsQuery jobAdsQuery, IJobAdViewsQuery jobAdViewsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IExecuteJobAdSearchCommand executeJobAdSearchCommand, IAnonymousUsersQuery anonymousUsersQuery, IEmployersQuery employersQuery, ICandidatesQuery candidatesQuery, ICandidateResumeFilesQuery candidateResumeFilesQuery, IFilesQuery filesQuery, ICacheManager cacheManager, IMemberStatusQuery memberStatusQuery, IResumesQuery resumesQuery, IVisitorStatusQuery visitorStatusQuery, IExternalJobAdsQuery externalJobAdsQuery, IInternalApplicationsCommand internalApplicationsCommand, IMemberApplicationsQuery memberApplicationsQuery, IJobAdApplicationSubmissionsCommand jobAdApplicationSubmissionsCommand, ISendApplicationsCommand sendApplicationsCommand, IJobAdFlagListsQuery jobAdFlagListsQuery, IJobAdFoldersQuery jobAdFoldersQuery, IJobAdFoldersCommand jobAdFoldersCommand)
            : base(jobAdViewsQuery, memberJobAdViewsQuery, executeJobAdSearchCommand, candidatesQuery, candidateResumeFilesQuery, filesQuery, cacheManager, memberStatusQuery, resumesQuery, externalJobAdsQuery, jobAdFlagListsQuery, jobAdFoldersQuery, jobAdFoldersCommand)
        {
            _jobAdsQuery = jobAdsQuery;
            _memberJobAdViewsQuery = memberJobAdViewsQuery;
            _anonymousUsersQuery = anonymousUsersQuery;
            _employersQuery = employersQuery;
            _visitorStatusQuery = visitorStatusQuery;
            _internalApplicationsCommand = internalApplicationsCommand;
            _memberApplicationsQuery = memberApplicationsQuery;
            _jobAdApplicationSubmissionsCommand = jobAdApplicationSubmissionsCommand;
            _sendApplicationsCommand = sendApplicationsCommand;
        }

        [EnsureHttps]
        public ActionResult JobAd(Guid jobAdId)
        {
            var member = CurrentMember;

            var jobAd = _memberJobAdViewsQuery.GetMemberJobAdView(member, jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            // Check the status of the job ad.

            var result = CheckStatus(jobAd);
            if (result != null)
                return result;

            // Check url.

            result = EnsureUrl(jobAd.GenerateJobAdUrl());
            if (result != null)
                return result;

            // Do some initial checks.

            var jobPoster = _employersQuery.GetEmployer(jobAd.PosterId);
            if (jobPoster == null)
                return NotFound("job poster", "id", jobAd.PosterId);

            // Need to check that this job ad is allowed to be seen through a community portal.

            result = CheckCommunity(jobAd, jobPoster, member);
            if (result != null)
                return result;

            var model = GetJobAdModel(member, jobAd, jobPoster);
            model.VisitorStatus = Prompts.GetVisitorStatus(member);
            return View(model);
        }

        public ActionResult LoggedInUserApplyArea(Guid jobAdId)
        {
            var member = CurrentMember;

            var jobAd = _memberJobAdViewsQuery.GetMemberJobAdView(member, jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            var jobPoster = _employersQuery.GetEmployer(jobAd.PosterId);
            if (jobPoster == null)
                return NotFound("job poster", "id", jobAd.PosterId);

            return View("LoggedInUserApply", GetJobAdModel(CurrentMember, jobAd, jobPoster));
        }

        public ActionResult JobAdQuestions(Guid jobAdId, Guid applicationId)
        {
            var model = GetJobAdQuestionsModel(jobAdId, applicationId);
            if (model == null)
                return NotFound("application", "id", applicationId);

            try
            {
                // If there are no questions then process now.

                if (model.JobAd.Integration.ApplicationRequirements.Questions.IsNullOrEmpty())
                    return SendApplication(model, new List<ApplicationAnswer>());
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new JobAdsErrorHandler());
            }

            return View(model);
        }

        [HttpPost, ActionName("JobAdQuestions")]
        public ActionResult PostJobAdQuestions(Guid jobAdId, Guid applicationId, string coverLetter)
        {
            var model = GetJobAdQuestionsModel(jobAdId, applicationId);
            if (model == null)
                return NotFound("application", "id", applicationId);

            try
            {
                // If the cover letter has been included, i.e. a value has been sent with the request then update it.

                if (ValueProvider.GetValue("CoverLetter") != null)
                    model.Application.CoverLetterText = coverLetter;

                // Send the application to the partner and then internally submit it.

                var answers = GetAnswers(model.JobAd);
                _internalApplicationsCommand.UpdateApplication(model.JobAd, model.Application, answers);

                return SendApplication(model, answers);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new JobAdsErrorHandler());
            }

            return View(model);
        }

        public ActionResult JobAdApplied(Guid jobAdId)
        {
            var member = CurrentMember;

            var jobAd = _memberJobAdViewsQuery.GetMemberJobAdView(member, jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            // stop direct browsing to this page for old jobs.
            if (jobAd.Status == JobAdStatus.Closed)
                return RedirectToUrl(jobAd.GenerateJobAdUrl(), true);

            var jobPoster = _employersQuery.GetEmployer(jobAd.PosterId);
            if (jobPoster == null)
                return NotFound("job poster", "id", jobAd.PosterId);

            return View(GetJobAdModel(CurrentMember, jobAd, jobPoster));
        }

        public ActionResult RedirectToExternal(Guid jobAdId, Guid? applicationId)
        {
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
            if (jobAd == null || string.IsNullOrEmpty(jobAd.Integration.ExternalApplyUrl))
                return NotFound("job ad", "id", jobAdId);

            if (applicationId == null)
            {
                // Try to get the current user's application for this job ad.

                var user = CurrentUser;
                if (user != null)
                {
                    var application = _memberApplicationsQuery.GetInternalApplication(user.Id, jobAdId);
                    if (application != null)
                        applicationId = application.Id;
                }
            }

            HttpContext.Response.AppendHeader("Referrer", jobAd.GenerateJobAdUrl().AbsoluteUri);
            return RedirectToUrl(jobAd.GetExternalApplyUrl(applicationId));
        }

        [EnsureAuthorized(UserType.Member, Reason = "Apply")]
        public ActionResult Apply(Guid jobAdId)
        {
            var member = CurrentMember;

            var jobAd = _memberJobAdViewsQuery.GetMemberJobAdView(member, jobAdId);
            if (jobAd == null)
                return NotFound("job ad", "id", jobAdId);

            // stop direct browsing to this page for old jobs.
            if (jobAd.Status == JobAdStatus.Closed)
                return RedirectToUrl(jobAd.GenerateJobAdUrl(), true);

            var jobPoster = _employersQuery.GetEmployer(jobAd.PosterId);
            if (jobPoster == null)
                return NotFound("job poster", "id", jobAd.PosterId);

            return View(GetJobAdModel(CurrentMember, jobAd, jobPoster));
        }

        private ActionResult CheckStatus(JobAdView jobAd)
        {
            if (jobAd.Status == JobAdStatus.Draft || jobAd.Status == JobAdStatus.Deleted)
                return NotFound("job ad", "id", jobAd.Id);

            return null;
        }

        private JobAdQuestionsModel GetJobAdQuestionsModel(Guid jobAdId, Guid applicationId)
        {
            var member = CurrentMember;
            var jobAd = _memberJobAdViewsQuery.GetJobAdView(jobAdId);
            if (jobAd == null)
                return null;

            var application = _memberApplicationsQuery.GetInternalApplication(applicationId);
            if (application == null)
                return null;

            // Get the user who is doing the applying.

            AnonymousContact anonymousContact = null;

            if (member == null)
            {
                if (CurrentAnonymousUser != null)
                {
                    anonymousContact = GetAnonymousContact(applicationId);
                    if (anonymousContact == null)
                        return null;
                }
            }
            else
            {
                if (member.Id != application.ApplicantId)
                    return null;
            }

            if (member == null && anonymousContact == null)
                return null;

            return new JobAdQuestionsModel
            {
                JobAd = jobAd,
                Application = application,
                Member = member,
                AnonymousContact = anonymousContact,
            };
        }

        private AnonymousContact GetAnonymousContact(Guid applicationId)
        {
            var application = _memberApplicationsQuery.GetInternalApplication(applicationId);
            return application == null
                ? null
                : _anonymousUsersQuery.GetContact(application.ApplicantId);
        }

        private IList<ApplicationAnswer> GetAnswers(JobAdView jobAd)
        {
            var answers = new List<ApplicationAnswer>();

            if (jobAd.Integration.ApplicationRequirements != null && jobAd.Integration.ApplicationRequirements.Questions != null)
            {
                foreach (var question in jobAd.Integration.ApplicationRequirements.Questions)
                {
                    // Each question will be sent with the request so find the answer.

                    var value = new GenericModelBinder().BindModel(ControllerContext, new ModelBindingContext { FallbackToEmptyPrefix = true, ValueProvider = ValueProvider, ModelName = "Question" + question.Id });
                    if (question is MultipleChoiceQuestion)
                        answers.Add(new MultipleChoiceAnswer { Question = question, Value = value == null ? null : value.ToString() });
                    else
                        answers.Add(new TextAnswer { Question = question, Value = value == null ? null : value.ToString() });
                }
            }

            return answers;
        }

        private ActionResult SendApplication(JobAdQuestionsModel model, IEnumerable<ApplicationAnswer> answers)
        {
            _sendApplicationsCommand.SendApplication(model.Application, answers);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(model.JobAd, model.Application);

            // If there is no external url then redirect straight to the applied page.

            if (string.IsNullOrEmpty(model.JobAd.Integration.ExternalApplyUrl))
                return RedirectToRoute(JobAdsRoutes.JobAdApplied, new { jobAdId = model.JobAd.Id });
            return RedirectToUrl(GetExternalApplyUrl(model.JobAd.Integration.ExternalApplyUrl, model.Member != null ? (ICommunicationUser)model.Member : model.AnonymousContact));
        }

        private ActionResult CheckCommunity(IJobAd jobAd, IEmployer jobPoster, IRegisteredUser member)
        {
            // Make sure if this is a community job ad, i.e. it has an affiliate,
            // then it is only accessible by logged in members from that community.

            var affiliateId = jobPoster.Organisation.AffiliateId;
            if (affiliateId != null)
            {
                if (member == null)
                    return RedirectToLoginUrl();
                
                if (member.AffiliateId != affiliateId)
                    return RedirectToRoute(ErrorsRoutes.ObjectNotFound, new { type = "job ad", propertyName = "jobAdId", propertyValue = jobAd.Id });
            }

            return null;
        }

        private static ReadOnlyUrl GetExternalApplyUrl(string externalApplyUrl, ICommunicationRecipient user)
        {
            var url = new Url(externalApplyUrl);
            if (!string.IsNullOrEmpty(url.QueryString["email"]))
                url.QueryString["email"] = user.EmailAddress;
            return url;
        }

        private Prompts Prompts
        {
            get { return _prompts ?? (_prompts = new Prompts(Session, _visitorStatusQuery)); }
        }
    }
}
