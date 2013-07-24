using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Employers.Controllers.Candidates
{
    public class CandidateFilesController
        : ViewController
    {
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand;
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery;
        private readonly IEmployerResumeFilesQuery _employerResumeFilesQuery;
        private readonly IFilesQuery _filesQuery;

        public CandidateFilesController(IEmployerMemberViewsCommand employerMemberViewsCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IEmployerResumeFilesQuery employerResumeFilesQuery, IFilesQuery filesQuery)
        {
            _employerMemberViewsCommand = employerMemberViewsCommand;
            _employerMemberViewsQuery = employerMemberViewsQuery;
            _employerResumeFilesQuery = employerResumeFilesQuery;
            _filesQuery = filesQuery;
        }

        [EnsureHttps, EnsureAuthorized(UserType.Employer)]
        public ActionResult Download(ResumeMimeType? mimeType, Guid[] candidateIds)
        {
            try
            {
                // Get the employer.

                var employer = CurrentEmployer;

                // Get the views to unlock the members.

                var professionalViews = _employerMemberViewsQuery.GetProfessionalViews(employer, candidateIds);
                _employerMemberViewsCommand.AccessMembers(ActivityContext.Channel.App, employer, professionalViews, MemberAccessReason.ResumeDownloaded);

                // Unlocking will change what the employer can see so get them again.

                var views = _employerMemberViewsQuery.GetEmployerMemberViews(employer, candidateIds);

                mimeType = GetMimeType(mimeType, candidateIds);
                switch (mimeType.Value)
                {
                    case ResumeMimeType.Doc:
                        return DocFile(_employerResumeFilesQuery.GetResumeFile(views[candidateIds[0]]));

                    default:
                        return ZipFile(_employerResumeFilesQuery.GetResumeFile(views));
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View("Error");
        }

        [EnsureHttps, EnsureAuthorized(UserType.Employer)]
        public ActionResult Photo(Guid candidateId)
        {
            // Get the employer.

            var employer = CurrentEmployer;

            // Get the view to the member.

            var view = _employerMemberViewsQuery.GetProfessionalView(employer, candidateId);
            if (view.PhotoId == null)
                return HttpNotFound();

            // Return the file.

            var fileReference = _filesQuery.GetFileReference(view.PhotoId.Value);
            if (fileReference == null)
                return HttpNotFound();

            return File(_filesQuery.OpenFile(fileReference), fileReference.MediaType);
        }

        private static ResumeMimeType GetMimeType(ResumeMimeType? mimeType, ICollection<Guid> candidateIds)
        {
            if (candidateIds.Count > 1)
                return ResumeMimeType.Zip;
            return mimeType == null ? ResumeMimeType.Doc : mimeType.Value;
        }
    }
}
