using System;
using System.Linq;
using System.Text;
using System.Web;
using LinkMe.Apps.Presentation.Domain.Search;
using LinkMe.Domain;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;

namespace LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search
{
    public static class JobAdSearchExtensions
    {
        private const string AllJobsText = "All jobs";

        private static readonly IIndustriesQuery IndustriesQuery = Container.Current.Resolve<IIndustriesQuery>();

        public static string GetDisplayText(this JobAdSearch search)
        {
            return search.Criteria == null ? string.Empty : search.Criteria.GetDisplayText();
        }

        public static string GetDisplayText(this JobAdSearchCriteria criteria)
        {
            var sb = new StringBuilder();
            sb.AppendText(criteria);
            return sb.Length == 0 ? AllJobsText : sb.ToString();
        }

        public static string GetDisplayHtml(this JobAdSearchCriteria criteria)
        {
            var sb = new StringBuilder();
            sb.AppendHtml(criteria);
            return sb.ToString();
        }

        public static string GetCriteriaHtml(this JobAdSearchCriteria criteria)
        {
            var sb = new StringBuilder();
            sb.Append(criteria.GetDisplayHtml());
            if (criteria.KeywordsExpression != null || criteria.AllKeywords != null || criteria.AnyKeywords != null ||
                criteria.ExactPhrase != null || criteria.AdTitleExpression != null || criteria.AdTitle != null ||
                    criteria.WithoutKeywords != null)
            {
                sb.Append("<span class=\"search-criterion-separator\">, </span>");
                sb.Append("<span class=\"synonyms-filter-holder\">");
                sb.Append(criteria.IncludeSynonyms
                    ? "<span class=\"synonyms-filter-text\">with synonyms</span><span class='mini-light-x_button synonyms js_synonyms-filter'></span>"
                    : "<span class=\"synonyms-filter-text\">without synonyms</span><span class='mini-light-x_button synonyms js_synonyms-filter'></span>");
                sb.Append("</span>");
            }
            return sb.ToString();
        }

        public static string GetItemsFoundHtml(this JobAdSearchResults results)
        {
            var sb = new StringBuilder();
            var matches = results.TotalMatches;
            sb.Append("<b>");
            sb.Append(matches);
            sb.Append(matches == 1 ? " job</b> was found" : " jobs</b> were found");

            return sb.ToString();
        }

        public static string GetCaptionDisplayHtml(this JobAdSearchResults results, JobAdSearchCriteria criteria, int startIndex, int numResults)
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

        private static void AppendText(this StringBuilder sb, JobAdSearchCriteria criteria)
        {
            // Title.

            if (criteria.AdTitle != null && criteria.AdTitleExpression != null)
            {
                var adTitle = criteria.AdTitleExpression.GetUserExpression();
                if (!string.IsNullOrEmpty(adTitle))
                    sb.Append(TextUtil.TrimEndBracketsFromExpression(adTitle));
            }

            // Keywords.

            if (criteria.KeywordsExpression != null)
            {
                var keywords = criteria.KeywordsExpression.GetUserExpression();
                if (!string.IsNullOrEmpty(keywords))
                {
                    if (sb.Length != 0)
                        sb.Append(", ");
                    sb.Append(TextUtil.TrimEndBracketsFromExpression(keywords));
                }
            }

            // Location.

            if (criteria.Location != null)
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(criteria.Location.IsCountry ? criteria.Location.Country.Name : criteria.Location.ToString());
            }

            // AdvertiserName.

            if (criteria.AdvertiserNameExpression != null)
            {
                var advertiserName = criteria.AdvertiserNameExpression.GetUserExpression();
                if (!string.IsNullOrEmpty(advertiserName))
                {
                    if (sb.Length != 0)
                        sb.Append(", ");
                    sb.Append(TextUtil.TrimEndBracketsFromExpression(advertiserName));
                }
            }

            // Salary.

            if (criteria.Salary != null)
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(criteria.Salary.GetDisplayText());
            }

            // Industries.

            if (criteria.IndustryIds != null && criteria.IndustryIds.Count > 0)
            {
                var industries = (from i in criteria.IndustryIds select IndustriesQuery.GetIndustry(i)).ToList();
                var industriesText = industries.GetCriteriaIndustriesDisplayText();
                if (!string.IsNullOrEmpty(industriesText))
                {
                    if (sb.Length != 0)
                        sb.Append(", ");
                    sb.Append(industriesText);
                }
            }

            // JobTypes.

