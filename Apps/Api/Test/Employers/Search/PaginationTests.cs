using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Search
{
    [TestClass]
    public class PaginationTests
        : SearchTests
    {
        [TestMethod]
        public void Test1Candidate()
        {
            var employer = CreateEmployer();
            CreateCandidates(1);

            // Access the page.

            LogIn(employer);

            TestPagination(1, 1, null, null);
            TestPagination(1, 1, 1, 10);
            TestPagination(1, 1, 1, 25);
            TestPagination(1, 1, 1, 50);
            TestPagination(1, 1, 1, 100);
        }

        [TestMethod]
        public void Test5Candidates()
        {
            var employer = CreateEmployer();
            CreateCandidates(5);

            // Access the page.

            LogIn(employer);

            TestPagination(5, 5, null, null);
            TestPagination(5, 5, 1, 10);
            TestPagination(5, 5, 1, 25);
            TestPagination(5, 5, 1, 50);
            TestPagination(5, 5, 1, 100);
        }

        [TestMethod]
        public void Test20Candidates()
        {
            var employer = CreateEmployer();
            CreateCandidates(20);

            // Access the page.

            LogIn(employer);

            TestPagination(20, 20, null, null);

            TestPagination(20, 10, 1, 10);
            TestPagination(20, 10, 2, 10);

            TestPagination(20, 20, 1, 25);
            TestPagination(20, 20, 1, 50);
            TestPagination(20, 20, 1, 100);
        }

        [TestMethod]
        public void Test40Candidates()
        {
            var employer = CreateEmployer();
            CreateCandidates(40);

            // Access the page.

            LogIn(employer);

            TestPagination(40, 25, null, null);

            TestPagination(40, 10, 1, 10);
            TestPagination(40, 10, 2, 10);
            TestPagination(40, 10, 3, 10);
            TestPagination(40, 10, 4, 10);

            TestPagination(40, 25, 1, 25);
            TestPagination(40, 15, 2, 25);

            TestPagination(40, 40, 1, 50);
            TestPagination(40, 40, 1, 100);
        }

        [TestMethod]
        public void Test60Candidates()
        {
            var employer = CreateEmployer();
            CreateCandidates(60);

            // Access the page.

            LogIn(employer);

            TestPagination(60, 25, null, null);

            TestPagination(60, 10, 1, 10);
            TestPagination(60, 10, 2, 10);
            TestPagination(60, 10, 3, 10);
            TestPagination(60, 10, 4, 10);
            TestPagination(60, 10, 5, 10);
            TestPagination(60, 10, 6, 10);

            TestPagination(60, 25, 1, 25);
            TestPagination(60, 25, 2, 25);
            TestPagination(60, 10, 3, 25);

            TestPagination(60, 50, 1, 50);
            TestPagination(60, 10, 2, 50);

            TestPagination(60, 60, 1, 100);
        }

        [TestMethod]
        public void Test150Candidates()
        {
            var employer = CreateEmployer();
            CreateCandidates(150);

            // Access the page.

            LogIn(employer);

            TestPagination(150, 25, null, null);

            TestPagination(150, 10, 1, 10);
            TestPagination(150, 10, 2, 10);
            TestPagination(150, 10, 3, 10);
            TestPagination(150, 10, 4, 10);
            TestPagination(150, 10, 5, 10);
            TestPagination(150, 10, 6, 10);
            TestPagination(150, 10, 7, 10);
            TestPagination(150, 10, 8, 10);
            TestPagination(150, 10, 9, 10);
            TestPagination(150, 10, 10, 10);
            TestPagination(150, 10, 11, 10);
            TestPagination(150, 10, 12, 10);
            TestPagination(150, 10, 13, 10);
            TestPagination(150, 10, 14, 10);
            TestPagination(150, 10, 15, 10);

            TestPagination(150, 25, 1, 25);
            TestPagination(150, 25, 2, 25);
            TestPagination(150, 25, 3, 25);
            TestPagination(150, 25, 4, 25);
            TestPagination(150, 25, 5, 25);
            TestPagination(150, 25, 6, 25);

            TestPagination(150, 50, 1, 50);
            TestPagination(150, 50, 2, 50);
            TestPagination(150, 50, 3, 50);

            TestPagination(150, 100, 1, 100);
            TestPagination(150, 50, 2, 100);
        }

        private void TestPagination(int totalCandidates, int candidates, int? page, int? items)
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            var model = Search(criteria, MemberSortOrder.DateUpdated, page, items);
            Assert.AreEqual(totalCandidates, model.TotalCandidates);
            Assert.AreEqual(candidates, model.Candidates.Count);
        }

        private void CreateCandidates(int count)
        {
            for (var index = 0; index < count; ++index)
                CreateMember(index);
        }
    }
}