using System;
using System.IO;
using System.Xml.Serialization;
using LinkMe.Apps.Services.External.HrCareers;
using LinkMe.Framework.Utility.Wcf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinkMe.Apps.Services.External.HrCareers.Schema;

namespace LinkMe.Apps.Services.Test.External.HrCareers
{
    [TestClass, Ignore]
    public class IntegrationTests
    {
        private const string Username = "linkme";
        private const string Password = "xy4TWj86m";

        private WcfHttpChannelManager<ISyndicate> _channelManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _channelManager = new WcfHttpChannelManager<ISyndicate>(
                "http://www.hrcareers.com.au/component/syndicate.cfc", "HrCareerTest");
        }

        [TestMethod]
        public void CanAuthenticate()
        {
            const string requestXml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><jobs companyid=\"348\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"jobschema.xsd\"></jobs>";
            string response;

            var channel = _channelManager.Create();
            try
            {
                response = channel.Sync(requestXml, Username, Password);
            }
            catch (Exception)
            {
                _channelManager.Abort(channel);
                throw;
            }
            _channelManager.Close(channel);

            Assert.AreEqual("0 rows processed", response);
        }

        [TestMethod]
        public void CanSubmitEmptyCollection()
        {
            var request = new JobCollection {companyid = 348};
            var serializer = new XmlSerializer(typeof(JobCollection));
            var writer = new StringWriter();
            serializer.Serialize(writer, request);
            writer.Flush();
            var requestXml = writer.ToString();
            string response;

            var channel = _channelManager.Create();
            try
            {
                response = channel.Sync(requestXml, Username, Password);
            }
            catch (Exception)
            {
                _channelManager.Abort(channel);
                throw;
            }
            _channelManager.Close(channel);

            Assert.AreEqual("0 rows processed", response);
        }

        [TestMethod]
        public void CanSubmitJob()
        {
            var category = new JobCategory { id = 27, name = "Compensation/Return-to-work" };

            var job = new Job
                          {
                              id = "187526",
                              title = "This is a TEST job",
                              description = "<strong>This is a TEST Job - Please dont apply</strong> Lorem ipsum dolor sit amet",
                              reference = "Dont Apply",
                              startdate = DateTime.Now, startdateSpecified = true,
                              categories = new[] { category },
                              jobtype = new JobType { id = 5, name = "Casual" },
                              region = new JobRegion { id = "20-163-5", name = "Sydney" },
                              application = new JobApplication { emailto = "colin.carlton@ahri.com.au", url = "http://ahri.com/Portal/JobApplication.aspx?Job=187526&Source=93310682-4273-4865-889c-0fff01d66ab7" },
                          };

            var request = new JobCollection { companyid = 348, jobs = new[] {job} };
            var serializer = new XmlSerializer(typeof(JobCollection));
            var writer = new StringWriter();
            serializer.Serialize(writer, request);
            writer.Flush();
            var requestXml = writer.ToString();
            string response;

            var channel = _channelManager.Create();
            try
            {
                response = channel.Sync(requestXml, Username, Password);
            }
            catch (Exception)
            {
                _channelManager.Abort(channel);
                throw;
            }
            _channelManager.Close(channel);

            Assert.AreEqual("1 rows processed, 1 updated", response, "Note: if it's been a while since this test was run this *may* say '1 renewed'. Re-running the test will resolve");
        }
    }
}