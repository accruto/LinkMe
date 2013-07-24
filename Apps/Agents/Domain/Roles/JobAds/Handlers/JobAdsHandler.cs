using System;
using System.IO;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Members.Queries;

namespace LinkMe.Apps.Agents.Domain.Roles.JobAds.Handlers
{
    public class JobAdsHandler
        : IJobAdsHandler
    {
        private readonly IMemberApplicationsQuery _memberApplicationsQuery;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobAdProcessingQuery _jobAdProcessingQuery;
        private readonly IMembersQuery _membersQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly IResumeFilesQuery _resumeFilesQuery;
        private readonly IFilesQuery _filesQuery;
        private readonly IEmailsCommand _emailsCommand;

        public JobAdsHandler(IMemberApplicationsQuery memberApplicationsQuery, IJobAdsQuery jobAdsQuery, IJobAdProcessingQuery jobAdProcessingQuery, IMembersQuery membersQuery, IEmployersQuery employersQuery, IEmployerMemberViewsQuery employerMemberViewsQuery, IResumesQuery resumesQuery, IResumeFilesQuery resumeFilesQuery, IFilesQuery filesQuery, IEmailsCommand emailsCommand)
        {
            _memberApplicationsQuery = memberApplicationsQuery;
            _jobAdsQuery = jobAdsQuery;
            _jobAdProcessingQuery = jobAdProcessingQuery;
            _membersQuery = membersQuery;
            _employersQuery = employersQuery;
            _employerMemberViewsQuery = employerMemberViewsQuery;
            _resumesQuery = resumesQuery;
            _resumeFilesQuery = resumeFilesQuery;
            _filesQuery = filesQuery;
            _emailsCommand = emailsCommand;
        }

        void IJobAdsHandler.OnApplicationSubmitted(Guid applicationId)
        {
            // Gather.

            var application = _memberApplicationsQuery.GetInternalApplication(applicationId);
            if (application == null)
                return;

            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(application.PositionId);
            if (jobAd == null)
                return;

            if (!ShouldSend(jobAd, application))
                return;

            var member = _membersQuery.GetMember(application.ApplicantId);
            if (member == null)
                return;

            // Send.

            if (application.ResumeFileId != null)
                SendEmailWithResumeFile(application, member, jobAd);
            else
                SendEmailWithResume(application, member, jobAd);
        }

        private void SendEmailWithResume(InternalApplication application, Member member, JobAd jobAd)
        {
            var employer = _employersQuery.GetEmployer(jobAd.PosterId);
            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);
            var resume = application.ResumeId == null ? new Resume() : _resumesQuery.GetResume(application.ResumeId.Value);
            var resumeFile = _resumeFilesQuery.GetResumeFile(view, resume);
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(resumeFile.Contents));

            // Send the email.

            _emailsCommand.TrySend(new JobApplicationEmail(member, application, jobAd, stream, resumeFile.FileName));
        }

        private void SendEmailWithResumeFile(InternalApplication application, ICommunicationUser member, JobAd jobAd)
        {
            var fileReference = _filesQuery.GetFileReference(application.ResumeFileId.Value);
            var stream = _filesQuery.OpenFile(fileReference);

            // Send the email.

            _emailsCommand.TrySend(new JobApplicationEmail(member, application, jobAd, stream, fileReference.FileName));
        }

        private bool ShouldSend(JobAdEntry jobAd, InternalApplication application)
        {
            // Do not send an email to the job poster if the job ad is external.
            // It is assumed that submitting the external application will send them a notification.

            return _jobAdProcessingQuery.GetJobAdProcessing(jobAd) == JobAdProcessing.ManagedInternally
                && !application.IsPending;
        }
    }
}
