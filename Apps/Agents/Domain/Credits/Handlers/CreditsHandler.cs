using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Domain.Users.Employers.Queries;

namespace LinkMe.Apps.Agents.Domain.Credits.Handlers
{
    public class CreditsHandler
        : ICreditsHandler
    {
        private readonly IEmailsCommand _emailsCommand;
        private readonly IEmployersQuery _employersQuery;
        private readonly IOrganisationsQuery _organisationsQuery;
        private readonly IRecruitersQuery _recruitersQuery;
        private readonly IAdministratorsQuery _administratorsQuery;
        private readonly IAllocationsCommand _allocationsCommand;
        private readonly IAllocationsQuery _allocationsQuery;
        private readonly ICreditsQuery _creditsQuery;
        private readonly IJobAdsCommand _jobAdsCommand;
        private readonly IJobAdsQuery _jobAdsQuery;

        private readonly IList<Tuple<int?, int>> _contactCreditThresholds = new List<Tuple<int?, int>>
        {
            new Tuple<int?, int>(200, 20),
            new Tuple<int?, int>(500, 15),
            new Tuple<int?, int>(null, 10),
        };

        public CreditsHandler(IEmailsCommand emailsCommand, IEmployersQuery employersQuery, IOrganisationsQuery organisationsQuery, IRecruitersQuery recruitersQuery, IAdministratorsQuery administratorsQuery, IAllocationsCommand allocationsCommand, IAllocationsQuery allocationsQuery, ICreditsQuery creditsQuery, IJobAdsCommand jobAdsCommand, IJobAdsQuery jobAdsQuery)
        {
            _emailsCommand = emailsCommand;
            _employersQuery = employersQuery;
            _organisationsQuery = organisationsQuery;
            _recruitersQuery = recruitersQuery;
            _administratorsQuery = administratorsQuery;
            _allocationsCommand = allocationsCommand;
            _allocationsQuery = allocationsQuery;
            _creditsQuery = creditsQuery;
            _jobAdsCommand = jobAdsCommand;
            _jobAdsQuery = jobAdsQuery;
        }

        void ICreditsHandler.OnCreditExercised(Guid creditId, Guid ownerId, bool allocationAdjusted)
        {
            var allocations = _allocationsQuery.GetActiveAllocations(ownerId, creditId);
            var initialQuantity = GetQuantity(allocations, a => a.InitialQuantity);
            var remainingQuantity = GetQuantity(allocations, a => a.RemainingQuantity);

            var credit = _creditsQuery.GetCredit(creditId);
            if (credit is ContactCredit)
                OnContactCreditExercised(ownerId, allocationAdjusted, initialQuantity, remainingQuantity);
            else if (credit is JobAdCredit)
                OnJobAdCreditExercised();
            else if (credit is ApplicantCredit)
                OnApplicantCreditExercised(ownerId, remainingQuantity);
        }

        private void OnApplicantCreditExercised(Guid ownerId, int? remainingQuantity)
        {
            if (remainingQuantity == 0)
            {
                // If they now have 0 applicant credits then no-one should be able to apply for their job ads.

                // Close all current job ads.

                CloseJobAds(ownerId);

                // Deallocate all existing job ad credits.

                DeallocateJobAdCredits(ownerId);
            }
        }

        private static void OnJobAdCreditExercised()
        {
        }

        private void OnContactCreditExercised(Guid ownerId, bool allocationAdjusted, int? initialQuantity, int? remainingQuantity)
        {
            // Send an email if needed.

            SendCreditEmail(ownerId, allocationAdjusted, initialQuantity, remainingQuantity, _contactCreditThresholds, (r, a) => new NoContactCreditsEmail(r, a), (r, a, q) => new LowContactCreditsEmail(r, a, q));
        }

        private void SendCreditEmail(Guid ownerId, bool allocationAdjusted, int? initialQuantity, int? remainingQuantity, IEnumerable<Tuple<int?, int>> thresholds, Func<Employer, Administrator, TemplateEmail> createNoCreditsEmail, Func<Employer, Administrator, int, TemplateEmail> createLowCreditsEmail)
        {
            if (initialQuantity == null || remainingQuantity == null || !allocationAdjusted)
                return;

            // Look to send reminder emails.

            if (remainingQuantity.Value == 0)
            {
                var recipients = GetRecipients(ownerId);
                foreach (var recipient in recipients.Item1)
                    _emailsCommand.TrySend(createNoCreditsEmail(recipient, recipients.Item2));
            }
            else if (AreCreditsLow(initialQuantity.Value, remainingQuantity.Value, thresholds))
            {
                var recipients = GetRecipients(ownerId);
                foreach (var recipient in recipients.Item1)
                    _emailsCommand.TrySend(createLowCreditsEmail(recipient, recipients.Item2, remainingQuantity.Value));
            }
        }

        private static bool AreCreditsLow(int initialQuantity, int remainingQuantity, IEnumerable<Tuple<int?, int>> thresholds)
        {
            foreach (var threshold in thresholds)
            {
                if (threshold.Item1 == null)
                    return AreCreditsLow(initialQuantity, remainingQuantity, threshold.Item2);

                if (initialQuantity <= threshold.Item1)
                    return AreCreditsLow(initialQuantity, remainingQuantity, threshold.Item2);
            }

            return false;
        }

        private static bool AreCreditsLow(int initialQuantity, int remainingQuantity, int trigger)
        {
            // Credits are low the first time the remaining quantity falls below the trigger percentage.

            return 100 * remainingQuantity <= initialQuantity * trigger
                && 100 * (remainingQuantity + 1) > initialQuantity * trigger;
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

            var verifiedOrganisation = (VerifiedOrganisation) organisation;
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

        private void DeallocateJobAdCredits(Guid ownerId)
        {
            var allocations = _allocationsQuery.GetActiveAllocations<JobAdCredit>(ownerId);
            if (allocations != null)
            {
                foreach (var allocation in allocations)
                    _allocationsCommand.Deallocate(allocation.Id);
            }
        }

        private void CloseJobAds(Guid ownerId)
        {
            // Find all affected job posters.

            var posterIds = _employersQuery.GetEmployer(ownerId) != null
                ? new List<Guid> {ownerId}
                : _recruitersQuery.GetRecruiters(_organisationsQuery.GetSubOrganisationHierarchy(ownerId));

            var hasCredits = new Dictionary<Guid, bool>();
            foreach (var posterId in posterIds)
                CloseJobAds(posterId, hasCredits);
        }

        private void CloseJobAds(Guid posterId, IDictionary<Guid, bool> hasCredits)
        {
            if (HasApplicantCredits(posterId, hasCredits))
                return;

            // No active allocations so close all their job ads.

            var jobAdIds = _jobAdsQuery.GetOpenJobAdIds(posterId);
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(jobAdIds);
            foreach (var jobAd in jobAds)
                _jobAdsCommand.CloseJobAd(jobAd);
        }

        private bool HasApplicantCredits(Guid posterId, IDictionary<Guid, bool> hasCredits)
        {
            // Check poster themselves.

            var hasPosterCredits = HasApplicantCredits(posterId);
            if (hasPosterCredits)
                return true;

            // Check organisation.

            var organisation = _recruitersQuery.GetOrganisation(posterId);
            if (!hasCredits.ContainsKey(organisation.Id))
                hasCredits[organisation.Id] = HasApplicantCredits(organisation.Id);
            if (hasCredits[organisation.Id])
                return true;

            return HasParentApplicantCredits(organisation.ParentId, hasCredits);
        }

        private bool HasParentApplicantCredits(Guid? organisationId, IDictionary<Guid, bool> hasCredits)
        {
            if (organisationId == null)
                return false;

            if (!hasCredits.ContainsKey(organisationId.Value))
                hasCredits[organisationId.Value] = HasApplicantCredits(organisationId.Value);
            if (hasCredits[organisationId.Value])
                return true;

            var organisation = _recruitersQuery.GetOrganisation(organisationId.Value);
            if (organisation == null)
                return false;
            return HasParentApplicantCredits(organisation.ParentId, hasCredits);
        }

        private bool HasApplicantCredits(Guid ownerId)
        {
            var allocations = _allocationsQuery.GetActiveAllocations<ApplicantCredit>(ownerId);
            var remainingQuantity = GetQuantity(allocations, a => a.RemainingQuantity);
            return remainingQuantity == null || remainingQuantity.Value > 0;
        }

        private static int? GetQuantity(IList<Allocation> allocations, Func<Allocation, int?> getQuantity)
        {
            // Look for unlimited.

            if (allocations.Any(a => a.RemainingQuantity == null))
                return null;

            switch (allocations.Count)
            {
                case 0:

                    // If there are no allocations then everything is 0.

                    return 0;

                case 1:

                    // If there is only 1 allocation use it.

                    return getQuantity(allocations[0]);

                default:

                    // Add them up, but don't include any that don't have any remaining.

                    return (from a in allocations where a.RemainingQuantity.Value > 0 select a).Sum(getQuantity);
            }
        }
    }
}