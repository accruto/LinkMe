using System;
using System.Linq;
using System.Text;
using System.Web;
using LinkMe.Apps.Presentation.Domain.Search;
using LinkMe.Domain;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Presentation.Domain.Users.Members.Search
{
    public static class MemberSearchExtensions
    {
        private const CandidateStatusFlags DefaultCandidateFlags =
            CandidateStatusFlags.AvailableNow | CandidateStatusFlags.ActivelyLooking | CandidateStatusFlags.OpenToOffers | CandidateStatusFlags.Unspecified;

        private static readonly ILocationQuery _locationQuery = Container.Current.Resolve<ILocationQuery>();
        private static readonly ICommunitiesQuery _communitiesQuery = Container.Current.Resolve<ICommunitiesQuery>();

        public static string GetDisplayHtml(this MemberSearchCriteria criteria)
        {
            // Include everything by default.

            return GetDisplayHtml(criteria, true);
        }

        public static string GetDisplayHtml(this MemberSearchCriteria criteria, bool includeTitleAndKeywords)
        {
            var sb = new StringBuilder();
            sb.AppendHtml(criteria, includeTitleAndKeywords);
            return sb.ToString();
        }

        public static string GetSavedSearchDisplayText(this MemberSearchCriteria criteria)
        {
            var sb = new StringBuilder();
            sb.AppendSavedSearchText(criteria);
            return sb.ToString();
        }

        public static string GetItemsFoundHtml(this MemberSearchResults results)
        {
            var sb = new StringBuilder();
            var matches = results.TotalMatches;

            if (matches == 1)
                sb.Append("<b>1 candidate</b> was found");
            else
                sb.Append("<b>").Append(matches).Append(" candidates</b> were found");

            return sb.ToString();
        }

        public static string GetDisplayHtml(this MemberSearch search)
        {
            // Use the name if set.

            if (!string.IsNullOrEmpty(search.Name))
                return search.Name;

            return search.Criteria == null ? null : search.Criteria.GetDisplayHtml();
        }

        public static string GetCaptionDisplayHtml(this MemberSearchResults results, MemberSearchCriteria criteria, int startIndex, int numResults)
        {
            var matches = results.TotalMatches;
            var sb = new StringBuilder();

            if (matches == 0)
                return "No results found";
            
            sb.AppendFormat("Results {0}-{1}<small> of {2}</small>", (startIndex + 1), Math.Min(startIndex + numResults, matches), matches);

            if (criteria != null)
                sb.Append(" matching " + criteria.GetDisplayHtml());

            return sb.ToString();
        }

        private static void AppendSavedSearchText(this StringBuilder sb, MemberSearchCriteria criteria)
        {
            // Keywords.

            if (criteria.KeywordsExpression != null)
            {
                var keywordExpression = criteria.KeywordsExpression.GetUserExpression();
                if (!string.IsNullOrEmpty(keywordExpression))
                {
                    if (sb.Length != 0)
                        sb.Append(", ");
                    if (keywordExpression.IndexOf("\"") != -1)
                    {
                        keywordExpression = keywordExpression.Replace("\"", "");
                    }
                    sb.Append(TextUtil.TrimEndBracketsFromExpression(keywordExpression));
                }
            }

            // Location.

            if (criteria.Location != null && !criteria.Location.IsCountry)
            {
                var locationExpression = criteria.Location.ToString();
                if (sb.Length != 0)
                    sb.Append(", ");
                if (criteria.Location.ToString().IndexOf("\"") != -1)
                {
                    locationExpression = criteria.Location.ToString().Replace("\"", "");
                }
                sb.Append(locationExpression);
            }
        }

        private static void AppendHtml(this StringBuilder sb, MemberSearchCriteria criteria, bool includeTitleAndKeywords)
        {
            //Suppress JobTitle and Keywords for suggestedCandidates since these can't be changed and are part of the "magic"

            // search by Name
            if (criteria.Name != null)
            {
                sb.AppendStartPartHtml("Candidate name matches ", "candidate_name");
                sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.Name));
                if (criteria.IncludeSimilarNames) sb.AppendCriterionDataHtml(" (include close matches)");
                sb.AppendEndPartHtml();
            }

            // Job title.

            if (criteria.JobTitleExpression != null && includeTitleAndKeywords)
            {
                // Show the original job title, but indicate to the user if it was changed.

                sb.AppendStartPartHtml("Job title contains ", "job-title");
                sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.JobTitle));
                sb.AppendEndPartHtml();
            }

            // Company keywords.

            if (criteria.CompanyKeywordsExpression != null)
            {
                sb.AppendStartPartHtml("Employer contains ", "employer");
                sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.CompanyKeywords));
                sb.AppendEndPartHtml();
            }

            // Education keywords.

            if (criteria.EducationKeywordsExpression != null)
            {
                sb.AppendStartPartHtml("Education contains ", "education");
                sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.EducationKeywords));
                sb.AppendEndPartHtml();
            }

            // Desired job title.

            if (criteria.DesiredJobTitleExpression != null)
            {
                sb.AppendSeparatorHtml();
                sb.AppendStartPartHtml("Desired job title contains ", "desired-job-title");
                sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.DesiredJobTitle));
                sb.AppendEndPartHtml();
            }

            // The rest.

            if (includeTitleAndKeywords)
                sb.AppendKeywordsHtml(criteria.GetKeywords());

            sb.AppendLocationHtml(criteria, criteria.IncludeRelocating);
            sb.AppendSalaryHtml(criteria);
            sb.AppendRecencyHtml(criteria);
            sb.AppendIndustriesHtml(criteria.IndustryIds);
            sb.AppendJobTypesHtml(criteria.JobTypes);
            sb.AppendCandidateFlagsHtml(criteria);
            sb.AppendVisaStatusHtml(criteria);
            sb.AppendEthnicStatusHtml(criteria);
            sb.AppendCommunityHtml(criteria);
        }

        private static void AppendLocationHtml(this StringBuilder sb, MemberSearchCriteria criteria, bool includeRelocating)
        {
            if (criteria.Location == null)
                return;

            // Don't include the location if it's the default country (ie. the user didn't really enter anything).

            if (criteria.Location.IsCountry)
            {
                var defaultCountry = _locationQuery.GetCountry("Australia");
                if (defaultCountry != null && defaultCountry.Id == criteria.Location.Country.Id)
                    return;
            }

            sb.AppendSeparatorHtml();
            sb.AppendStartPartHtml("Location: ", "location");

            if (criteria.Location.Locality != null)
            {
                sb.AppendFormat("within {0} km of ", criteria.EffectiveDistance);
                sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.Location.ToString()));
            }
            else if (criteria.Location.Region != null)
            {
                // Why " region"?
                // A little extra feedback to differentiate between a suburb (eg. "Melbourne VIC 3000") and
                // a region (eg. "Melbourne" for Greater Melbourne).
                if (criteria.EffectiveDistance == 0)
                {
                    sb.AppendFormat("in ");
                }
                else
                {
                    sb.AppendFormat("within {0} km of ", criteria.EffectiveDistance);
                }
                sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.Location + " region"));
            }
            else if (criteria.Location.CountrySubdivision != null)
            {
                sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.Location.IsCountry ? criteria.Location.Country.Name : criteria.Location.ToString()));
            }

            if (includeRelocating)
            {
                sb.Append(" ");
                var locationNote = criteria.IncludeInternational
                    ? "(or willing to relocate there, include international candidates)"
                    : "(or willing to relocate there)";
                sb.AppendNoteHtml(locationNote);
            }

            sb.AppendEndPartHtml();
        }

        private static void AppendSalaryHtml(this StringBuilder sb, MemberSearchCriteria criteria)
        {
            if (criteria.Salary != null)
            {
                sb.AppendSeparatorHtml();
                sb.AppendStartPartHtml("Salary: ", "salary");
                var excludeNoSalary = criteria.ExcludeNoSalary ? " (exclude candidates without a desired salary)" : "";
                sb.AppendCriterionDataHtml(criteria.Salary.GetDisplayText() + excludeNoSalary);
                sb.AppendEndPartHtml();
            }
            else if (criteria.ExcludeNoSalary)
            {
                sb.AppendSeparatorHtml();
                sb.AppendStartPartHtml("Salary: ", "salary");
                sb.AppendCriterionDataHtml("Any salary (exclude candidates without a desired salary)");
                sb.AppendEndPartHtml();
            }
        }

        private static void AppendRecencyHtml(this StringBuilder sb, MemberSearchCriteria criteria)
        {
            if (criteria.Recency != null)
            {
                sb.AppendSeparatorHtml();
                sb.AppendStartPartHtml("Recency: ", "recency");
                sb.AppendCriterionDataHtml(criteria.Recency.Value.GetRecencyDisplayText());
                sb.AppendEndPartHtml();
            }
        }

        private static void AppendKeywordsHtml(this StringBuilder sb, string keywords)
        {
            if (string.IsNullOrEmpty(keywords))
                return;

            sb.AppendSeparatorHtml();
            sb.AppendStartPartHtml("Keywords: ", "keywords");
            sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(keywords).TagBooleanOperators());
            sb.AppendEndPartHtml();
        }

        private static void AppendCommunityHtml(this StringBuilder sb, MemberSearchCriteria criteria)
        {
            if (criteria.CommunityId == null)
                return;

            var community = _communitiesQuery.GetCommunity(criteria.CommunityId.Value);
            if (community != null)
            {
                sb.AppendSeparatorHtml();
                sb.AppendStartPartHtml("Community: ", "community");
                sb.AppendCriterionDataHtml(community.Name);
                sb.AppendEndPartHtml();
            }
        }

        private static void AppendCandidateFlagsHtml(this StringBuilder sb, MemberSearchCriteria criteria)
        {
            if (criteria.CandidateStatusFlags == null || criteria.CandidateStatusFlags == DefaultCandidateFlags)
                return;

            sb.AppendSeparatorHtml();
            sb.AppendStartPartHtml();
            sb.AppendCriterionDataHtml(string.Join(", ", criteria.CandidateStatusFlags.Value.GetDisplayTexts(CandidateStatusDisplay.Values, CandidateStatusDisplay.GetDisplayText).ToArray()));
            sb.AppendEndPartHtml();
        }

        private static void AppendEthnicStatusHtml(this StringBuilder sb, MemberSearchCriteria criteria)
        {
            if (!criteria.EthnicStatus.HasValue)
                return;

            sb.AppendSeparatorHtml();
            sb.AppendStartPartHtml();
            sb.AppendCriterionDataHtml(string.Join(", ", criteria.EthnicStatus.Value.GetDisplayTexts(EthnicStatusDisplay.Values, EthnicStatusDisplay.GetDisplayText).ToArray()));
            sb.AppendEndPartHtml();
        }

        private static void AppendVisaStatusHtml(this StringBuilder sb, MemberSearchCriteria criteria)
        {
            if (!criteria.VisaStatusFlags.HasValue)
                return;

            sb.AppendSeparatorHtml();
            sb.AppendStartPartHtml();
            sb.AppendCriterionDataHtml(string.Join(", ", criteria.VisaStatusFlags.Value.GetDisplayTexts(VisaStatusFlagsDisplay.Values, VisaStatusFlagsDisplay.GetDisplayText).ToArray()));
            sb.AppendEndPartHtml();
        }
    }
}