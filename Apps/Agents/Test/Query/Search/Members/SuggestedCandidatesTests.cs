using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Test.Search;
using LinkMe.Framework.Text.Synonyms;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Search.Members.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Query.Search.Members
{
    [TestClass]
    public class SuggestedCandidatesTests
        : TestClass
    {
        private readonly ISuggestedMembersQuery _suggestedMembersQuery = Resolve<ISuggestedMembersQuery>();
        private readonly ISynonymsCommand _synonymsCommand = Resolve<ISynonymsCommand>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _synonymsCommand.CreateTestSynonyms();
        }

        [TestMethod]
        public void TestIndustries()
        {
            var jobAd = new JobAd {Description = new JobAdDescription {Industries = null}};
            Assert.IsNull(_suggestedMembersQuery.GetCriteria(jobAd).KeywordsExpression);

            jobAd = new JobAd { Description = new JobAdDescription { Industries = new Industry[0] } };
            Assert.IsNull(_suggestedMembersQuery.GetCriteria(jobAd).KeywordsExpression);

            jobAd = new JobAd { Description = new JobAdDescription { Industries = new[] { _industriesQuery.GetIndustry("Other") } } };
            Assert.IsNull(_suggestedMembersQuery.GetCriteria(jobAd).KeywordsExpression);

            jobAd = new JobAd
            {
                Description = new JobAdDescription
                {
                    Industries = new[] { _industriesQuery.GetIndustry("Government/Defence") }
                }
            };
            Assert.AreEqual("Government OR Defence", _suggestedMembersQuery.GetCriteria(jobAd).KeywordsExpression.GetUserExpression());

            jobAd = new JobAd
            {
                Description = new JobAdDescription
                {
                    Industries = new[] { _industriesQuery.GetIndustry("I.T. & T"), _industriesQuery.GetIndustry("Accounting") }
                }
            };
            Assert.AreEqual("(IT OR Telecommunications) Accounting", _suggestedMembersQuery.GetCriteria(jobAd).KeywordsExpression.GetUserExpression());
        }

        [TestMethod]
        public void TestTitle()
        {
            TestTitle("senior programmer recognised", "programmer", "senior OR recognised");
            TestTitle("senior something unrecognised", "senior something unrecognised", null);
            TestTitle("Something unrecognisable - these are keywords", "Something unrecognisable", "these OR keywords");
            TestTitle("Something un-reco(gnisable) - these are keywords", "Something un-reco gnisable", "these OR keywords");
            TestTitle("Tax Supervisor (Senior or Manager)", "Tax Supervisor", "Senior OR Manager");
            TestTitle("Some duplicated Duplicated words are DUPLICATED again", "Some duplicated words again", null);
        }

        private void TestTitle(string input, string expectedTitle, string expectedKeywords)
        {
            var jobAd = new JobAd { Title = input };
            var criteria = _suggestedMembersQuery.GetCriteria(jobAd);
            Assert.AreEqual(expectedTitle, criteria.JobTitle);
            Assert.AreEqual(expectedKeywords, criteria.KeywordsExpression == null ? null : criteria.KeywordsExpression.GetUserExpression());
        }
    }
}
