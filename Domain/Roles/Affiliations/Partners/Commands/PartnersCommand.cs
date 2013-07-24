using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Affiliations.Partners.Commands
{
    public class PartnersCommand
        : IPartnersCommand
    {
        private readonly IPartnersRepository _repository;

        public PartnersCommand(IPartnersRepository repository)
        {
            _repository = repository;
        }

        void IPartnersCommand.CreatePartner(Partner partner)
        {
            partner.Prepare();
            partner.Validate();
            _repository.CreatePartner(partner);
        }

        void IPartnersCommand.SetPartner(Guid userId, Guid? partnerId)
        {
            _repository.SetPartner(userId, partnerId);
        }
    }
}
