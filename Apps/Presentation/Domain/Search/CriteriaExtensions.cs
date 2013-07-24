using System;
using System.Collections.Generic;
using System.Text;
using LinkMe.Domain.Industries;
using LinkMe.Framework.Utility.Expressions;

namespace LinkMe.Apps.Presentation.Domain.Search
{
    public static class CriteriaExtensions
    {
        public static string GetCriteriaIndustriesDisplayText(this IList<Industry> industries)
        {
            var sb = new StringBuilder();

            if (industries != null && industries.Count > 0)
            {
                foreach (var industry in industries)
                {
                    if (sb.Length != 0)
                        sb.Append(" OR ");
                    sb.Append(industry.Name);
                }
            }

            return sb.ToString();
        }

        public static string GetCriteriaJobTitleDisplayText(this string jobTitle)
        {
            return jobTitle == null ? null : jobTitle.TrimStart(Expression.Prefixes);
        }

        public static string GetRecencyDisplayText(this TimeSpan ts)
        {
            switch (ts.Days)
            {
                case 1:
                    return "Today";
                case 2:
                    return "Yesterday";
                case 3:
                    return "2 days";
                case 4:
                    return "3 days";
                case 7:
                    return "1 week";
                case 14:
                    return "2 weeks";
                case 30:
                    return "1 month";
                case 61:
                    return "2 months";
                case 91:
                    return "3 months";
                case 183:
                    return "6 months";
                case 365:
                    return "1 year";
                case 548:
                    return "18 months";
                case 731:
                    return "2+ years";
                default:
                    return ts.Days + " days";
            }
        }
    }
}