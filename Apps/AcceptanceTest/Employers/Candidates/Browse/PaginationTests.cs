using System;
using System.Globalization;
using System.Web;
using HtmlAgilityPack;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Browse
{
    [TestClass]
    public class PaginationTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        private const string Country = "Australia";
        private const string Location = "Melbourne VIC 3000";
        private const string Region = "Melbourne";
        private const decimal LowerBound = 80000;
        private const decimal UpperBound = 90000;
        private const string Title = "Manager";
        private const int PageSize = 25;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestBrowse()
        {
            const int count = 100;

            // Create 100 members.

            var salary = new Salary { LowerBound = LowerBound, UpperBound = UpperBound };
            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location);
            var region = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Region);
            for (var index = 0; index < count; ++index)
                CreateMember(index, Title, location, salary);

            var baseUrl = new ReadOnlyApplicationUrl(true, "~/candidates/" + GetUrlSegment(region) + "/" + GetUrlSegment(salary));

            // Go to the first page.

            const int totalPages = count / PageSize;
            var page = 1;
            var url = baseUrl;
            Get(url);
            AssertUrl(url);
            AssertNoHash();
            AssertPagination(true, 1, totalPages, page, false, true, baseUrl);

            // Using the 0 index should get you the same page.

            var otherUrl = new ReadOnlyApplicationUrl("~/candidates/" + GetUrlSegment(region) + "/" + GetUrlSegment(salary) + "/0");
            Get(otherUrl);
            AssertUrl(url);
            AssertNoHash();
            AssertPagination(true, 1, totalPages, page, false, true, url);

            // Using the 1 index should get you the same page.

            otherUrl = new ReadOnlyApplicationUrl("~/candidates/" + GetUrlSegment(region) + "/" + GetUrlSegment(salary) + "/1");
            Get(otherUrl);
            AssertUrl(url);
            AssertNoHash();
            AssertPagination(true, 1, totalPages, page, false, true, url);

            // Iterate through all pages.

            ++page;
            for (; page <= totalPages; ++page)
            {
                url = new ReadOnlyApplicationUrl(true, "~/candidates/" + GetUrlSegment(region) + "/" + GetUrlSegment(salary) + "/" + page);
                Get(url);
                AssertUrl(url);
                AssertNoHash();
                AssertPagination(true, 1, totalPages, page, page > 1, page != totalPages, baseUrl);
            }

            // Check that going beyond the last page returns to the first page

            var firstPageUrl = new ReadOnlyApplicationUrl(true, "~/candidates/" + GetUrlSegment(region) + "/" + GetUrlSegment(salary));
            url = new ReadOnlyApplicationUrl("~/candidates/" + GetUrlSegment(region) + "/" + GetUrlSegment(salary) + "/" + page);
            Get(url);
            AssertUrl(firstPageUrl);
        }

        [TestMethod]
        public void TestSearch()
        {
            const int count = 100;

            // Create 100 members.

            var salary = new Salary { LowerBound = LowerBound, UpperBound = UpperBound };
            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location);
            for (var index = 0; index < count; ++index)
                CreateMember(index, Title, location, salary);

            var searchUrl = new ReadOnlyApplicationUrl("~/search/candidates", new ReadOnlyQueryString("Keywords", Title));
            var resultsUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates/results");

            // Go to the first page.

            const int totalPages = count / PageSize;
            const int page = 1;
            var url = searchUrl;
            Get(url);
            AssertUrl(resultsUrl);
            AssertHash();
            AssertPagination(false, 1, totalPages, page, false, true, searchUrl);
        }

        private void CreateMember(int index, string desiredJobTitle, LocationReference location, Salary salary)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            member.Address.Location = location;
            _memberAccountsCommand.UpdateMember(member);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.DesiredJobTitle = desiredJobTitle;
            candidate.DesiredSalary = salary;
            _candidatesCommand.UpdateCandidate(candidate);
            _candidateResumesCommand.AddTestResume(candidate);
        }

        private void AssertHash()
        {
            Assert.IsTrue(Browser.CurrentPageText.Contains("window.location.hash ="));
        }

        private void AssertNoHash()
        {
            Assert.IsFalse(Browser.CurrentPageText.Contains("window.location.hash ="));
        }

        private void AssertPagination(bool browse, int start, int end, int current, bool previous, bool next, ReadOnlyUrl baseUrl)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='page-numbers']/a[@class='page']");
            var currentNode = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='page-numbers']/div[@class='page-selected']");
            var previousNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(string.Format("//div[@class='pagination-holder']/{0}[@class='previous{1}']", previous ? "a" : "div", previous ? " active" : ""));
            var nextNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(string.Format("//div[@class='pagination-holder']/{0}[@class='next{1}']", next ? "a" : "div", next ? " active" : ""));
            var firstNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(string.Format("//div[@class='pagination-holder']/{0}[@class='first{1}']", previous ? "a" : "div", previous ? " active secondary" : ""));
            var lastNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(string.Format("//div[@class='pagination-holder']/{0}[@class='last{1}']", next ? "a" : "div", next ? " active secondary" : ""));

            Assert.IsNotNull(nodes);
            Assert.IsNotNull(currentNode);
            Assert.IsNotNull(previousNode);
            Assert.IsNotNull(nextNode);
            Assert.IsNotNull(firstNode);
            Assert.IsNotNull(lastNode);

            var count = end - start + 1;

            Assert.AreEqual(1, currentNode.Count);
            Assert.AreEqual(count, nodes.Count + currentNode.Count);

            if (previous)
            {
                AssertPaginationNode(browse, current - 1, baseUrl, "<\xA0Previous", previousNode);
                AssertPaginationNode(browse, 1, baseUrl, "First", firstNode);
            }
            else
            {
                AssertCurrentPaginationNode("<\xA0Previous", previousNode);
                AssertCurrentPaginationNode("First", firstNode);
            }

            if (next)
            {
                AssertPaginationNode(browse, current + 1, baseUrl, "Next\xA0>", nextNode);
                AssertPaginationNode(browse, end, baseUrl, "Last", lastNode);
            }
            else
            {
                AssertCurrentPaginationNode("Next\xA0>", nextNode);
                AssertCurrentPaginationNode("Last", lastNode);
            }

            // Pages.

            var index = 0;
            var currentOffset = 0;
            for (var page = start; page <= end; ++page)
            {
                if (page == current)
                {
                    AssertCurrentPaginationNode(page.ToString(CultureInfo.InvariantCulture), currentNode[0]);
                    currentOffset = 1;
                }
                else
                {
                    AssertPaginationNode(browse, page, baseUrl, page.ToString(CultureInfo.InvariantCulture), nodes[index - currentOffset]);
                }
                ++index;
            }
        }

        private static void AssertCurrentPaginationNode(string text, HtmlNode node)
        {
            Assert.IsNull(node.Attributes["href"]);
            Assert.AreEqual(text, HttpUtility.HtmlDecode(node.InnerText));
        }

        private static void AssertPaginationNode(bool browse, int page, ReadOnlyUrl baseUrl, string text, HtmlNode node)
        {
            if (browse)
            {
                var path = baseUrl.Path + (page <= 1 ? "" : "/" + page);
                var actual = node.Attributes["href"].Value;
                Assert.IsTrue(string.Equals(path, actual, StringComparison.InvariantCultureIgnoreCase), string.Format("Expected {0} but got {1}", path, actual));
            }
            else
            {
                Assert.AreEqual("onChangePage(" + page + ");return false;", node.Attributes["onclick"].Value);
            }
            Assert.AreEqual(text, HttpUtility.HtmlDecode(node.InnerText));
        }

        private static string GetUrlSegment(LocationReference location)
        {
            if (location == null)
                return "-";
            return ((IUrlNamedLocation)location.NamedLocation).UrlName + "-candidates";
        }

        private static string GetUrlSegment(Salary salary)
        {
            if (salary == null)
                return "-";

            if (!salary.LowerBound.HasValue && !salary.UpperBound.HasValue)
                return "-";

            salary = salary.ToRate(SalaryRate.Year);
            if (!salary.LowerBound.HasValue || salary.LowerBound.Value == 0)
                return "up-to-" + GetUrlSegment(salary.UpperBound.Value) + "-candidates";

            if (!salary.UpperBound.HasValue || salary.UpperBound.Value == 0)
                return GetUrlSegment(salary.LowerBound.Value) + "-and-above" + "-candidates";

            return GetUrlSegment(salary.LowerBound.Value) + "-" + GetUrlSegment(salary.UpperBound.Value) + "-candidates";
        }

        private static string GetUrlSegment(decimal value)
        {
            return (int)(value / 1000) + "k";
        }
    }
}