using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Contenders.Commands
{
    public class ApplicationsCommand
        : IApplicationsCommand
    {
        private readonly IApplicationsRepository _repository;

        public ApplicationsCommand(IApplicationsRepository repository)
        {
            _repository = repository;
        }

        void IApplicationsCommand.CreateApplication<TApplication>(TApplication application)
        {
            application.Prepare();
            application.Validate();
            _repository.CreateApplication(application);
        }

        void IApplicationsCommand.UpdateApplication<TApplication>(TApplication application)
        {
            application.Validate();
            _repository.UpdateApplication(application);
        }

        void IApplicationsCommand.DeleteApplication<TApplication>(Guid id)
        {
            _repository.DeleteApplication<TApplication>(id);
        }
    }
}