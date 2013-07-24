using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Employers.Commands
{
    public class EmployersCommand
        : IEmployersCommand
    {
        private readonly IEmployersRepository _repository;

        public EmployersCommand(IEmployersRepository repository)
        {
            _repository = repository;
        }

        void IEmployersCommand.CreateEmployer(Employer employer)
        {
            employer.Prepare();
            employer.Validate();
            _repository.CreateEmployer(employer);
        }

        void IEmployersCommand.UpdateEmployer(Employer employer)
        {
            employer.Validate();
            _repository.UpdateEmployer(employer);
        }
    }
}
