using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Mvc.Models.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Mvc.Models
{
    [TestClass]
    public class PaginationQueryStringTests
        : QueryStringTests<Pagination>
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
            Test(new Pagination(), "");
        }

        [TestMethod]
        public void TestDefaultPage()
        {
            Test(new Pagination { Page = 1 }, "");
        }

        [TestMethod]
        public void TestPage()
        {
            Test(new Pagination { Page = 2 }, "Page=2");
        }

        [TestMethod]
        public void TestDefaultItems()
        {
            Test(new Pagination { Items = 25 }, "");
        }

        [TestMethod]
        public void TestItems()
        {
            Test(new Pagination { Items = 10 }, "Items=10");
        }

        [TestMethod]
        public void TestAll()
        {
            Test(new Pagination { Page = 2, Items = 10 }, "Page=2&Items=10");
        }

        private void Test(Pagination pagination, string expectedQueryString)
        {
            Test(pagination, expectedQueryString, () => new PaginationConverter(DefaultItems), () => new PaginationConverter(DefaultItems));
        }

        private static bool IsEmpty(Pagination pagination)
        {
            if (pagination == null)
                return true;

            return (pagination.Page == null || pagination.Page == 1)
                && (pagination.Items == null || pagination.Items == DefaultItems);
        }

        protected override void AssertAreEqual(Pagination expectedPagination, Pagination pagination)
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