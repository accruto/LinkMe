using System;

namespace LinkMe.Domain.Roles.Registration.Commands
{
    public interface IReferralsCommand
    {
        void CreateAffiliationReferral(AffiliationReferral affiliationReferral);

        void CreateExternalReferral(ExternalReferral externalReferral);
        void UpdateExternalReferral(ExternalReferral externalReferral);
        void DeleteExternalReferral(Guid userId);
    }
}