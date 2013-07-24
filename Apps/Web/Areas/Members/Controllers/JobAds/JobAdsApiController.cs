using System;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications;
using LinkMe.Apps.Agents.Users.Members.Queries;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Members.Models.JobAds;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    public class JobAdsApiController
        : MembersApiController
    {
        private static readonly EventSource EventSource = new EventSource<JobAdsApiController>();
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobAdViewsCommand _jobAdViewsCommand;
        private readonly IApplicationsCommand _applicationsCommand;
        private readonly IVisitorStatusQuery _visitorStatusQuery;
        private readonly IEmailsCommand _emailsCommand;
        private Prompts _prompts;

        public JobAdsApiController(IJobAdsQuery jobAdsQuery, IJobAdViewsCommand jobAdViewsCommand, IApplicationsCommand applicationsCommand, IVisitorStatusQuery visitorStatusQuery, IEmailsCommand emailsCommand)
        {
            _jobAdsQuery = jobAdsQuery;
            _jobAdViewsCommand = jobAdViewsCommand;
            _applicationsCommand = applicationsCommand;
            _visitorStatusQuery = visitorStatusQuery;
            _emailsCommand = emailsCommand;
        }

        [HttpPost]
        public ActionResult EmailJobAds(EmailJobAdsModel emailJobAdsModel)
        {
            try
            {
                // If there is a current member then it needs to come from them.

                var member = CurrentMember;
                if (member != null)
                {
                    emailJobAdsModel.FromName = member.FullName;
                    emailJobAdsModel.FromEmailAddress = member.GetBestEmailAddress().Address;
                }

                emailJobAdsModel.Prepare();
                emailJobAdsModel.Validate();

                foreach (var jobAdId in emailJobAdsModel.JobAdIds)
                {
                    var jobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(jobAdId);
                    if (jobAd == null)
                        return JsonNotFound("job ad");

                    // Send to each recipient.

                    foreach (var to in emailJobAdsModel.Tos)
                    {
                        var email = new SendJobToFriendEmail(to.ToEmailAddress, to.ToName,
                            emailJobAdsModel.FromEmailAddress, emailJobAdsModel.FromName, jobAd, null);
                        _emailsCommand.TrySend(email);
                    }
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new JobAdsErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult ExternallyApplied(Guid jobAdId)
        {
            const string method = "ExternallyApplied";

            try
            {
                var jobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(jobAdId);
                if (jobAd == null)
                    throw new ValidationErrorsException(new NotFoundValidationError("JobAdId", jobAdId));
                if (jobAd.Integration.IntegratorUserId == null)
                    throw new ValidationErrorsException(new RequiredValidationError("IntegratorUserId"));

                // Get the applicant id.

                var user = CurrentUser;
                var applicantId = user == null ? (Guid?)null : user.Id;
                if (applicantId == null)
                    throw new ValidationErrorsException(new RequiredValidationError("ApplicantId"));

                // Record the external application.

                _applicationsCommand.CreateApplication(new ExternalApplication { PositionId = jobAdId, ApplicantId = applicantId.Value });

                Prompts.AddApplication();
            }
            catch (UserException ex)
            {
                EventSource.Raise(Event.Error, method, "Cannot create an external application for job ad '" + jobAdId + "'.", ex, new JobAdsErrorHandler());
                ModelState.AddModelError(ex, new JobAdsErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult Viewed(Guid jobAdId)
        {
            try
            {
                var user = CurrentUser;
                _jobAdViewsCommand.ViewJobAd(user == null ? (Guid?) null : user.Id, jobAdId);

                Prompts.AddView();
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new JobAdsErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        private Prompts Prompts
        {
            get
            {
                if (_prompts == null)
                    _prompts = new Prompts(Session, _visitorStatusQuery);
                return _prompts;
            }
        }
    }
}
