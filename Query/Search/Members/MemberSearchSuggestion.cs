using System.Collections.Generic;

namespace LinkMe.Query.Search.Members
{
    public abstract class MemberSearchSuggestion
    {
        public MemberSearchCriteria Criteria { get; set; }
    }

    public class SpellingSuggestion
        : MemberSearchSuggestion
    {
        public IList<KeyValuePair<string, string>> Corrections;
    }

    public class KeywordsSuggestion
        : MemberSearchSuggestion
    {
    }

    public class OrKeywordsSuggestion
        : MemberSearchSuggestion
    {
    }

    public class JobTitleAsKeywordsSuggestion
        : MemberSearchSuggestion
    {
    }

    public class AllJobsSuggestion
        : MemberSearchSuggestion
    {
    }
}
