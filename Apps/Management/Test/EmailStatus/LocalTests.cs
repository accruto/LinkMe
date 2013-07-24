using System;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using LinkMe.Apps.Management.EmailStatus;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Unity.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Management.Test.EmailStatus
{
    [TestClass]
    public class LocalTests
    {
        private const string BaseUrl = "http://localhost:8003/Management/Test/EmailStatus/";
        private static WebServiceHost _host;
        private static MockEmailVerificationsCommand _emailVerificationsCommand;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Create a singlton MockMembersCommand that will be used by EmailStatusService.

            Container.Push();
            _emailVerificationsCommand = new MockEmailVerificationsCommand();
            Container.Current.RegisterInstance<IEmailVerificationsCommand>(_emailVerificationsCommand);
             
            _host = new WebServiceHost(typeof(EmailStatusService), new Uri(BaseUrl));
            _host.Description.Behaviors.Add(new UnityServiceBehavior(Container.Current));
            _host.Open();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _host.Close();
            Container.Pop();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _emailVerificationsCommand.Reset();
        }

        [TestMethod]
        public void HealthTest()
        {
            var status = PostFile(FileSystem.GetAbsolutePath(@"Apps\Management\Test\EmailStatus\TestData\HealthTest.eml", RuntimeEnvironment.GetSourceFolder()));
            Assert.AreEqual(HttpStatusCode.Accepted, status);
        }

        [TestMethod]
        public void Said550Test()
        {
            var status = PostFile(FileSystem.GetAbsolutePath(@"Apps\Management\Test\EmailStatus\TestData\Said550Test.eml", RuntimeEnvironment.GetSourceFolder()));
            Assert.AreEqual(HttpStatusCode.NoContent, status);
            Assert.AreEqual("suzanne.douglas7@ntlworld.com", _emailVerificationsCommand.Member.GetBestEmailAddress().Address);
            Assert.IsFalse(_emailVerificationsCommand.Member.GetBestEmailAddress().IsVerified);
            StringAssert.StartsWith(_emailVerificationsCommand.EmailBounceReason, "X-Postfix; host smtpin.ntlworld.com[81.103.221.10] said: 550");
        }

        [TestMethod]
        public void Smtp550Test()
        {
            var status = PostFile(FileSystem.GetAbsolutePath(@"Apps\Management\Test\EmailStatus\TestData\Smtp550Test.eml", RuntimeEnvironment.GetSourceFolder()));
            Assert.AreEqual(HttpStatusCode.NoContent, status);
            Assert.AreEqual("arathib@vnet.ibm.com", _emailVerificationsCommand.Member.GetBestEmailAddress().Address);
            Assert.IsFalse(_emailVerificationsCommand.Member.GetBestEmailAddress().IsVerified);
            StringAssert.StartsWith("smtp; 550 'arathib@vnet.IBM.COM' is not a registered gateway user", _emailVerificationsCommand.EmailBounceReason);
        }

        [TestMethod]
        public void Said554Test()
        {
            var status = PostFile(FileSystem.GetAbsolutePath(@"Apps\Management\Test\EmailStatus\TestData\Said554Test.eml", RuntimeEnvironment.GetSourceFolder()));
            Assert.AreEqual(HttpStatusCode.NoContent, status);
            Assert.IsNull(_emailVerificationsCommand.Member);
        }

        [TestMethod]
        public void Smtp446Test()
        {
            var status = PostFile(FileSystem.GetAbsolutePath(@"Apps\Management\Test\EmailStatus\TestData\Smtp446Test.eml", RuntimeEnvironment.GetSourceFolder()));
            Assert.AreEqual(HttpStatusCode.NoContent, status);
            Assert.IsNull(_emailVerificationsCommand.Member);
        }

        [TestMethod]
        public void Smtp200Test()
        {
            var status = PostFile(FileSystem.GetAbsolutePath(@"Apps\Management\Test\EmailStatus\TestData\Smtp200Test.eml", RuntimeEnvironment.GetSourceFolder()));
            Assert.AreEqual(HttpStatusCode.NoContent, status);
            Assert.IsNull(_emailVerificationsCommand.Member);
        }

        private static HttpStatusCode PostFile(string fileName)
        {
            var request = (HttpWebRequest) WebRequest.Create(BaseUrl);
            request.Method = "POST";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.ContentType = "text/plain";

            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var requestStream = request.GetRequestStream())
                {
                    StreamUtil.CopyStream(fileStream, requestStream);
                }
            }

            var response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode;
        }
    }
}