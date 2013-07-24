using System;
using LinkMe.Domain.Resources;
using LinkMe.Query.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.Resources
{
    [TestClass]
    public class SearchTests
        : ResourceSearchTests
    {
        [TestMethod]
        public void MultipleTypesTest()
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

            var qna1 = new QnA
                                        {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now,
                SubcategoryId = subcategory1,
                Text = "An answered question",
                Title = "An answered question title",
            };

            IndexItem(qna1);

            var faq1 = new Faq
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now,
                SubcategoryId = subcategory1,
                Text = "How do I do this",
                Title = "Read the manual",
            };

            IndexItem(faq1);

            // search

            var resourceQuery = new ResourceSearchQuery { ResourceType = ResourceType.Article };
            var results = Search(resourceQuery, 10);
            Assert.AreEqual(1, results.ResourceIds.Count);
            Assert.AreEqual(article1.Id, results.ResourceIds[0]);
        }
    }
}
