using System.Text;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Mvc.Models.Converters;
using LinkMe.Apps.Asp.Mvc.Models.JavaScriptConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Mvc.Models
{
    [TestClass]
    public class PaginationJavaScriptSerializerTests
        : JavaScriptSerializerTests<Pagination>
    {
        private const int DefaultItems = 25;

        [TestMethod]
        public void TestNull()
        {
            Test(null);
        }

        [TestMethod]
        public void TestDefault()
        {
            Test(new Pagination());
        }

        [TestMethod]
        public void TestDefaultPage()
        {
            Test(new Pagination { Page = 1 });
        }

        [TestMethod]
        public void TestPage()
        {
            Test(new Pagination { Page = 2 });
        }

        [TestMethod]
        public void TestDefaultItems()
        {
            Test(new Pagination { Items = 25 });
        }

        [TestMethod]
        public void TestItems()
        {
            Test(new Pagination { Items = 10 });
        }

        [TestMethod]
        public void TestAll()
        {
            Test(new Pagination { Page = 2, Items = 10 });
        }

        private void Test(Pagination pagination)
        {
            Test(pagination, () => new PaginationJavaScriptConverter(DefaultItems), () => new PaginationConverter(DefaultItems));
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

        protected override string GetExpectedSerialization(Pagination model)
        {
            if (model == null)
                return "null";

            var sb = new StringBuilder();
            sb.Append("{");
            if (model.Page != null && model.Page.Value != 1)
                sb.Append("\"Page\":").Append(model.Page.Value);
            if (model.Items != null && model.Items.Value != DefaultItems)
            {
                if (sb.Length != 1)
                    sb.Append(",");
                sb.Append("\"Items\":").Append(model.Items.Value);
            }

            sb.Append("}");
            return sb.ToString();
        }
    }
}