using System;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Members.JobAds;

namespace LinkMe.Domain.Users.Anonymous.JobAds.Commands
{
    public class InternalApplicationsCommand
        : IInternalApplicationsCommand
    {
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand;
        private readonly IJobAdApplicationSubmissionsQuery _jobAdApplicationSubmissionsQuery;

        public InternalApplicationsCommand(IJobAdApplicationSubmissionsCommand jobAdApplicationSubmissionsCommand, IJobAdApplicationSubmissionsQuery jobAdApplicationSubmissionsQuery)
        {
            _jobAdApplicationSubmissionsCommand = jobAdApplicationSubmissionsCommand;
            _jobAdApplicationSubmissionsQuery = jobAdApplicationSubmissionsQuery;
        }

        Guid IInternalApplicationsCommand.Submit(AnonymousContact contact, IJobAd jobAd, Guid fileReferenceId)
        {
            // Check that the job hasn't already been applied for.

            if (_jobAdApplicationSubmissionsQuery.HasSubmittedApplication(contact.Id, jobAd.Id))
                throw new AlreadyAppliedException();

            // Create the application.

            var application = new InternalApplication
            {
                PositionId = jobAd.Id,
                ApplicantId = contact.Id,
                ResumeFileId = fileReferenceId,
            };

            // Submit it.

            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            return application.Id;
        }
    }
}
