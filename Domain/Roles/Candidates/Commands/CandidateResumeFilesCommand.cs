using System;
using System.IO;
using System.Linq;
using LinkMe.Domain.Files;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Candidates.Commands
{
    public class CandidateResumeFilesCommand
        : ICandidateResumeFilesCommand
    {
        private readonly ICandidatesRepository _repository;

        private static readonly string[] ValidExtensions = new[] { "doc", "html", "htm", "txt", "rtf", "pdf", "docx" };

        public CandidateResumeFilesCommand(ICandidatesRepository repository)
        {
            _repository = repository;
        }

        void ICandidateResumeFilesCommand.ValidateFile(string fileName, FileContents fileContents)
        {
            // Check the file extension.

            var extension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
                throw new InvalidResumeExtensionException(extension);

            if (!ValidExtensions.Contains(extension.TrimStart('.').ToLower()))
                throw new InvalidResumeExtensionException(extension);

            // Must not be too large.

            if (fileContents.Length > Constants.MaxResumeFileSize)
                throw new FileTooLargeException { MaxFileSize = Constants.MaxResumeFileSize };
        }

        void ICandidateResumeFilesCommand.CreateResumeFile(Guid candidateId, ResumeFileReference resumeFileReference)
        {
            resumeFileReference.Prepare();
            resumeFileReference.Validate();
            _repository.CreateResumeFile(candidateId, resumeFileReference);
        }

        void ICandidateResumeFilesCommand.UpdateLastUsedTime(ResumeFileReference resumeFileReference)
        {
            resumeFileReference.LastUsedTime = DateTime.Now;
            resumeFileReference.Validate();
            _repository.UpdateResumeFile(resumeFileReference);
        }
    }
}