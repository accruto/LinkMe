using System;
using LinkMe.Domain.Files;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Domain.Roles.Candidates.Commands
{
    public class CandidateResumesCommand
        : ICandidateResumesCommand
    {
        private readonly ICandidatesRepository _repository;
        private readonly ICandidateResumeFilesCommand _candidateResumeFilesCommand;
        private readonly ICandidateResumeFilesQuery _candidateResumeFilesQuery;
        private readonly IResumesCommand _resumesCommand;
        private readonly IResumesQuery _resumesQuery;

        public CandidateResumesCommand(ICandidatesRepository repository, ICandidateResumeFilesCommand candidateResumeFilesCommand, ICandidateResumeFilesQuery candidateResumeFilesQuery, IResumesCommand resumesCommand, IResumesQuery resumesQuery)
        {
            _repository = repository;
            _candidateResumeFilesCommand = candidateResumeFilesCommand;
            _candidateResumeFilesQuery = candidateResumeFilesQuery;
            _resumesCommand = resumesCommand;
            _resumesQuery = resumesQuery;
        }

        void ICandidateResumesCommand.CreateResumeFile(Candidate candidate, FileReference fileReference)
        {
            CreateResumeFileReference(candidate.Id, fileReference.Id);
        }

        void ICandidateResumesCommand.CreateResume(Candidate candidate, Resume resume, FileReference fileReference)
        {
            CreateResume(candidate, resume, fileReference);
        }

        void ICandidateResumesCommand.CreateResume(Candidate candidate, Resume resume)
        {
            CreateResume(candidate, resume, null);
        }

        void ICandidateResumesCommand.UpdateResume(Candidate candidate, Resume resume)
        {
            _resumesCommand.UpdateResume(resume);

            // Fire events.

            var updatedHandlers = ResumeUpdated;
            if (updatedHandlers != null)
                updatedHandlers(this, new ResumeUpdatedEventArgs(candidate.Id, resume.Id));

            var handlers = ResumeEdited;
            if (handlers != null)
                handlers(this, new ResumeEventArgs(candidate.Id, resume.Id, false));
        }

        [Publishes(PublishedEvents.ResumeUploaded)]
        public event EventHandler<ResumeEventArgs> ResumeUploaded;

        [Publishes(PublishedEvents.ResumeReloaded)]
        public event EventHandler<ResumeEventArgs> ResumeReloaded;

        [Publishes(PublishedEvents.ResumeEdited)]
        public event EventHandler<ResumeEventArgs> ResumeEdited;

        [Publishes(PublishedEvents.ResumeUpdated)]
        public event EventHandler<ResumeUpdatedEventArgs> ResumeUpdated;

        private void CreateResume(Candidate candidate, Resume resume, FileReference fileReference)
        {
            // Create the file reference is needed.

            var resumeFileReference = fileReference == null
                ? null
                : CreateResumeFileReference(candidate.Id, fileReference.Id);

            var existingResumeId = candidate.ResumeId;
            if (existingResumeId == null)
            {
                // Create the resume.

                _resumesCommand.CreateResume(resume);
                candidate.ResumeId = resume.Id;

                // Add the resume to the candidate.

                _repository.AddResume(candidate.Id, resume.Id, resumeFileReference == null ? (Guid?)null : resumeFileReference.Id);
            }
            else
            {
                // Update the resume.

                var existingResume = _resumesQuery.GetResume(existingResumeId.Value);
                resume.Id = existingResumeId.Value;
                resume.CreatedTime = existingResume.CreatedTime;

                _resumesCommand.UpdateResume(resume);

                // Update the resume on the candidate.

                _repository.UpdateResume(candidate.Id, resume.Id, resumeFileReference == null ? (Guid?)null : resumeFileReference.Id);
            }

            // Fire events.

            var updatedHandlers = ResumeUpdated;
            if (updatedHandlers != null)
                updatedHandlers(this, new ResumeUpdatedEventArgs(candidate.Id, candidate.ResumeId));

            if (candidate.ResumeId != null)
            {
                if (fileReference != null)
                {
                    if (existingResumeId != null)
                    {
                        var handlers = ResumeReloaded;
                        if (handlers != null)
                            handlers(this, new ResumeEventArgs(candidate.Id, resume.Id, false));
                    }
                    else
                    {
                        var handlers = ResumeUploaded;
                        if (handlers != null)
                            handlers(this, new ResumeEventArgs(candidate.Id, resume.Id, true));
                    }
                }
                else
                {
                    var handlers = ResumeEdited;
                    if (handlers != null)
                        handlers(this, new ResumeEventArgs(candidate.Id, resume.Id, existingResumeId == null));
                }
            }
        }

        private ResumeFileReference CreateResumeFileReference(Guid candidateId, Guid fileReferenceId)
        {
            // Look for an existing one.

            var resumeFileReference = _candidateResumeFilesQuery.GetResumeFile(candidateId, fileReferenceId);
            if (resumeFileReference != null)
            {
                // Update it to reflect it has been used again.

                _candidateResumeFilesCommand.UpdateLastUsedTime(resumeFileReference);
                return resumeFileReference;
            }

            // Create a new one.

            var now = DateTime.Now;
            resumeFileReference = new ResumeFileReference
            {
                FileReferenceId = fileReferenceId,
                LastUsedTime = now,
                UploadedTime = now
            };
            _candidateResumeFilesCommand.CreateResumeFile(candidateId, resumeFileReference);
            return resumeFileReference;
        }
    }
}
