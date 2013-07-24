using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Integration.LinkedIn;

namespace LinkMe.Apps.Agents.Users.Employers.Commands
{
    public interface IEmployerAccountsCommand
    {
        void CreateEmployer(Employer employer, LoginCredentials credentials);
        void CreateEmployer(Employer employer, LinkedInProfile profile);
        void UpdateEmployer(Employer employer);
    }
}