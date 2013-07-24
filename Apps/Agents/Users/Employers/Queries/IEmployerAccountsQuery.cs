using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Users.Employers.Queries
{
    public interface IEmployerAccountsQuery
    {
        Employer GetEmployer(string loginId);
    }
}