            if (criteria.JobTypes != JobTypes.All)
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(criteria.JobTypes.GetDisplayText(" OR ", false, false));
            }
        }

        private static void AppendHtml(this StringBuilder sb, JobAdSearchCriteria criteria)
        {
            if (criteria.HasFilters)
            {
                sb.AppendAdTitleHtml(criteria);
                sb.AppendAdvertiserNameHtml(criteria);
                sb.AppendKeywordsHtml(criteria, criteria.GetKeywords());
                sb.AppendJobTypesHtml(criteria.JobTypes);
                sb.AppendLocationHtml(criteria);
                sb.AppendSalaryHtml(criteria);
                sb.AppendRecencyHtml(criteria);
                sb.AppendIndustriesHtml(criteria.IndustryIds);
            }
            else
            {
                sb.AppendHeadingHtml(AllJobsText);
            }
        }

        private static void AppendRecencyHtml(this StringBuilder sb, JobAdSearchCriteria criteria)
        {
            if (criteria.Recency == null) return;
            sb.AppendSeparatorHtml();
            sb.AppendStartPartHtml("Date posted: ", "recency");
            var r = (TimeSpan) criteria.Recency;
            sb.AppendCriterionDataHtml((r.Days == 60 ? "60+ days" : (r.Days == 30 ? "30 days" : r.GetRecencyDisplayText())) +  (r.Days > 2 ? " ago" : ""));
            sb.AppendEndPartHtml();
        }

        private static void AppendAdTitleHtml(this StringBuilder sb, JobAdSearchCriteria criteria)
        {
            if (criteria.AdTitleExpression == null)
                return;

            sb.AppendSeparatorHtml();

            // Show the original ad title, but indicate to the user if it was changed.

            sb.AppendStartPartHtml();
            sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.AdTitle));
            sb.AppendEndPartHtml();
        }

        private static void AppendKeywordsHtml(this StringBuilder sb, JobAdSearchCriteria criteria, string value)
        {
            if (string.IsNullOrEmpty(value) || criteria.KeywordsExpression == null)
                return;

            sb.AppendSeparatorHtml();
            sb.AppendStartPartHtml("Keywords: ", "keywords");
            sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(value).TagBooleanOperators());
            sb.AppendEndPartHtml();
        }

        private static void AppendLocationHtml(this StringBuilder sb, JobAdSearchCriteria criteria)
        {
            if (criteria.Location == null)
                return;

            if (criteria.Location.Locality != null)
            {
                sb.AppendSeparatorHtml();
                sb.AppendStartPartHtml("within " + criteria.EffectiveDistance + "km of ", "location");
                sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.Location.ToString()));
                sb.AppendEndPartHtml();
            }
            else if (criteria.Location.Region != null)
            {
                sb.AppendSeparatorHtml();
                if (criteria.EffectiveDistance == 0)
                {
                    sb.AppendStartPartHtml("in ", "location");
                    // A little extra feedback to differentiate between a suburb (eg. "Melbourne VIC 3000") and
                    // a region (eg. "Melbourne" for Greater Melbourne).
                    sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.Location + " region"));
                } else
                {
                    sb.AppendStartPartHtml("within " + criteria.EffectiveDistance + "km of ", "location");
                    // A little extra feedback to differentiate between a suburb (eg. "Melbourne VIC 3000") and
                    // a region (eg. "Melbourne" for Greater Melbourne).
                    sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.Location + " region"));
                }

                sb.AppendEndPartHtml();
            }
            else if (criteria.Location.CountrySubdivision != null)
            {
                if (criteria.Location.IsCountry)
                {
                    sb.AppendSeparatorHtml();
                    sb.AppendStartPartHtml("in ", "location");
                    sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.Location.Country.Name));
                    sb.AppendEndPartHtml();
                }
                else
                {
                    sb.AppendSeparatorHtml();
                    sb.AppendStartPartHtml("in ", "location");
                    sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.Location.ToString()));
                    sb.AppendEndPartHtml();
                }
            }
        }

        private static void AppendAdvertiserNameHtml(this StringBuilder sb, JobAdSearchCriteria criteria)
        {
            if (criteria.AdvertiserNameExpression == null)
                return;

            sb.AppendSeparatorHtml();
            sb.AppendStartPartHtml("advertiser: ", "advertiser");
            sb.AppendCriterionDataHtml(HttpUtility.HtmlEncode(criteria.AdvertiserName));
            sb.AppendEndPartHtml();
        }

        private static void AppendSalaryHtml(this StringBuilder sb, JobAdSearchCriteria criteria)
        {
            if (criteria.Salary == null)
                return;

            sb.AppendSeparatorHtml();
            if (criteria.Salary.Rate.Equals(SalaryRate.Hour))
            {
                sb.AppendStartPartHtml("with hourly salary: ", "salary");
                sb.AppendCriterionDataHtml(criteria.Salary.ToRate(SalaryRate.Hour).GetDisplayText());
            }
            else
            {
                sb.AppendStartPartHtml("with salary: ", "salary");
                sb.AppendCriterionDataHtml(criteria.Salary.GetDisplayText());
            }
            sb.AppendEndPartHtml();
        }
    }
}
