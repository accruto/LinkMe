using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Files;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Files;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Api.Models.Resumes;

namespace LinkMe.Web.Areas.Api.Controllers
{
    public class ResumesApiController
        : ApiController
    {
        private readonly IFilesCommand _filesCommand;
        private readonly IFilesQuery _filesQuery;
        private readonly ICandidateResumeFilesCommand _candidateResumeFilesCommand;
        private readonly IParseResumesCommand _parseResumesCommand;
        private readonly IParsedResumesCommand _parsedResumesCommand;

        public ResumesApiController(IFilesCommand filesCommand, IFilesQuery filesQuery, ICandidateResumeFilesCommand candidateResumeFilesCommand, IParseResumesCommand parseResumesCommand, IParsedResumesCommand parsedResumesCommand)
        {
            _filesCommand = filesCommand;
            _filesQuery = filesQuery;
            _candidateResumeFilesCommand = candidateResumeFilesCommand;
            _parseResumesCommand = parseResumesCommand;
            _parsedResumesCommand = parsedResumesCommand;
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                if (file == null)
                    throw new ValidationErrorsException(new RequiredValidationError("file"));

                var fileName = Path.GetFileName(file.FileName);
                var fileContents = new HttpPostedFileContents(file);

                // Do a check first.

                _candidateResumeFilesCommand.ValidateFile(fileName, fileContents);

                // Save the resume.

                var fileReferenceId = _filesCommand.SaveFile(FileType.Resume, fileContents, fileName).Id;

                // Must send text/plain mime type for legacy browsers.

                return Json(new JsonResumeModel
                {
                    Id = fileReferenceId,
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

        [HttpPost]
        public ActionResult Parse(Guid fileReferenceId)
        {
            Guid? parsedResumeId = null;

            try
            {
                // Load the resume.

                var fileReference = _filesQuery.GetFileReference(fileReferenceId);
                if (fileReference == null)
                    return JsonNotFound("file");

                var stream = _filesQuery.OpenFile(fileReference);
                var parsedResume = _parseResumesCommand.ParseResume(stream, fileReference.FileName);

                // Save the parsed resume.

                _parsedResumesCommand.CreateParsedResume(parsedResume);
                parsedResumeId = parsedResume.Id;

                if (parsedResume.Resume == null || parsedResume.Resume.IsEmpty)
                    throw new InvalidResumeException();
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonParsedResumeModel
            {
                Id = parsedResumeId,
            });
        }
    }
}