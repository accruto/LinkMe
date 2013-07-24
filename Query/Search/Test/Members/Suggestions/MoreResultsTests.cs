using System.Collections.Generic;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Suggestions
{
    [TestClass]
    public class MoreResultsTests
        : TestClass
    {
        private readonly IMemberSearchSuggestionsQuery _memberSearchSuggestionsQuery = Resolve<IMemberSearchSuggestionsQuery>();

        [TestMethod]
        public void TestKeywords()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("simple");
            AssertSuggestions(_memberSearchSuggestionsQuery.GetMoreResultsSuggestions(criteria), new KeywordsSuggestion());

            criteria.SetKeywords("software tester");
            var suggestedCriteria = new MemberSearchCriteria();
            suggestedCriteria.SetKeywords("software OR tester");
            AssertSuggestions(_memberSearchSuggestionsQuery.GetMoreResultsSuggestions(criteria), new KeywordsSuggestion(), new OrKeywordsSuggestion { Criteria = suggestedCriteria });
        }

        [TestMethod]
        public void TestJobTitle()
        {
            const string jobTitle = "archeologist";

            // No keywords.

            var criteria = new MemberSearchCriteria { JobTitle = jobTitle, JobTitlesToSearch = JobsToSearch.AllJobs };
            var suggestedCriteria = new MemberSearchCriteria();
            suggestedCriteria.SetKeywords(jobTitle);
            AssertSuggestions(_memberSearchSuggestionsQuery.GetMoreResultsSuggestions(criteria), new JobTitleAsKeywordsSuggestion { Criteria = suggestedCriteria });

            // Not all jobs.

            criteria.JobTitlesToSearch = JobsToSearch.RecentJobs;
            var suggestedCriteria2 = new MemberSearchCriteria { JobTitle = jobTitle, JobTitlesToSearch = JobsToSearch.AllJobs };
            AssertSuggestions(_memberSearchSuggestionsQuery.GetMoreResultsSuggestions(criteria),
                new JobTitleAsKeywordsSuggestion { Criteria = suggestedCriteria },
                new AllJobsSuggestion { Criteria = suggestedCriteria2 });

            // Already has keywords.

            criteria.SetKeywords("simple");
            AssertSuggestions(_memberSearchSuggestionsQuery.GetMoreResultsSuggestions(criteria),
                new AllJobsSuggestion { Criteria = suggestedCriteria2 });
        }

        private static void AssertSuggestions(IList<MemberSearchSuggestion> suggestions, params MemberSearchSuggestion[] expectedSuggestions)
        {
            Assert.AreEqual(expectedSuggestions.Length, suggestions.Count);
            for (var index = 0; index < expectedSuggestions.Length; ++index)
                AssertSuggestion(expectedSuggestions[index], suggestions[index]);
        }

        private static void AssertSuggestion(MemberSearchSuggestion expectedSuggestion, MemberSearchSuggestion suggestion)
        {
            Assert.IsInstanceOfType(suggestion, expectedSuggestion.GetType());
        }
    }
}
