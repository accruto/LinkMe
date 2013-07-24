using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Asp.Files;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using Linkme.Domain.Users.Employers.Contacts;
using LinkMe.Domain.Users.Employers.Contacts.Commands;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Files;
using LinkMe.Web.Areas.Employers.Models.Candidates;

namespace LinkMe.Web.Areas.Employers.Controllers.Candidates
{
    [EnsureHttps, ApiEnsureAuthorized(UserType.Employer)]
    public class CandidatesApiController
        : EmployersApiController
    {
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand;
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery;
        private readonly IEmployerMemberContactsCommand _employerMemberContactsCommand;
        private readonly IEmployerResumeFilesQuery _employerResumeFilesQuery;
        private readonly IEmailsCommand _emailsCommand;
        private readonly IJobAdApplicantsQuery _jobAdApplicantsQuery;

        public CandidatesApiController(IEmployerMemberViewsCommand employerMemberViewsCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IEmployerMemberContactsCommand employerMemberContactsCommand, IEmployerResumeFilesQuery employerResumeFilesQuery, IEmailsCommand emailsCommand, IJobAdApplicantsQuery jobAdApplicantsQuery)
        {
            _employerMemberViewsCommand = employerMemberViewsCommand;
            _employerMemberViewsQuery = employerMemberViewsQuery;
            _employerMemberContactsCommand = employerMemberContactsCommand;
            _employerResumeFilesQuery = employerResumeFilesQuery;
            _emailsCommand = emailsCommand;
            _jobAdApplicantsQuery = jobAdApplicantsQuery;
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult PhoneNumbers(Guid[] candidateIds)
        {
            try
            {
                var employer = CurrentEmployer;
                var views = _employerMemberViewsQuery.GetProfessionalViews(employer, candidateIds);
                _employerMemberViewsCommand.AccessMembers(ActivityContext.Channel.App, employer, views, MemberAccessReason.PhoneNumberViewed);

                return Json(new JsonResponseModel());
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult Unlock(Guid[] candidateIds)
        {
            try
            {
                var employer = CurrentEmployer;
                var views = _employerMemberViewsQuery.GetProfessionalViews(employer, candidateIds);
                _employerMemberViewsCommand.AccessMembers(ActivityContext.Channel.App, employer, views, MemberAccessReason.Unlock);

                return Json(new JsonResponseModel());
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult DownloadResumes(Guid[] candidateIds)
        {
            try
            {
                // This call is to support the UI.  No resume is actually downloaded,
                // rather checks are made to ensure that the users have resumes
                // and the employer has access to them.  After this method indicates
                // everything is OK a call to download the resumes themselves will proceed.

                // Get the employer.

                var employer = CurrentEmployer;

                // Get the members and unlock them ready for the download.

                var views = _employerMemberViewsQuery.GetProfessionalViews(employer, candidateIds);
                _employerMemberViewsCommand.AccessMembers(ActivityContext.Channel.App, employer, views, MemberAccessReason.ResumeDownloaded);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult SendResumes(ResumeMimeType? mimeType, Guid[] candidateIds)
        {
            try
            {
                // Get the employer.

                var employer = CurrentEmployer;

                // Get the views to unlock the members.

                var professionalViews = _employerMemberViewsQuery.GetProfessionalViews(employer, candidateIds);
                _employerMemberViewsCommand.AccessMembers(ActivityContext.Channel.App, employer, professionalViews, MemberAccessReason.ResumeSent);

                // Get them again because unlocking will change what the employer can see.

                var views = _employerMemberViewsQuery.GetEmployerMemberViews(employer, candidateIds);

                EmployerEmail email;
                mimeType = GetMimeType(mimeType, candidateIds);
                switch (mimeType.Value)
                {
                    case ResumeMimeType.Doc:
                        email = new CandidateResumeEmail(employer, views[candidateIds[0]]);
                        email.AddAttachments(new[] { GetResumeFile(views[candidateIds[0]]) });
                        break;

                    default:
                        email = new CandidateResumesEmail(employer, views);
                        email.AddAttachments(new[] { GetResumeFile(views) });
                        break;
                }

                // Email it.

                _emailsCommand.TrySend(email);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult Attach(Guid[] attachmentIds, HttpPostedFileBase file)
        {
            try
            {
                // Get the employer.

                var employer = CurrentEmployer;

                var fileName = Path.GetFileName(file.FileName);
                var attachment = _employerMemberContactsCommand.CreateMessageAttachment(employer, attachmentIds, new HttpPostedFileContents(file), fileName);

                // Must send text/plain mime type for legacy browsers.

                return Json(new JsonFileModel
                {
                    Id = attachment.Id,
                    Name = fileName,
                    Size = file.ContentLength,
                }, MediaType.Text);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel(), MediaType.Text);
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult Detach(Guid attachmentId)
        {
            try
            {
                // Get the employer.

                _employerMemberContactsCommand.DeleteMessageAttachment(CurrentEmployer, attachmentId);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult CheckCanSendMessages(Guid[] candidateIds)
        {
            try
            {
                // Get the employer.

                var employer = CurrentEmployer;

                // Get the members.

                var views = _employerMemberViewsQuery.GetProfessionalViews(employer, candidateIds);
                _employerMemberContactsCommand.CheckCanContactMembers(ActivityContext.Channel.App, employer, views);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost, ValidateInput(false)]
        public ActionResult SendMessages(ContactMemberMessage message, Guid[] candidateIds, Guid[] attachmentIds)
        {
            try
            {
                // Get the employer.

                var employer = CurrentEmployer;

                // Get the members.

                var views = _employerMemberViewsQuery.GetProfessionalViews(employer, candidateIds);

                message.AttachmentIds = attachmentIds;

                _employerMemberContactsCommand.ContactMembers(ActivityContext.Channel.App, employer, views, message);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        /// <summary>
        /// Send rejection emails to specified candidateId(s)
        /// Messages will only be sent to those candidates who currently have a rejected status and 
        /// who applied for the job themselves (i.e. they have an application)
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <param name="jobAdId">The jobAd this rejection email is for</param>
        /// <param name="candidateIds">The candidates to send to</param>
        /// <returns>Empty JsonResponseModel</returns>
        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost, ValidateInput(false)]
        public ActionResult SendRejections(RejectionMemberMessage message, Guid jobAdId, Guid[] candidateIds)
        {
            try
            {
                // Get the employer.

                var employer = CurrentEmployer;

                // Ensure all candidateIds have applications. At this stage they *should*
                var validCandidateIds = (from a in _jobAdApplicantsQuery.GetApplicationsByPositionId(jobAdId)
                    where candidateIds.Contains(a.ApplicantId)
                    select a.ApplicantId);
                
                // Get the members.

                var views = _employerMemberViewsQuery.GetProfessionalViews(employer, validCandidateIds);

                _employerMemberContactsCommand.RejectMembers(employer, views, message);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        private static ResumeMimeType GetMimeType(ResumeMimeType? mimeType, ICollection<Guid> candidateIds)
        {
            if (candidateIds.Count > 1)
                return ResumeMimeType.Zip;
            return mimeType == null ? ResumeMimeType.Doc : mimeType.Value;
        }

        private ContentAttachment GetResumeFile(EmployerMemberView view)
        {
            var resumeFile = _employerResumeFilesQuery.GetResumeFile(view);

            // Save the contents into a stream.

            var stream = new MemoryStream();
            stream.Write(resumeFile.Contents, 0, resumeFile.Contents.Length);
            stream.Position = 0;
            return new ContentAttachment(stream, resumeFile.Name, MediaType.GetMediaTypeFromExtension(Path.GetExtension(resumeFile.Name), MediaType.Text));
        }

        private ContentAttachment GetResumeFile(IEnumerable<EmployerMemberView> views)
        {
            var resumeFile = _employerResumeFilesQuery.GetResumeFile(views);
            var fileName = resumeFile.Name;

            // Save the contents of the zip file into a stream.

            var stream = new MemoryStream();
            resumeFile.Save(stream);
            stream.Position = 0;
            return new ContentAttachment(stream, fileName, MediaType.GetMediaTypeFromExtension(Path.GetExtension(fileName), MediaType.Text));
        }
    }
}
