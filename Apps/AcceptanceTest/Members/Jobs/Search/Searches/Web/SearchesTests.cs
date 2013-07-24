using LinkMe.Query.Search.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches.Web
{
    public abstract class SearchesTests
        : SearchTests
    {
        protected readonly IJobAdSearchesCommand _jobAdSearchesCommand = Resolve<IJobAdSearchesCommand>();

        protected void AssertEmptyText(string text)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='text empty']");
            Assert.IsNotNull(node);
            Assert.AreEqual(text, node.InnerText);
        }

        protected void AssertNoEmptyText()
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='text empty']");
            Assert.IsNull(node);
        }

        protected void AssertSearches(params string[] keywords)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='title']/a/span");
            if (keywords.Length == 0)
            {
                Assert.IsNull(nodes);
            }
            else
            {
                Assert.AreEqual(keywords.Length, nodes.Count);
                for (var index = 0; index < keywords.Length; ++index)
                    Assert.IsTrue(nodes[index].InnerText.Contains(keywords[index]));
            }
        }
    }
}