using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Registration.Commands
{
    public class ReferralsCommand
        : IReferralsCommand
    {
        private readonly IRegistrationRepository _repository;

        public ReferralsCommand(IRegistrationRepository repository)
        {
            _repository = repository;
        }

        void IReferralsCommand.CreateAffiliationReferral(AffiliationReferral affiliationReferral)
        {
            affiliationReferral.Prepare();
            affiliationReferral.Validate();
            _repository.CreateAffiliationReferral(affiliationReferral);
        }

        void IReferralsCommand.CreateExternalReferral(ExternalReferral externalReferral)
        {
            externalReferral.Prepare();
            externalReferral.Validate();
            _repository.CreateExternalReferral(externalReferral);
        }

        void IReferralsCommand.UpdateExternalReferral(ExternalReferral externalReferral)
        {
            externalReferral.Validate();
            _repository.UpdateExternalReferral(externalReferral);
        }

        void IReferralsCommand.DeleteExternalReferral(Guid userId)
        {
            _repository.DeleteExternalReferral(userId);
        }
    }
}