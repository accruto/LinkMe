using System;
using System.Collections.Generic;
using System.Threading;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Resources;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Framework.Host.Wcf;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Resources;
using LinkMe.Query.Search.Engine.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Resources.MultipleEngines
{
    [TestClass]
    public class MultipleEnginesTests
        : WebTestClass
    {
        private readonly IResourcesQuery _resourcesQuery = Resolve<IResourcesQuery>();

        private const int MonitorInterval = 2;

        private WcfTcpHost _host1;
        private IResourceSearchService _service1;
        private WcfTcpHost _host2;
        private IResourceSearchService _service2;

        [TestInitialize]
        public void TestInitialize()
        {
            ResourceSearchHost.Stop();
            StartHosts();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            StopHosts();
            ResourceSearchHost.Start();
        }

        [TestMethod]
        public void TestViewedArticles()
        {
            TestViewedItems(ResourceType.Article, () => _resourcesQuery.GetArticles(), GetViewArticleUrl);
        }

        [TestMethod]
        public void TestViewedVideos()
        {
            TestViewedItems(ResourceType.Video, () => _resourcesQuery.GetVideos(), GetViewVideoUrl);
        }

        [TestMethod, Ignore]
        public void TestViewedAnsweredQuestions()
        {
            TestViewedItems(ResourceType.QnA, () => _resourcesQuery.GetQnAs(), GetViewAnsweredQuestionUrl);
        }

        private void TestViewedItems<TResource>(ResourceType type, Func<IList<TResource>> getItems, Func<Guid, ReadOnlyUrl> getUrl)
            where TResource : Resource
        {
            // Pick some random items.

            var items = getItems();
            var item1 = items[5];
            var item2 = items[7];

            // Search.

            var query = new ResourceSearchQuery { SortOrder = ResourceSortOrder.Popularity, ResourceType = type };

            var results = _service1.Search(query, true);
            Assert.AreEqual(items.Count, results.ResourceIds.Count);

            results = _service2.Search(query, true);
            Assert.AreEqual(items.Count, results.ResourceIds.Count);

            // View the items to push them to the top.

            var url = getUrl(item1.Id);
            for (var index = 0; index < 5; ++index)
                AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(url)));

            url = getUrl(item2.Id);
            for (var index = 0; index < 7; ++index)
                AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(url)));

            // Do some searches again.

            results = _service1.Search(query, true);
            Assert.AreEqual(items.Count, results.ResourceIds.Count);
            Assert.AreEqual(item2.Id, results.ResourceIds[0]);
            Assert.AreEqual(item1.Id, results.ResourceIds[1]);

            // Wait for the polling to kick in.

            Thread.Sleep(3 * MonitorInterval * 1000);
            results = _service2.Search(query, true);
            Assert.AreEqual(items.Count, results.ResourceIds.Count);
            Assert.AreEqual(item2.Id, results.ResourceIds[0]);
            Assert.AreEqual(item1.Id, results.ResourceIds[1]);
        }

        private static ReadOnlyUrl GetViewArticleUrl(Guid id)
        {
            return new ReadOnlyApplicationUrl("~/members/resources/articles/api/view/" + id);
        }

        private static ReadOnlyUrl GetViewVideoUrl(Guid id)
        {
            return new ReadOnlyApplicationUrl("~/members/resources/videos/api/view/" + id);
        }

        private static ReadOnlyUrl GetViewAnsweredQuestionUrl(Guid id)
        {
            return new ReadOnlyApplicationUrl("~/members/resources/qnas/api/view/" + id);
        }

        private void StartHosts()
        {
            // The first service is the standard local service.

            var service = Resolve<ResourceSearchService>();
            _service1 = service;

            service.InitialiseIndex = true;
            service.RebuildIndex = true;
            service.MonitorForChanges = true;
            service.MonitorInterval = new TimeSpan(0, 0, 0, MonitorInterval);

            var serviceDefinition = new ServiceDefinition
            {
                Service = service,
                Address = Resolve<string>("linkme.search.resources.tcpAddress"),
                BindingName = "linkme.search.resources.tcp",
            };

            _host1 = new WcfTcpHost { ServiceDefinitions = new[] { serviceDefinition } };
            _host1.Open();
            _host1.Start();

            // The second service represents the remote service.

            service = Resolve<ResourceSearchService>("linkme.search.resources.otherservice");
            _service2 = service;

            service.InitialiseIndex = true;
            service.RebuildIndex = true;
            service.MonitorForChanges = true;
            service.MonitorInterval = new TimeSpan(0, 0, 0, MonitorInterval);

            serviceDefinition = new ServiceDefinition
            {
                Service = service,
                Address = Resolve<string>("linkme.search.resources.other.tcpAddress"),
                BindingName = "linkme.search.resources.tcp",
            };

            _host2 = new WcfTcpHost { ServiceDefinitions = new[] { serviceDefinition } };
            _host2.Open();
            _host2.Start();
        }

        private void StopHosts()
        {
            _host1.Stop();
            _host1.Close();
            _host1 = null;
            _service1 = null;

            _host2.Stop();
            _host2.Close();
            _host2 = null;
            _service2 = null;
        }
    }
}
