using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Support
{
    public abstract class SupportTests
        : WebTestClass
    {
        protected void AssertUrl(ReadOnlyUrl url, ReadOnlyUrl expectedUrl, string expectedPageTitle)
        {
            Get(url);
            AssertUrl(expectedUrl);

            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='" + PageTitleCssClass + "']/h1");
            
            if (string.IsNullOrEmpty(expectedPageTitle))
                Assert.IsNull(node);
            else
            {
                Assert.IsNotNull(node);
                Assert.AreEqual(expectedPageTitle, node.InnerText);
            }
        }

        protected virtual string PageTitleCssClass
        {
            get { return "page-title"; }
        }
    }
}
