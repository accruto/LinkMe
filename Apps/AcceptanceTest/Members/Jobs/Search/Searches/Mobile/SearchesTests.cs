using System;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches.Mobile
{
    [TestClass]
    public abstract class SearchesTests
        : SearchTests
    {
        protected readonly IJobAdSearchesCommand _jobAdSearchesCommand = Resolve<IJobAdSearchesCommand>();
        protected readonly IJobAdSearchesQuery _jobAdSearchesQuery = Resolve<IJobAdSearchesQuery>();
        protected readonly IJobAdSearchAlertsCommand _jobAdSearchAlertsCommand = Resolve<IJobAdSearchAlertsCommand>();
        protected readonly IJobAdSearchAlertsQuery _jobAdSearchAlertsQuery = Resolve<IJobAdSearchAlertsQuery>();

        protected ReadOnlyUrl _savedSearchesUrl;
        protected ReadOnlyUrl _saveSearchesUrl;

        [TestInitialize]
        public void SearchesTestInitialize()
        {
            Browser.UseMobileUserAgent = true;
            _savedSearchesUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/saved");
            _saveSearchesUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/save");
        }

        protected void AssertEmptyText(string text)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='noresults']/div[@class='desc']");
            Assert.IsNotNull(node);
            Assert.AreEqual(text, node.InnerText);
        }

        protected void AssertNoEmptyText()
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='noresults']/div[@class='desc']");
            Assert.IsNull(node);
        }

        protected void AssertRecentSearches(params string[] keywords)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='recentsearches']/a/span[@class='title']");
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

        protected void AssertSavedSearches(params Tuple<JobAdSearch, bool>[] searches)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='savedsearches']/a[@class='row']");
            if (searches.Length == 0)
            {
                Assert.IsNull(nodes);
            }
            else
            {
                Assert.AreEqual(searches.Length, nodes.Count);
                for (var index = 0; index < searches.Length; ++index)
                {
                    Assert.AreEqual(searches[index].Item1.Name ?? "", nodes[index].SelectSingleNode(".//span[@class='title']").InnerText);
                    Assert.AreEqual(searches[index].Item1.Criteria.GetDisplayText() ?? "", nodes[index].SelectSingleNode(".//span[@class='criteria']").InnerText);

                    if (searches[index].Item2)
                    {
                        Assert.IsNotNull(nodes[index].SelectSingleNode(".//div[@class='icon alert active']"));
                        Assert.IsNull(nodes[index].SelectSingleNode(".//div[@class='icon alert ']"));
                    }
                    else
                    {
                        Assert.IsNull(nodes[index].SelectSingleNode(".//div[@class='icon alert active']"));
                        Assert.IsNotNull(nodes[index].SelectSingleNode(".//div[@class='icon alert ']"));
                    }
                }
            }
        }
    }
}