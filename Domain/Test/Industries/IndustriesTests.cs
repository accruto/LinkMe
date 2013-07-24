using System.Linq;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Industries
{
    [TestClass]
    public class IndustriesTest
        : TestClass
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestMethod]
        public void TestGetIndustryById()
        {
            var industry = _industriesQuery.GetIndustries()[10];
            AssertAreEqual(industry, _industriesQuery.GetIndustry(industry.Id));
        }

        [TestMethod]
        public void TestGetIndustryByName()
        {
            var industry = _industriesQuery.GetIndustries()[10];
            AssertAreEqual(industry, _industriesQuery.GetIndustry(industry.Name));
        }

        [TestMethod]
        public void TestGetIndustryByAlias()
        {
            // Find an industry with aliases.

            var industry = _industriesQuery.GetIndustries().First(i => i.Aliases.Count() > 0);
            Assert.AreEqual(industry, _industriesQuery.GetIndustry(industry.Name));
            foreach (var alias in industry.Aliases)
                AssertAreEqual(industry, _industriesQuery.GetIndustry(alias));
        }

        [TestMethod]
        public void TestGetIndustryByUrlName()
        {
            var industry = _industriesQuery.GetIndustries().First(i => i.UrlAliases.Count == 0);
            AssertAreEqual(industry, _industriesQuery.GetIndustryByUrlName(industry.UrlName));

            industry = _industriesQuery.GetIndustries().First(i => i.UrlAliases.Count > 0);
            AssertAreEqual(industry, _industriesQuery.GetIndustryByUrlName(industry.UrlName));
            foreach (var urlAlias in industry.UrlAliases)
                AssertAreEqual(industry, _industriesQuery.GetIndustryByUrlName(urlAlias));
        }

        [TestMethod]
        public void TestGetIndustryByAnyName()
        {
            // Find an industry that has no aliases.

            var industry = _industriesQuery.GetIndustries().First(i => i.Aliases.Count == 0 && i.UrlAliases.Count == 0);
            AssertAreEqual(industry, _industriesQuery.GetIndustryByAnyName(industry.Name));
            AssertAreEqual(industry, _industriesQuery.GetIndustryByAnyName(industry.ShortName));
            AssertAreEqual(industry, _industriesQuery.GetIndustryByAnyName(industry.UrlName));

            // Find an industry that has an alias.

            industry = _industriesQuery.GetIndustries().First(i => i.Aliases.Count > 0);
            AssertAreEqual(industry, _industriesQuery.GetIndustryByAnyName(industry.Name));
            AssertAreEqual(industry, _industriesQuery.GetIndustryByAnyName(industry.ShortName));
            AssertAreEqual(industry, _industriesQuery.GetIndustryByAnyName(industry.UrlName));
            foreach (var alias in industry.Aliases)
                AssertAreEqual(industry, _industriesQuery.GetIndustryByAnyName(alias));

            // Find an industry that has a url alias.

            industry = _industriesQuery.GetIndustries().First(i => i.UrlAliases.Count > 0);
            AssertAreEqual(industry, _industriesQuery.GetIndustryByAnyName(industry.Name));
            AssertAreEqual(industry, _industriesQuery.GetIndustryByAnyName(industry.ShortName));
            AssertAreEqual(industry, _industriesQuery.GetIndustryByAnyName(industry.UrlName));
            foreach (var urlAlias in industry.UrlAliases)
                AssertAreEqual(industry, _industriesQuery.GetIndustryByAnyName(urlAlias));
        }

        private static void AssertAreEqual(Industry expectedIndustry, Industry industry)
        {
            Assert.AreEqual(expectedIndustry.Id, industry.Id);
            Assert.AreEqual(expectedIndustry.Name, industry.Name);
            Assert.AreEqual(expectedIndustry.ShortName, industry.ShortName);
            Assert.AreEqual(expectedIndustry.UrlName, industry.UrlName);
            Assert.AreEqual(expectedIndustry.KeywordExpression, industry.KeywordExpression);
            Assert.AreEqual(expectedIndustry.Aliases.Count, industry.Aliases.Count);
            for (var index = 0; index < industry.Aliases.Count; ++index)
                Assert.AreEqual(expectedIndustry.Aliases[index], industry.Aliases[index]);
            Assert.AreEqual(expectedIndustry.UrlAliases.Count, industry.UrlAliases.Count);
            for (var index = 0; index < industry.UrlAliases.Count; ++index)
                Assert.AreEqual(expectedIndustry.UrlAliases[index], industry.UrlAliases[index]);
        }
    }
}