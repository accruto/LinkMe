using System.Collections.Generic;

namespace LinkMe.Query.Search.Members.Queries
{
    public interface IMemberSearchSuggestionsQuery
    {
        IList<SpellingSuggestion> GetSpellingSuggestions(MemberSearchCriteria criteria);
        IList<MemberSearchSuggestion> GetMoreResultsSuggestions(MemberSearchCriteria criteria);
        IList<MemberSearchSuggestion> GetLessResultsSuggestions(MemberSearchCriteria criteria);
    }
}