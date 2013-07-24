using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public class ExpiringCreditsEmailTask
        : CommunicationsTask
    {
        private static readonly EventSource EventSource = new EventSource<ExpiringCreditsEmailTask>();
        private readonly IEmployerCreditsQuery _employerCreditsQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IOrganisationsQuery _organisationsQuery;
        private readonly IRecruitersQuery _recruitersQuery;
        private readonly IAdministratorsQuery _administratorsQuery;

        public ExpiringCreditsEmailTask(IEmailsCommand emailsCommand, IEmployerCreditsQuery employerCreditsQuery, IEmployersQuery employersQuery, IOrganisationsQuery organisationsQuery, IRecruitersQuery recruitersQuery, IAdministratorsQuery administratorsQuery)
            : base(EventSource, emailsCommand)
        {
            _employerCreditsQuery = employerCreditsQuery;
            _employersQuery = employersQuery;
            _organisationsQuery = organisationsQuery;
            _recruitersQuery = recruitersQuery;
            _administratorsQuery = administratorsQuery;
        }

        public override void ExecuteTask()
        {
            // Send emails for those employers and organisations who expire tomorrow, in a week and in a month.

            Execute<ApplicantCredit>();
            Execute<ContactCredit>();
            Execute<JobAdCredit>();
        }

        private void Execute<TCredit>()
            where TCredit : Credit
        {
            var today = DateTime.Now.Date;
            var expiryDate = today.AddDays(30);
            var notIfLastSentLaterThanThis = today.AddMonths(-1);

            // Send to those recipients whose last expiration matches the expiry date.

            var allocations = _employerCreditsQuery.GetEffectiveExpiringAllocations<TCredit>(expiryDate);
            Execute<TCredit>(allocations, notIfLastSentLaterThanThis);
        }

        private void Execute<TCredit>(IEnumerable<KeyValuePair<Guid, Allocation>> allocations, DateTime notIfLastSentLaterThanThis)
        {
            const string method = "ExecuteEmployers";

            try
            {
                // Get all employers who fit into the list.

                foreach (var allocation in allocations)
                {
                    var recipients = GetRecipients(allocation.Key);

                    EventSource.Raise(Event.Information, method, string.Format("Sending expiry reminder emails to {0} employers...", recipients.Item1.Count));

                    var total = Execute<TCredit>(allocation.Value, recipients, notIfLastSentLaterThanThis);

                    EventSource.Raise(Event.Information, method, string.Format("Finished sending {0} out of a total of {1} expiry reminder emails to employers.", total, recipients.Item1.Count));
                }
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.Error, method, string.Format("Failed to send expiry reminder emails to employers."), ex, new StandardErrorHandler());
            }
        }

        private int Execute<TCredit>(Allocation allocation, Tuple<IList<Employer>, Administrator> recipients, DateTime notIfLastSentLaterThanThis)
        {
            const string method = "Execute";

            var total = 0;
            foreach (var recipient in recipients.Item1)
            {
                // Emails will only be sent to those employers whose organisations have limited credits.

                try
                {
                    if (typeof(TCredit) == typeof(ContactCredit))
                    {
                        if (allocation.RemainingQuantity != null)
                        {
                            if (allocation.RemainingQuantity.Value > 0)
                                _emailsCommand.TrySend(new ExpiringContactCreditsEmail(recipient, recipients.Item2, allocation.RemainingQuantity.Value), notIfLastSentLaterThanThis);
                        }
                        else
                        {
                            if (recipients.Item2 != null)
                                _emailsCommand.TrySend(new ExpiringUnlimitedContactCreditsEmail(recipient, recipients.Item2), notIfLastSentLaterThanThis);
                        }
                    }
                    else if (typeof(TCredit) == typeof(ApplicantCredit))
                    {
                        if (allocation.RemainingQuantity != null && allocation.RemainingQuantity.Value > 0)
                            _emailsCommand.TrySend(new ExpiringApplicantCreditsEmail(recipient, recipients.Item2, allocation.RemainingQuantity.Value), notIfLastSentLaterThanThis);
                    }
                    else if (typeof(TCredit) == typeof(JobAdCredit))
                    {
                        if (allocation.RemainingQuantity != null && allocation.RemainingQuantity.Value > 0)
                            _emailsCommand.TrySend(new ExpiringJobAdCreditsEmail(recipient, recipients.Item2, allocation.RemainingQuantity.Value), notIfLastSentLaterThanThis);
                    }
                    ++total;
                }
                catch (Exception ex)
                {
                    EventSource.Raise(Event.Error, method, string.Format("Failed to send a expiry reminder email to employer '{0}'", recipient.Id), ex, new StandardErrorHandler());
                }
            }

            return total;
        }

        private Tuple<IList<Employer>, Administrator> GetRecipients(Guid ownerId)
        {
            // Look for employer.

            var employer = _employersQuery.GetEmployer(ownerId);
            if (employer != null)
                return new Tuple<IList<Employer>, Administrator>(new[] { employer }, null);

            // Look for organisation.

            var organisation = _organisationsQuery.GetOrganisation(ownerId);
            if (organisation == null)
                return new Tuple<IList<Employer>, Administrator>(new List<Employer>(), null);

            // If unverified send to all employers.

            if (!organisation.IsVerified)
                return new Tuple<IList<Employer>, Administrator>(_employersQuery.GetEmployers(_recruitersQuery.GetRecruiters(ownerId)), null);

            // Look for contact.

            var verifiedOrganisation = (VerifiedOrganisation)organisation;
            if (verifiedOrganisation.ContactDetails != null && verifiedOrganisation.ContactDetails.EmailAddress != null && verifiedOrganisation.ContactDetails.FirstName != null && verifiedOrganisation.ContactDetails.LastName != null)
                return new Tuple<IList<Employer>, Administrator>(new List<Employer>
                {
                    new Employer
                    {
                        Id = organisation.Id,
                        IsEnabled = true,
                        EmailAddress = new EmailAddress { Address = verifiedOrganisation.ContactDetails.EmailAddress, IsVerified = true },
                        FirstName = verifiedOrganisation.ContactDetails.FirstName,
                        LastName = verifiedOrganisation.ContactDetails.LastName,
                    }
                },
                _administratorsQuery.GetAdministrator(verifiedOrganisation.AccountManagerId));

            // Send to all employers.

            return new Tuple<IList<Employer>, Administrator>(_employersQuery.GetEmployers(_recruitersQuery.GetRecruiters(ownerId)), _administratorsQuery.GetAdministrator(verifiedOrganisation.AccountManagerId));
        }
    }
}
