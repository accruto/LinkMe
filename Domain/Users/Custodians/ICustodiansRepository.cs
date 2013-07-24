using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Custodians
{
    public interface ICustodiansRepository
    {
        void CreateCustodian(Custodian custodian);
        void UpdateCustodian(Custodian custodian);
        Custodian GetCustodian(Guid id);
        IList<Custodian> GetCustodians(IEnumerable<Guid> ids);

        IList<Custodian> GetAffiliationCustodians(Guid affiliateId);
        Guid? GetAffiliationId(Guid custodianId);
        void SetAffiliation(Guid custodianId, Guid? affiliateId);
    }
}