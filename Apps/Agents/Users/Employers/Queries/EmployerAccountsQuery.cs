using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Queries;

namespace LinkMe.Apps.Agents.Users.Employers.Queries
{
    public class EmployerAccountsQuery
        : IEmployerAccountsQuery
    {
        private readonly IEmployersQuery _employersQuery;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;

        public EmployerAccountsQuery(IEmployersQuery employersQuery, ILoginCredentialsQuery loginCredentialsQuery)
        {
            _employersQuery = employersQuery;
            _loginCredentialsQuery = loginCredentialsQuery;
        }

        Employer IEmployerAccountsQuery.GetEmployer(string loginId)
        {
            var employerId = _loginCredentialsQuery.GetUserId(loginId);
            return employerId == null ? null : _employersQuery.GetEmployer(employerId.Value);
        }
    }
}
