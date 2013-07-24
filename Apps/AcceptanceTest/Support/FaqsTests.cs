using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Presentation;
using LinkMe.Domain.Resources;
using LinkMe.Domain.Resources.Commands;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Support
{
    [TestClass]
    public class FaqsTests
        : WebTestClass
    {
        private readonly IFaqsCommand _faqsCommand = Resolve<IFaqsCommand>();
        private readonly IFaqsQuery _faqsQuery = Resolve<IFaqsQuery>();

        private ReadOnlyUrl _faqsUrl;
        private ReadOnlyUrl _oldFaqsUrl;
        private ReadOnlyUrl _markHelpfulUrl;
        private ReadOnlyUrl _markNotHelpfulUrl;
        private HtmlDropDownListTester _categoryIdDropDownList;

        private const string Title1 = "Are my details secure?";
        private const string Title2 = "Can I delete my notes?";
        private const string Title3 = "Can I delete or rename my folders?";
        private const string Title4 = "Can I assign the same note to more than one candidate?";

        [TestInitialize]
        public void TestInitialize()
        {
            _faqsUrl = new ReadOnlyApplicationUrl("~/faqs");
            _oldFaqsUrl = new ReadOnlyApplicationUrl("~/faq");
            _markHelpfulUrl = new ReadOnlyApplicationUrl("~/faqs/api/helpful");
            _markNotHelpfulUrl = new ReadOnlyApplicationUrl("~/faqs/api/nothelpful");

            _categoryIdDropDownList = new HtmlDropDownListTester(Browser, "CategoryId");
        }

        [TestMethod]
        public void TestFaqs()
        {
            var categories = _faqsQuery.GetCategories();
            var candidatesCategory = categories.First(c => c.Name == "Candidates").Subcategories[0];
            var employersSubcategory = categories.First(c => c.Name == "Employers").Subcategories[0];
            var securitySubcategory = categories.First(c => c.Name == "Online Security").Subcategories[0];

            // Top 5 FAQs.

            var membersFaqs = (from f in _faqsQuery.GetFaqs() where f.SubcategoryId == candidatesCategory.Id select f).Take(5).ToList();
            var employersFaqs = (from f in _faqsQuery.GetFaqs() where f.SubcategoryId == employersSubcategory.Id select f).Take(5).ToList();

            // Make sure they are helpful.

            for (var index = 0; index < membersFaqs.Count; ++index)
            {
                for (var count = 0; count < 10 - index; ++count)
                    _faqsCommand.MarkHelpful(membersFaqs[index].Id);
            }

            for (var index = 0; index < employersFaqs.Count; ++index)
            {
                for (var count = 0; count < 10 - index; ++count)
                    _faqsCommand.MarkHelpful(employersFaqs[index].Id);
            }

            // Get the page.

            Get(_faqsUrl);
            AssertBreadcrumbs();

            // Looking for ...

            AssertLink("//a[@class='button forcandidates']", GetSubcategoryUrl(candidatesCategory), "");
            AssertLink("//a[@class='button foremployers']", GetSubcategoryUrl(employersSubcategory), "");
            AssertLink("//a[@class='button onlinesecurity']", GetSubcategoryUrl(securitySubcategory), "");

            // Members.

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@id='top5c']//a");
            Assert.AreEqual(membersFaqs.Count, nodes.Count);
            for (var index = 0; index < nodes.Count; ++index)
            {
                Assert.IsTrue(string.Equals(GetFaqUrl(membersFaqs[index], null, categories).PathAndQuery, nodes[index].Attributes["href"].Value, StringComparison.CurrentCultureIgnoreCase));
                Assert.AreEqual(membersFaqs[index].Title, nodes[index].InnerText);
            }

            // Employers.

            nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@id='top5e']//a");
            Assert.AreEqual(employersFaqs.Count, nodes.Count);
            for (var index = 0; index < nodes.Count; ++index)
            {
                Assert.IsTrue(string.Equals(GetFaqUrl(employersFaqs[index], null, categories).PathAndQuery, nodes[index].Attributes["href"].Value, StringComparison.CurrentCultureIgnoreCase));
                Assert.AreEqual(employersFaqs[index].Title, nodes[index].InnerText);
            }
        }

        [TestMethod]
        public void TestSubcategory()
        {
            var categories = _faqsQuery.GetCategories();
            var subcategory = categories.First(c => c.Name == "Employers").Subcategories[1];

            var url = GetSubcategoryUrl(subcategory);
            Get(url);
            AssertUrl(url);

            // Bread crumbs.

            AssertBreadcrumbs();

            // Categories.

            AssertCategories(categories, subcategory);

            // Faqs.

            AssertFaqs(categories, subcategory, (from f in _faqsQuery.GetFaqs() where f.SubcategoryId == subcategory.Id select f).ToList());
        }

        [TestMethod]
        public void TestPartialSubcategory()
        {
            var categories = _faqsQuery.GetCategories();
            var subcategory = categories.First(c => c.Name == "Employers").Subcategories[1];

            var url = GetPartialSubcategoryUrl(subcategory);
            Get(url);
            AssertUrl(url);

            // Faqs.

            AssertFaqs(categories, subcategory, (from f in _faqsQuery.GetFaqs() where f.SubcategoryId == subcategory.Id select f).ToList());
        }

        [TestMethod]
        public void TestFaq()
        {
            var categories = _faqsQuery.GetCategories();
            var faq = _faqsQuery.GetFaqs()[14];
            var subcategory = (from s in categories.SelectMany(c => c.Subcategories) where s.Id == faq.SubcategoryId select s).Single();

            var url = GetFaqUrl(faq, null, categories);
            Get(url);
            AssertUrl(url);

            // Bread crumbs.

            AssertBreadcrumbs();

            // Categories.

            AssertCategories(categories, subcategory);

            // Faqs.

            AssertFaq(faq, false);
        }

        [TestMethod]
        public void TestPartialFaq()
        {
            var faq = _faqsQuery.GetFaqs()[14];

            var url = GetPartialFaqUrl(faq.Id, null);
            Get(url);
            AssertUrl(url);

            // Faqs.

            AssertFaq(faq, false);
        }

        [TestMethod]
        public void TestSearchWithoutCategory()
        {
            const string keywords = "company";
            var categories = _faqsQuery.GetCategories();

            // Uses a default of Members.

            var membersCategory = categories.First(c => c.Name == "Candidates");
            var faqs = (from f in _faqsQuery.GetFaqs()
                        where (f.Title.ToLower().Contains(keywords) || f.Text.ToLower().Contains(keywords))
                        && membersCategory.Subcategories.Select(s => s.Id).Contains(f.SubcategoryId)
                        select f).ToList();

            var url = GetSearchUrl(null, keywords);
            Get(url);
            AssertUrl(url);

            // Bread crumbs.

            AssertBreadcrumbs();

            // Categories.

            AssertCategories(categories, null);

            // Faqs.

            AssertFaqs(categories, faqs);
        }

        [TestMethod]
        public void TestSearchWithCategory()
        {
            const string keywords = "company";
            var categories = _faqsQuery.GetCategories();
            var candidatesCategory = categories.First(c => c.Name == "Candidates");
            var faqs = (from f in _faqsQuery.GetFaqs()
                        where (f.Title.ToLower().Contains(keywords) || f.Text.ToLower().Contains(keywords))
                        && candidatesCategory.Subcategories.Select(s => s.Id).Contains(f.SubcategoryId)
                        select f).ToList();

            var url = GetSearchUrl(candidatesCategory.Id, keywords);
            Get(url);
            AssertUrl(url);

            // Bread crumbs.

            AssertBreadcrumbs();

            // Categories.

            AssertCategories(categories, null);

            // Faqs.

            AssertFaqs(categories, faqs);
        }

        [TestMethod]
        public void TestPartialSearchWithoutCategory()
        {
            const string keywords = "company";
            var categories = _faqsQuery.GetCategories();

            // Uses a default of Members.

            var candidatesCategory = categories.First(c => c.Name == "Candidates");
            var faqs = (from f in _faqsQuery.GetFaqs()
                        where (f.Title.ToLower().Contains(keywords) || f.Text.ToLower().Contains(keywords))
                        && candidatesCategory.Subcategories.Select(s => s.Id).Contains(f.SubcategoryId)
                        select f).ToList();

            var url = GetPartialSearchUrl(null, keywords);
            Get(url);
            AssertUrl(url);

            // Faqs.

            AssertFaqs(categories, faqs);
        }

        [TestMethod]
        public void TestPartialSearchWithCategory()
        {
            const string keywords = "company";
            var categories = _faqsQuery.GetCategories();
            var candidatesCategory = categories.First(c => c.Name == "Candidates");
            var faqs = (from f in _faqsQuery.GetFaqs()
                        where (f.Title.ToLower().Contains(keywords) || f.Text.ToLower().Contains(keywords))
                        && candidatesCategory.Subcategories.Select(s => s.Id).Contains(f.SubcategoryId)
                        select f).ToList();

            var url = GetPartialSearchUrl(candidatesCategory.Id, keywords);
            Get(url);
            AssertUrl(url);

            // Faqs.

            AssertFaqs(categories, faqs);
        }

        [TestMethod]
        public void TestPartialSearchFaq()
        {
            const string keywords = "company";
            var faqs = (from f in _faqsQuery.GetFaqs() where f.Title.ToLower().Contains(keywords) || f.Text.ToLower().Contains(keywords) select f).ToList();
            var faq = faqs[0];

            var url = GetPartialFaqUrl(faq.Id, keywords);
            Get(url);
            AssertUrl(url);

            // Faqs.

            AssertFaq(faq, true);
        }

        [TestMethod]
        public void TestHelpfulFaqs()
        {
            var categories = _faqsQuery.GetCategories();
            var faqs = _faqsQuery.GetFaqs();

            var category1 = categories.Single(c => c.Name == "Candidates");
            var faq1 = (from f in faqs where f.Title == Title1 select f).Single();

            // These titles all belong to the same category.

            var category2 = categories.Single(c => c.Name == "Employers");
            var faq2 = (from f in faqs where f.Title == Title2 select f).Single();
            var faq3 = (from f in faqs where f.Title == Title3 select f).Single();
            var faq4 = (from f in faqs where f.Title == Title4 select f).Single();

            MarkHelpful(faq1.Id);

            MarkHelpful(faq2.Id);
            MarkHelpful(faq2.Id);
            MarkNotHelpful(faq2.Id);
            MarkHelpful(faq2.Id);

            MarkHelpful(faq3.Id);
            MarkHelpful(faq3.Id);
            MarkHelpful(faq3.Id);

            MarkHelpful(faq4.Id);

            faqs = _faqsQuery.GetHelpfulFaqs(category1.Id, 1);
            Assert.AreEqual(1, faqs.Count);
            Assert.AreEqual(faq1.Id, faqs[0].Id);

            faqs = _faqsQuery.GetHelpfulFaqs(category2.Id, 3);
            Assert.AreEqual(3, faqs.Count);
            Assert.AreEqual(faq3.Id, faqs[0].Id);
            Assert.AreEqual(faq2.Id, faqs[1].Id);
            Assert.AreEqual(faq4.Id, faqs[2].Id);

            faqs = _faqsQuery.GetHelpfulFaqs(category2.Id, 2);
            Assert.AreEqual(2, faqs.Count);
            Assert.AreEqual(faq3.Id, faqs[0].Id);
            Assert.AreEqual(faq2.Id, faqs[1].Id);

            faqs = _faqsQuery.GetHelpfulFaqs(category2.Id, 1);
            Assert.AreEqual(1, faqs.Count);
            Assert.AreEqual(faq3.Id, faqs[0].Id);
        }

        [TestMethod]
        public void TestRedirects()
        {
            var categories = _faqsQuery.GetCategories();
            var faq = _faqsQuery.GetFaqs()[14];
            var subcategory = (from s in categories.SelectMany(c => c.Subcategories) where s.Id == faq.SubcategoryId select s).Single();
            const string keywords = "company";
            var category = categories.First(c => c.Name == "Candidates");

            // Faqs.

            Get(_oldFaqsUrl);
            AssertUrl(_faqsUrl);

            // Faq.

            var url = GetOldFaqUrl(faq, categories);
            var redirectUrl = GetFaqUrl(faq, null, categories);

            Get(url);
            AssertUrl(redirectUrl);

            // Subcategory.

            url = GetOldSubcategoryUrl(subcategory);
            redirectUrl = GetSubcategoryUrl(subcategory);

            Get(url);
            AssertUrl(redirectUrl);

            // Search.

            url = GetOldSearchUrl(category.Id, keywords);
            redirectUrl = GetSearchUrl(category.Id, keywords);

            Get(url);
            AssertUrl(redirectUrl);
        }

        [TestMethod]
        public void TestFaqHash()
        {
            var categories = _faqsQuery.GetCategories();
            var faq = _faqsQuery.GetFaqs()[14];

            var faqUrl = GetFaqUrl(faq, null, categories);
            var hashUrl = GetHashUrl(faq.Id, null, null, null);

            Get(hashUrl);
            AssertUrl(faqUrl);
        }

        [TestMethod]
        public void TestFaqKeywordsHash()
        {
            var categories = _faqsQuery.GetCategories();
            var faq = _faqsQuery.GetFaqs()[14];
            const string keywords = "company";

            var faqUrl = GetFaqUrl(faq, keywords, categories);
            var hashUrl = GetHashUrl(faq.Id, null, null, keywords);

            Get(hashUrl);
            AssertUrl(faqUrl);
        }

        [TestMethod]
        public void TestSubcategoryHash()
        {
            var categories = _faqsQuery.GetCategories();
            var subcategory = categories.First(c => c.Name == "Employers").Subcategories[1];

            var subcategoryUrl = GetSubcategoryUrl(subcategory);
            var hashUrl = GetHashUrl(null, null, subcategory.Id, null);

            Get(hashUrl);
            AssertUrl(subcategoryUrl);
        }

        [TestMethod]
        public void TestSearchHash()
        {
            var category = _faqsQuery.GetCategories()[1];
            const string keywords = "company";

            var searchUrl = GetSearchUrl(category.Id, keywords);
            var hashUrl = GetHashUrl(null, category.Id, null, keywords);

            Get(hashUrl);
            AssertUrl(searchUrl);
        }

        private void MarkHelpful(Guid id)
        {
            var parameters = new NameValueCollection { { "id", id.ToString() } };
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(_markHelpfulUrl, parameters)));
        }

        private void MarkNotHelpful(Guid id)
        {
            var parameters = new NameValueCollection { { "id", id.ToString() } };
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(_markNotHelpfulUrl, parameters)));
        }

        private void AssertFaqs(IList<Category> categories, Subcategory subcategory, ICollection<Faq> faqs)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='subcategory-faqlist']");
            Assert.AreEqual(subcategory.Name, node.SelectSingleNode("./div[@class='title']").InnerText);

            AssertFaqs(categories, faqs, node);
        }

        private void AssertFaqs(IList<Category> categories, ICollection<Faq> faqs)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='search-faqlist']");
            Assert.AreEqual("Results", node.SelectSingleNode("./div[@class='title']").InnerText);

            AssertFaqs(categories, faqs, node);
        }

        private static void AssertFaqs(IList<Category> categories, ICollection<Faq> faqs, HtmlNode node)
        {
            var faqNodes = node.SelectNodes(".//a[@class='faqitem']");
            Assert.AreEqual(faqs.Count, faqNodes.Count);

            foreach (var faq in faqs)
            {
                var url = GetFaqUrl(faq, null, categories);
                var faqNode = (from n in faqNodes
                               where string.Equals(n.Attributes["href"].Value, url.PathAndQuery, StringComparison.InvariantCultureIgnoreCase)
                               select n).Single();

                Assert.IsTrue(faqNode.InnerText.TrimStart().StartsWith(faq.Title));
            }
        }

        private void AssertFaq(Resource faq, bool isSearch)
        {
            Assert.AreEqual(faq.Title, Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='faq" + (isSearch ? " search" : "") + "']//div[@class='title']").InnerText);
            var contentNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='faq" + (isSearch ? " search" : "") + "']//div[@class='content']");
            Assert.IsTrue(contentNode.InnerText.TrimStart().StartsWith(GetInnerText(faq.Text)));
        }

        private void AssertCategories(IList<Category> categories, Subcategory currentSubcategory)
        {
            // Drop down.

            var items = _categoryIdDropDownList.Items;
            Assert.IsTrue(categories.Select(c => (Guid?)c.Id).Concat(new[] { (Guid?)null }).CollectionEqual(items.Select(i => string.IsNullOrEmpty(i.Value) ? (Guid?)null : new Guid(i.Value))));

            // Left bar.

            foreach (var category in categories)
            {
                var categoryNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='subcategories " + category.Name.ToLower().Replace(" ", "-") + "']");
                if (currentSubcategory == null)
                {
                    if (category.Name == "Candidates")
                        Assert.IsNull(categoryNode.Attributes["style"]);
                    else
                        Assert.AreEqual("display:none;", categoryNode.Attributes["style"].Value);
                }
                else
                {
                    if (category.Subcategories.Any(s => s.Id == currentSubcategory.Id))
                        Assert.IsNull(categoryNode.Attributes["style"]);
                    else
                        Assert.AreEqual("display:none;", categoryNode.Attributes["style"].Value);
                }

                var subcategoryNodes = categoryNode.SelectNodes("a");
                Assert.AreEqual(category.Subcategories.Count, subcategoryNodes.Count);

                foreach (var subcategory in category.Subcategories)
                {
                    var url = GetSubcategoryUrl(subcategory);
                    var subcategoryNode = (from n in subcategoryNodes
                                           where string.Equals(n.Attributes["href"].Value, url.PathAndQuery, StringComparison.InvariantCultureIgnoreCase)
                                           select n).Single();

                    Assert.AreEqual(currentSubcategory != null && subcategory.Id == currentSubcategory.Id ? "subcategory current" : "subcategory", subcategoryNode.Attributes["class"].Value);
                    Assert.AreEqual(subcategory.Name, subcategoryNode.InnerText.Trim());
                }
            }
        }

        private static ReadOnlyUrl GetFaqUrl(Resource faq, string keywords, IEnumerable<Category> categories)
        {
            var subcategory = (from s in categories.SelectMany(c => c.Subcategories) where s.Id == faq.SubcategoryId select s).Single();
            var url = new ApplicationUrl("~/faqs/" + subcategory.Name.EncodeUrlSegment() + "/" + faq.Title.EncodeUrlSegment() + "/" + faq.Id);
            if (!string.IsNullOrEmpty(keywords))
                url.QueryString["keywords"] = keywords;
            return url;
        }

        private static ReadOnlyUrl GetOldFaqUrl(Resource faq, IEnumerable<Category> categories)
        {
            var subcategory = (from s in categories.SelectMany(c => c.Subcategories) where s.Id == faq.SubcategoryId select s).Single();
            return new ReadOnlyApplicationUrl("~/faq/" + subcategory.Name.EncodeUrlSegment() + "/" + faq.Title.EncodeUrlSegment() + "/" + faq.Id);
        }

        private static ReadOnlyUrl GetPartialFaqUrl(Guid faqId, string keywords)
        {
            var url = new ApplicationUrl("~/faqs/faq/partial", new ReadOnlyQueryString("id", faqId.ToString()));
            if (!string.IsNullOrEmpty(keywords))
                url.QueryString["keywords"] = keywords;
            return url;
        }

        private static ReadOnlyUrl GetSubcategoryUrl(Subcategory subcategory)
        {
            return new ReadOnlyApplicationUrl("~/faqs/" + subcategory.Name.EncodeUrlSegment() + "/" + subcategory.Id);
        }

        private static ReadOnlyUrl GetOldSubcategoryUrl(Subcategory subcategory)
        {
            return new ReadOnlyApplicationUrl("~/faq/" + subcategory.Name.EncodeUrlSegment() + "/" + subcategory.Id);
        }

        private static ReadOnlyUrl GetPartialSubcategoryUrl(Subcategory subcategory)
        {
            return new ReadOnlyApplicationUrl("~/faqs/subcategory/partial", new ReadOnlyQueryString("subcategoryId", subcategory.Id.ToString()));
        }

        private static ReadOnlyUrl GetSearchUrl(Guid? categoryId, string keywords)
        {
            var url = new ApplicationUrl("~/faqs/search");
            if (categoryId != null)
                url.QueryString["categoryId"] = categoryId.Value.ToString();
            if (!string.IsNullOrEmpty(keywords))
                url.QueryString["keywords"] = keywords;
            return url;
        }

        private static ReadOnlyUrl GetOldSearchUrl(Guid? categoryId, string keywords)
        {
            var url = new ApplicationUrl("~/faq/search");
            if (categoryId != null)
                url.QueryString["categoryId"] = categoryId.Value.ToString();
            if (!string.IsNullOrEmpty(keywords))
                url.QueryString["keywords"] = keywords;
            return url;
        }

        private static ReadOnlyUrl GetPartialSearchUrl(Guid? categoryId, string keywords)
        {
            var url = new ApplicationUrl("~/faqs/search/partial");
            if (categoryId != null)
                url.QueryString["categoryId"] = categoryId.Value.ToString();
            if (!string.IsNullOrEmpty(keywords))
                url.QueryString["keywords"] = keywords;
            return url;
        }

        private static ReadOnlyUrl GetHashUrl(Guid? faqId, Guid? categoryId, Guid? subcategoryId, string keywords)
        {
            var url = new ApplicationUrl("~/faqs/hash");
            if (faqId != null)
                url.QueryString["faqId"] = faqId.Value.ToString();
            if (categoryId != null)
                url.QueryString["categoryId"] = categoryId.Value.ToString();
            if (subcategoryId != null)
                url.QueryString["subcategoryId"] = subcategoryId.Value.ToString();
            if (!string.IsNullOrEmpty(keywords))
                url.QueryString["keywords"] = keywords;
            return url;
        }

        private void AssertBreadcrumbs()
        {
            var breadcrumbs = new[] { "FAQs", "Search for \"\"", "", "" };
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//ul[@class='breadcrumbs']/li");
            Assert.IsTrue((from b in breadcrumbs select b.Trim()).SequenceEqual(from n in nodes select n.InnerText.Trim()));
        }

        private static string GetInnerText(string text)
        {
            var document = new HtmlDocument();
            document.LoadHtml(text);
            return document.DocumentNode.InnerText;
        }
    }
}
