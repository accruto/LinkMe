using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Registration.Queries
{
    public interface IReferralsQuery
    {
        AffiliationReferral GetAffiliationReferral(Guid refereeId);

        ExternalReferral GetExternalReferral(Guid userId);
        IList<ExternalReferralSource> GetExternalReferralSources();
        ExternalReferralSource GetExternalReferralSource(int id);
        ExternalReferralSource GetExternalReferralSource(string name);
    }
}
