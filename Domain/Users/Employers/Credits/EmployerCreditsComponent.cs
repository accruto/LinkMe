using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;

namespace LinkMe.Domain.Users.Employers.Credits
{
    public abstract class EmployerCreditsComponent
    {
        protected readonly ICreditsQuery _creditsQuery;
        private readonly IAllocationsQuery _allocationsQuery;

        protected EmployerCreditsComponent(ICreditsQuery creditsQuery, IAllocationsQuery allocationsQuery)
        {
            _creditsQuery = creditsQuery;
            _allocationsQuery = allocationsQuery;
        }

        protected Allocation GetEffectiveActiveAllocation<TCredit>(IEnumerable<Guid> ownerIds)
            where TCredit : Credit
        {
            var credit = _creditsQuery.GetCredit<TCredit>();
            return GetEffectiveAllocation(_allocationsQuery.GetActiveAllocations(ownerIds, new[] { credit.Id }).SelectMany(a => a.Value));
        }

        protected IDictionary<Guid, Allocation> GetEffectiveActiveAllocations(IEnumerable<Guid> ownerIds, IEnumerable<Guid> creditIds)
        {
            var allocations = _allocationsQuery.GetActiveAllocations(ownerIds, creditIds).SelectMany(a => a.Value);
            return (from c in creditIds
                    select new {c, Allocation = GetEffectiveAllocation(from a in allocations where a.CreditId == c select a)}).ToDictionary(x => x.c, x => x.Allocation);
        }

        protected IDictionary<Guid, Allocation> GetEffectiveExpiringAllocations<TCredit>(DateTime expiryDate)
            where TCredit : Credit
        {
            var credit = _creditsQuery.GetCredit<TCredit>();
            return _allocationsQuery.GetExpiringAllocations(credit.Id, expiryDate).ToDictionary(p => p.Key, p => GetEffectiveAllocation(p.Value));
        }

        private static Allocation GetEffectiveAllocation(IEnumerable<Allocation> allocations)
        {
            // No allocations means no effective allocation.

            if (!allocations.Any())
                return new Allocation { ExpiryDate = DateTime.MinValue, InitialQuantity = 0, RemainingQuantity = 0 };

            // Roll up the allocations.

            int? initialQuantity = 0;
            int? remainingQuantity = 0;
            DateTime? expiryDate = DateTime.MinValue;
            foreach (var allocation in allocations)
            {
                // Quantity.

                if (allocation.InitialQuantity == null)
                    initialQuantity = null;
                else if (initialQuantity != null)
                    initialQuantity += allocation.InitialQuantity.Value;

                if (allocation.RemainingQuantity == null)
                    remainingQuantity = null;
                else if (remainingQuantity != null)
                    remainingQuantity += allocation.RemainingQuantity.Value;

                // Expiry date.

                if (allocation.ExpiryDate == null)
                    expiryDate = null;
                else if (expiryDate != null && allocation.ExpiryDate.Value > expiryDate.Value)
                    expiryDate = allocation.ExpiryDate;
            }

            return new Allocation { ExpiryDate = expiryDate, InitialQuantity = initialQuantity, RemainingQuantity = remainingQuantity };
        }
    }
}
