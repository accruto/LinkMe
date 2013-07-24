using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Test.Users
{
    public static class UsersTestExtensions
    {
        public static string GetLoginId(this IUser user)
        {
            if (user is Member)
                return MemberAccountsTestExtensions.GetLoginId((Member) user);
            if (user is Employer)
                return EmployerAccountsTestExtensions.GetLoginId((Employer)user);
            if (user is Administrator)
                return AdministratorAccountsTestExtensions.GetLoginId((Administrator)user);
            if (user is Custodian)
                return CustodianAccountsTestExtensions.GetLoginId((Custodian)user);
            return null;
        }

        public static string GetPassword(this IUser user)
        {
            if (user is Member)
                return MemberAccountsTestExtensions.GetPassword((Member)user);
            if (user is Employer)
                return EmployerAccountsTestExtensions.GetPassword((Employer)user);
            if (user is Administrator)
                return AdministratorAccountsTestExtensions.GetPassword((Administrator)user);
            if (user is Custodian)
                return CustodianAccountsTestExtensions.GetPassword((Custodian)user);
            return null;
        }
    }
}