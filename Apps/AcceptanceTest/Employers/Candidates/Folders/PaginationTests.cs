using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Folders
{
    [TestClass]
    public class PaginationTests
        : FoldersTests
    {
        [TestMethod]
        public void Test1Candidate()
        {
            var employer = CreateEmployer();
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            CreateCandidates(1, employer, folder);

            // Access the page.

            LogIn(employer);

            TestPagination(folder.Id, null, null, 1, 1, 1, 1, 1, 1);
            TestPagination(folder.Id, 1, 10, 1, 1, 1, 1, 1, 1);
            TestPagination(folder.Id, 1, 25, 1, 1, 1, 1, 1, 1);
            TestPagination(folder.Id, 1, 50, 1, 1, 1, 1, 1, 1);
            TestPagination(folder.Id, 1, 100, 1, 1, 1, 1, 1, 1);
        }

        [TestMethod]
        public void Test5Candidates()
        {
            var employer = CreateEmployer();
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            CreateCandidates(5, employer, folder);

            // Access the page.

            LogIn(employer);

            TestPagination(folder.Id, null, null, 1, 5, 5, 1, 1, 1);
            TestPagination(folder.Id, 1, 10, 1, 5, 5, 1, 1, 1);
            TestPagination(folder.Id, 1, 25, 1, 5, 5, 1, 1, 1);
            TestPagination(folder.Id, 1, 50, 1, 5, 5, 1, 1, 1);
            TestPagination(folder.Id, 1, 100, 1, 5, 5, 1, 1, 1);
        }

        [TestMethod]
        public void Test20Candidates()
        {
            var employer = CreateEmployer();
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            CreateCandidates(20, employer, folder);

            // Access the page.

            LogIn(employer);

            TestPagination(folder.Id, null, null, 1, 20, 20, 1, 1, 1);

            TestPagination(folder.Id, 1, 10, 1, 10, 20, 2, 1, 2);
            TestPagination(folder.Id, 2, 10, 11, 20, 20, 2, 1, 2);

            TestPagination(folder.Id, 1, 25, 1, 20, 20, 1, 1, 1);
            TestPagination(folder.Id, 1, 50, 1, 20, 20, 1, 1, 1);
            TestPagination(folder.Id, 1, 100, 1, 20, 20, 1, 1, 1);
        }

        [TestMethod]
        public void Test40Candidates()
        {
            var employer = CreateEmployer();
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            CreateCandidates(40, employer, folder);

            // Access the page.

            LogIn(employer);

            TestPagination(folder.Id, null, null, 1, 25, 40, 2, 1, 2);

            TestPagination(folder.Id, 1, 10, 1, 10, 40, 4, 1, 4);
            TestPagination(folder.Id, 2, 10, 11, 20, 40, 4, 1, 4);
            TestPagination(folder.Id, 3, 10, 21, 30, 40, 4, 1, 4);
            TestPagination(folder.Id, 4, 10, 31, 40, 40, 4, 1, 4);

            TestPagination(folder.Id, 1, 25, 1, 25, 40, 2, 1, 2);
            TestPagination(folder.Id, 2, 25, 26, 40, 40, 2, 1, 2);

            TestPagination(folder.Id, 1, 50, 1, 40, 40, 1, 1, 1);
            TestPagination(folder.Id, 1, 100, 1, 40, 40, 1, 1, 1);
        }

        [TestMethod]
        public void Test60Candidates()
        {
            var employer = CreateEmployer();
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            CreateCandidates(60, employer, folder);

            // Access the page.

            LogIn(employer);

            TestPagination(folder.Id, null, null, 1, 25, 60, 3, 1, 3);

            TestPagination(folder.Id, 1, 10, 1, 10, 60, 6, 1, 6);
            TestPagination(folder.Id, 2, 10, 11, 20, 60, 6, 1, 6);
            TestPagination(folder.Id, 3, 10, 21, 30, 60, 6, 1, 6);
            TestPagination(folder.Id, 4, 10, 31, 40, 60, 6, 1, 6);
            TestPagination(folder.Id, 5, 10, 41, 50, 60, 6, 1, 6);
            TestPagination(folder.Id, 6, 10, 51, 60, 60, 6, 1, 6);

            TestPagination(folder.Id, 1, 25, 1, 25, 60, 3, 1, 3);
            TestPagination(folder.Id, 2, 25, 26, 50, 60, 3, 1, 3);
            TestPagination(folder.Id, 3, 25, 51, 60, 60, 3, 1, 3);

            TestPagination(folder.Id, 1, 50, 1, 50, 60, 2, 1, 2);
            TestPagination(folder.Id, 2, 50, 51, 60, 60, 2, 1, 2);

            TestPagination(folder.Id, 1, 100, 1, 60, 60, 1, 1, 1);
        }

        [TestMethod]
        public void Test150Candidates()
        {
            var employer = CreateEmployer();
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            CreateCandidates(150, employer, folder);

            // Access the page.

            LogIn(employer);

            TestPagination(folder.Id, null, null, 1, 25, 150, 6, 1, 6);

            TestPagination(folder.Id, 1, 10, 1, 10, 150, 15, 1, 11);
            TestPagination(folder.Id, 2, 10, 11, 20, 150, 15, 1, 11);
            TestPagination(folder.Id, 3, 10, 21, 30, 150, 15, 1, 11);
            TestPagination(folder.Id, 4, 10, 31, 40, 150, 15, 1, 11);
            TestPagination(folder.Id, 5, 10, 41, 50, 150, 15, 1, 11);
            TestPagination(folder.Id, 6, 10, 51, 60, 150, 15, 1, 11);
            TestPagination(folder.Id, 7, 10, 61, 70, 150, 15, 2, 12);
            TestPagination(folder.Id, 8, 10, 71, 80, 150, 15, 3, 13);
            TestPagination(folder.Id, 9, 10, 81, 90, 150, 15, 4, 14);
            TestPagination(folder.Id, 10, 10, 91, 100, 150, 15, 5, 15);
            TestPagination(folder.Id, 11, 10, 101, 110, 150, 15, 5, 15);
            TestPagination(folder.Id, 12, 10, 111, 120, 150, 15, 5, 15);
            TestPagination(folder.Id, 13, 10, 121, 130, 150, 15, 5, 15);
            TestPagination(folder.Id, 14, 10, 131, 140, 150, 15, 5, 15);
            TestPagination(folder.Id, 15, 10, 141, 150, 150, 15, 5, 15);

            TestPagination(folder.Id, 1, 25, 1, 25, 150, 6, 1, 6);
            TestPagination(folder.Id, 2, 25, 26, 50, 150, 6, 1, 6);
            TestPagination(folder.Id, 3, 25, 51, 75, 150, 6, 1, 6);
            TestPagination(folder.Id, 4, 25, 76, 100, 150, 6, 1, 6);
            TestPagination(folder.Id, 5, 25, 101, 125, 150, 6, 1, 6);
            TestPagination(folder.Id, 6, 25, 126, 150, 150, 6, 1, 6);

            TestPagination(folder.Id, 1, 50, 1, 50, 150, 3, 1, 3);
            TestPagination(folder.Id, 2, 50, 51, 100, 150, 3, 1, 3);
            TestPagination(folder.Id, 3, 50, 101, 150, 150, 3, 1, 3);

            TestPagination(folder.Id, 1, 100, 1, 100, 150, 2, 1, 2);
            TestPagination(folder.Id, 2, 100, 101, 150, 150, 2, 1, 2);
        }

        private void TestPagination(Guid folderId, int? page, int? items, int start, int end, int total, int totalPages, int firstPage, int lastPage)
        {
            Get(GetFolderUrl(folderId, page, items));

            AssertCount(start, "start");
            AssertCount(end, "end");
            AssertCount(total, "total");
            AssertNavigation(page ?? 1, totalPages);
            AssertPages(page ?? 1, totalPages, firstPage, lastPage);
        }

        private void AssertPages(int page, int totalPages, int firstPage, int lastPage)
        {
            var nodes = GetPageNodes();

            if (firstPage > 1)
            {
                Assert.AreEqual("...", nodes[0].InnerText);
                nodes = nodes.Skip(1).ToList();
            }

            if (lastPage < totalPages)
            {
                Assert.AreEqual("...", nodes[nodes.Count - 1].InnerText);
                nodes = nodes.Take(nodes.Count - 1).ToList();
            }

            Assert.AreEqual(lastPage - firstPage + 1, nodes.Count);

            for (var index = 0; index < nodes.Count; ++index)
            {
                Assert.AreEqual((firstPage + index).ToString(CultureInfo.InvariantCulture), nodes[index].InnerText);
                Assert.AreEqual(firstPage + index == page ? "page-selected" : "page", nodes[index].Attributes["class"].Value);
            }
        }

        private IList<HtmlNode> GetPageNodes()
        {
            return (from n in Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/div[@class='page-holder']/div[@class='page-numbers']").ChildNodes
                    where n.NodeType != HtmlNodeType.Text
                    select n).ToList();
        }

        private void AssertNavigation(int page, int totalPages)
        {
            // first and previous

            if (page == 1)
            {
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/div[@class='first']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/div[@class='first active secondary']"));

                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/div[@class='previous']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/div[@class='previous active']"));
            }
            else
            {
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/a[@class='first']"));
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/a[@class='first active secondary']"));

                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/a[@class='previous']"));
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/a[@class='previous active']"));
            }

            // next and last

            if (page == totalPages)
            {
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/div[@class='last']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/div[@class='last active secondary']"));

                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/div[@class='next']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/div[@class='next active']"));
            }
            else
            {
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/a[@class='last']"));
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/a[@class='last active secondary']"));

                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/a[@class='next']"));
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-holder']/a[@class='next active']"));
            }
        }

        private void AssertCount(int expectedCount, string countClass)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='pagination-results-container']/div[@class='pagination-results-holder']/span[@class='" + countClass + "']");
            Assert.AreEqual(expectedCount, int.Parse(node.InnerText));
        }

        private void CreateCandidates(int count, IEmployer employer, CandidateFolder folder)
        {
            for (var index = 0; index < count; ++index)
            {
                var member = CreateMember(index);
                _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
            }
        }
    }
}
