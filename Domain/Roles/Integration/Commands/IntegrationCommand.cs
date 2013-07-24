using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Integration.Commands
{
    public class IntegrationCommand
        : IIntegrationCommand
    {
        private readonly IIntegrationRepository _repository;

        public IntegrationCommand(IIntegrationRepository repository)
        {
            _repository = repository;
        }

        void IIntegrationCommand.CreateIntegrationSystem(IntegrationSystem system)
        {
            system.Prepare();
            system.Validate();
            _repository.CreateIntegrationSystem(system);
        }

        void IIntegrationCommand.CreateIntegratorUser(IntegratorUser user)
        {
            user.Prepare();
            user.Validate();
            _repository.CreateIntegratorUser(user);
        }
    }
}