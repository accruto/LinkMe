using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Deprecated
{
    [TestClass]
    public class BlogsTests
        : WebTestClass
    {
        private ReadOnlyUrl _blogsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _blogsUrl = new ReadOnlyApplicationUrl("~/blogs");
        }

        [TestMethod]
        public void TestDeprecation()
        {
            TestDeprecation("~/ui/registered/blogs/BlogAdd.aspx");
            TestDeprecation("~/ui/registered/blog/BlogAdd.aspx");
            TestDeprecation("~/ui/registered/blogs/BlogEdit.aspx");
            TestDeprecation("~/ui/registered/blog/BlogEdit.aspx");
            TestDeprecation("~/ui/registered/blogs/BlogPostAdd.aspx");
            TestDeprecation("~/ui/registered/blog/BlogPostAdd.aspx");
            TestDeprecation("~/ui/registered/blogs/BlogPostEdit.aspx");
            TestDeprecation("~/ui/registered/blog/BlogPostEdit.aspx");
            TestDeprecation("~/ui/registered/blogs/BlogCommentEdit.aspx");
            TestDeprecation("~/ui/registered/blog/BlogCommentEdit.aspx");
            TestDeprecation("~/ui/unregistered/blogs/AllBlogs.aspx");
            TestDeprecation("~/ui/unregistered/blog/AllBlogs.aspx");
            TestDeprecation("~/ui/unregistered/blogs/BlogMain.aspx");
            TestDeprecation("~/ui/unregistered/blog/BlogMain.aspx");
            TestDeprecation("~/ui/unregistered/blogs/BlogPost.aspx");
            TestDeprecation("~/ui/unregistered/blog/BlogPost.aspx");
            TestDeprecation("~/ui/unregistered/blogs/BlogTag.aspx");
            TestDeprecation("~/ui/unregistered/blog/BlogTag.aspx");
            TestDeprecation("~/ui/unregistered/blogs/BlogTagArchive.aspx");
            TestDeprecation("~/ui/unregistered/blog/BlogTagArchive.aspx");
            TestDeprecation("~/ui/unregistered/blogs/BlogSearch.aspx");
            TestDeprecation("~/ui/unregistered/blog/BlogSearch.aspx");
            TestDeprecation("~/ui/unregistered/blogs/BlogArchive.aspx");
            TestDeprecation("~/ui/unregistered/blog/BlogArchive.aspx");
            TestDeprecation("~/ui/unregistered/blogs/Feed.aspx");
            TestDeprecation("~/ui/unregistered/blog/Feed.aspx");
        }

        private void TestDeprecation(string url)
        {
            Get(new ReadOnlyApplicationUrl(url));
            AssertUrl(_blogsUrl);
            AssertPageContains("Unfortunately, this means that Blogs will no longer be available on our site.");
        }
    }
}
