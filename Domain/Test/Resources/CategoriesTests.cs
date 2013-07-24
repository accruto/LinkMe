using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources;
using LinkMe.Domain.Resources.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Resources
{
    [TestClass]
    public class CategoriesTests
        : ResourcesTests
    {
        private readonly IFaqsQuery _faqsQuery = Resolve<IFaqsQuery>();

        [TestMethod]
        public void TestResourceCategories()
        {
            var categories = _resourcesQuery.GetCategories();
            Assert.AreEqual(6, categories.Count);

            var resumeWriting = (from c in categories where c.Name == "Resume writing" select c).Single();
            AssertCategory("Resume writing", 10, 4, resumeWriting);
            AssertCategory("Resume writing", 10, 4, _resourcesQuery.GetCategory(resumeWriting.Id));

            var jobInterviewing = (from c in categories where c.Name == "Job interviewing" select c).Single();
            AssertCategory("Job interviewing", 20, 4, jobInterviewing);
            AssertCategory("Job interviewing", 20, 4, _resourcesQuery.GetCategory(jobInterviewing.Id));

            AssertCategory("Job search", 30, 2, (from c in categories where c.Name == "Job search" select c).Single());
            AssertCategory("Career management", 40, 2, (from c in categories where c.Name == "Career management" select c).Single());
            AssertCategory("Students and grads", 50, 2, (from c in categories where c.Name == "Students and grads" select c).Single());
            AssertCategory("Australian job market", 60, 2, (from c in categories where c.Name == "Australian job market" select c).Single());
        }

        [TestMethod]
        public void TestFaqCategories()
        {
            var categories = _faqsQuery.GetCategories();
            Assert.AreEqual(3, categories.Count);

            AssertCategory("Employers", 10, 7, (from c in categories where c.Name == "Employers" select c).Single());
            AssertCategory("Candidates", 20, 8, (from c in categories where c.Name == "Candidates" select c).Single());
            AssertCategory("Online Security", 30, 1, (from c in categories where c.Name == "Online Security" select c).Single());
        }

        [TestMethod]
        public void TestFaqUserTypeCategory()
        {
            
            AssertCategory("Employers", 10, 7, _faqsQuery.GetCategory(UserType.Employer));
            AssertCategory("Candidates", 20, 8, _faqsQuery.GetCategory(UserType.Member));
            Assert.IsNull(_faqsQuery.GetCategory(UserType.Administrator));
        }

        private static void AssertCategory(string expectedName, int expectedDisplayOrder, int expectedSubcategoryCount, Category category)
        {
            Assert.AreEqual(expectedName, category.Name);
            Assert.AreEqual(expectedDisplayOrder, category.DisplayOrder);
            Assert.AreEqual(expectedSubcategoryCount, category.Subcategories.Count);
        }
    }
}
