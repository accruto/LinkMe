using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Custodians.Queries
{
    public interface ICustodiansQuery
    {
        Custodian GetCustodian(Guid id);
        IList<Custodian> GetCustodians(IEnumerable<Guid> ids);
        IList<Custodian> GetAffiliationCustodians(Guid affiliateId);
    }
}