using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Web
{
    [TestClass]
    public class KeywordsTests
        : CriteriaTests
    {
        private const string Keywords = "architect";
        private const string AllKeywords = "one two";
        private const string ExactPhrase = "three four";
        private const string AnyKeywords = "five six seven";
        private const string WithoutKeywords = "eight none";
        private const string QuotedKeywords = "\"compliance officer\" OR \"compliance manager\"";
        private const string PartlyQuotedKeywords = "Records \"Information Manager\"";
        private const string SimplyQuotedKeywords = "\"Records\"";

        protected override void TestDisplay()
        {
            var criteria = new JobAdSearchCriteria();

            criteria.SetKeywords(AllKeywords, null, null, null);
            TestDisplay(false, criteria);

            criteria.SetKeywords(null, ExactPhrase, null, null);
            TestDisplay(false, criteria);

            criteria.SetKeywords(null, null, AnyKeywords, null);
            TestDisplay(false, criteria);

//            criteria.SetKeywords(AllKeywords, null, null, WithoutKeywords);
//            TestDisplay(false, criteria);

            criteria.SetKeywords(Keywords);
            TestDisplay(false, criteria);

            criteria.SetKeywords(QuotedKeywords);
            TestDisplay(false, criteria);

            criteria.SetKeywords(PartlyQuotedKeywords);
            TestDisplay(false, criteria);

//            criteria.SetKeywords(SimplyQuotedKeywords);
//            TestDisplay(false, criteria);
        }
    }
}