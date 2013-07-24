using System;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Query.Search.JobAds;

namespace LinkMe.Apps.Asp.Mvc.Models.Converters
{
    public class JobAdSearchCriteriaAdSenseConverter
        : Converter<JobAdSearchCriteria>
    {
        public override void Convert(JobAdSearchCriteria criteria, ISetValues values)
        {
            if (criteria == null)
                return;

            if (ConvertKeywords(criteria, values))
                ConvertLocation(criteria, values);
        }

        public override JobAdSearchCriteria Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            throw new NotImplementedException();
        }

        private static void ConvertLocation(JobAdSearchCriteria criteria, ISetValues values)
        {
            if (criteria.Location == null)
                return;

            if (criteria.Location.IsCountry)
            {
                values.SetValue(JobAdSearchCriteriaKeys.CountryId, "in " + criteria.Location.Country);
            }
            else
            {
                var location = criteria.Location.ToString();
                if (!string.IsNullOrEmpty(location))
                    values.SetValue(JobAdSearchCriteriaKeys.Location, "in " + location + " " + criteria.Location.Country);
            }
        }

        private static bool ConvertKeywords(JobAdSearchCriteria criteria, ISetValues values)
        {
            var keywords = criteria.GetKeywords();
            if (string.IsNullOrEmpty(keywords))
                return false;

            // Convert NOT into google '-'.

            keywords = keywords.ToLower();
            if (keywords.StartsWith("not "))
                keywords = "-" + keywords.Substring("not ".Length);
            keywords = keywords.Replace(" and not ", " -");
            keywords = keywords.Replace(" not ", " -");

            // Add jobs onto the end.

            keywords = keywords + " jobs";

            values.SetValue(JobAdSearchCriteriaKeys.Keywords, keywords);
            return true;
        }
    }
}
