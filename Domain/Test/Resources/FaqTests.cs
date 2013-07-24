using System;
using System.Linq;
using LinkMe.Domain.Resources;
using LinkMe.Domain.Resources.Commands;
using LinkMe.Domain.Resources.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Resources
{
    [TestClass]
    public class FaqTests
        : ResourcesTests
    {
        private readonly IFaqsCommand _faqsCommand = Resolve<IFaqsCommand>();
        private readonly IFaqsQuery _faqsQuery = Resolve<IFaqsQuery>();

        private const string Title1 = "Are my details secure?";
        private const string Title2 = "Can I delete my notes?";
        private const string Title3 = "Can I delete or rename my folders?";
        private const string Title4 = "Can I assign the same note to more than one candidate?";
        private const string Text1 = "<p>Having your resume on LinkMe is completely safe and secure. You are always in control of who can see your details on the LinkMe site.</p>";
        private const string Text2 = "<p>You can delete your notes at any time from the candidate record by going into the resume and deleting notes from the profile. You will not be able to delete notes made by other users within your organisation. </p>";

        [TestMethod]
        public void TestFaqs()
        {
            var categories = _faqsQuery.GetCategories();
            var subcategory1 = categories.Single(c => c.Name == "Candidates").Subcategories.Single(s => s.Name == "About LinkMe");
            var subcategory2 = categories.Single(c => c.Name == "Employers").Subcategories.Single(s => s.Name == "Folders and notes");

            // GetFaqs()

            var faqs = _faqsQuery.GetFaqs();
            Assert.AreEqual(173, faqs.Count);

            var faq1 = (from f in faqs where f.Title == Title1 select f).Single();
            var faq2 = (from f in faqs where f.Title == Title2 select f).Single();

            AssertFaq(Title1, Text1, subcategory1, faq1);
            AssertFaq(Title2, Text2, subcategory2, faq2);

            // GetFaq(id)

            AssertFaq(Title1, Text1, subcategory1, _faqsQuery.GetFaq(faq1.Id));
            AssertFaq(Title2, Text2, subcategory2, _faqsQuery.GetFaq(faq2.Id));
            Assert.IsNull(_faqsQuery.GetFaq(Guid.NewGuid()));

            // GetFaqs(ids)

            faqs = _faqsQuery.GetFaqs(new[] { faq1.Id });
            Assert.AreEqual(1, faqs.Count);
            AssertFaq(Title1, Text1, subcategory1, faqs[0]);

            faqs = _faqsQuery.GetFaqs(new[] { faq1.Id, faq2.Id });
            Assert.AreEqual(2, faqs.Count);
            AssertFaq(Title1, Text1, subcategory1, (from f in faqs where f.Id == faq1.Id select f).Single());
            AssertFaq(Title2, Text2, subcategory2, (from f in faqs where f.Id == faq2.Id select f).Single());

            Assert.AreEqual(0, _faqsQuery.GetFaqs(new[] { Guid.NewGuid() }).Count);
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

            _faqsCommand.MarkHelpful(faq1.Id);

            _faqsCommand.MarkHelpful(faq2.Id);
            _faqsCommand.MarkHelpful(faq2.Id);
            _faqsCommand.MarkNotHelpful(faq2.Id);
            _faqsCommand.MarkHelpful(faq2.Id);

            _faqsCommand.MarkHelpful(faq3.Id);
            _faqsCommand.MarkHelpful(faq3.Id);
            _faqsCommand.MarkHelpful(faq3.Id);

            _faqsCommand.MarkHelpful(faq4.Id);

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

        private static void AssertFaq(string expectedTitle, string expectedText, Subcategory expectedSubCategory, Resource faq)
        {
            Assert.AreEqual(expectedTitle, faq.Title);
            Assert.AreEqual(expectedText, faq.Text);
            Assert.AreEqual(expectedSubCategory.Id, faq.SubcategoryId);
        }
    }
}
