using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;

namespace LinkMe.Domain.Users.Employers.Credits.Commands
{
    public class EmployerAllocationsCommand
        : IEmployerAllocationsCommand
    {
        private readonly IAllocationsCommand _allocationsCommand;
        private readonly IAllocationsQuery _allocationsQuery;
        private readonly ICreditsQuery _creditsQuery;
        private readonly IEmployerCreditsQuery _employerCreditsQuery;

        public EmployerAllocationsCommand(IAllocationsCommand allocationsCommand, IAllocationsQuery allocationsQuery, ICreditsQuery creditsQuery, IEmployerCreditsQuery employerCreditsQuery)
        {
            _allocationsCommand = allocationsCommand;
            _allocationsQuery = allocationsQuery;
            _creditsQuery = creditsQuery;
            _employerCreditsQuery = employerCreditsQuery;
        }

        void IEmployerAllocationsCommand.CreateAllocation(Allocation allocation)
        {
            CreateAllocation(allocation);
        }

        void IEmployerAllocationsCommand.Deallocate(Guid allocationId)
        {
            _allocationsCommand.Deallocate(allocationId);
        }

        void IEmployerAllocationsCommand.EnsureJobAdCredits(IEmployer employer)
        {
            var jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();
            var applicantCredit = _creditsQuery.GetCredit<ApplicantCredit>();

            // Check whether they have any credits.

            var jobAdAllocation = _employerCreditsQuery.GetEffectiveActiveAllocation<JobAdCredit>(employer);
            if (jobAdAllocation.RemainingQuantity == 0)
            {
                // No credits, so allocate 1 now, giving an expiry of 1 day so it is used now.

                jobAdAllocation = new Allocation
                {
                    CreditId = jobAdCredit.Id,
                    OwnerId = employer.Id,
                    InitialQuantity = 1,
                    ExpiryDate = DateTime.Now.AddDays(1)
                };
                CreateAllocation(jobAdAllocation);
            }

            // Check that they have applicant credits.

            var applicantAllocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            if (applicantAllocation.RemainingQuantity == 0)
            {
                // No applicant credits so give them an unlimited amount, and make the expiry last 2 months after the job ad credits.

                _allocationsCommand.CreateAllocation(new Allocation
                {
                    CreditId = applicantCredit.Id,
                    OwnerId = employer.Id,
                    InitialQuantity = null,
                    ExpiryDate = jobAdAllocation.ExpiryDate == null ? (DateTime?)null : jobAdAllocation.ExpiryDate.Value.AddMonths(2)
                });
            }
        }

        private void CreateAllocation(Allocation allocation)
        {
            _allocationsCommand.CreateAllocation(allocation);

            if (allocation.CreditId == _creditsQuery.GetCredit<JobAdCredit>().Id)
                EnsureApplicantCredits(allocation);
            else if (allocation.CreditId == _creditsQuery.GetCredit<ApplicantCredit>().Id)
                EnsureJobAdCredits(allocation);
        }

        private void EnsureJobAdCredits(Allocation allocation)
        {
            // Get current allocations.

            var allocations = _allocationsQuery.GetActiveAllocations<JobAdCredit>(allocation.OwnerId);

            if (allocation.InitialQuantity != null)
            {
                // Don't allocate if there are existing non-zero limited credits.
                // This is based on the assumption that the user was given a finite number of both JobAd and Applicant credits.

                if ((from a in allocations where a.RemainingQuantity != null && a.RemainingQuantity.Value != 0 select a).Any())
                    return;
            }

            // Need to ensure there are unlimited credits.

            var unlimitedAllocations = (from a in allocations where a.RemainingQuantity == null select a).ToList();
            if (unlimitedAllocations.Count > 0)
            {
                // If there are any that do not expire then that is enough.

                if (unlimitedAllocations.Any(a => a.ExpiryDate == null))
                    return;

                // There are unlimited credits but need to check their expiry.

                if (allocation.ExpiryDate != null)
                {
                    if (unlimitedAllocations.Any(a => a.ExpiryDate != null && allocation.ExpiryDate != null && a.ExpiryDate.Value >= allocation.ExpiryDate.Value))
                        return;
                }
            }

            // Need to create an allocation.

            _allocationsCommand.CreateAllocation(new Allocation
            {
                OwnerId = allocation.OwnerId,
                CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id,
                InitialQuantity = null,
                ExpiryDate = allocation.ExpiryDate == null ? (DateTime?) null : allocation.ExpiryDate.Value.Date
            });
        }

        private void EnsureApplicantCredits(Allocation allocation)
        {
            // Get current allocations.

            var allocations = _allocationsQuery.GetActiveAllocations<ApplicantCredit>(allocation.OwnerId);

            if (allocation.InitialQuantity != null)
            {
                // Don't allocate if there are existing non-zero limited credits.
                // This is based on the assumption that the user was given a finite number of both JobAd and Applicant credits.

                if ((from a in allocations where a.RemainingQuantity != null && a.RemainingQuantity.Value != 0 select a).Any())
                    return;
            }

            // Need to ensure there are unlimited credits.

            var unlimitedAllocations = (from a in allocations where a.RemainingQuantity == null select a).ToList();
            if (unlimitedAllocations.Count > 0)
            {
                // If there are any that do not expire then that is enough.

                if (unlimitedAllocations.Any(a => a.ExpiryDate == null))
                    return;

                // There are unlimited credits but need to check their expiry.

                if (allocation.ExpiryDate != null)
                {
                    if (unlimitedAllocations.Any(a => a.ExpiryDate != null && allocation.ExpiryDate != null && a.ExpiryDate.Value >= allocation.ExpiryDate.Value.AddMonths(2)))
                        return;
                }
            }

            // Need to create an allocation.

            var expiryDate = allocation.ExpiryDate == null
                ? (DateTime?)null
                : allocation.ExpiryDate.Value.AddMonths(2) > DateTime.Now.AddYears(1)
                    ? allocation.ExpiryDate.Value.Date.AddMonths(2)
                    : DateTime.Now.Date.AddYears(1);

            _allocationsCommand.CreateAllocation(new Allocation
            {
                OwnerId = allocation.OwnerId,
                CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id,
                InitialQuantity = null,
                ExpiryDate = expiryDate
            });
        }
    }
}
