using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Custodians.Commands
{
    public class CustodiansCommand
        : ICustodiansCommand
    {
        private readonly ICustodiansRepository _repository;

        public CustodiansCommand(ICustodiansRepository repository)
        {
            _repository = repository;
        }

        void ICustodiansCommand.CreateCustodian(Custodian custodian)
        {
            custodian.Prepare();
            custodian.Validate();
            _repository.CreateCustodian(custodian);
        }

        void ICustodiansCommand.UpdateCustodian(Custodian custodian)
        {
            custodian.Validate();
            _repository.UpdateCustodian(custodian);
        }
    }
}
