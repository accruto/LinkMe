using System;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Test.Users.Custodians
{
    public static class CustodianAccountsTestExtensions
    {
        private const string UserIdFormat = "admin{0}";
        private const string EmailAddressFormat = "{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Paul{0}";
        private const string LastNameFormat = "Hodgman{0}";
        private const string DefaultPassword = "password";

        public static string GetPassword(this Custodian custodian)
        {
            return DefaultPassword;
        }

        public static string GetLoginId(this Custodian custodian)
        {
            return custodian.EmailAddress.Address.Substring(0, custodian.EmailAddress.Address.IndexOf("@"));
        }

        public static Custodian CreateTestCustodian(this ICustodianAccountsCommand custodianAccountsCommand, int index, Guid affiliateId)
        {
            return custodianAccountsCommand.CreateTestCustodian(string.Format(UserIdFormat, index), string.Format(FirstNameFormat, index), string.Format(LastNameFormat, index), affiliateId);
        }

        public static Custodian CreateTestCustodian(this ICustodianAccountsCommand custodianAccountsCommand, string loginId, Guid affiliateId)
        {
            return custodianAccountsCommand.CreateTestCustodian(loginId, string.Format(FirstNameFormat, 0), string.Format(LastNameFormat, 0), affiliateId);
        }

        public static Custodian CreateTestCustodian(this ICustodianAccountsCommand custodianAccountsCommand, string loginId, string firstName, string lastName, Guid affiliateId)
        {
            var custodian = new Custodian
            {
                EmailAddress = new EmailAddress { Address = string.Format(EmailAddressFormat, loginId) },
                FirstName = firstName,
                LastName = lastName,
            };

            custodianAccountsCommand.CreateCustodian(custodian, new LoginCredentials { LoginId = loginId, PasswordHash = LoginCredentials.HashToString(DefaultPassword) }, affiliateId);
            return custodian;
        }
    }
}