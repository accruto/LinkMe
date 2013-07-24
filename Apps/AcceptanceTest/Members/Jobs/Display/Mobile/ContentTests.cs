using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display.Mobile
{
    [TestClass]
    public class ContentTests
        : MobileDisplayTests
    {
        [TestMethod]
        public void TestContent()
        {
            TestContent("This is the content.", "This is the content.");
        }

        [TestMethod]
        public void TestHtmlComments()
        {
            TestContent("<b>This</b> is <!-- a comment --> the content.", "<b>This</b> is  the content.");
        }

        [TestMethod]
        public void TestLineBreak()
        {
            TestContent(@"<b>This</b> is
the content.", "<b>This</b> is<br>the content.");
        }

        [TestMethod]
        public void TestHttp()
        {
            TestContent("<b>This</b> is <img src=\"http://www.google.com\" /> the content.", "<b>This</b> is <img src=\"https://www.google.com\"> the content.");
        }

        [TestMethod]
        public void TestHttps()
        {
            TestContent("<b>This</b> is <img src=\"https://www.google.com\" /> the content.", "<b>This</b> is <img src=\"https://www.google.com\"> the content.");
        }

        private void TestContent(string content, string expectedContentHtml)
        {
            var employer = CreateEmployer();
            var jobAd = PostJobAd(employer, j => { j.Description.Content = content; });

            Get(GetJobUrl(jobAd.Id));

            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='bg']/div[@class='content']");
            Assert.IsNotNull(node);
            Assert.AreEqual(expectedContentHtml, node.InnerHtml);
        }
    }
}
