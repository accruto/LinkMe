using HtmlAgilityPack;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.JobAds.ManageCandidates
{
    [TestClass]
    public class JobAdApplicationsTests
        : ManageCandidatesTests
    {
        [TestMethod]
        public void TestNewApplicant()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var member = CreateNewCandidate(jobAd, 0);
            var application = _memberApplicationsQuery.GetApplications(member.Id)[0];

            LogIn(employer);

            // Check.

            AssertNewCandidates((n, m) => AssertHasApplication(n, application), jobAd.Id, member);
            AssertShortlistedCandidates(jobAd.Id);
            AssertRejectedCandidates(jobAd.Id);

            // Shortlist them.

            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] { member.Id });
            AssertNewCandidates(jobAd.Id);
            AssertShortlistedCandidates((n, m) => AssertHasApplication(n, application), jobAd.Id, member);
            AssertRejectedCandidates(jobAd.Id);

            // Reject them.

            _jobAdApplicantsCommand.RejectApplicants(employer, jobAd, new[] { member.Id });
            AssertNewCandidates(jobAd.Id);
            AssertShortlistedCandidates(jobAd.Id);
            AssertRejectedCandidates((n, m) => AssertHasApplication(n, application), jobAd.Id, member);
        }

        [TestMethod]
        public void TestShortlistedApplicant()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var member = CreateShortlistedCandidate(employer, jobAd, 0);

            LogIn(employer);

            // Check.

            AssertNewCandidates(jobAd.Id);
            AssertShortlistedCandidates(AssertHasNoApplication, jobAd.Id, member);
            AssertRejectedCandidates(jobAd.Id);

            // Reject them.

            _jobAdApplicantsCommand.RejectApplicants(employer, jobAd, new[] { member.Id });
            AssertNewCandidates(jobAd.Id);
            AssertShortlistedCandidates(jobAd.Id);
            AssertRejectedCandidates(AssertHasNoApplication, jobAd.Id, member);
        }

        private static void AssertHasApplication(HtmlNode node, Application application)
        {
            AssertApplication(node, application.CreatedTime.ToString("dd MMM yyyy"));
        }

        private static void AssertHasNoApplication(HtmlNode node, Member member)
        {
            AssertApplication(node, "N/A");
        }

        private static void AssertApplication(HtmlNode node, string addedText)
        {
            var dateNode = node.SelectSingleNode(".//div[@class='date-applied js_ellipsis']");
            Assert.IsNotNull(dateNode);
            Assert.AreEqual("Added: " + addedText, dateNode.InnerText);
        }
    }
}
