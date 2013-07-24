using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Registration.Queries
{
    public class ReferralsQuery
        : IReferralsQuery
    {
        private readonly IRegistrationRepository _repository;

        public ReferralsQuery(IRegistrationRepository repository)
        {
            _repository = repository;
        }

        AffiliationReferral IReferralsQuery.GetAffiliationReferral(Guid refereeId)
        {
            return _repository.GetAffiliationReferral(refereeId);
        }

        IList<ExternalReferralSource> IReferralsQuery.GetExternalReferralSources()
        {
            return _repository.GetExternalReferralSources();
        }

        ExternalReferralSource IReferralsQuery.GetExternalReferralSource(int id)
        {
            return _repository.GetExternalReferralSource(id);
        }

        ExternalReferralSource IReferralsQuery.GetExternalReferralSource(string name)
        {
            return _repository.GetExternalReferralSource(name);
        }

        ExternalReferral IReferralsQuery.GetExternalReferral(Guid userId)
        {
            return _repository.GetExternalReferral(userId);
        }
    }
}