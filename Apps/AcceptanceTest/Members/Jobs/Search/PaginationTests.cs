using System;
using System.Globalization;
using System.Web;
using HtmlAgilityPack;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search
{
    [TestClass]
    public class PaginationTests
        : JobsTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private const string Country = "Australia";
        private const string Location = "Melbourne VIC 3000";
        private const string Title = "Manager";

        private const int MaxPages = 11;
        private const int ItemsPerPage = 10;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestBrowseSomeJobs()
        {
            TestBrowseJobs(45);
        }

        [TestMethod]
        public void TestBrowseLotsJobs()
        {
            TestBrowseJobs(145);
        }

        [TestMethod]
        public void TestSearchSomeJobs()
        {
            TestSearchJobs(45);
        }

        [TestMethod]
        public void TestSearchLotsJobs()
        {
            TestSearchJobs(145);
        }

        private void TestBrowseJobs(int count)
        {
            var employer = CreateEmployer();
            var industry = _industriesQuery.GetIndustry("Accounting");
            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location);
            for (var job = 0; job < count; ++job)
                CreateJobAd(employer, "Manager" + job, location, industry);

            var baseUrl = new ReadOnlyApplicationUrl("~/jobs/melbourne-jobs/accounting-jobs");
            var totalPages = count / ItemsPerPage + 1;

            // Go to the first page.

            var page = 1;
            var url = baseUrl;
            Get(url);
            AssertUrl(url);
            AssertNoHash();
            AssertPagination(true, page, totalPages, baseUrl);

            // Using the 0 index should get you the same page.

            var otherUrl = new ReadOnlyApplicationUrl("~/jobs/melbourne-jobs/accounting-jobs/0");
            Get(otherUrl);
            AssertUrl(url);
            AssertNoHash();
            AssertPagination(true, page, totalPages, url);

            // Using the 1 index should get you the same page.

            otherUrl = new ReadOnlyApplicationUrl("~/jobs/melbourne-jobs/accounting-jobs/1");
            Get(otherUrl);
            AssertUrl(url);
            AssertNoHash();
            AssertPagination(true, page, totalPages, url);

            // Iterate through all pages.

            ++page;
            for (; page <= totalPages; ++page)
            {
                url = new ReadOnlyApplicationUrl("~/jobs/melbourne-jobs/accounting-jobs/" + page);
                Get(url);
                AssertUrl(url);
                AssertNoHash();
                AssertPagination(true, page, totalPages, baseUrl);
            }

            // Check that going beyond the last page returns to the first page

            var firstPageUrl = new ReadOnlyApplicationUrl("~/jobs/melbourne-jobs/accounting-jobs");
            url = new ReadOnlyApplicationUrl("~/jobs/melbourne-jobs/accounting-jobs/" + page);
            Get(url);
            AssertUrl(firstPageUrl);
        }

        private void TestSearchJobs(int count)
        {
            // Create 100 ads.

            var employer = CreateEmployer();
            var industry = _industriesQuery.GetIndustry("Accounting");
            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location);
            for (var job = 0; job < count; ++job)
                CreateJobAd(employer, Title + " " + job, location, industry);

            var searchUrl = new ReadOnlyApplicationUrl("~/search/jobs", new ReadOnlyQueryString("Keywords", Title));
            var resultsUrl = new ReadOnlyApplicationUrl("~/search/jobs/results");
            var totalPages = count / ItemsPerPage + 1;

            // Go to the first page.

            const int page = 1;
            var url = searchUrl;
            Get(url);
            AssertUrl(resultsUrl);
            AssertHash();
            AssertPagination(false, page, totalPages, resultsUrl);
        }

        private void CreateJobAd(IEmployer employer, string jobTitle, LocationReference location, Industry industry)
        {
            var jobAd = employer.CreateTestJobAd(jobTitle, "Blah blah blah", industry, location);
            _jobAdsCommand.PostJobAd(jobAd);
        }

        private void AssertHash()
        {
            Assert.IsTrue(Browser.CurrentPageText.Contains("window.location.hash ="));
        }

        private void AssertNoHash()
        {
            Assert.IsFalse(Browser.CurrentPageText.Contains("window.location.hash ="));
        }

        private void AssertPagination(bool browse, int currentPage, int totalPages, ReadOnlyUrl baseUrl)
        {
            // Only show a maximum number of page links.

            int firstPage;
            int lastPage;

            if (totalPages <= MaxPages)
            {
                firstPage = 1;
                lastPage = totalPages;
            }
            else
            {
                if (currentPage < MaxPages / 2 + 1)
                {
                    // If at left end ...

                    firstPage = 1;
                    lastPage = MaxPages;
                }
                else if (currentPage > totalPages - MaxPages / 2)
                {
                    // If at right end ...

                    firstPage = totalPages - MaxPages + 1;
                    lastPage = totalPages;
                }
                else
                {
                    // If in the middle ...

                    firstPage = currentPage < MaxPages / 2 + 1 ? 1 : currentPage - MaxPages / 2;
                    lastPage = currentPage > totalPages - MaxPages / 2 ? totalPages : currentPage + MaxPages / 2;
                }
            }

            // First, previous, next, last.

            var firstANode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(string.Format("//a[@class='page first secondary{0}']", currentPage != 1 ? " active" : ""));
            var firstDivNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(string.Format("//div[@class='page first secondary{0}']", currentPage != 1 ? "" : " active"));

            var previousANode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(string.Format("//a[@class='page previous{0}']", currentPage != 1 ? " active" : ""));
            var previousDivNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(string.Format("//div[@class='page previous{0}']", currentPage != 1 ? "" : " active"));

            var nextANode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(string.Format("//a[@class='page next{0}']", currentPage != totalPages ? " active" : ""));
            var nextDivNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(string.Format("//div[@class='page next{0}']", currentPage != totalPages ? "" : " active"));

            var lastANode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(string.Format("//a[@class='page last secondary{0}']", currentPage != totalPages ? " active" : ""));
            var lastDivNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(string.Format("//div[@class='page last secondary{0}']", currentPage != totalPages ? "" : " active"));

            Assert.IsNotNull(firstDivNode);
            Assert.IsNotNull(previousDivNode);
            Assert.IsNotNull(nextDivNode);
            Assert.IsNotNull(lastDivNode);

            Assert.IsNotNull(firstANode);
            Assert.IsNotNull(previousANode);
            Assert.IsNotNull(nextANode);
            Assert.IsNotNull(lastANode);

            AssertPaginationNode(browse, 1, baseUrl, "First", firstANode);
            AssertPaginationNode(browse, currentPage - 1, baseUrl, "<\xA0Previous", previousANode);

            AssertPaginationNode(1, "First", firstDivNode);
            AssertPaginationNode(currentPage - 1, "<\xA0Previous", previousDivNode);

            AssertPaginationNode(browse, currentPage + 1, baseUrl, "Next\xA0>", nextANode);
            AssertPaginationNode(browse, totalPages, baseUrl, "Last", lastANode);

            AssertPaginationNode(currentPage + 1, "Next\xA0>", nextDivNode);
            AssertPaginationNode(totalPages, "Last", lastDivNode);

            // Ellipsis.

            if (firstPage > 1)
            {
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//span[@class='pagination-ellipsis left active']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//span[@class='pagination-ellipsis left']"));
            }
            else
            {
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//span[@class='pagination-ellipsis left active']"));
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//span[@class='pagination-ellipsis left']"));
            }

            if (lastPage < totalPages)
            {
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//span[@class='pagination-ellipsis right active']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//span[@class='pagination-ellipsis right']"));
            }
            else
            {
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//span[@class='pagination-ellipsis right active']"));
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//span[@class='pagination-ellipsis right']"));
            }

            // Pages.

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//a[@class='page active']");
            var currentANode = Browser.CurrentHtml.DocumentNode.SelectNodes("//a[@class='page']");
            var currentDivNode = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='page current']");

            Assert.IsNotNull(currentANode);
            Assert.IsNotNull(currentDivNode);
            Assert.IsNotNull(nodes);
            Assert.AreEqual(1, currentDivNode.Count);
            Assert.AreEqual(1, currentANode.Count);
            Assert.AreEqual(lastPage - firstPage + 1, nodes.Count + currentANode.Count);

            var index = 0;
            var currentOffset = 0;
            for (var page = firstPage; page <= lastPage; ++page)
            {
                if (page == currentPage)
                {
                    AssertCurrentPaginationNode(page.ToString(CultureInfo.InvariantCulture), currentANode[0]);
                    currentOffset = 1;
                }
                else
                {
                    if (browse)
                        AssertPaginationNode(true, page, baseUrl, page.ToString(CultureInfo.InvariantCulture), nodes[index - currentOffset]);
                    else
                        AssertPaginationNode(page, page.ToString(CultureInfo.InvariantCulture), nodes[index - currentOffset]);
                }
                ++index;
            }
        }

        private static void AssertCurrentPaginationNode(string text, HtmlNode node)
        {
            Assert.AreEqual(text, node.InnerText);
        }

        private static void AssertPaginationNode(bool browse, int page, ReadOnlyUrl baseUrl, string text, HtmlNode node)
        {
            var path = browse ? baseUrl.Path + (page < 2 ? "" : "/" + page) : baseUrl.Path;
            var actual = node.Attributes["href"].Value;
            Assert.IsTrue(string.Equals(path, actual, StringComparison.InvariantCultureIgnoreCase), string.Format("Expected {0} but got {1}", path, actual));
            Assert.AreEqual(page.ToString(CultureInfo.InvariantCulture), node.Attributes["page"].Value);
            Assert.AreEqual(text, HttpUtility.HtmlDecode(node.InnerText));
        }

        private static void AssertPaginationNode(int page, string text, HtmlNode node)
        {
            Assert.AreEqual(page.ToString(CultureInfo.InvariantCulture), node.Attributes["page"].Value);
            Assert.AreEqual(text, HttpUtility.HtmlDecode(node.InnerText));
        }
    }
}