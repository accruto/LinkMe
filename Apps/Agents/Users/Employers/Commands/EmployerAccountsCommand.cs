using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Integration.LinkedIn;
using LinkMe.Domain.Roles.Integration.LinkedIn.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Queries;

namespace LinkMe.Apps.Agents.Users.Employers.Commands
{
    public class EmployerAccountsCommand
        : IEmployerAccountsCommand
    {
        private readonly IEmployersCommand _employersCommand;
        private readonly IEmployersQuery _employersQuery;
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IJobPostersCommand _jobPostersCommand;
        private readonly ILinkedInCommand _linkedInCommand;

        public EmployerAccountsCommand(IEmployersCommand employersCommand, IEmployersQuery employersQuery, ILoginCredentialsCommand loginCredentialsCommand, ILoginCredentialsQuery loginCredentialsQuery, IJobPostersCommand jobPostersCommand, ILinkedInCommand linkedInCommand)
        {
            _employersCommand = employersCommand;
            _employersQuery = employersQuery;
            _loginCredentialsCommand = loginCredentialsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
            _jobPostersCommand = jobPostersCommand;
            _linkedInCommand = linkedInCommand;
        }

        void IEmployerAccountsCommand.CreateEmployer(Employer employer, LoginCredentials credentials)
        {
            // Check login credentials.

            if (_loginCredentialsQuery.DoCredentialsExist(credentials))
                throw new DuplicateUserException();

            // Create the employer.

            CreateEmployer(employer);

            // Create the credentials.

            _loginCredentialsCommand.CreateCredentials(employer.Id, credentials);
        }

        void IEmployerAccountsCommand.CreateEmployer(Employer employer, LinkedInProfile profile)
        {
            // Create the employer.

            CreateEmployer(employer);

            // Update the profile.

            profile.UserId = employer.Id;
            _linkedInCommand.UpdateProfile(profile);
        }

        void IEmployerAccountsCommand.UpdateEmployer(Employer employer)
        {
            // Always make sure the phone number is a work phone number.

            if (employer.PhoneNumber != null)
                employer.PhoneNumber.Type = PhoneNumberType.Work;

            // Maintain the state of the email address.

            if (employer.EmailAddress != null)
            {
                var originalEmployer = _employersQuery.GetEmployer(employer.Id);
                employer.EmailAddress.IsVerified = originalEmployer.EmailAddress == null
                    || employer.EmailAddress.Address != originalEmployer.EmailAddress.Address
                    || originalEmployer.EmailAddress.IsVerified;
            }

            _employersCommand.UpdateEmployer(employer);
        }

        private void CreateEmployer(Employer employer)
        {
            // Set some defaults.

            employer.IsEnabled = true;
            employer.IsActivated = true;

            // Always make sure the email is verified when created.

            if (employer.EmailAddress != null)
                employer.EmailAddress.IsVerified = true;

            // Always make sure the phone number is a work phone number.

            if (employer.PhoneNumber != null)
                employer.PhoneNumber.Type = PhoneNumberType.Work;

            // Save.

            _employersCommand.CreateEmployer(employer);

            // Create a job poster for this account.

            var poster = new JobPoster { Id = employer.Id, SendSuggestedCandidates = false, ShowSuggestedCandidates = true };
            _jobPostersCommand.CreateJobPoster(poster);
        }
    }
}