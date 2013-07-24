using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.Profiles
{
    [EnsureHttps, EnsureAuthorized(UserType.Member)]
    public class ProfileFilesController
        : ViewController
    {
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly ICandidateResumeFilesQuery _candidateResumeFilesQuery;
        private readonly IResumeFilesQuery _resumeFilesQuery;
        private readonly IFilesQuery _filesQuery;

        public ProfileFilesController(ICandidatesQuery candidatesQuery, IResumesQuery resumesQuery, ICandidateResumeFilesQuery candidateResumeFilesQuery, IResumeFilesQuery resumeFilesQuery, IFilesQuery filesQuery)
        {
            _candidatesQuery = candidatesQuery;
            _resumesQuery = resumesQuery;
            _candidateResumeFilesQuery = candidateResumeFilesQuery;
            _resumeFilesQuery = resumeFilesQuery;
            _filesQuery = filesQuery;
        }

        public ActionResult Download()
        {
            try
            {
                var member = CurrentMember;
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

                // Download.

                var resumeFile = _resumeFilesQuery.GetResumeFile(member, member, candidate, resume);
                return DocFile(new DocFile(resumeFile.FileName, resumeFile.Contents));
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View("Error");
        }

        public ActionResult DownloadResume(Guid fileReferenceId)
        {
            try
            {
                var member = CurrentMember;

                // Make sure the file is on of the member's resume files.

                var resumeFileReference = _candidateResumeFilesQuery.GetResumeFile(member.Id, fileReferenceId);
                if (resumeFileReference == null)
                    return HttpNotFound();

                var fileReference = _filesQuery.GetFileReference(fileReferenceId);
                if (fileReference == null)
                    return HttpNotFound();

                return File(_filesQuery.OpenFile(fileReference), fileReference.MediaType, fileReference.FileName);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View("Error");
        }

        public ActionResult Photo()
        {
            var member = CurrentMember;
            if (member.PhotoId == null)
                return HttpNotFound();

            var fileReference = _filesQuery.GetFileReference(member.PhotoId.Value);
            if (fileReference == null)
                return HttpNotFound();

            // Return the file.

            return File(_filesQuery.OpenFile(fileReference), fileReference.MediaType);
        }
    }
}
