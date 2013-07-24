using System.ServiceModel;
using System.Web.Services.Protocols;
using LinkMe.Apps.Integration.Test.JobG8.AdvertPostService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Integration.Test.JobG8
{
    [TestClass]
    public class SoapTests
    {
        private const string Url = "http://localhost:8001/Integration/Test/JobG8/MockAdvertPostService";
        private ServiceHost _host;
        private MockAdvertPostService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _service = new MockAdvertPostService();
            _host = new ServiceHost(_service);
            _host.Open();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _host.Close();
        }

        [TestMethod]
        public void CanInvokePostAdvertTest()
        {
            var soapClient = new AdvertPostServicePort
            {
                Url = Url
            };

            var request = new PostAdvertType
            {
                Adverts = new PostAdvertTypeAdverts 
                {   
                    AccountNumber = "123456",
                    PostAdvert = new[] { new PostAdvertTypeAdvertsPostAdvert() }
                }   
            };

            PostAdvertResponse response = soapClient.PostAdvert(request);

            Assert.AreEqual("123456", _service.AccountNumber);
            Assert.AreEqual(1, _service.AdvertCount);
            Assert.AreEqual("Hello", response.Success);
        }

        [TestMethod]
        public void CanInvokeAmendAdvertTest()
        {
            var soapClient = new AdvertPostServicePort
            {
                Url = Url
            };

            var request = new AmendAdvertType
            {
                Adverts = new AmendAdvertTypeAdverts
                {
                    AccountNumber = "123456",
                    AmendAdvert = new[] { new AmendAdvertTypeAdvertsAmendAdvert() }
                }
            };

            AmendAdvertResponse response = soapClient.AmendAdvert(request);

            Assert.AreEqual("123456", _service.AccountNumber);
            Assert.AreEqual(1, _service.AdvertCount);
            Assert.AreEqual("Hello", response.Success);
        }

        [TestMethod]
        public void CanInvokeDeleteAdvertTest()
        {
            var soapClient = new AdvertPostServicePort
            {
                Url = Url
            };

            var request = new DeleteAdvertType
            {
                Adverts = new DeleteAdvertTypeAdverts
                {
                    AccountNumber = "123456",
                    DeleteAdvert = new[] { new DeleteAdvertTypeAdvertsDeleteAdvert() }
                }
            };

            DeleteAdvertResponse response = soapClient.DeleteAdvert(request);

            Assert.AreEqual("123456", _service.AccountNumber);
            Assert.AreEqual("Hello", response.Success);
        }

        [TestMethod]
        [ExpectedException(typeof(SoapHeaderException), "Bad user")]
        public void FaultExceptionTest()
        {
            var soapClient = new AdvertPostServicePort
            {
                Url = Url,
                UserCredentials = new Credentials
                {
                    Username = "BadUser"
                }
            };

            soapClient.PostAdvert(new PostAdvertType());
        }

        [TestMethod]
        [ExpectedException(typeof(SoapException), "Bad password")]
        public void ArbitraryExceptionTest()
        {
            var soapClient = new AdvertPostServicePort
            {
                Url = Url,
                UserCredentials = new Credentials
                {
                    Password = "BadPassword"
                }
            };

            soapClient.PostAdvert(new PostAdvertType());
        }
    }
}
