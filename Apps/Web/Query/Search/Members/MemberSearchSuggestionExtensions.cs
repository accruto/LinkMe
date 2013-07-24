using LinkMe.Apps.Presentation.Domain.Search;
using LinkMe.Query.Search.Members;
using LinkMe.Web.Areas.Employers.Models.Search;

namespace LinkMe.Web.Query.Search.Members
{
    public static class MemberSearchSuggestionExtensions
    {
        public static string GetDisplayHtml(this MemberSearchSuggestion suggestion)
        {
            if (suggestion is KeywordsSuggestion)
                return "Try different <a href=\"javascript:void(0);\" class=\"js_modify-search\">keywords</a>";
            if (suggestion is OrKeywordsSuggestion)
                return "Find candidates with at least one of <a href=\"" + suggestion.Criteria.GetSearchUrl() + "\">" + suggestion.Criteria.GetKeywords() + "</a>";
            if (suggestion is JobTitleAsKeywordsSuggestion)
                return "Search for <a href=\"" + suggestion.Criteria.GetSearchUrl() + "\">" + suggestion.Criteria.JobTitle.GetCriteriaJobTitleDisplayText() + "</a> as keywords in the whole resume, not just the last 3 job titles";
            if (suggestion is AllJobsSuggestion)
                return "Match <a href=\"" + suggestion.Criteria.GetSearchUrl() + "\">" + suggestion.Criteria.JobTitle.GetCriteriaJobTitleDisplayText() + "</a> in any job title, not just the last 3 job titles";
            return null;
        }
    }
}
