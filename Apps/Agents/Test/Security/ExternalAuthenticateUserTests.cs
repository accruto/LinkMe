using System;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Security
{
    [TestClass]
    public class ExternalAuthenticateUserTests
        : TestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IExternalCredentialsCommand _externalCredentialsCommand = Resolve<IExternalCredentialsCommand>();
        private readonly IExternalAuthenticationCommand _externalAuthenticationCommand = Resolve<IExternalAuthenticationCommand>();

        private const string EmailAddress = "test@test.linkme.net.au";
        private const string Password = "passWORD";
        private const string FirstName = "Barney";
        private const string LastName = "Gumble";
        private const string ExternalId = "abcdefgh";

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestAuthenticateUser()
        {
            var member = _memberAccountsCommand.CreateTestMember(EmailAddress, Password, FirstName, LastName);
            var providerId = Guid.NewGuid();
            _externalCredentialsCommand.CreateCredentials(member.Id, new ExternalCredentials { ProviderId = providerId, ExternalId = ExternalId });
            Assert.AreEqual(AuthenticationStatus.Authenticated, _externalAuthenticationCommand.AuthenticateUser(new ExternalCredentials { ProviderId = providerId, ExternalId = ExternalId }).Status);
            Assert.AreEqual(AuthenticationStatus.Failed, _externalAuthenticationCommand.AuthenticateUser(new ExternalCredentials { ProviderId = providerId, ExternalId = string.Empty }).Status);
            Assert.AreEqual(AuthenticationStatus.Failed, _externalAuthenticationCommand.AuthenticateUser(new ExternalCredentials { ProviderId = providerId, ExternalId = ExternalId.ToUpper() }).Status);
            Assert.AreEqual(AuthenticationStatus.Failed, _externalAuthenticationCommand.AuthenticateUser(new ExternalCredentials { ProviderId = providerId, ExternalId = "invalidId" }).Status);
            Assert.AreEqual(AuthenticationStatus.Failed, _externalAuthenticationCommand.AuthenticateUser(new ExternalCredentials { ProviderId = Guid.NewGuid(), ExternalId = ExternalId }).Status);
        }

        [TestMethod]
        public void TestAuthenticateUserFlags()
        {
            var member = _memberAccountsCommand.CreateTestMember(EmailAddress, Password, FirstName, LastName);
            var providerId = Guid.NewGuid();
            _externalCredentialsCommand.CreateCredentials(member.Id, new ExternalCredentials { ProviderId = providerId, ExternalId = ExternalId });

            // Enabled and Activated.

            var credentials = new ExternalCredentials { ProviderId = providerId, ExternalId = ExternalId };
            Assert.AreEqual(AuthenticationStatus.Authenticated, _externalAuthenticationCommand.AuthenticateUser(credentials).Status);

            // Disabled and Activated.

            member.IsEnabled = false;
            member.IsActivated = true;
            _memberAccountsCommand.UpdateMember(member);
            Assert.AreEqual(AuthenticationStatus.Disabled, _externalAuthenticationCommand.AuthenticateUser(credentials).Status);

            // Enabled and Deactivated.

            member.IsEnabled = true;
            member.IsActivated = false;
            _memberAccountsCommand.UpdateMember(member);
            Assert.AreEqual(AuthenticationStatus.Deactivated, _externalAuthenticationCommand.AuthenticateUser(credentials).Status);

            // Disabled and Deactivated.

            member.IsEnabled = false;
            member.IsActivated = false;
            _memberAccountsCommand.UpdateMember(member);
            Assert.AreEqual(AuthenticationStatus.Disabled, _externalAuthenticationCommand.AuthenticateUser(credentials).Status);
        }
    }
}