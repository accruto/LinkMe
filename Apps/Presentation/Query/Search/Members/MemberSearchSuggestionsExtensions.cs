using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Presentation.Query.Search.Members
{
    public static class MemberSearchSuggestionsExtensions
    {
        public static string GetDisplayHtml(this SpellingSuggestion suggestion)
        {
            //Query must have either key words or job title or name
            //There shouldn't ever be spelling suggestions for names, but handle it anyway
            var keywords = string.Empty;

            if (suggestion.Criteria.KeywordsExpression != null)
                keywords = suggestion.Criteria.KeywordsExpression.GetUserExpression();
            else if (suggestion.Criteria.JobTitleExpression != null)
                keywords = suggestion.Criteria.JobTitleExpression.GetUserExpression();
            else if (!string.IsNullOrEmpty(suggestion.Criteria.Name))
                keywords = suggestion.Criteria.Name;

            foreach (var correction in suggestion.Corrections)
                keywords = keywords.Replace(correction.Value, "<b><i>" + correction.Value + "</i></b>");
            return keywords;
        }
    }
}
