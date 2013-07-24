using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Members.JobAds.Commands
{
    public class InternalApplicationsCommand
        : IInternalApplicationsCommand
    {
        private readonly IApplicationsCommand _applicationsCommand;
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand;
        private readonly IJobAdApplicationSubmissionsQuery _jobAdApplicationSubmissionsQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly ICandidateResumesCommand _candidateResumesCommand;
        private readonly ICandidateResumeFilesCommand _candidateResumeFilesCommand;
        private readonly ICandidateResumeFilesQuery _candidateResumeFilesQuery;
        private readonly IParseResumesCommand _parseResumesCommand;
        private readonly IFilesQuery _filesQuery;

        public InternalApplicationsCommand(IApplicationsCommand applicationsCommand, IJobAdApplicationSubmissionsCommand jobAdApplicationSubmissionsCommand, IJobAdApplicationSubmissionsQuery jobAdApplicationSubmissionsQuery, ICandidatesQuery candidatesQuery, ICandidateResumesCommand candidateResumesCommand, ICandidateResumeFilesCommand candidateResumeFilesCommand, ICandidateResumeFilesQuery candidateResumeFilesQuery, IParseResumesCommand parseResumesCommand, IFilesQuery filesQuery)
        {
            _applicationsCommand = applicationsCommand;
            _jobAdApplicationSubmissionsCommand = jobAdApplicationSubmissionsCommand;
            _jobAdApplicationSubmissionsQuery = jobAdApplicationSubmissionsQuery;
            _candidatesQuery = candidatesQuery;
            _candidateResumesCommand = candidateResumesCommand;
            _candidateResumeFilesCommand = candidateResumeFilesCommand;
            _candidateResumeFilesQuery = candidateResumeFilesQuery;
            _parseResumesCommand = parseResumesCommand;
            _filesQuery = filesQuery;
        }

        Guid IInternalApplicationsCommand.CreateApplication(IMember member, IJobAd jobAd, string coverLetterText)
        {
            return Create(member.Id, jobAd, coverLetterText, a => SetResume(member.Id, a));
        }

        Guid IInternalApplicationsCommand.CreateApplicationWithLastUsedResume(IMember member, IJobAd jobAd, string coverLetterText)
        {
            return Create(member.Id, jobAd, coverLetterText, a => a.ResumeFileId = GetLastUsedResumeFileId(member.Id));
        }

        Guid IInternalApplicationsCommand.CreateApplicationWithResume(IMember member, IJobAd jobAd, Guid fileReferenceId, bool useForProfile, string coverLetterText)
        {
            return Create(member.Id, jobAd, coverLetterText, a => a.ResumeFileId = GetResumeFileId(member.Id, fileReferenceId, useForProfile));
        }

        void IInternalApplicationsCommand.UpdateApplication(IJobAd jobAd, InternalApplication application, IEnumerable<ApplicationAnswer> answers)
        {
            // Check if the cover letter is required.

            if (jobAd.Application != null)
            {
                if (jobAd.Application.IncludeCoverLetter
                    && string.IsNullOrEmpty(application.CoverLetterText))
                    throw new ValidationErrorsException(new RequiredValidationError("CoverLetter"));

                // Check if the answers are required.

                if (jobAd.Application.Questions != null)
                {
                    foreach (var question in jobAd.Application.Questions)
                    {
                        if (question is TextQuestion && question.IsRequired)
                        {
                            // Look for an answer.

                            var found = false;
                            if (answers != null)
                            {
                                var questionId = question.Id;
                                var answer = (from a in answers where a.Question.Id == questionId select a).SingleOrDefault();
                                if (answer != null && !string.IsNullOrEmpty(answer.Value))
                                    found = true;
                            }

                            if (!found)
                                throw new ValidationErrorsException(new RequiredValidationError(question.Text));
                        }
                    }
                }
            }

            _applicationsCommand.UpdateApplication(application);
        }

        Guid IInternalApplicationsCommand.SubmitApplication(IMember member, IJobAd jobAd, string coverLetterText)
        {
            return Submit(member.Id, jobAd, coverLetterText, a => SetResume(member.Id, a));
        }

        Guid IInternalApplicationsCommand.SubmitApplicationWithLastUsedResume(IMember member, IJobAd jobAd, string coverLetterText)
        {
            return Submit(member.Id, jobAd, coverLetterText, a => a.ResumeFileId = GetLastUsedResumeFileId(member.Id));
        }

        Guid IInternalApplicationsCommand.SubmitApplicationWithResume(IMember member, IJobAd jobAd, Guid fileReferenceId, bool useForProfile, string coverLetterText)
        {
            return Submit(member.Id, jobAd, coverLetterText, a => a.ResumeFileId = GetResumeFileId(member.Id, fileReferenceId, useForProfile));
        }

        private void SetResume(Guid memberId, InternalApplication application)
        {
            application.ResumeId = GetProfileResumeId(memberId);
        }

        private Guid Create(Guid memberId, IJobAd jobAd, string coverLetterText, Action<InternalApplication> setApplication)
        {
            var application = CreateApplication(memberId, jobAd.Id, coverLetterText, setApplication);
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            return application.Id;
        }

        private Guid Submit(Guid memberId, IJobAd jobAd, string coverLetterText, Action<InternalApplication> setApplication)
        {
            var application = CreateApplication(memberId, jobAd.Id, coverLetterText, setApplication);
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            return application.Id;
        }

        private InternalApplication CreateApplication(Guid memberId, Guid jobAdId, string coverLetterText, Action<InternalApplication> setApplication)
        {
            // Check that the job hasn't already been applied for.

            if (_jobAdApplicationSubmissionsQuery.HasSubmittedApplication(memberId, jobAdId))
                throw new AlreadyAppliedException();

            // Create the application.

            var application = new InternalApplication
            {
                PositionId = jobAdId,
                ApplicantId = memberId,
                CoverLetterText = coverLetterText,
            };
            setApplication(application);

            return application;
        }

        private Guid? GetProfileResumeId(Guid memberId)
        {
            var candidate = _candidatesQuery.GetCandidate(memberId);
            if (candidate == null || candidate.ResumeId == null)
                throw new NoResumeException();
            return candidate.ResumeId.Value;
        }

        private Guid GetLastUsedResumeFileId(Guid memberId)
        {
            var lastUsedResumeFile = _candidateResumeFilesQuery.GetLastUsedResumeFile(memberId);
            if (lastUsedResumeFile == null)
                throw new NoResumeFileException();

            // Update it to indicate that it is being used again.

            _candidateResumeFilesCommand.UpdateLastUsedTime(lastUsedResumeFile);
            return lastUsedResumeFile.FileReferenceId;
        }

        private Guid GetResumeFileId(Guid memberId, Guid fileReferenceId, bool useForProfile)
        {
            // Make sure the id is valid.

            var fileReference = _filesQuery.GetFileReference(fileReferenceId);
            if (fileReference == null)
                throw new NoResumeFileException();

            // Create or update the resume file reference.

            var resumeFileReference = _candidateResumeFilesQuery.GetResumeFile(memberId, fileReference.Id);
            if (resumeFileReference != null)
            {
                _candidateResumeFilesCommand.UpdateLastUsedTime(resumeFileReference);
            }
            else
            {
                resumeFileReference = new ResumeFileReference { FileReferenceId = fileReference.Id };
                _candidateResumeFilesCommand.CreateResumeFile(memberId, resumeFileReference);
            }

            if (useForProfile)
                CreateResume(memberId, fileReference);

            return fileReferenceId;
        }

        private void CreateResume(Guid memberId, FileReference fileReference)
        {
            // If the resume is no good then ignore (correct decision?).

            var candidate = _candidatesQuery.GetCandidate(memberId);
            var parsedResume = _parseResumesCommand.ParseResume(fileReference);
            if (parsedResume != null && parsedResume.Resume != null && !parsedResume.Resume.IsEmpty)
                _candidateResumesCommand.CreateResume(candidate, parsedResume.Resume.Clone(), fileReference);
        }
    }
}
