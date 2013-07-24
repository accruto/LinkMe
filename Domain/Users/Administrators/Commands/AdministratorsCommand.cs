using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Administrators.Commands
{
    public class AdministratorsCommand
        : IAdministratorsCommand
    {
        private readonly IAdministratorsRepository _repository;

        public AdministratorsCommand(IAdministratorsRepository repository)
        {
            _repository = repository;
        }

        void IAdministratorsCommand.CreateAdministrator(Administrator administrator)
        {
            administrator.Prepare();
            administrator.Validate();
            _repository.CreateAdministrator(administrator);
        }

        void IAdministratorsCommand.UpdateAdministrator(Administrator administrator)
        {
            administrator.Validate();
            _repository.UpdateAdministrator(administrator);
        }
    }
}
