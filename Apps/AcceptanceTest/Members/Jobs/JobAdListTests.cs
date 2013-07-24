using System;
using System.Globalization;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs
{
    [TestClass]
    public abstract class JobAdListTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        protected readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        protected readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();
        protected readonly IJobAdFlagListsQuery _jobAdFlagListsQuery = Resolve<IJobAdFlagListsQuery>();

        protected HtmlDropDownListTester _sortOrderDropDownList;
        protected HtmlDropDownListTester _itemsPerPageDropDownList;

        private const int MaxPages = 11;
        private const int DefaultItemsPerPage = 10;

        [TestInitialize]
        public void JobAdListTestInitialize()
        {
            _sortOrderDropDownList = new HtmlDropDownListTester(Browser, "SortOrder");
            _itemsPerPageDropDownList = new HtmlDropDownListTester(Browser, "ItemsPerPage");
        }

        protected Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }

        protected Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
        }

        protected JobAd CreateJobAd(IEmployer employer)
        {
            return CreateJobAd(employer, null);
        }

        protected JobAd CreateJobAd(IEmployer employer, Action<JobAd> prepareCreate)
        {
            var jobAd = employer.CreateTestJobAd();
            if (prepareCreate != null)
                prepareCreate(jobAd);
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }

        protected static ReadOnlyUrl GetUrl(Url url, JobAdSearchSortCriteria criteria, int? page, int? items)
        {
            if (criteria != null)
            {
                url.QueryString["SortOrder"] = criteria.SortOrder.ToString();
                url.QueryString["SortOrderDirection"] = criteria.ReverseSortOrder ? "SortOrderIsAscending" : "SortOrderIsDescending";
            }
            if (page != null)
                url.QueryString["Page"] = page.Value.ToString(CultureInfo.InvariantCulture);
            if (items != null)
                url.QueryString["Items"] = items.Value.ToString(CultureInfo.InvariantCulture);

            return url;
        }

        protected void AssertEmptyJobAds(bool expected)
        {
            var emptyRowNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='row empty']/div[@class='topbar']");
            if (expected)
                Assert.IsNotNull(emptyRowNode);
            else
                Assert.IsNull(emptyRowNode);
        }

        protected void AssertJobAds(bool assertSequential, params JobAd[] expectedJobAds)
        {
            var ids = from n in Browser.CurrentHtml.DocumentNode.SelectNodes("//div[starts-with(@class, 'jobad-list-view')]/div[@class='topbar']")
                      select n.ParentNode.Attributes["id"].Value;
            if (assertSequential)
                Assert.IsTrue(expectedJobAds.Select(j => j.Id.ToString()).SequenceEqual(ids));
            else
                Assert.IsTrue(expectedJobAds.Select(j => j.Id.ToString()).CollectionEqual(ids));
        }

        protected void AssertMobileJobAds(params JobAd[] expectedJobAds)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[starts-with(@class, 'jobad-list-view')]");
            Assert.AreEqual(expectedJobAds.Length, nodes.Count);
            for (var index = 0; index < expectedJobAds.Length; ++index)
                Assert.AreEqual(expectedJobAds[index].Id.ToString(), nodes[index].Attributes["id"].Value);
        }

        protected void AssertSortOrders(JobAdSortOrder expectedSelectedValue, params JobAdSortOrder[] expectedSortOrders)
        {
            Assert.IsTrue(expectedSortOrders.Select(o => o.ToString()).CollectionEqual(_sortOrderDropDownList.Items.Select(i => i.Value)));
            Assert.AreEqual(expectedSelectedValue.ToString(), _sortOrderDropDownList.SelectedValue);
        }

        protected void AssertSortOrder(JobAdSearchSortCriteria criteria)
        {
            Assert.AreEqual(criteria.SortOrder.ToString(), _sortOrderDropDownList.SelectedItem.Value);
            if (criteria.ReverseSortOrder)
            {
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='ascending active']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='ascending ']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='descending active']"));
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='descending ']"));
            }
            else
            {
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='ascending active']"));
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='ascending ']"));
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='descending active']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='descending ']"));
            }
        }

        protected void AssertItemsPerPage(int? items)
        {
            Assert.IsTrue(new[] { 10, 25, 50, 100 }.Select(i => i.ToString(CultureInfo.InvariantCulture)).SequenceEqual(_itemsPerPageDropDownList.Items.Select(i => i.Value)));
            Assert.AreEqual((items ?? 10).ToString(CultureInfo.InvariantCulture), _itemsPerPageDropDownList.SelectedValue);
        }

        protected void AssertPagination(int? page, int? items, int totalItems)
        {
            // Only show a maximum number of page links.

            int firstPage;
            int lastPage;
            var currentPage = page ?? 1;
            var totalPages = (int)Math.Ceiling((double)totalItems / (items ?? DefaultItemsPerPage));

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

            // The url in the page is always the results url.
            // Redirecting to the correct url is handled by JavaScript.

            var url = new ReadOnlyApplicationUrl("~/search/jobs/results");
            AssertPaginationNode(1, url, "First", firstANode);
            AssertPaginationNode(currentPage - 1, url, "<\xA0Previous", previousANode);

            AssertPaginationNode(1, "First", firstDivNode);
            AssertPaginationNode(currentPage - 1, "<\xA0Previous", previousDivNode);

            AssertPaginationNode(currentPage + 1, url, "Next\xA0>", nextANode);
            AssertPaginationNode(totalPages, url, "Last", lastANode);

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
            for (var pageIndex = firstPage; pageIndex <= lastPage; ++pageIndex)
            {
                if (pageIndex == currentPage)
                {
                    AssertCurrentPaginationNode(pageIndex.ToString(CultureInfo.InvariantCulture), currentANode[0]);
                    currentOffset = 1;
                }
                else
                {
                    AssertPaginationNode(pageIndex, pageIndex.ToString(CultureInfo.InvariantCulture), nodes[index - currentOffset]);
                }
                ++index;
            }
        }

        private static void AssertCurrentPaginationNode(string text, HtmlNode node)
        {
            Assert.AreEqual(text, node.InnerText);
        }

        private static void AssertPaginationNode(int page, ReadOnlyUrl url, string text, HtmlNode node)
        {
            var actual = node.Attributes["href"].Value;
            Assert.IsTrue(string.Equals(url.Path, actual, StringComparison.InvariantCultureIgnoreCase), string.Format("Expected {0} but got {1}", url.Path, actual));
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
