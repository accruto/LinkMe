using System;
using System.Net;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navigation
{
    /// <summary>
    /// This test case is specifically testing the rewriting of external urls.
    /// The functionality of the pages that are returned is not being tested.
    /// </summary>
    [TestClass]
    public class RewriteTests
        : WebTestClass
    {
        private const string UserId = "employer";

        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsQuery _verticalsQuery = Resolve<IVerticalsQuery>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();
        }

        [TestMethod]
        public void TestTalent2Rewrite()
        {
            var url = new ReadOnlyApplicationUrl("~/partners/t2");
            Get(url);
            AssertUrl(HomeUrl);
        }

        [TestMethod]
        public void TestServiceRewrite()
        {
            var id = Guid.NewGuid();

            // GetJobAds

            var url = new ReadOnlyApplicationUrl("~/jobads");
            Get(url);
            AssertPageContains("<GetJobAdsResponse");

            url = new ReadOnlyApplicationUrl("~/jobads/");
            Get(url);
            AssertPageContains("<GetJobAdsResponse");

            url = new ReadOnlyApplicationUrl("~/jobadids");
            Get(url);
            AssertPageContains("No username was specified in the HTTP request");

            url = new ReadOnlyApplicationUrl("~/jobadids/");
            Get(url);
            AssertPageContains("No username was specified in the HTTP request");

            // CloseJobAds

            url = new ReadOnlyApplicationUrl("~/jobads/close");
            Assert.IsTrue(Post(url).Contains("<CloseJobAdsResponse"));

            url = new ReadOnlyApplicationUrl("~/jobads/close/");
            Assert.IsTrue(Post(url).Contains("<CloseJobAdsResponse"));

            // PostJobAds

            url = new ReadOnlyApplicationUrl("~/jobads");
            Assert.IsTrue(Post(url).Contains("<PostJobAdsResponse"));

            url = new ReadOnlyApplicationUrl("~/jobads/");
            Assert.IsTrue(Post(url).Contains("<PostJobAdsResponse"));

            // GetJobApplication

            url = new ReadOnlyApplicationUrl("~/jobapplication/" + id.ToString("N"));
            Get(url);
            AssertPageContains("<GetJobApplicationResponse");

            url = new ReadOnlyApplicationUrl("~/jobapplication/" + id.ToString("N") + "/");
            Get(url);
            AssertPageContains("<GetJobApplicationResponse");

            // SetJobApplicationStatus

            url = new ReadOnlyApplicationUrl("~/jobapplications/status");
            Assert.IsTrue(Post(url).Contains("<SetJobApplicationStatusResponse"));

            // File

            url = new ReadOnlyApplicationUrl("~/file/" + id.ToString("N"));
            Get(HttpStatusCode.NotFound, url);

            url = new ReadOnlyApplicationUrl("~/file/" + id.ToString("N") + "/FileName.txt");
            Get(HttpStatusCode.NotFound, url);

            url = new ReadOnlyApplicationUrl("~/file/" + id.ToString("N") + "/FileName.ppt");
            Get(HttpStatusCode.NotFound, url);

            // Resume

            url = new ReadOnlyApplicationUrl("~/resume/" + id.ToString("N") + "/file/rtf");
            Get(url);
            AssertPageContains("Error: No username was specified in the HTTP request");
        }

        [TestMethod]
        public void TestJobAdsRewrite()
        {
            var region = _locationQuery.GetRegion(_locationQuery.GetCountry("Australia"), "Melbourne");
            var industry = _industriesQuery.GetIndustry("Accounting");
            var jobAd = PostJobAd(industry, region);

            // Apply.

            var url = new ReadOnlyApplicationUrl("~/jobads/" + jobAd.Id.ToString("N") + "/apply");
            Get(url);
            AssertUrl(new ReadOnlyApplicationUrl(true, "~/jobs/" + region.UrlName + "/" + industry.UrlName + "/" + jobAd.Title.ToLower() + "/" + jobAd.Id));

            // View.

            url = new ReadOnlyApplicationUrl("~/jobads/" + jobAd.Id);
            Get(url);
            AssertUrl(new ReadOnlyApplicationUrl(true, "~/jobs/" + region.UrlName + "/" + industry.UrlName + "/" + jobAd.Title.ToLower() + "/" + jobAd.Id));
        }

        [TestMethod]
        public void TestJobsRewrite()
        {
            var region = _locationQuery.GetRegion(_locationQuery.GetCountry("Australia"), "Melbourne");

            // Choose an industry that has a url alias.

            var industry = _industriesQuery.GetIndustry("Healthcare, Medical & Pharmaceutical");

            // Location and industry.

            var expectedUrl = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlName + "-jobs");

            var url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlName + "-jobs/");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "/" + industry.UrlName + "/");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlName + "-jobs");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "/" + industry.UrlName);
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlAliases[0] + "-jobs/");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "/" + industry.UrlAliases[0] + "/");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlAliases[0] + "-jobs");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "/" + industry.UrlAliases[0]);
            Get(url);
            AssertUrl(expectedUrl);

            // Location and industry and index.

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlName + "-jobs/0/");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlName + "-jobs/0");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlAliases[0] + "-jobs/0/");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlAliases[0] + "-jobs/0");
            Get(url);
            AssertUrl(expectedUrl);

            expectedUrl = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlName + "-jobs");

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlName + "-jobs/1/");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlName + "-jobs/1");
            Get(url);
            AssertUrl(expectedUrl);


            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlAliases[0] + "-jobs/1/");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlAliases[0] + "-jobs/1");
            Get(url);
            AssertUrl(expectedUrl);

            // Job.

            var jobAd = PostJobAd(industry, region);

            expectedUrl = new ReadOnlyApplicationUrl(true, "~/jobs/" + region.UrlName + "/" + industry.UrlName + "/" + jobAd.Title + "/" + jobAd.Id);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlName + "-jobs/" + jobAd.Id.ToString("N") + "-xyz");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "/" + industry.UrlName + "/" + jobAd.Id.ToString("N") + "-xyz");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "-jobs/" + industry.UrlAliases[0] + "-jobs/" + jobAd.Id.ToString("N") + "-xyz");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + region.UrlName + "/" + industry.UrlAliases[0] + "/" + jobAd.Id.ToString("N") + "-xyz");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/jobs/" + jobAd.Id);
            Get(url);
            AssertUrl(expectedUrl);
        }

        [TestMethod]
        public void TestCandidatesRewrite()
        {
            var region = _locationQuery.GetRegion(_locationQuery.GetCountry("Australia"), "Melbourne");

            // Choose an industry that has a url alias.

            var salary = new Salary { LowerBound = 80000, UpperBound = 90000 };

            // Location and band.

            var expectedUrl = new ReadOnlyApplicationUrl(true, "~/candidates/" + region.UrlName + "-candidates/" + GetUrlName(salary) + "-candidates");

            var url = new ReadOnlyApplicationUrl("~/candidates/" + region.UrlName + "-candidates/" + GetUrlName(salary) + "-candidates/");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/candidates/" + region.UrlName + "/" + GetUrlName(salary) + "/");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/candidates/" + region.UrlName + "-candidates/" + GetUrlName(salary) + "-candidates");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/candidates/" + region.UrlName + "/" + GetUrlName(salary));
            Get(url);
            AssertUrl(expectedUrl);

            // Location and band and index.

            url = new ReadOnlyApplicationUrl("~/candidates/" + region.UrlName + "-candidates/" + GetUrlName(salary) + "-candidates/0/");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/candidates/" + region.UrlName + "-candidates/" + GetUrlName(salary) + "-candidates/0");
            Get(url);
            AssertUrl(expectedUrl);

            expectedUrl = new ReadOnlyApplicationUrl(true, "~/candidates/" + region.UrlName + "-candidates/" + GetUrlName(salary) + "-candidates");

            url = new ReadOnlyApplicationUrl("~/candidates/" + region.UrlName + "-candidates/" + GetUrlName(salary) + "-candidates/1/");
            Get(url);
            AssertUrl(expectedUrl);

            url = new ReadOnlyApplicationUrl("~/candidates/" + region.UrlName + "-candidates/" + GetUrlName(salary) + "-candidates/1");
            Get(url);
            AssertUrl(expectedUrl);
        }

        [TestMethod]
        public void TestSearchJobsRewrite()
        {
            // Search

            var searchUrl = new ReadOnlyApplicationUrl("~/search/jobs");
            Get(searchUrl);
            AssertUrl(searchUrl);

            // AdvancedSearch

            var url = new ReadOnlyApplicationUrl("~/search/jobs/advanced");
            Get(url);
            AssertUrl(searchUrl);
        }

        [TestMethod]
        public void TestCommunitiesRewrite()
        {
            var community = TestCommunity.MonashGsb.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            Assert.IsNotNull(community);
            var vertical = _verticalsQuery.GetVertical(community.Id);

            // Home page.

            var url = new ReadOnlyApplicationUrl("~/" + vertical.Url + "/");
            Get(url);
            AssertUrl(HomeUrl);

            // Join.

            url = new ReadOnlyApplicationUrl("~/" + vertical.Url + "/join.aspx");
            Get(url);
            AssertUrl(new ReadOnlyApplicationUrl(true, "~/join"));

            // Login.

            url = new ReadOnlyApplicationUrl("~/" + vertical.Url + "/login.aspx");
            Get(url);
            AssertUrl(LogInUrl);
        }

        private JobAd PostJobAd(Industry industry, NamedLocation location)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(UserId, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = employer.CreateTestJobAd("Manager", "Blah blah blah", industry, new LocationReference(location));
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        private static string GetUrlName(Salary salary)
        {
            salary = salary.ToRate(SalaryRate.Year);
            if (!salary.LowerBound.HasValue || salary.LowerBound.Value == 0)
                return "up-to-" + GetUrlSegment(salary.UpperBound.Value);

            if (!salary.UpperBound.HasValue || salary.UpperBound.Value == 0)
                return GetUrlSegment(salary.LowerBound.Value) + "-and-above";

            return GetUrlSegment(salary.LowerBound.Value) + "-" + GetUrlSegment(salary.UpperBound.Value);
        }

        private static string GetUrlSegment(decimal value)
        {
            return (int)(value / 1000) + "k";
        }
    }
}
