using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Anonymous.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.JobAds;
using IMemberJobAdApplicationsCommand = LinkMe.Domain.Users.Members.JobAds.Commands.IInternalApplicationsCommand;
using IAnonymousJobAdApplicationsCommand = LinkMe.Domain.Users.Anonymous.JobAds.Commands.IInternalApplicationsCommand;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    public class ApplicationsApiController
        : MembersApiController
    {
        private readonly IMemberJobAdViewsQuery _memberJobAdViewsQuery;
        private readonly IMemberJobAdApplicationsCommand _memberJobAdApplicationsCommand;
        private readonly IAnonymousJobAdApplicationsCommand _anonymousJobAdApplicationsCommand;
        private readonly IAnonymousUsersCommand _anonymousUsersCommand;
        private readonly IFilesQuery _filesQuery;

        public ApplicationsApiController(IMemberJobAdViewsQuery memberJobAdViewsQuery, IMemberJobAdApplicationsCommand memberJobAdApplicationsCommand, IAnonymousJobAdApplicationsCommand anonymousJobAdApplicationsCommand, IAnonymousUsersCommand anonymousUsersCommand, IFilesQuery filesQuery)
        {
            _memberJobAdViewsQuery = memberJobAdViewsQuery;
            _memberJobAdApplicationsCommand = memberJobAdApplicationsCommand;
            _anonymousJobAdApplicationsCommand = anonymousJobAdApplicationsCommand;
            _anonymousUsersCommand = anonymousUsersCommand;
            _filesQuery = filesQuery;
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Member)]
        public ActionResult ApplyWithLastUsedResume(Guid jobAdId, string coverLetterText)
        {
            try
            {
                var jobAd = GetJobAd(jobAdId, JobAdProcessing.ManagedExternally, JobAdProcessing.ManagedInternally);
                if (jobAd == null)
                    return JsonNotFound("job ad");

                // Process the application.

                var applicationId = jobAd.Application.Questions.IsNullOrEmpty()
                    ? _memberJobAdApplicationsCommand.SubmitApplicationWithLastUsedResume(CurrentMember, jobAd, coverLetterText)
                    : _memberJobAdApplicationsCommand.CreateApplicationWithLastUsedResume(CurrentMember, jobAd, coverLetterText);
                return Json(new JsonApplicationResponseModel {Id = applicationId});
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Member)]
        public ActionResult ApplyWithUploadedResume(Guid jobAdId, Guid fileReferenceId, bool useForProfile, string coverLetterText)
        {
            try
            {
                var jobAd = GetJobAd(jobAdId, JobAdProcessing.ManagedExternally, JobAdProcessing.ManagedInternally);
                if (jobAd == null)
                    return JsonNotFound("job ad");

                // Check the file exists.

                var fileReference = _filesQuery.GetFileReference(fileReferenceId);
                if (fileReference == null)
                    return JsonNotFound("file");

                // Process the application.

                var applicationId = jobAd.Application.Questions.IsNullOrEmpty()
                    ? _memberJobAdApplicationsCommand.SubmitApplicationWithResume(CurrentMember, jobAd, fileReferenceId, useForProfile, coverLetterText)
                    : _memberJobAdApplicationsCommand.CreateApplicationWithResume(CurrentMember, jobAd, fileReferenceId, useForProfile, coverLetterText);
                return Json(new JsonApplicationResponseModel { Id = applicationId });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Member)]
        public ActionResult ApplyWithProfile(Guid jobAdId, string coverLetterText)
        {
            try
            {
                var jobAd = GetJobAd(jobAdId, JobAdProcessing.ManagedExternally, JobAdProcessing.ManagedInternally);
                if (jobAd == null)
                    return JsonNotFound("job ad");

                // Process the application.

                var applicationId = jobAd.Application.Questions.IsNullOrEmpty()
                    ? _memberJobAdApplicationsCommand.SubmitApplication(CurrentMember, jobAd, coverLetterText)
                    : _memberJobAdApplicationsCommand.CreateApplication(CurrentMember, jobAd, coverLetterText);
                return Json(new JsonApplicationResponseModel { Id = applicationId });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureNotAuthorized]
        public ActionResult Apply(Guid jobAdId, Guid fileReferenceId, ContactDetailsModel contactDetails)
        {
            try
            {
                var jobAd = GetJobAd(jobAdId, JobAdProcessing.ManagedExternally);
                if (jobAd == null)
                    return JsonNotFound("job ad");

                // Check the file exists.

                var fileReference = _filesQuery.GetFileReference(fileReferenceId);
                if (fileReference == null)
                    return JsonNotFound("file");

                // Submit the application.

                var contact = _anonymousUsersCommand.CreateContact(CurrentAnonymousUser, new ContactDetails { EmailAddress = contactDetails.EmailAddress, FirstName = contactDetails.FirstName, LastName = contactDetails.LastName });
                var applicationId = _anonymousJobAdApplicationsCommand.Submit(contact, jobAd, fileReferenceId);
                return Json(new JsonApplicationResponseModel { Id = applicationId });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        private IJobAd GetJobAd(Guid jobAdId, params JobAdProcessing[] validProcessings)
        {
            var jobAd = _memberJobAdViewsQuery.GetJobAdView(jobAdId);
            if (jobAd == null)
                return null;

            // Also make sure it is the right sort of ad.

            return validProcessings.Contains(jobAd.Processing)
                ? jobAd
                : null;
        }
    }
}
