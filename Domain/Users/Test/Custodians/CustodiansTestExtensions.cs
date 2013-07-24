using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Custodians.Commands;

namespace LinkMe.Domain.Users.Test.Custodians
{
    public static class CustodiansTestExtensions
    {
        private const string EmailAddressFormat = "custodian{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Paul{0}";
        private const string LastNameFormat = "Hodgman{0}";

        public static Custodian CreateTestCustodian(this ICustodiansCommand custodianAccountsCommand, int index)
        {
            return custodianAccountsCommand.CreateTestCustodian(string.Format(EmailAddressFormat, index), string.Format(FirstNameFormat, index), string.Format(LastNameFormat, index));
        }

        public static Custodian CreateTestCustodian(this ICustodiansCommand custodianAccountsCommand, string emailAddress)
        {
            return custodianAccountsCommand.CreateTestCustodian(emailAddress, string.Format(FirstNameFormat, 0), string.Format(LastNameFormat, 0));
        }

        public static Custodian CreateTestCustodian(this ICustodiansCommand custodianAccountsCommand, string emailAddress, string firstName, string lastName)
        {
            var custodian = new Custodian
            {
                IsEnabled = true,
                IsActivated = true,
                EmailAddress = new EmailAddress { Address = emailAddress, IsVerified = true },
                FirstName = firstName,
                LastName = lastName,
            };

            custodianAccountsCommand.CreateCustodian(custodian);
            return custodian;
        }
    }
}