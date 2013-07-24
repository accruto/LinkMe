using HtmlAgilityPack;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.Previous.Mobile
{
    [TestClass]
    public class MobilePreviousApplicationsTests
        : PreviousApplicationsTests
    {
        private ReadOnlyUrl _applicationsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _applicationsUrl = new ReadOnlyApplicationUrl(true, "~/members/jobs/applications");
            Browser.UseMobileUserAgent = true;
        }

        [TestMethod]
        public void TestNoApplications()
        {
            var member = CreateMember();

            LogIn(member);
            Get(_applicationsUrl);
            AssertUrl(_applicationsUrl);

            AssertPageContains("You have not applied for any jobs.");
            AssertNoApplications();
        }

        [TestMethod]
        public void TestInternalApplication()
        {
            // Apply.

            var member = CreateMember();
            var employer = CreateEmployer();
            var jobAd = CreateInternalJobAd(employer);
            ApplyForInternalJob(jobAd, member.Id);

            // View the page.

            LogIn(member);
            Get(_applicationsUrl);
            AssertUrl(_applicationsUrl);

            AssertPageDoesNotContain("You have not applied for any jobs.");
            AssertApplication(jobAd);
        }

        [TestMethod]
        public void TestExternalApplication()
        {
            // Apply.

            var member = CreateMember();
            var employer = CreateEmployer();
            var jobAd = CreateExternalJobAd(employer);
            ApplyForExternalJob(jobAd, member.Id);

            // View the page.

            LogIn(member);
            Get(_applicationsUrl);
            AssertUrl(_applicationsUrl);

            AssertPageDoesNotContain("You have not applied for any jobs.");
            AssertApplication(jobAd);
        }

        [TestMethod]
        public void TestInternalExternalApplication()
        {
            // Apply.

            var member = CreateMember();
            var employer = CreateEmployer();
            var jobAd = CreateInternalJobAd(employer);

            // For historic reasons it is possible there will be an internal and an external application ...

            ApplyForInternalJob(jobAd, member.Id);
            ApplyForExternalJob(jobAd, member.Id);

            // View the page.

            LogIn(member);
            Get(_applicationsUrl);
            AssertUrl(_applicationsUrl);

            AssertPageDoesNotContain("You have not applied for any jobs.");
            AssertApplication(jobAd);
        }

        private void AssertNoApplications()
        {
            Assert.IsNull(GetJobAdNodes());
        }

        private void AssertApplication(IJobAd jobAd)
        {
            var applicationNodes = GetJobAdNodes();
            Assert.AreEqual(1, applicationNodes.Count);
            AssertApplication(applicationNodes[0], jobAd);
        }

        private static void AssertApplication(HtmlNode node, IJobAd jobAd)
        {
            Assert.AreEqual(jobAd.Id.ToString(), node.Attributes["id"].Value);
        }

        private HtmlNodeCollection GetJobAdNodes()
        {
            return Browser.CurrentHtml.DocumentNode.SelectNodes("//div[starts-with(@class, 'jobad-list-view')]");
        }
    }
}