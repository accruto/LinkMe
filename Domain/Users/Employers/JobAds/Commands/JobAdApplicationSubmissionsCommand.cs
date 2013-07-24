using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.Applicants;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Domain.Users.Employers.JobAds.Commands
{
    public class JobAdApplicationSubmissionsCommand
        : JobAdApplicantsComponent, IJobAdApplicationSubmissionsCommand
    {
        private readonly IApplicationsCommand _applicationsCommand;
        private readonly IApplicationsQuery _applicationsQuery;
        private readonly IEmployerCreditsCommand _employerCreditsCommand;
        private readonly IEmployerCreditsQuery _employerCreditsQuery;
        private readonly IJobAdProcessingQuery _jobAdProcessingQuery;

        public JobAdApplicationSubmissionsCommand(IApplicationsCommand applicationsCommand, IApplicationsQuery applicationsQuery, IContenderListsCommand contenderListsCommand, IContenderListsQuery contenderListsQuery, IEmployerCreditsCommand employerCreditsCommand, IEmployerCreditsQuery employerCreditsQuery, IJobAdProcessingQuery jobAdProcessingQuery)
            : base(contenderListsCommand, contenderListsQuery)
        {
            _applicationsCommand = applicationsCommand;
            _applicationsQuery = applicationsQuery;
            _employerCreditsCommand = employerCreditsCommand;
            _employerCreditsQuery = employerCreditsQuery;
            _jobAdProcessingQuery = jobAdProcessingQuery;
        }

        void IJobAdApplicationSubmissionsCommand.CreateApplication(IJobAd jobAd, InternalApplication application)
        {
            // Create the application.

            var credits = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(new Employer { Id = jobAd.PosterId });

            application.PositionId = jobAd.Id;
            application.IsPositionFeatured = credits.RemainingQuantity > 0;

            var processing = _jobAdProcessingQuery.GetJobAdProcessing(jobAd);
            application.IsPending = processing != JobAdProcessing.ManagedInternally;

            _applicationsCommand.CreateApplication(application);
        }

        void IJobAdApplicationSubmissionsCommand.SubmitApplication(IJobAd jobAd, InternalApplication application)
        {
            var list = EnsureList(jobAd);

            if (_contenderListsQuery.IsListed(list.Id, null, application.ApplicantId))
            {
                // Ensure that the applicant has a 'new' status and has the application associated with it.

                _contenderListsCommand.ChangeStatus(list.Id, application.ApplicantId, ApplicantStatus.New);
                _contenderListsCommand.UpdateApplication(list.Id, application.ApplicantId, application.Id);
            }
            else
            {
                // Otherwise the application that is submitted must be added to the list for the job.

                var entry = new ApplicantListEntry
                {
                    ApplicantId = application.ApplicantId,
                    ApplicantStatus = ApplicantStatus.New,
                    ApplicationId = application.Id,
                };
                _contenderListsCommand.CreateEntry(list.Id, entry);

                // Exercise the applicant credit.

                _employerCreditsCommand.ExerciseApplicantCredit(application, jobAd);
            }

            // Fire events.

            var handlers = ApplicationSubmitted;
            if (handlers != null)
                handlers(this, new ApplicationSubmittedEventArgs(application.Id));
        }

        void IJobAdApplicationSubmissionsCommand.RevokeApplication(IJobAd jobAd, Guid applicantId)
        {
            // Need to delete the entry.

            _contenderListsCommand.DeleteEntry(jobAd.Id, applicantId);

            // Delete the application itself.

            var application = _applicationsQuery.GetApplication<InternalApplication>(applicantId, jobAd.Id, true);
            if (application != null)
                _applicationsCommand.DeleteApplication<InternalApplication>(application.Id);
        }

        [Publishes(PublishedEvents.ApplicationSubmitted)]
        public event EventHandler<ApplicationSubmittedEventArgs> ApplicationSubmitted;
    }
}