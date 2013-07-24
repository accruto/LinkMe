using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Domain.Users.Custodians.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.Queries;

namespace LinkMe.Domain.Users.Queries
{
    public class UsersQuery
        : IUsersQuery
    {
        private readonly IMembersQuery _membersQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly ICustodiansQuery _custodiansQuery;
        private readonly IAdministratorsQuery _administratorsQuery;

        public UsersQuery(IMembersQuery membersQuery, IEmployersQuery employersQuery, ICustodiansQuery custodiansQuery, IAdministratorsQuery administratorsQuery)
        {
            _membersQuery = membersQuery;
            _employersQuery = employersQuery;
            _custodiansQuery = custodiansQuery;
            _administratorsQuery = administratorsQuery;
        }

        RegisteredUser IUsersQuery.GetUser(Guid id)
        {
            // Go through the list.

            var member = _membersQuery.GetMember(id);
            if (member != null)
                return member;

            var employer = _employersQuery.GetEmployer(id);
            if (employer != null)
                return employer;

            var custodian = _custodiansQuery.GetCustodian(id);
            if (custodian != null)
                return custodian;

            return _administratorsQuery.GetAdministrator(id);
        }
    }
}