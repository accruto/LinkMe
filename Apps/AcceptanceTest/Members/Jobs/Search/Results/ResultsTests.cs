using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results
{
    [TestClass]
    public abstract class ResultsTests
        : SearchTests
    {
        [TestInitialize]
        public void ResultsTestsInitialize()
        {
            JobAdSearchHost.ClearIndex();
            JobAdSortHost.ClearIndex();
        }

        protected void AssertResults(bool assertOrder, params JobAd[] jobAds)
        {
            AssertResults(assertOrder, (from j in jobAds select j.Id).ToArray());
        }

        protected void AssertResults(bool assertOrder, params Guid[] jobAdIds)
        {
            Assert.IsTrue(assertOrder ? GetJobAdIds().SequenceEqual(jobAdIds) : GetJobAdIds().CollectionEqual(jobAdIds));
        }

        protected void AssertApiResults(JsonSearchResponseModel model, bool assertOrder, params JobAd[] jobAds)
        {
            AssertApiResults(model, assertOrder, (from j in jobAds select j.Id).ToArray());
        }

        protected void AssertApiResults(JsonSearchResponseModel model, bool assertOrder, params Guid[] jobAdIds)
        {
            Assert.IsTrue(
                assertOrder
                ? (from j in model.JobAds select j.JobAdId).ToList().SequenceEqual(jobAdIds)
                : (from j in model.JobAds select j.JobAdId).ToList().CollectionEqual(jobAdIds));
        }

        private IEnumerable<Guid> GetJobAdIds()
        {
            var nodes = GetJobAdNodes();
            return (from j in nodes
                    select new Guid(j.Attributes["id"].Value)).ToList();
        }

        private IEnumerable<HtmlNode> GetJobAdNodes()
        {
            var resultsNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='results']");
            return (from c in resultsNode.ChildNodes
                    let a = c.Attributes["class"]
                    where c.Name == "div"
                    && a != null
                    && a.Value.StartsWith("jobad-list-view")
                    && !a.Value.Contains("empty")
                    select c).ToList();
        }
    }
}