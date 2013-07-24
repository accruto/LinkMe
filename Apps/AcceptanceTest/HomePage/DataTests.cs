using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using LinkMe.Apps.Agents.Featured;
using LinkMe.Apps.Agents.Featured.Commands;
using LinkMe.Apps.Agents.Featured.Queries;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Reports.Accounts.Queries;
using LinkMe.Query.Reports.Roles.JobAds.Queries;
using LinkMe.Query.Reports.Search.Queries;
using LinkMe.Query.Reports.Users.Employers.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.HomePage
{
    [TestClass]
    public class DataTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IFeaturedCommand _featuredCommand = Resolve<IFeaturedCommand>();
        private readonly IFeaturedQuery _featuredQuery = Resolve<IFeaturedQuery>();
        private readonly IJobAdReportsQuery _jobAdReportsQuery = Resolve<IJobAdReportsQuery>();
        private readonly IAccountReportsQuery _accountReportsQuery = Resolve<IAccountReportsQuery>();
        private readonly IEmployerMemberAccessReportsQuery _employerMemberAccessReportsQuery = Resolve<IEmployerMemberAccessReportsQuery>();
        private readonly IMemberSearchReportsQuery _searchReportsQuery = Resolve<IMemberSearchReportsQuery>();

        private const string JobTitleFormat = "Job title {0}";

        [TestMethod]
        public void TestJobAds()
        {
            // Refresh whatever may be in the cache.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            _featuredCommand.UpdateFeaturedStatistics(GetFeaturedStatistics());
            ClearCache(administrator);

            // No job ads.

            Get(HomeUrl);
            Assert.AreEqual(0, GetJobAds());

            // Post a job ad yesterday.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = employer.CreateTestJobAd();
            jobAd.CreatedTime = DateTime.Now.AddDays(-1);
            _jobAdsCommand.CreateJobAd(jobAd);

            // Should not show up until cache is refreshed.

            Get(HomeUrl);
            Assert.AreEqual(0, GetJobAds());

            _featuredCommand.UpdateFeaturedStatistics(GetFeaturedStatistics());
            ClearCache(administrator);
            Get(HomeUrl);
            Assert.AreEqual(1, GetJobAds());

            // Post another job ad.

            jobAd = employer.CreateTestJobAd();
            jobAd.CreatedTime = DateTime.Now.AddDays(-4);
            _jobAdsCommand.CreateJobAd(jobAd);

            Get(HomeUrl);
            Assert.AreEqual(1, GetJobAds());

            _featuredCommand.UpdateFeaturedStatistics(GetFeaturedStatistics());
            ClearCache(administrator);
            Get(HomeUrl);
            Assert.AreEqual(2, GetJobAds());

            // Post another outside the 7 day timeframe.

            jobAd = employer.CreateTestJobAd();
            jobAd.CreatedTime = DateTime.Now.AddDays(-14);
            _jobAdsCommand.CreateJobAd(jobAd);

            Get(HomeUrl);
            Assert.AreEqual(2, GetJobAds());

            _featuredCommand.UpdateFeaturedStatistics(GetFeaturedStatistics());
            ClearCache(administrator);
            Get(HomeUrl);
            Assert.AreEqual(2, GetJobAds());
        }

        [TestMethod]
        public void TestFeaturedJobAds()
        {
            // Refresh whatever may be in the cache.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            _featuredCommand.UpdateFeaturedJobAds(new FeaturedItem[0]);
            ClearCache(administrator);

            // No job ads.

            Get(HomeUrl);
            Assert.IsNull(GetFeaturedJobAds());

            // Post a job ad yesterday.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd1 = CreateFeaturedJobAd(employer, 1);

            // Should not show up until cache is refreshed.

            Get(HomeUrl);
            Assert.IsNull(GetFeaturedJobAds());

            _featuredCommand.UpdateFeaturedJobAds(GetFeaturedJobAds(jobAd1));
            ClearCache(administrator);
            Get(HomeUrl);
            AssertFeaturedJobAds(jobAd1);

            // Post another job ad.

            var jobAd2 = CreateFeaturedJobAd(employer, 2);
            Get(HomeUrl);
            AssertFeaturedJobAds(jobAd1);

            _featuredCommand.UpdateFeaturedJobAds(GetFeaturedJobAds(jobAd1, jobAd2));
            ClearCache(administrator);
            Get(HomeUrl);
            AssertFeaturedJobAds(jobAd1, jobAd2);

            // Post 12 more.

            var jobAds = new JobAd[14];
            jobAds[0] = jobAd1;
            jobAds[1] = jobAd2;
            for (var index = 2; index < 14; ++index)
                jobAds[index] = CreateFeaturedJobAd(employer, index + 1);

            Get(HomeUrl);
            AssertFeaturedJobAds(jobAd1, jobAd2);

            _featuredCommand.UpdateFeaturedJobAds(GetFeaturedJobAds(jobAds));
            ClearCache(administrator);
            Get(HomeUrl);
            AssertFeaturedJobAds(jobAds);
        }

        [TestMethod]
        public void TestFeaturedEmployers()
        {
            Get(HomeUrl);

            var employers = _featuredQuery.GetFeaturedEmployers().OrderBy(e => e.Name).ToList();
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='homepage-section jobs-by-employers']//div[@class='homepage-centered-image']/img");

            Assert.IsNotNull(nodes);
            Assert.AreEqual(employers.Count, nodes.Count);
            foreach (var node in nodes)
            {
                var found = false;

                var url = HttpUtility.HtmlDecode(node.Attributes["src"].Value);
                foreach (var employer in employers)
                {
                    // The url is a content url and therefore contains versioning information, so ignore that.

                    if (!employer.LogoUrl.StartsWith("~/content", StringComparison.InvariantCultureIgnoreCase))
                        Assert.Fail("Featured employer logo url '" + employer.LogoUrl + "' is not a content url.");

                    var logoUrl = employer.LogoUrl.Substring("~/content".Length);
                    if (url.EndsWith(logoUrl, StringComparison.InvariantCultureIgnoreCase))
                    {
                        Assert.IsTrue(string.Compare("javascript:loadPage('" + new ReadOnlyApplicationUrl("~/search/jobs", new ReadOnlyQueryString("advertiser", employer.Name, "SortOrder", "2")).PathAndQuery + "');", node.Attributes["onclick"].Value, true) == 0);
                        found = true;
                        break;
                    }
                }

                Assert.IsTrue(found);
            }
        }

        private FeaturedStatistics GetFeaturedStatistics()
        {
            var today = DateTime.Today;
            var dateRange = new DateRange(today.AddDays(-7), today);

            return new FeaturedStatistics
            {
                CreatedJobAds = _jobAdReportsQuery.GetCreatedJobAds(dateRange),
                Members = _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now),
                MemberSearches = _searchReportsQuery.GetAllMemberSearches(dateRange),
                MemberAccesses = _employerMemberAccessReportsQuery.GetMemberAccesses(dateRange)
            };
        }

        private static IEnumerable<FeaturedItem> GetFeaturedJobAds(params JobAd[] jobAds)
        {
            return from j in jobAds
                   where !string.IsNullOrEmpty(j.Title)
                   select new FeaturedItem
                   {
                       Id = j.Id,
                       Url = GetJobUrl(j),
                       Title = GetTitle(j),
                   };
        }

        private static string GetJobUrl(JobAd jobAd)
        {
            var jobsUrl = new ReadOnlyApplicationUrl("~/jobs");
            var jobAdPath = jobAd.GetJobRelativePath();
            var path = jobsUrl.Path.AddUrlSegments(jobAdPath);
            return Application.ApplicationPathStartChar + new ReadOnlyApplicationUrl(path).AppRelativePathAndQuery;
        }

        private static string GetTitle(JobAd jobAd)
        {
            if (jobAd.Description != null && jobAd.Description.Location != null && !jobAd.Description.Location.IsCountry)
                return jobAd.Title + " (" + jobAd.Description.Location + ")";
            return jobAd.Title;
        }

        private int GetJobAds()
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='homepage-section find-jobs']//div[@class='shaded-jobs-bottom']/div[@class='shaded-box-content']/div[@class='title']/div[@class='digit']");
            return int.Parse(node.InnerText);
        }

        private JobAd CreateFeaturedJobAd(IEmployer employer, int index)
        {
            var jobAd = employer.CreateTestJobAd(string.Format(JobTitleFormat, index));
            jobAd.CreatedTime = DateTime.Now.AddDays(-1);
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }

        private void AssertFeaturedJobAds(params JobAd[] jobAds)
        {
            var featuredJobAds = GetFeaturedJobAds();
            Assert.AreEqual(Math.Min(10, jobAds.Length), featuredJobAds.Count);

            foreach (var featuredJobAd in featuredJobAds)
            {
                var found = false;
                foreach (var jobAd in jobAds)
                {
                    if (jobAd.Title + " (" + jobAd.Description.Location + ")" == featuredJobAd.InnerText)
                    {
                        // Ensure the url is https.

                        Assert.AreEqual(new ReadOnlyApplicationUrl(true, "~/jobs/" + jobAd.GetJobRelativePath()).AbsoluteUri, featuredJobAd.Attributes["href"].Value);

                        found = true;
                        break;
                    }
                }

                Assert.IsTrue(found);
            }
        }

        private IList<HtmlNode> GetFeaturedJobAds()
        {
            return Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@id='jobad-ticker-items']//ul/li/a");
        }
    }
}