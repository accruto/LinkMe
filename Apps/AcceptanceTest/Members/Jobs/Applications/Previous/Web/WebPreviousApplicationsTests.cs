using HtmlAgilityPack;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application = LinkMe.Domain.Roles.Contenders.Application;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.Previous.Web
{
    [TestClass]
    public class WebPreviousApplicationsTests
        : PreviousApplicationsTests
    {
        private ReadOnlyUrl _previousApplicationsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _previousApplicationsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/networkers/previousapplications.aspx");
        }

        [TestMethod]
        public void TestNoApplications()
        {
            var member = CreateMember();

            LogIn(member);
            Get(_previousApplicationsUrl);
            AssertUrl(_previousApplicationsUrl);

            AssertPageContains("You currently have no job applications.");
            AssertNoApplications();
        }

        [TestMethod]
        public void TestInternalApplication()
        {
            // Apply.

            var member = CreateMember();
            var employer = CreateEmployer();
            var jobAd = CreateInternalJobAd(employer);
            var application = ApplyForInternalJob(jobAd, member.Id);

            // View the page.

            LogIn(member);
            Get(_previousApplicationsUrl);
            AssertUrl(_previousApplicationsUrl);

            AssertPageDoesNotContain("You currently have no job applications.");
            AssertApplication(jobAd, application);
        }

        [TestMethod]
        public void TestExternalApplication()
        {
            // Apply.

            var member = CreateMember();
            var employer = CreateEmployer();
            var jobAd = CreateExternalJobAd(employer);
            var application = ApplyForExternalJob(jobAd, member.Id);

            // View the page.

            LogIn(member);
            Get(_previousApplicationsUrl);
            AssertUrl(_previousApplicationsUrl);

            AssertPageDoesNotContain("You currently have no job applications.");
            AssertApplication(jobAd, application);
        }

        [TestMethod]
        public void TestInternalExternalApplication()
        {
            // Apply.

            var member = CreateMember();
            var employer = CreateEmployer();
            var jobAd = CreateInternalJobAd(employer);

            // For historic reasons it is possible there will be an internal and an external application ...

            var application = ApplyForInternalJob(jobAd, member.Id);
            ApplyForExternalJob(jobAd, member.Id);

            // View the page.

            LogIn(member);
            Get(_previousApplicationsUrl);
            AssertUrl(_previousApplicationsUrl);

            AssertPageDoesNotContain("You have not applied for any jobs.");
            AssertApplication(jobAd, application);
        }

        private void AssertNoApplications()
        {
            Assert.IsNull(GetApplicationNodes());
        }

        private void AssertApplication(JobAd jobAd, Application application)
        {
            var applicationNodes = GetApplicationNodes();
            Assert.AreEqual(1, applicationNodes.Count);
            AssertApplication(applicationNodes[0], jobAd, application);
        }

        private void AssertApplication(HtmlNode node, JobAd jobAd, Application application)
        {
            Assert.AreEqual(GetJobAdUrl(jobAd).Path.ToLower(), node.SelectSingleNode("td/a").Attributes["href"].Value.ToLower());
            Assert.AreEqual(application is InternalApplication ? "New" : "Managed externally", node.SelectSingleNode("td[position()=3]").InnerText.Trim());
        }

        private HtmlNodeCollection GetApplicationNodes()
        {
            return Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@id='job-ad-table']/tr[position()>1]");
        }
    }
}