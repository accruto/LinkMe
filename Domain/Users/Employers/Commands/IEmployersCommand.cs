using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Commands
{
    public interface IEmployersCommand
    {
        void CreateEmployer(Employer employer);
        void UpdateEmployer(Employer employer);
    }
}
