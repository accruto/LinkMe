using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Test.Mvc.Models;
using LinkMe.Web.Areas.Employers.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Web.Test.Employers.Models
{
    [TestClass]
    public class PresentationQueryStringTests
        : QueryStringTests<CandidatesPresentationModel>
    {
        private const int DefaultItems = 25;

        [TestMethod]
        public void TestNull()
        {
            Test(null, "");    
        }

        [TestMethod]
        public void TestDefault()
        {
            Test(new CandidatesPresentationModel { DefaultItemsPerPage = DefaultItems }, "");
        }

        [TestMethod]
        public void TestDetailLevel()
        {
            Test(new CandidatesPresentationModel { DetailLevel = DetailLevel.Compact, DefaultItemsPerPage = DefaultItems }, "DetailLevel=Compact");
        }

        [TestMethod]
        public void TestDefaultPagination()
        {
            Test(new CandidatesPresentationModel { Pagination = new Pagination(), DefaultItemsPerPage = DefaultItems }, "");
        }

        [TestMethod]
        public void TestPagePagination()
        {
            Test(new CandidatesPresentationModel { Pagination = new Pagination { Page = 2 }, DefaultItemsPerPage = DefaultItems }, "Page=2");
        }

        [TestMethod]
        public void TestItemsPagination()
        {
            Test(new CandidatesPresentationModel { Pagination = new Pagination { Items = 10 }, DefaultItemsPerPage = DefaultItems }, "Items=10");
        }

        [TestMethod]
        public void TestAll()
        {
            Test(new CandidatesPresentationModel { Pagination = new Pagination { Page = 2, Items = 10 }, DetailLevel = DetailLevel.Compact, DefaultItemsPerPage = DefaultItems }, "Page=2&Items=10&DetailLevel=Compact");
        }

        private void Test(CandidatesPresentationModel presentation, string expectedQueryString)
        {
            Test(presentation, expectedQueryString, () => new CandidatesPresentationModelConverter(), () => new CandidatesPresentationModelConverter());
        }

        private static bool IsEmpty(CandidatesPresentationModel presentation)
        {
            if (presentation == null)
                return true;

            return presentation.DetailLevel == default(DetailLevel)
                && IsEmpty(presentation.Pagination);
        }

        protected override void AssertAreEqual(CandidatesPresentationModel expectedPresentation, CandidatesPresentationModel presentation)
        {
            if (IsEmpty(expectedPresentation))
            {
                Assert.IsNull(presentation);
            }
            else
            {
                Assert.AreEqual(expectedPresentation.DetailLevel, presentation.DetailLevel);
                AssertAreEqual(expectedPresentation.Pagination, presentation.Pagination);
            }
        }

        private static bool IsEmpty(Pagination pagination)
        {
            if (pagination == null)
                return true;

            return pagination.Page == null
                && pagination.Items == null;
        }

        private static void AssertAreEqual(Pagination expectedPagination, Pagination pagination)
        {
            if (IsEmpty(expectedPagination))
            {
                Assert.IsNull(pagination);
            }
            else
            {
                Assert.AreEqual(expectedPagination.Page, pagination.Page);
                Assert.AreEqual(expectedPagination.Items, pagination.Items);
            }
        }
    }
}
