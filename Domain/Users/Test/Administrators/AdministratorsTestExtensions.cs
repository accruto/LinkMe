using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Administrators.Commands;

namespace LinkMe.Domain.Users.Test.Administrators
{
    public static class AdministratorsTestExtensions
    {
        private const string EmailAddressFormat = "admin{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Paul{0}";
        private const string LastNameFormat = "Hodgman{0}";

        public static Administrator CreateTestAdministrator(this IAdministratorsCommand administratorAccountsCommand, int index)
        {
            return administratorAccountsCommand.CreateTestAdministrator(string.Format(EmailAddressFormat, index), string.Format(FirstNameFormat, index), string.Format(LastNameFormat, index));
        }

        public static Administrator CreateTestAdministrator(this IAdministratorsCommand administratorAccountsCommand, string emailAddress)
        {
            return administratorAccountsCommand.CreateTestAdministrator(emailAddress, string.Format(FirstNameFormat, 0), string.Format(LastNameFormat, 0));
        }

        public static Administrator CreateTestAdministrator(this IAdministratorsCommand administratorAccountsCommand, string emailAddress, string firstName, string lastName)
        {
            var administrator = new Administrator
            {
                IsEnabled = true,
                IsActivated = true,
                EmailAddress = new EmailAddress { Address = emailAddress, IsVerified = true },
                FirstName = firstName,
                LastName = lastName,
            };

            administratorAccountsCommand.CreateAdministrator(administrator);
            return administrator;
        }
    }
}