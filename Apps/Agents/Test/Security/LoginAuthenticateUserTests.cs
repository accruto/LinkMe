using System;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Security
{
    [TestClass]
    public class LoginAuthenticateUserTests
        : TestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IUsersQuery _usersQuery = Resolve<IUsersQuery>();
        private readonly ILoginCredentialsCommand _loginCredentialsCommand = Resolve<ILoginCredentialsCommand>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();

        private const string EmailAddress = "test@test.linkme.net.au";
        private const string Password = "passWORD";
        private const string OverridePassword = "aaaaaa";
        private const string EncryptedOverridePassword = "C056Dl/oStNftflbnO6seQ==";
        private const string FirstName = "Barney";
        private const string LastName = "Gumble";

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestAuthenticateUser()
        {
            _memberAccountsCommand.CreateTestMember(EmailAddress, Password, FirstName, LastName);

            var loginAuthenticationCommand = CreateLoginAuthenticationCommand(false, null);
            Assert.AreEqual(AuthenticationStatus.Authenticated, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = Password }).Status);
            Assert.AreEqual(AuthenticationStatus.Failed, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = string.Empty }).Status);
            Assert.AreEqual(AuthenticationStatus.Failed, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = Password.ToLower() }).Status);
            Assert.AreEqual(AuthenticationStatus.Failed, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = "invalidPassword" }).Status);
        }

        [TestMethod]
        public void TestAuthenticateUserFlags()
        {
            var member = _memberAccountsCommand.CreateTestMember(EmailAddress, Password, FirstName, LastName);
            var credentials = new LoginCredentials { LoginId = EmailAddress, Password = Password };

            // Enabled and Activated.

            var loginAuthenticationCommand = CreateLoginAuthenticationCommand(false, null);
            Assert.AreEqual(AuthenticationStatus.Authenticated, loginAuthenticationCommand.AuthenticateUser(credentials).Status);

            // Disabled and Activated.

            member.IsEnabled = false;
            member.IsActivated = true;
            _memberAccountsCommand.UpdateMember(member);
            Assert.AreEqual(AuthenticationStatus.Disabled, loginAuthenticationCommand.AuthenticateUser(credentials).Status);

            // Enabled and Deactivated.

            member.IsEnabled = true;
            member.IsActivated = false;
            _memberAccountsCommand.UpdateMember(member);
            Assert.AreEqual(AuthenticationStatus.Deactivated, loginAuthenticationCommand.AuthenticateUser(credentials).Status);

            // Disabled and Deactivated.

            member.IsEnabled = false;
            member.IsActivated = false;
            _memberAccountsCommand.UpdateMember(member);
            Assert.AreEqual(AuthenticationStatus.Disabled, loginAuthenticationCommand.AuthenticateUser(credentials).Status);
        }

        [TestMethod]
        public void TestAuthenticateOverridePassword()
        {
            var member = _memberAccountsCommand.CreateTestMember(EmailAddress, Password, FirstName, LastName);

            // Override disabled.

            var loginAuthenticationCommand = CreateLoginAuthenticationCommand(false, null);
            Assert.AreEqual(AuthenticationStatus.Authenticated, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = Password }).Status);

            // Bad password.

            Assert.AreEqual(AuthenticationStatus.Failed, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = "invalidPassword" }).Status);

            // Override password.

            Assert.AreEqual(AuthenticationStatus.Failed, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = OverridePassword }).Status);

            // Override enabled, no override needed.

            loginAuthenticationCommand = CreateLoginAuthenticationCommand(true, EncryptedOverridePassword);
            Assert.AreEqual(AuthenticationStatus.Authenticated, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = Password }).Status);

            // Override needed.

            Assert.AreEqual(AuthenticationStatus.AuthenticatedWithOverridePassword, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = OverridePassword }).Status);

            // Disabled with override.

            member.IsEnabled = false;
            member.IsActivated = true;
            _memberAccountsCommand.UpdateMember(member);
            Assert.AreEqual(AuthenticationStatus.AuthenticatedWithOverridePassword, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = OverridePassword }).Status);

            // Deactivated with override.

            member.IsEnabled = true;
            member.IsActivated = false;
            _memberAccountsCommand.UpdateMember(member);
            Assert.AreEqual(AuthenticationStatus.AuthenticatedWithOverridePassword, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = OverridePassword }).Status);

            // Both fail.

            Assert.AreEqual(AuthenticationStatus.Failed, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = "invalidPassword" }).Status);
        }

        [TestMethod]
        public void TestAuthenticateMustChangePassword()
        {
            var member = _memberAccountsCommand.CreateTestMember(EmailAddress, Password, FirstName, LastName);
            var credentials = _loginCredentialsQuery.GetCredentials(member.Id);
            credentials.MustChangePassword = true;
            _loginCredentialsCommand.UpdateCredentials(member.Id, credentials, Guid.NewGuid());

            // Normal login.

            var loginAuthenticationCommand = CreateLoginAuthenticationCommand(false, null);
            Assert.AreEqual(AuthenticationStatus.AuthenticatedMustChangePassword, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = Password }).Status);

            // Bad password.

            Assert.AreEqual(AuthenticationStatus.Failed, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = "invalidPassword" }).Status);

            // Override password not enabled.

            Assert.AreEqual(AuthenticationStatus.Failed, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = OverridePassword }).Status);

            // Override enabled.

            loginAuthenticationCommand = CreateLoginAuthenticationCommand(true, EncryptedOverridePassword);
            Assert.AreEqual(AuthenticationStatus.AuthenticatedMustChangePassword, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = Password }).Status);
            Assert.AreEqual(AuthenticationStatus.AuthenticatedWithOverridePassword, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = OverridePassword }).Status);

            // Disabled.

            member.IsEnabled = false;
            member.IsActivated = true;
            _memberAccountsCommand.UpdateMember(member);
            Assert.AreEqual(AuthenticationStatus.Disabled, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = Password }).Status);
            Assert.AreEqual(AuthenticationStatus.AuthenticatedWithOverridePassword, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = OverridePassword }).Status);

            // Deactivated with override.

            member.IsEnabled = true;
            member.IsActivated = false;
            _memberAccountsCommand.UpdateMember(member);
            Assert.AreEqual(AuthenticationStatus.AuthenticatedMustChangePassword, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = Password }).Status);
            Assert.AreEqual(AuthenticationStatus.AuthenticatedWithOverridePassword, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = OverridePassword }).Status);

            // Both fail.

            Assert.AreEqual(AuthenticationStatus.Failed, loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = EmailAddress, Password = "invalidPassword" }).Status);
        }

        private ILoginAuthenticationCommand CreateLoginAuthenticationCommand(bool overridePasswordEnabled, string overridePasswordHash)
        {
            return new LoginAuthenticationCommand(
                _loginCredentialsQuery,
                _usersQuery,
                overridePasswordEnabled,
                overridePasswordHash,
                true);
        }
    }
}