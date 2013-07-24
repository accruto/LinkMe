using System;
using System.Linq;
using LinkMe.Domain.Resources;
using LinkMe.Query.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.Resources
{
    [TestClass]
    public class SortTests
        : ResourceSearchTests
    {
        [TestMethod]
        public void CreatedTimeSortTest()
        {
            //Create items

            var subcategory1 = Guid.NewGuid();

            var article1 = new Article
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now.AddDays(-2),
                SubcategoryId = subcategory1,
                Text = "An article",
                Title = "An article title",
            };

            IndexItem(article1);

            var article2 = new Article
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now,
                SubcategoryId = subcategory1,
                Text = "Another article",
                Title = "Another article title",
            };

            IndexItem(article2);

            var article3 = new Article
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now.AddDays(-5),
                SubcategoryId = subcategory1,
                Text = "Another article",
                Title = "Another article title",
            };

            IndexItem(article3);

            var video = new Video
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now,
                ExternalVideoId = "12345",
                SubcategoryId = subcategory1,
                Text = "A Video",
                Title = "A video title",
            };

            IndexItem(video);

            var qna = new QnA
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now.AddDays(-1),
                SubcategoryId = subcategory1,
                Text = "An answer",
                Title = "A question",
            };

            IndexItem(qna);

            // Sort by createdTime.

            var query = new ResourceSearchQuery { SortOrder = ResourceSortOrder.CreatedTime, ResourceType = ResourceType.Article };
            var results = Search(query, 10);
            Assert.AreEqual(3, results.ResourceIds.Count);
            Assert.AreEqual(article2.Id, results.ResourceIds[0]);
            Assert.AreEqual(article1.Id, results.ResourceIds[1]);
            Assert.AreEqual(article3.Id, results.ResourceIds[2]);
            var resourceTypeHits = results.ResourceTypeHits.ToDictionary(h => h.Key, h => h.Value);
            Assert.AreEqual(3, resourceTypeHits[ResourceType.Article]);
            Assert.AreEqual(1, resourceTypeHits[ResourceType.Video]);
            Assert.AreEqual(1, resourceTypeHits[ResourceType.QnA]);

            query = new ResourceSearchQuery { SortOrder = ResourceSortOrder.CreatedTime, ReverseSortOrder = false, ResourceType = ResourceType.Article };
            results = Search(query, 10);
            Assert.AreEqual(3, results.ResourceIds.Count);
            Assert.AreEqual(article2.Id, results.ResourceIds[0]);
            Assert.AreEqual(article1.Id, results.ResourceIds[1]);
            Assert.AreEqual(article3.Id, results.ResourceIds[2]);
            resourceTypeHits = results.ResourceTypeHits.ToDictionary(h => h.Key, h => h.Value);
            Assert.AreEqual(3, resourceTypeHits[ResourceType.Article]);
            Assert.AreEqual(1, resourceTypeHits[ResourceType.Video]);
            Assert.AreEqual(1, resourceTypeHits[ResourceType.QnA]);

            query = new ResourceSearchQuery { SortOrder = ResourceSortOrder.CreatedTime, ReverseSortOrder = true, ResourceType = ResourceType.Article };
            results = Search(query, 10);
            Assert.AreEqual(3, results.ResourceIds.Count);
            Assert.AreEqual(article3.Id, results.ResourceIds[0]);
            Assert.AreEqual(article1.Id, results.ResourceIds[1]);
            Assert.AreEqual(article2.Id, results.ResourceIds[2]);
            resourceTypeHits = results.ResourceTypeHits.ToDictionary(h => h.Key, h => h.Value);
            Assert.AreEqual(3, resourceTypeHits[ResourceType.Article]);
            Assert.AreEqual(1, resourceTypeHits[ResourceType.Video]);
            Assert.AreEqual(1, resourceTypeHits[ResourceType.QnA]);
        }

        [TestMethod]
        public void PopularitySortTest()
        {
            //Create items

            var subcategory1 = Guid.NewGuid();

            var article1 = new Article
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now.AddDays(-2),
                SubcategoryId = subcategory1,
                Text = "An article",
                Title = "An article title",
            };

            IndexItem(article1);

            var article2 = new Article
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now,
                SubcategoryId = subcategory1,
                Text = "Another article",
                Title = "Another article title",
            };

            IndexItem(article2);

            var article3 = new Article
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now,
                SubcategoryId = subcategory1,
                Text = "Yet another article",
                Title = "Yet another article title",
            };

            var userId = Guid.NewGuid();

            _resourcesCommand.ViewArticle(userId, article3.Id);
            _resourcesCommand.ViewArticle(Guid.NewGuid(), article3.Id);

            IndexItem(article3);

            var article4 = new Article
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now,
                SubcategoryId = subcategory1,
                Text = "Still another article",
                Title = "Still another article title",
            };

            IndexItem(article4);

            // Sort by popularity.

            var resourceQuery = new ResourceSearchQuery { SortOrder = ResourceSortOrder.Popularity, ResourceType = ResourceType.Article };
            var results = Search(resourceQuery, 10);
            Assert.AreEqual(4, results.ResourceIds.Count);
            Assert.AreEqual(article3.Id, results.ResourceIds[0]);

            // View and reindex
            _resourcesCommand.ViewArticle(userId, article4.Id);
            _resourcesCommand.ViewArticle(Guid.NewGuid(), article4.Id);
            _resourcesCommand.ViewArticle(Guid.NewGuid(), article4.Id);
            _resourcesCommand.ViewArticle(Guid.NewGuid(), article4.Id);

            IndexItem(article4, false);

            resourceQuery = new ResourceSearchQuery { SortOrder = ResourceSortOrder.Popularity, ResourceType = ResourceType.Article };
            results = Search(resourceQuery, 10);
            Assert.AreEqual(4, results.ResourceIds.Count);
            Assert.AreEqual(article4.Id, results.ResourceIds[0]);
            Assert.AreEqual(article3.Id, results.ResourceIds[1]);

            // View article 3 again by the same person
            _resourcesCommand.ViewArticle(userId, article3.Id);
            _resourcesCommand.ViewArticle(userId, article3.Id);
            _resourcesCommand.ViewArticle(userId, article3.Id);

            IndexItem(article3, false);

            resourceQuery = new ResourceSearchQuery { SortOrder = ResourceSortOrder.Popularity, ResourceType = ResourceType.Article };
            results = Search(resourceQuery, 10);
            Assert.AreEqual(4, results.ResourceIds.Count);
            Assert.AreEqual(article3.Id, results.ResourceIds[0]);
            Assert.AreEqual(article4.Id, results.ResourceIds[1]);
        }
    }
}
