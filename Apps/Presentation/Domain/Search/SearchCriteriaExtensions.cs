using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Domain;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Presentation.Domain.Search
{
    public static class SearchCriteriaExtensions
    {
        private const string PartSeparator = "<span class=\"search-criterion-separator\">, </span>";
        private const string PartPrefixWithoutHeading = "<span class=\"search-criterion\">";
        private const string PartPrefixWithHeading = "<span class=\"{0}_search-criterion search-criterion\">";
        private const string PartSuffix = "</span>";
        private const string HeadingPrefix = "<span class=\"search-criterion-name\">";
        private const string HeadingSuffix = "</span>";
        private const string NotePrefix = "<span class=\"search-criterion-note\">";
        private const string NoteSuffix = "</span>";
        private const string CriterionDataPrefix = "<span class=\"search-criterion-data\">";
        private const string CriterionDataSuffix = "</span>";

        public const string OrHtml = "<span class=\"or_boolean-operator boolean-operator\">OR</span>";
        public const string AndHtml = "<span class=\"and_boolean-operator boolean-operator\">AND</span>";
        public const string NotHtml = "<span class=\"not_boolean-operator boolean-operator\">NOT</span>";

        private static readonly IIndustriesQuery IndustriesQuery = Container.Current.Resolve<IIndustriesQuery>();


        // MF: Should we remove dependency of Utility package upon Presentation package?
        //     If we did so, this function could be moved to Utility.Utilities.StringUtils
        //     without creating a circular dependency... possibly a very large refactor...
        //
        private static string StripToAlphaNumeric(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var sb = new StringBuilder(text);

            int i = 0;
            while (i < sb.Length)
            {
                char c = sb[i];

                if (char.IsLetterOrDigit(c))
                    i++;
                else
                    sb.Remove(i, 1);
            }

            return (sb.Length == text.Length ? text : sb.ToString());
        }



        public static string TagBooleanOperators(this string keywords)
        {
            return keywords
                .Replace(" OR ", " " + OrHtml + " ")
                .Replace(" AND ", " " + AndHtml + " ")
                .Replace(" NOT ", " " + NotHtml + " ");
        }



        public static void AppendSeparatorHtml(this StringBuilder sb)
        {
            if (sb.Length > 0)
                sb.Append(PartSeparator);
        }

        public static void AppendStartPartHtml(this StringBuilder sb)
        {
            sb.Append(PartPrefixWithoutHeading);
        }

        public static void AppendStartPartHtml(this StringBuilder sb, string heading, string criteriaName)
        {
            if (!string.IsNullOrEmpty(heading))
            {
                // Capitalise the first letter of the heading
                heading = char.ToUpper(heading[0]) + heading.Substring(1, heading.Length - 1);
                sb.Append(string.Format(PartPrefixWithHeading, StripToAlphaNumeric(criteriaName.ToLower())));
                sb.AppendHeadingHtml(heading);
            }
            else
            {
                AppendStartPartHtml(sb);
            }
        }

        public static void AppendEndPartHtml(this StringBuilder sb)
        {
            sb.Append(PartSuffix);
        }

        public static void AppendHeadingHtml(this StringBuilder sb, string text)
        {
            sb.Append(HeadingPrefix);
            sb.Append(text);
            sb.Append(HeadingSuffix);
        }

        public static void AppendNoteHtml(this StringBuilder sb, string text)
        {
            sb.Append(NotePrefix);
            sb.Append(text);
            sb.Append(NoteSuffix);
        }

        public static void AppendCriterionDataHtml(this StringBuilder sb, string text)
        {
            sb.Append(CriterionDataPrefix);
            sb.Append(text);
            sb.Append(CriterionDataSuffix);
        }

        public static void AppendJobTypesHtml(this StringBuilder sb, JobTypes jobTypes)
        {
            if (jobTypes == JobTypes.All)
                return;

            sb.AppendSeparatorHtml();
            sb.AppendStartPartHtml();
            sb.AppendCriterionDataHtml(jobTypes.GetDisplayText(", ", false, false));
            sb.AppendEndPartHtml();
        }

        public static void AppendIndustriesHtml(this StringBuilder sb, IList<Guid> industryIds)
        {
            if (industryIds == null || industryIds.Count == 0)
                return;

            sb.AppendSeparatorHtml();
            sb.AppendStartPartHtml((industryIds.Count == 1 ? "in industry: " : "in industries: "), "industry");
            var industryNames = IndustriesQuery.GetIndustries(industryIds).Select(i => i.Name).ToArray();
            if (industryNames.Length == IndustriesQuery.GetIndustries().ToArray().Length) sb.AppendCriterionDataHtml("All");
            else sb.AppendCriterionDataHtml(string.Join(", ", industryNames));
            sb.AppendEndPartHtml();
        }
    }
}