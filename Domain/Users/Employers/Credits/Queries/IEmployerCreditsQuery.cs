using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;

namespace LinkMe.Domain.Users.Employers.Credits.Queries
{
    public interface IEmployerCreditsQuery
    {
        Allocation GetEffectiveActiveAllocation<TCredit>(IEmployer employer) where TCredit : Credit;
        IDictionary<Guid, Allocation> GetEffectiveActiveAllocations(IEmployer employer, IEnumerable<Guid> creditIds);
        IDictionary<Guid, Allocation> GetEffectiveExpiringAllocations<TCredit>(DateTime expiryDate) where TCredit : Credit;
    }
}