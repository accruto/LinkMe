using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Configuration;
using LinkMe.Web.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.PostJobAds
{
    [TestClass]
    public class PostJobAdContentTests
        : PostJobAdsTests
    {
        private const int MaxSummaryLength = 300;

        private const string SamplePostJobAdsFile = @"Test\Data\Jobs\SamplePostJobAds.xml";

        private const string RealJobAdText = @"This is the real job ad text. This is the real job ad text.
            This is the real job ad text. This is the real job ad text. This is the real job ad text. 
            This is the real job ad text. This is the real job ad text. This is the real job ad text. 
            This is the real job ad text. This is the real job ad text.";
        private const string ExtraMarquees1 = @"<center><span style=""color: red; font-size: 15px; font-family: Arial; text-decoration: blink;"">
            ---NOW RECRUITING---</span><br><marquee width=""100%"" direction=""left"" scrolldelay=""65""
            style=""font-family: Verdana; font-size: 15px; color: black;""><b>
            <br>...........www. Structural jobs.com.au......................Structural Drafting Manager
            $115-100K Post Review? Still not happy?.................
            www. Structural jobs.com.au.......................Structural Designer $75-68K Juicy Large
            Scale Projects................</b></marquee></center><br><br><p>";

        private ReadOnlyUrl _jobAdsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _jobAdsUrl = new ReadOnlyApplicationUrl("~/jobads");
        }

        [TestMethod]
        public void TestLifetime()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(0);

            // Submit a new job ad.

            var jobAd1 = CreateJobAd(1);
            PostJobAds(integratorUser, employer, true, new[] { jobAd1 }, 1, 0, 0, 0);
            AssertJobAd(jobAd1, AssertJobAd(integratorUser, employer.Id, jobAd1.ExternalReferenceId, JobAdStatus.Open));

            // Update the first and submit a second.

            var jobAd2 = CreateJobAd(2);

            jobAd1.Title = "Updated ad one title";
            jobAd1.ContactDetails.PhoneNumber = "02 4444 4444";
            jobAd1.Industries = new List<string> { "Accounting" };
            jobAd1.Location = "Melbourne";
            jobAd1.Postcode = "3000";

            PostJobAds(integratorUser, employer, true, new[] { jobAd1, jobAd2 }, 1, 1, 0, 0);
            AssertJobAd(jobAd1, AssertJobAd(integratorUser, employer.Id, jobAd1.ExternalReferenceId, JobAdStatus.Open));
            AssertJobAd(jobAd2, AssertJobAd(integratorUser, employer.Id, jobAd2.ExternalReferenceId, JobAdStatus.Open));

            // Update the second.

            jobAd2.Title = "Updated ad 2 title";

            PostJobAds(integratorUser, employer, false, new[] { jobAd2 }, 0, 1, 0, 0);
            AssertJobAd(jobAd1, AssertJobAd(integratorUser, employer.Id, jobAd1.ExternalReferenceId, JobAdStatus.Open));
            AssertJobAd(jobAd2, AssertJobAd(integratorUser, employer.Id, jobAd2.ExternalReferenceId, JobAdStatus.Open));

            // Close the first by updating the second.

            PostJobAds(integratorUser, employer, true, new[] { jobAd2 }, 0, 1, 1, 0);
            AssertJobAd(integratorUser, employer.Id, jobAd1.ExternalReferenceId, JobAdStatus.Closed);
            AssertJobAd(integratorUser, employer.Id, jobAd2.ExternalReferenceId, JobAdStatus.Open);

            // Close the second.

            CloseJobAds(integratorUser, employer, new[] { jobAd2 }, 1, 0);
            AssertJobAd(integratorUser, employer.Id, jobAd1.ExternalReferenceId, JobAdStatus.Closed);
            AssertJobAd(integratorUser, employer.Id, jobAd2.ExternalReferenceId, JobAdStatus.Closed);

            // Close both which should still succeed.

            CloseJobAds(integratorUser, employer, new[] { jobAd1, jobAd2 }, 2, 0);
            AssertJobAd(integratorUser, employer.Id, jobAd1.ExternalReferenceId, JobAdStatus.Closed);
            AssertJobAd(integratorUser, employer.Id, jobAd2.ExternalReferenceId, JobAdStatus.Closed);

            // Close both with a non-existing ad.

            var jobAd3 = CreateJobAd(3);
            CloseJobAds(integratorUser, employer, new[] { jobAd1, jobAd2, jobAd3 }, 2, 1, "There is no job ad with external reference ID 'TEST0003' for the specified employer/integrator combination.");
            AssertJobAd(integratorUser, employer.Id, jobAd1.ExternalReferenceId, JobAdStatus.Closed);
            AssertJobAd(integratorUser, employer.Id, jobAd2.ExternalReferenceId, JobAdStatus.Closed);
        }

        [TestMethod]
        public void TestStripExtraText()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(0);

            var jobAd = CreateJobAd(1);
            jobAd.Content = RealJobAdText + ExtraMarquees1;

            // Submit ad.

            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 1, 0, 0, 0);
            jobAd.Content = RealJobAdText;
            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Open));
        }

        [TestMethod]
        public void TestErrors()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(0);

            // Empty job ad.

            PostJobAds(integratorUser, employer, false, new[] { new JobAdElement() }, 0, 0, 0, 1, "The content is required.", "The title is required.");

            // Create a good ad.

            var jobAd = CreateJobAd(1);
            PostJobAds(integratorUser, employer, false, new[] { jobAd }, 1, 0, 0, 0);
            var previous = AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Open);

            // Unsucessful update.

            jobAd.Summary = new string('x', MaxSummaryLength + 1);
            PostJobAds(integratorUser, employer, false, new[] { jobAd }, 0, 0, 0, 1, string.Format("The summary must be no more than {0} characters in length.", MaxSummaryLength));
            AssertJobAd(previous, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Open));

            // Unsucessful update with closeAllOtherAds=true.

            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 0, 0, 0, 1, string.Format("The summary must be no more than {0} characters in length.", MaxSummaryLength));
            AssertJobAd(previous, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Open));
        }

        [TestMethod]
        public void TestNoIndustryErrors()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(0);

            // Unrecognised industry.

            var jobAd = CreateJobAd(1);
            jobAd.Industries = new List<string> { "Non-existing industry" };

            PostJobAds(integratorUser, employer, false, new[] { jobAd }, 1, 0, 0, 0);

            jobAd.Industries = null;
            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Open));
        }

        [TestMethod]
        public void TestSalary()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(0);

            // Case 4099 - minimum and maximum salaries are swapped around in the PostJobAds request, but should
            // be posted successfully and should be correct when the ad is read back.

            var jobAd = CreateJobAd(1);
            var swappedSalaryAd = CreateJobAd(1);
            swappedSalaryAd.Salary = GetSwappedSalary(jobAd.Salary);

            PostJobAds(integratorUser, employer, true, new[] { swappedSalaryAd }, 1, 0, 0, 0);
            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, swappedSalaryAd.ExternalReferenceId, JobAdStatus.Open));
        }

        [TestMethod]
        public void TestSalaryErrors()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(0);

            // One bad job ad and one good one.

            var jobAd1 = CreateJobAd(1);

            var jobAd2 = CreateJobAd(2);
            jobAd2.Salary = new Salary { LowerBound = -20000M, UpperBound = 50000, Rate = SalaryRate.Year, Currency = Currency.AUD };

            PostJobAds(integratorUser, employer, false, new[] { jobAd1, jobAd2 }, 1, 0, 0, 1, "The lower and upper bounds for the salary must not be negative.");
            AssertJobAd(jobAd1, AssertJobAd(integratorUser, employer.Id, jobAd1.ExternalReferenceId, JobAdStatus.Open));
            Assert.AreEqual(0, _jobAdIntegrationQuery.GetJobAdIds(integratorUser.Id, employer.Id, jobAd2.ExternalReferenceId).Count);
        }

        [TestMethod]
        public void TestLocation()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(0);

            var index = 0;

            // Empty location.

            TestLocation(integratorUser, employer, ++index, null, "Australia");

            // Locality

            TestLocation(integratorUser, employer, ++index, "Armadale VIC 3143", "", "Armadale VIC 3143");

            // Region

            TestLocation(integratorUser, employer, ++index, "Melbourne", "Melbourne", "Melbourne");

            // Subdivision

            TestLocation(integratorUser, employer, ++index, "VIC", "VIC", "VIC");
            TestLocation(integratorUser, employer, ++index, "Victoria", "VIC", "VIC");

            // Rubbish

            TestLocation(integratorUser, employer, ++index, "xyz", "xyz", "xyz");

            // Explicitly set some locations and postcodes.

            TestLocation(integratorUser, employer, ++index, "Armadale VIC 3143", "", "Armadale VIC 3143");
            TestLocation(integratorUser, employer, ++index, "Armadale VIC", "3143", "Armadale VIC 3143");
            TestLocation(integratorUser, employer, ++index, "Armadale", "3143", "Armadale VIC 3143");
            TestLocation(integratorUser, employer, ++index, "", "3143", "3143 VIC");
            TestLocation(integratorUser, employer, ++index, "VIC", "3143", "3143 VIC");
            TestLocation(integratorUser, employer, ++index, "VIC 3143", "", "3143 VIC");
        }

        [TestMethod]
        public void TestBadLocations()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(0);

            var index = 0;

            // These were actual job location sent through from candle which were causing failures but now should get through.

            TestLocation(integratorUser, employer, ++index, "SYDNEY, NSW, AU", "20", "SYDNEY, NSW, AU");
            TestLocation(integratorUser, employer, ++index, "SYDNEY, NSW, AU", "2", "SYDNEY, NSW, AU");

            // These have valid postcodes but the location does not resolve fully.

            TestLocation(integratorUser, employer, ++index, "SYDNEY, NSW, AU", "2060", "SYDNEY, NSW, AU 2060");
            TestLocation(integratorUser, employer, ++index, "SYDNEY, NSW, AU", "2017", "SYDNEY, NSW, AU 2017");

            // This one resolves fully.

            TestLocation(integratorUser, employer, ++index, "SYDNEY, NSW, AU", "2000", "Sydney NSW 2000");
        }

        [TestMethod]
        public void TestFailure()
        {
            var integratorUser = CreateIntegratorUser(0);

            // Garbage data.

            TestFailure(integratorUser, "blah", "Data at the root level is invalid");

            // Non-existing job poster ID

            const string requestXml = "<PostJobAdsRequest version='1.1' xmlns='http://www.linkme.com.au/'>"
                + " <JobAds jobPosterUserId='no such job poster' closeAllOtherAds='true'></JobAds>"
                    + "</PostJobAdsRequest>";
            TestFailure(integratorUser, requestXml, "no job poster with user ID 'no such job poster'");

            // Invalid credentials.

            TestFailure((string) null, null, requestXml, "No username was specified");
            TestFailure("someuser", "", requestXml, "No password was specified");
            TestFailure("baduser", "somepassword", requestXml, "unknown user 'baduser'");
            TestFailure(integratorUser.LoginId, "badpassword", requestXml, " the password for user '" + integratorUser.LoginId + "' is incorrect");

            // Check that the password is case-sensitive.

            TestFailure(integratorUser.LoginId, "PASSWORD".ToUpper(), requestXml, " the password for user '" + integratorUser.LoginId + "' is incorrect");

            // An integrator with a valid username password who doesn't have permissions to this service.

            integratorUser = CreateIntegratorUser(1, IntegratorPermissions.GetJobApplication);
            TestFailure(integratorUser.LoginId, "password", requestXml, string.Format("Web service authorization failed: user '{0}' does not have permission to access the requested service.", integratorUser.LoginId));
        }

        [TestMethod]
        public void TestSampleJobAds()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(0);

            // Load the file.

            string requestXml;
            using (TextReader tr = new StreamReader(FileSystem.GetAbsolutePath(SamplePostJobAdsFile, RuntimeEnvironment.GetSourceFolder())))
            {
                requestXml = tr.ReadToEnd();
            }

            requestXml = requestXml.Replace("jobPosterUserId=\"nizzarecruitment-ats\"", "jobPosterUserId=\"" + employer.GetLoginId() + "\"");

            // Call the service.

            var responseXml = Post(_jobAdsUrl, integratorUser, Password, requestXml);

            // Load the XML.

            var xmlDocument = CreateDocument(responseXml);
            var xmlNsMgr = CreateNamespaceManager(xmlDocument);

            // Select the top level response element, ensuring there are no errors.

            const WebServiceReturnCode returnCode = WebServiceReturnCode.Success;
            var xmlResponse = SelectResponse(xmlDocument, xmlNsMgr, "PostJobAdsResponse", returnCode);

            // Assert stats.

            var xmlJobAds = xmlResponse.SelectSingleNode("lm:JobAds", xmlNsMgr);
            Assert.IsNotNull(xmlJobAds);
            AssertAttributeValue(xmlJobAds, "added", 157.ToString());
            AssertAttributeValue(xmlJobAds, "updated", 0.ToString());
            AssertAttributeValue(xmlJobAds, "closed", 0.ToString());
            AssertAttributeValue(xmlJobAds, "failed", 0.ToString());
        }

        [TestMethod]
        public void TestDoubleApostrophe()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(0);

            const string testSummary = "This job ad's summary contains a two apostrophes. It's working fine.";
            const string testContent = "This job ad's content contains a two apostrophes. It's getting truncated.";

            // Bug 7104 - the job ad content between the two apostrophes is not shown on the View Job Ad page.

            var jobAd = CreateJobAd(0);
            jobAd.Content = testContent;
            jobAd.Summary = testSummary;

            PostJobAds(integratorUser, employer, false, new[] { jobAd }, 1, 0, 0, 0);

            // Browse to the job ad on the site.

            var ids = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open);
            Assert.AreEqual(1, ids.Count);

            Get(new ReadOnlyApplicationUrl("~/jobs/x/y/z/" + ids[0]));
            AssertPageContains(jobAd.Title);
            AssertPageContains(jobAd.Content);
        }

        [TestMethod]
        public void TestRepostWithCloseAllAdsUpdates()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(0);

            var jobAd = CreateJobAd(0);
            jobAd.ExternalReferenceId = "monkey";

            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 1, 0, 0, 0);

            var secondJobAd = CreateJobAd(0);

            // This will close off the first job ad
            PostJobAds(integratorUser, employer, true, new[] { secondJobAd }, 1, 0, 1, 0);

            // When we post the first ad again, it should be marked as updated, not added
            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 0, 1, 1, 0);
        }

        #region Static methods

        private static void AssertFailure(string responseXml, string rootElementName, string[] errorSubstrings)
        {
            // Load the XML.

            var xmlDocument = CreateDocument(responseXml);
            var xmlNsMgr = CreateNamespaceManager(xmlDocument);

            // Select the response.

            var xmlResponse = SelectResponse(xmlDocument, xmlNsMgr, rootElementName, WebServiceReturnCode.Failure);

            // Assert the errors.

            AssertErrors(xmlResponse, xmlNsMgr, errorSubstrings);
        }

        private static XmlDocument CreateDocument(string xml)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            return xmlDocument;
        }

        private static XmlNamespaceManager CreateNamespaceManager(XmlDocument xmlDocument)
        {
            var xmlNsMgr = new XmlNamespaceManager(xmlDocument.NameTable);
            xmlNsMgr.AddNamespace("lm", WebConstants.WEB_SERVICE_NAMESPACE);
            return xmlNsMgr;
        }

        private static XmlNode SelectResponse(XmlNode xmlDocument, XmlNamespaceManager xmlNsMgr, string rootElementName, WebServiceReturnCode returnCode)
        {
            // Select the node.

            var xmlResponse = xmlDocument.SelectSingleNode("lm:" + rootElementName, xmlNsMgr);
            Assert.IsNotNull(xmlResponse);

            // Assert the return code.

            var xmlReturnCode = xmlResponse.SelectSingleNode("lm:ReturnCode", xmlNsMgr);
            Assert.IsNotNull(xmlReturnCode);

            Assert.AreEqual(
                returnCode.ToString(),
                xmlReturnCode.InnerText,
                string.Format("The expected return code was '{0}', but the actual return code was '{1}'. Response XML: {2}", returnCode, xmlReturnCode.InnerText, xmlDocument.InnerXml));

            return xmlResponse;
        }

        private static void AssertErrors(XmlNode xmlResponse, XmlNamespaceManager xmlNsMgr, string[] errorSubstrings)
        {
            // Select the error nodes.

            var xmlErrors = xmlResponse.SelectNodes("lm:Errors/lm:Error", xmlNsMgr);
            Assert.IsNotNull(xmlErrors);
            Assert.IsTrue(xmlErrors.Count > 0);

            // Look for all errors.

            var errorsToFind = new ArrayList(errorSubstrings);
            foreach (XmlNode xmlError in xmlErrors)
            {
                for (var i = 0; i < errorsToFind.Count; i++)
                {
                    if (xmlError.InnerText.IndexOf((string)errorsToFind[i]) != -1)
                    {
                        errorsToFind.RemoveAt(i);
                        break;
                    }
                }
            }

            if (errorsToFind.Count > 0)
            {
                Assert.Fail("The following error sub-strings were not found in the response XML: '"
                    + string.Join("', '", (string[])errorsToFind.ToArray(typeof(string)))
                        + "'. The error XML was: " + xmlResponse.OuterXml);
            }
        }

        private static void AssertAttributeValue(XmlNode xmlElement, string attributeName, string expected)
        {
            var xmlAttribute = xmlElement.Attributes.GetNamedItem(attributeName);
            Assert.IsNotNull(xmlAttribute);
            Assert.AreEqual(expected, xmlAttribute.InnerText, "XML attribute '" + attributeName + "' value was '" + xmlAttribute.InnerText + "', but the expected value was '" + expected + "'.");
        }

        private void TestFailure(IntegratorUser integratorUser, string requestXml, params string[] errorSubstrings)
        {
            TestFailure(integratorUser.LoginId, "password", requestXml, errorSubstrings);
        }

        private void TestFailure(string integratorLoginId, string integratorPassword, string requestXml, params string[] errorSubstrings)
        {
            var responseXml = Post(_jobAdsUrl, new IntegratorUser { LoginId = integratorLoginId }, integratorPassword, requestXml);
            AssertFailure(responseXml, "PostJobAdsResponse", errorSubstrings);
        }

        private static Salary GetSwappedSalary(Salary original)
        {
            return new Salary { LowerBound = original.UpperBound, UpperBound = original.LowerBound, Rate = original.Rate, Currency = original.Currency};
        }

        private void TestLocation(IntegratorUser integratorUser, Employer jobPoster, int index, string location, string expected)
        {
            var jobAd = CreateJobAd(index, j => { j.Location = location; j.Postcode = null; });
            PostJobAds(integratorUser, jobPoster, false, new[] { jobAd }, 1, 0, 0, 0);
            var found = AssertJobAd(integratorUser, jobPoster.Id, jobAd.ExternalReferenceId, JobAdStatus.Open);
            AssertJobAd(jobAd, found);
            TestLocation(found.Id, expected);
        }

        private void TestLocation(IntegratorUser integratorUser, Employer jobPoster, int index, string location, string postcode, string expected)
        {
            var jobAd = CreateJobAd(index, j => { j.Location = location; j.Postcode = postcode; });

            // Explicitly set the location and postcode.

            jobAd.Location = location;
            jobAd.Postcode = postcode;

            PostJobAds(integratorUser, jobPoster, false, new[] { jobAd }, 1, 0, 0, 0);
            var found = AssertJobAd(integratorUser, jobPoster.Id, jobAd.ExternalReferenceId, JobAdStatus.Open);

            AssertJobAd(jobAd, found);

            TestLocation(found.Id, expected);
        }

        private void TestLocation(Guid jobAdId, string expected)
        {
            Get(new ReadOnlyApplicationUrl("~/jobs/x/y/z/" + jobAdId));

            // First item is the location.

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='summary']//div[@class='item']/div[@class='desc']");
            Assert.IsTrue(nodes.Count > 0);
            Assert.AreEqual(expected, nodes[0].InnerText);
        }

        #endregion
    }
}