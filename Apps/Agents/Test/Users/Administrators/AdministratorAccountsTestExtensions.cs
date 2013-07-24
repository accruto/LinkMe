using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Test.Users.Administrators
{
    public static class AdministratorAccountsTestExtensions
    {
        private const string UserIdFormat = "admin{0}";
        private const string EmailAddressFormat = "{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Paul{0}";
        private const string LastNameFormat = "Hodgman{0}";
        private const string DefaultPassword = "password";

        public static string GetPassword(this Administrator administrator)
        {
            return DefaultPassword;
        }

        public static string GetLoginId(this Administrator administrator)
        {
            return administrator.EmailAddress.Address.Substring(0, administrator.EmailAddress.Address.IndexOf("@"));
        }

        public static Administrator CreateTestAdministrator(this IAdministratorAccountsCommand administratorAccountsCommand, int index)
        {
            return administratorAccountsCommand.CreateTestAdministrator(string.Format(UserIdFormat, index), string.Format(FirstNameFormat, index), string.Format(LastNameFormat, index));
        }

        public static Administrator CreateTestAdministrator(this IAdministratorAccountsCommand administratorAccountsCommand, string loginId)
        {
            return administratorAccountsCommand.CreateTestAdministrator(loginId, string.Format(FirstNameFormat, 0), string.Format(LastNameFormat, 0));
        }

        public static Administrator CreateTestAdministrator(this IAdministratorAccountsCommand administratorAccountsCommand, string loginId, string firstName, string lastName)
        {
            var administrator = new Administrator
            {
                EmailAddress = new EmailAddress { Address = string.Format(EmailAddressFormat, loginId) },
                FirstName = firstName,
                LastName = lastName,
            };

            administratorAccountsCommand.CreateAdministrator(administrator, new LoginCredentials { LoginId = loginId, PasswordHash = LoginCredentials.HashToString(DefaultPassword) });
            return administrator;
        }
    }
}