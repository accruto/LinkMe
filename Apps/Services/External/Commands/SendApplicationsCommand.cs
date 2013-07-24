using System.Collections.Generic;
using System.IO;
using System.Text;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Apps.Services.External.JobG8.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Anonymous.Queries;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Members.Queries;

namespace LinkMe.Apps.Services.External.Commands
{
    public class SendApplicationsCommand
        : ISendApplicationsCommand
    {
        private readonly IMembersQuery _membersQuery;
        private readonly IAnonymousUsersQuery _anonymousUsersQuery;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IFilesQuery _filesQuery;
        private readonly IResumeFilesQuery _resumeFilesQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly ISendJobG8Command _sendJobG8Command;

        public SendApplicationsCommand(IMembersQuery membersQuery, IAnonymousUsersQuery anonymousUsersQuery, IJobAdsQuery jobAdsQuery, IFilesQuery filesQuery, IResumeFilesQuery resumeFilesQuery, ICandidatesQuery candidatesQuery, IResumesQuery resumesQuery, ISendJobG8Command sendJobG8Command)
        {
            _membersQuery = membersQuery;
            _anonymousUsersQuery = anonymousUsersQuery;
            _jobAdsQuery = jobAdsQuery;
            _filesQuery = filesQuery;
            _resumeFilesQuery = resumeFilesQuery;
            _candidatesQuery = candidatesQuery;
            _resumesQuery = resumesQuery;
            _sendJobG8Command = sendJobG8Command;
        }

        void ISendApplicationsCommand.SendApplication(InternalApplication application, IEnumerable<ApplicationAnswer> answers)
        {
            var jobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(application.PositionId);

            // Look for a member first.

            var member = _membersQuery.GetMember(application.ApplicantId);
            if (member != null)
            {
                // Send.

                if (application.ResumeFileId != null)
                    SendResumeFile(application, member, jobAd, answers);
                else
                    SendResume(application, member, jobAd, answers);
            }
            else
            {
                var user = _anonymousUsersQuery.GetContact(application.ApplicantId);
                if (user != null && application.ResumeFileId != null)
                    SendResumeFile(application, user, jobAd, answers);
            }
        }

        private void SendResume(InternalApplication application, Member member, JobAdEntry jobAd, IEnumerable<ApplicationAnswer> answers)
        {
            // Generate the resume file and send it.

            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = application.ResumeId == null || candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            var view = new EmployerMemberView(member, candidate, resume, null, ProfessionalContactDegree.Applicant, false, false);
            var resumeFile = _resumeFilesQuery.GetResumeFile(view, resume);

            using (var stream = new MemoryStream())
            {
                var buffer = Encoding.ASCII.GetBytes(resumeFile.Contents);
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;
                _sendJobG8Command.SendApplication(member, jobAd, resumeFile.FileName, new StreamFileContents(stream), application, answers);
            }
        }

        private void SendResumeFile(InternalApplication application, ICommunicationUser member, JobAdEntry jobAd, IEnumerable<ApplicationAnswer> answers)
        {
            // Open the file and send it.

            var resumeFileReference = _filesQuery.GetFileReference(application.ResumeFileId.Value);
            using (var stream = _filesQuery.OpenFile(resumeFileReference))
            {
                _sendJobG8Command.SendApplication(member, jobAd, resumeFileReference.FileName, new StreamFileContents(stream), application, answers);
            }
        }
    }
}
