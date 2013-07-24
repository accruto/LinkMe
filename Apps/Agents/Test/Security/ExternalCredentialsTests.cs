using System;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Security
{
    [TestClass]
    public class ExternalCredentialsTests
        : TestClass
    {
        private readonly IExternalCredentialsCommand _externalCredentialsCommand = Resolve<IExternalCredentialsCommand>();
        private readonly IExternalCredentialsQuery _externalCredentialsQuery = Resolve<IExternalCredentialsQuery>();

        private const string ExternalId1 = "abcdefgh";
        private const string ExternalId2 = "ijklmnop";

        [TestMethod]
        public void TestCreateCredentials()
        {
            var userId = Guid.NewGuid();
            var providerId = Guid.NewGuid();
            _externalCredentialsCommand.CreateCredentials(userId, new ExternalCredentials {ProviderId = providerId, ExternalId = ExternalId1});
            AssertCredentials(userId, new ExternalCredentials { ProviderId = providerId, ExternalId = ExternalId1 });
        }

        [TestMethod]
        public void TestMultipleCredentialsMultipleUsers()
        {
            var userId1 = Guid.NewGuid();
            var providerId1 = Guid.NewGuid();
            _externalCredentialsCommand.CreateCredentials(userId1, new ExternalCredentials { ProviderId = providerId1, ExternalId = ExternalId1 });

            var userId2 = Guid.NewGuid();
            var providerId2 = Guid.NewGuid();
            _externalCredentialsCommand.CreateCredentials(userId2, new ExternalCredentials { ProviderId = providerId2, ExternalId = ExternalId2 });

            AssertCredentials(userId1, new ExternalCredentials { ProviderId = providerId1, ExternalId = ExternalId1 });
            AssertCredentials(userId2, new ExternalCredentials { ProviderId = providerId2, ExternalId = ExternalId2 });
        }

        [TestMethod]
        public void TestMultipleCredentialsMultipleProviders()
        {
            var userId = Guid.NewGuid();
            var providerId1 = Guid.NewGuid();
            _externalCredentialsCommand.CreateCredentials(userId, new ExternalCredentials { ProviderId = providerId1, ExternalId = ExternalId1 });

            var providerId2 = Guid.NewGuid();
            _externalCredentialsCommand.CreateCredentials(userId, new ExternalCredentials { ProviderId = providerId2, ExternalId = ExternalId2 });

            AssertCredentials(
                userId,
                new ExternalCredentials {ProviderId = providerId2, ExternalId = ExternalId2},
                new ExternalCredentials {ProviderId = providerId2, ExternalId = ExternalId2});
        }

        private void AssertCredentials(Guid userId, params ExternalCredentials[] expectedCredentialsList)
        {
            foreach (var expectedCredentials in expectedCredentialsList)
            {
                Assert.IsTrue(_externalCredentialsQuery.DoCredentialsExist(expectedCredentials));
                Assert.AreEqual(userId, _externalCredentialsQuery.GetUserId(expectedCredentials.ProviderId, expectedCredentials.ExternalId));
            }

            for (var index = 0; index < expectedCredentialsList.Length; ++index)
            {
                var credentials = _externalCredentialsQuery.GetCredentials(userId, expectedCredentialsList[index].ProviderId);
                Assert.AreEqual(expectedCredentialsList[index].ExternalId, credentials.ExternalId);
            }
        }
    }
}