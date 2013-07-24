using System.ServiceModel;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Integration.JobG8;
using LinkMe.Apps.Services.External.JobG8.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Commands;
using LinkMe.Domain.Roles.Integration.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Integration.Test.JobG8
{
    [TestClass]
    public class FaultTests
        : AdvertPostServiceTests
    {
        private const string Password = "password";
        private readonly IIntegrationCommand _integrationCommand = Resolve<IIntegrationCommand>();
        private readonly IIntegrationQuery _integrationQuery = Resolve<IIntegrationQuery>();
        private readonly IJobG8Query _jobG8Query = Resolve<IJobG8Query>();

        [TestMethod]
        [ExpectedException(typeof(FaultException), "UserCredentials are not specified.")]
        public void TestNoCredentials()
        {
            var employer = CreateEmployer(0);
            var request = new PostAdvertRequestMessage();
            PostAdvert(employer, request);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "No username was specified in the HTTP request.")]
        public void TestNoUsername()
        {
            var employer = CreateEmployer(0);
            var request = new PostAdvertRequestMessage
            {
                UserCredentials = new Credentials()
            };
            PostAdvert(employer, request);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "No password was specified in the HTTP request.")]
        public void TestNoPassword()
        {
            var request = new PostAdvertRequestMessage
            {
                UserCredentials = new Credentials
                {
                    Username = "someuser"
                }
            };

            var employer = CreateEmployer(0);
            PostAdvert(employer, request);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Web service authorization failed: unknown user 'someuser'.")]
        public void TestUnknownUser()
        {
            var request = new PostAdvertRequestMessage
            {
                UserCredentials = new Credentials
                {
                    Username = "someuser",
                    Password = "somepassword"
                }
            };

            var employer = CreateEmployer(0);
            PostAdvert(employer, request);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Web service authorization failed: the password for user 'JobG8' is incorrect.")]
        public void TestBadPassword()
        {
            var request = new PostAdvertRequestMessage
            {
                UserCredentials = new Credentials
                {
                    Username = _jobG8Query.GetIntegratorUser().LoginId,
                    Password = "badpassword"
                }
            };

            var employer = CreateEmployer(0);
            PostAdvert(employer, request);
        }

        /// <summary>
        /// An integrator with a valid username password who doesn't have permissions to this service.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FaultException), "Web service authorization failed: user 'JobAdFeedTestUser' does not have permission to access the requested service.")]
        public void TestValidUserWithoutPermissions()
        {
            var ats = _integrationQuery.GetIntegrationSystem<Ats>(_jobG8Query.GetIntegratorUser().IntegrationSystemId);
            var integratorUser = new IntegratorUser
            {
                LoginId = "JobAdFeedTestUser",
                PasswordHash = LoginCredentials.HashToString(Password),
                Permissions = IntegratorPermissions.GetJobApplication,
                IntegrationSystemId = ats.Id,
            };
            _integrationCommand.CreateIntegratorUser(integratorUser);

            var request = new PostAdvertRequestMessage
            {
                UserCredentials = new Credentials
                {
                    Username = "JobAdFeedTestUser",
                    Password = Password
                }
            };

            var employer = CreateEmployer(0);
            PostAdvert(employer, request);
        }
    }
}
