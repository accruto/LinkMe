using System;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches.Web
{
    [TestClass]
    public class RecentSearchesTests
        : SearchesTests
    {
        private ReadOnlyUrl _recentSearchesUrl;
        private ReadOnlyUrl _partialRecentSearchesUrl;

        private const string Keywords = "Archeologist";
        private const int MaxPages = 11;
        private const int ItemsPerPage = 10;

        [TestInitialize]
        public void TestInitialize()
        {
            _recentSearchesUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/recent");
            _partialRecentSearchesUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/partial/recent");
        }

        [TestMethod]
        public void TestNoRecentSearches()
        {
            var member = CreateMember(0);
            LogIn(member);

            Get(_recentSearchesUrl);
            AssertUrl(_recentSearchesUrl);
            AssertEmptyText("You don't have any recent searches.");
            AssertSearches();

            Get(_partialRecentSearchesUrl);
            AssertEmptyText("You don't have any recent searches.");
            AssertSearches();
        }

        [TestMethod]
        public void TestRecentSearch()
        {
            var member = CreateMember(0);
            Search(member.Id, Keywords, DateTime.Now);

            LogIn(member);

            Get(_recentSearchesUrl);
            AssertUrl(_recentSearchesUrl);
            AssertNoEmptyText();
            AssertSearches(Keywords);

            Get(_partialRecentSearchesUrl);
            AssertNoEmptyText();
            AssertSearches(Keywords);
        }

        [TestMethod]
        public void TestPagination()
        {
            var member = CreateMember(0);

            const int count = 145;
            for (var index = 0; index < count; ++index)
                Search(member.Id, Keywords + index, DateTime.Now.AddDays(-1*index));
            const int totalPages = count / ItemsPerPage + 1;
            
            LogIn(member);
            Get(_recentSearchesUrl);
            AssertUrl(_recentSearchesUrl);
            AssertNoEmptyText();

            AssertSearches((from i in Enumerable.Range(0, ItemsPerPage) select Keywords + i).ToArray());
            AssertPagination(1, totalPages);

            // Iterate through the pages.

            for (var page = 1; page <= totalPages; ++page)
            {
                var url = _partialRecentSearchesUrl.AsNonReadOnly();
                url.QueryString["page"] = page.ToString();
                Get(url);
                AssertPagination(page, totalPages);
            }
        }

        [TestMethod]
        public void TestConsolidation()
        {
            var member = CreateMember(0);

            const int count = 72;
            for (var index = 0; index < count; ++index)
                Search(member.Id, Keywords + "A", DateTime.Now.AddDays(-1 * index));
            for (var index = 0; index < count; ++index)
                Search(member.Id, Keywords + "B", DateTime.Now.AddDays(-1 * index - 1));

            LogIn(member);
            Get(_recentSearchesUrl);
            AssertUrl(_recentSearchesUrl);
            AssertNoEmptyText();

            AssertSearches(Keywords + "A", Keywords + "B");
            AssertPagination(1, 1);

            // Iterate through the pages.

            var url = _partialRecentSearchesUrl.AsNonReadOnly();
            url.QueryString["page"] = 1.ToString();
            Get(url);
            AssertPagination(1, 1);
        }

        [TestMethod]
        public void TestDate()
        {
            var member = CreateMember(0);

            const int count = 12;
            for (var index = 1; index <= count; ++index)
                Search(member.Id, Keywords + index, DateTime.Now.AddMonths(-1 * index).AddDays(1));

            LogIn(member);
            Get(_recentSearchesUrl);
            AssertUrl(_recentSearchesUrl);
            AssertNoEmptyText();

            // Only the last 6 months of searches are considered.

            AssertSearches((from i in Enumerable.Range(1, 6) select Keywords + i).ToArray());
            AssertPagination(1, 1);
        }

        private void Search(Guid memberId, string keywords, DateTime startTime)
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(keywords);
            _jobAdSearchesCommand.CreateJobAdSearchExecution(new JobAdSearchExecution
            {
                Criteria = criteria,
                SearcherId = memberId,
                StartTime = startTime,
                Results = new JobAdSearchResults(),
                Context = "test"
            });
        }

        private void AssertPagination(int currentPage, int totalPages)
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

            var firstANode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@class='page first active secondary']");
            var firstDivNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='page first']");

            var previousANode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@class='page previous active']");
            var previousDivNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='page previous']");

            var nextANode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@class='page next active']");
            var nextDivNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='page next']");

            var lastANode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@class='page last active secondary']");
            var lastDivNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='page last']");

            if (currentPage == 1)
            {
                AssertPaginationNode(1, "First", firstDivNode);
                AssertPaginationNode(currentPage - 1, "&lt;&nbsp;Previous", previousDivNode);
                Assert.IsNull(firstANode);
                Assert.IsNull(previousANode);
            }
            else
            {
                Assert.IsNull(firstDivNode);
                Assert.IsNull(previousDivNode);
                AssertPaginationNode(1, _partialRecentSearchesUrl, "First", firstANode);
                AssertPaginationNode(currentPage - 1, _partialRecentSearchesUrl, "&lt;&nbsp;Previous", previousANode);
            }

            if (currentPage == totalPages)
            {
                AssertPaginationNode(currentPage + 1, "Next&nbsp;&gt;", nextDivNode);
                AssertPaginationNode(totalPages, "Last", lastDivNode);
                Assert.IsNull(nextANode);
                Assert.IsNull(lastANode);
            }
            else
            {
                Assert.IsNull(nextDivNode);
                Assert.IsNull(lastDivNode);
                AssertPaginationNode(currentPage + 1, _partialRecentSearchesUrl, "Next&nbsp;&gt;", nextANode);
                AssertPaginationNode(totalPages, _partialRecentSearchesUrl, "Last", lastANode);
            }

            // Pages.

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//a[@class='page']");
            var currentNode = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='page page-selected']");

            if (totalPages == 1)
                Assert.IsNull(nodes);
            else
                Assert.IsNotNull(nodes);

            Assert.IsNotNull(currentNode);
            Assert.AreEqual(1, currentNode.Count);
            Assert.AreEqual(lastPage - firstPage + 1, (nodes == null ? 0 : nodes.Count) + currentNode.Count);

            var index = 0;
            var currentOffset = 0;
            for (var page = firstPage; page <= lastPage; ++page)
            {
                if (page == currentPage)
                {
                    AssertCurrentPaginationNode(page.ToString(), currentNode[0]);
                    currentOffset = 1;
                }
                else
                {
                    if (nodes != null)
                        AssertPaginationNode(page, page.ToString(), nodes[index - currentOffset]);
                    else
                        Assert.Fail();
                }
                ++index;
            }
        }

        private static void AssertCurrentPaginationNode(string text, HtmlNode node)
        {
            Assert.AreEqual(text, node.InnerText);
        }

        private static void AssertPaginationNode(int page, ReadOnlyUrl baseUrl, string text, HtmlNode node)
        {
            var url = baseUrl.AsNonReadOnly();
            url.QueryString["page"] = page.ToString();
            var actual = node.Attributes["href"].Value;
            Assert.IsTrue(string.Equals(url.PathAndQuery, actual, StringComparison.InvariantCultureIgnoreCase), string.Format("Expected {0} but got {1}", url.PathAndQuery, actual));
            Assert.AreEqual(page.ToString(), node.Attributes["page"].Value);
            Assert.AreEqual(text, node.InnerText);
        }

        private static void AssertPaginationNode(int page, string text, HtmlNode node)
        {
            Assert.AreEqual(page.ToString(), node.Attributes["page"].Value);
            Assert.AreEqual(text, node.InnerText);
        }
    }
}