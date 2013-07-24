using System;
using System.Collections.Generic;
using LinkMe.Apps.Presentation;
using LinkMe.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Web.Models
{
    public static class UrlSegmentsExtensions
    {
        public static string GetTitleUrlSegment(this string title, string suffix)
        {
            if (string.IsNullOrEmpty(title))
                return "-";
            title = title.EncodeUrlSegment();
            return string.IsNullOrEmpty(title) ? "-" : title + GetSuffix(suffix);
        }

        public static string GetUrlSegment(this IList<Industry> industries, string suffix)
        {
            if (industries == null)
                return "-";

            // If there is only one industry then use it.  Do not concatenate more as this can easily lead to
            // urls being longer then url or segment lengths.

            return industries.Count == 1 ? industries[0].GetUrlSegment(suffix) : "-";
        }

        public static string GetUrlSegment(this Industry industry, string suffix)
        {
            return industry.UrlName + GetSuffix(suffix);
        }

        public static Industry GetIndustryByUrlSegment(this IIndustriesQuery industriesQuery, string segment, string suffix)
        {
            var industryUrlName = GetUrlName(segment, suffix);
            return industryUrlName != null
                ? industriesQuery.GetIndustryByUrlName(industryUrlName)
                : null;
        }

        public static string GetUrlSegment(this LocationReference locationReference, string suffix)
        {
            if (locationReference == null)
                return "-";

            var location = locationReference.IsCountry
                ? locationReference.Country.ToString()
                : locationReference.ToString();
            location = location.EncodeUrlSegment();
            return string.IsNullOrEmpty(location) ? "-" : location + GetSuffix(suffix);
        }

        public static string GetUrlSegment(this IUrlNamedLocation location, string suffix)
        {
            return location.UrlName + GetSuffix(suffix);
        }

        public static IUrlNamedLocation GetLocationByUrlSegment(this ILocationQuery locationQuery, Country country, string segment, string suffix)
        {
            var locationUrlName = GetUrlName(segment, suffix);
            return locationUrlName != null
                ? locationQuery.ResolveUrlNamedLocation(country, locationUrlName)
                : null;
        }

        public static string GetUrlSegment(this Salary salary, string suffix)
        {
            if (salary == null)
                return "-";
            var segment = GetUrlSegment(salary);
            return string.IsNullOrEmpty(segment) ? "-" : segment + GetSuffix(suffix);
        }

        public static Salary GetSalaryByUrlSegment(this string segment, string suffix)
        {
            if (string.IsNullOrEmpty(segment))
                return null;

            try
            {
                decimal? lowerBound = null;
                decimal? upperBound = null;

                segment = GetUrlName(segment, suffix);
                if (segment.StartsWith("up-to-", StringComparison.InvariantCultureIgnoreCase))
                    upperBound = ParseSalary(segment.Replace("up-to-", string.Empty));
                else if (segment.EndsWith("-and-above", StringComparison.InvariantCultureIgnoreCase))
                    lowerBound = ParseSalary(segment.Replace("-and-above", string.Empty));
                else
                {
                    var bands = segment.Split('-');
                    if (bands.Length == 2)
                    {
                        lowerBound = ParseSalary(bands[0]);
                        upperBound = ParseSalary(bands[1]);
                    }
                }

                return new Salary { LowerBound = lowerBound, UpperBound = upperBound };
            }
            catch (Exception)
            {
            }

            return null;
        }

        private static string GetSuffix(string suffix)
        {
            return string.IsNullOrEmpty(suffix) ? "" : "-" + suffix;
        }

        private static string GetUrlSegment(Salary salary)
        {
            if (salary == null)
                return string.Empty;

            if (!salary.HasLowerBound && !salary.HasUpperBound)
                return string.Empty;

            salary = salary.ToRate(SalaryRate.Year);
            if (!salary.LowerBound.HasValue || salary.LowerBound.Value == 0)
                return "up-to-" + GetUrlSegment(salary.UpperBound.Value);

            if (!salary.UpperBound.HasValue || salary.UpperBound.Value == 0)
                return GetUrlSegment(salary.LowerBound.Value) + "-and-above";

            return GetUrlSegment(salary.LowerBound.Value) + "-" + GetUrlSegment(salary.UpperBound.Value);
        }

        private static decimal ParseSalary(string value)
        {
            value = value.Replace("k", string.Empty);
            return decimal.Parse(value) * 1000;
        }

        private static string GetUrlSegment(decimal value)
        {
            return (int)(value / 1000) + "k";
        }

        private static string GetUrlName(string segment, string suffix)
        {
            if (string.IsNullOrEmpty(segment))
                return null;

            // Remove any suffix.

            return segment.EndsWith("-" + suffix)
                ? segment.Substring(0, segment.Length - suffix.Length - 1)
                : segment;
        }
    }
}
