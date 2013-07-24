using System;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Search.JobAds;

namespace LinkMe.Apps.Presentation.Converters
{
    public class JobAdSearchCriteriaConverter
        : Converter<JobAdSearchCriteria>
    {
        private readonly ILocationQuery _locationQuery;
        private readonly IIndustriesQuery _industriesQuery;

        public JobAdSearchCriteriaConverter(ILocationQuery locationQuery, IIndustriesQuery industriesQuery)
        {
            _locationQuery = locationQuery;
            _industriesQuery = industriesQuery;
        }

        public override void Convert(JobAdSearchCriteria criteria, ISetValues values)
        {
            if (criteria == null)
                return;

            ConvertKeywords(criteria, values);
            ConvertLocation(criteria, values);
            ConvertIncludeSynonyms(criteria, values);

            if (!string.IsNullOrEmpty(criteria.AdTitle))
                values.SetValue(JobAdSearchCriteriaKeys.AdTitle, criteria.AdTitle);

            if (!string.IsNullOrEmpty(criteria.AdvertiserName))
                values.SetValue(JobAdSearchCriteriaKeys.AdvertiserName, criteria.AdvertiserName);

            if (criteria.JobTypes != JobAdSearchCriteria.DefaultJobTypes)
                values.SetFlagsValue(criteria.JobTypes);
            
            ConvertIndustries(criteria, values);
            ConvertSalary(criteria, values);
            ConvertRecency(criteria, values);
            ConvertActivity(criteria, values);
            ConvertCommunity(criteria, values);
            ConvertSortOrder(criteria, values);
        }

        public override JobAdSearchCriteria Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            var criteria = new JobAdSearchCriteria();

            DeconvertKeywords(criteria, values, errors);
            DeconvertLocation(criteria, values);

            DeconvertIncludeSynonyms(criteria, values);
            DeconvertSortOrder(criteria, values, errors);

            criteria.AdTitle = values.GetStringValue(JobAdSearchCriteriaKeys.AdTitle);
            criteria.AdvertiserName = values.GetStringValue(JobAdSearchCriteriaKeys.AdvertiserName);

            criteria.IndustryIds = values.GetGuidArrayValue(JobAdSearchCriteriaKeys.IndustryIds);
            if (criteria.IndustryIds.IsNullOrEmpty())
                criteria.IndustryIds = values.GetGuidArrayValue(JobAdSearchCriteriaKeys.Industries);

            DeconvertJobTypes(criteria, values);
            DeconvertSalary(criteria, values);
            DeconvertRecency(criteria, values);
            DeconvertActivity(criteria, values);
            DeconvertCommunity(criteria, values);

            return criteria;
        }

        private static void ConvertLocation(JobAdSearchCriteria criteria, ISetValues values)
        {
            if (criteria.Location != null)
            {
                values.SetValue(JobAdSearchCriteriaKeys.CountryId, criteria.Location.Country.Id);
                var location = criteria.Location.ToString();
                if (!string.IsNullOrEmpty(location))
                    values.SetValue(JobAdSearchCriteriaKeys.Location, location);
            }

            if (criteria.Distance != null && criteria.Distance != JobAdSearchCriteria.DefaultDistance)
                values.SetValue(JobAdSearchCriteriaKeys.Distance, criteria.Distance);
        }

        private static void ConvertKeywords(JobAdSearchCriteria criteria, ISetValues values)
        {
            // Look for specifics first.

            if (!string.IsNullOrEmpty(criteria.ExactPhrase) || !string.IsNullOrEmpty(criteria.AnyKeywords) || !string.IsNullOrEmpty(criteria.WithoutKeywords))
            {
                values.SetValue(JobAdSearchCriteriaKeys.AnyKeywords, string.IsNullOrEmpty(criteria.AnyKeywords) ? null : criteria.AnyKeywords.Split(new[] { ' ' }));
                values.SetValue(JobAdSearchCriteriaKeys.AllKeywords, criteria.AllKeywords);
                values.SetValue(JobAdSearchCriteriaKeys.ExactPhrase, criteria.ExactPhrase);
                values.SetValue(JobAdSearchCriteriaKeys.WithoutKeywords, criteria.WithoutKeywords);
            }
            else
            {
                // Look for the general second.

                values.SetValue(JobAdSearchCriteriaKeys.Keywords, criteria.GetKeywords());
            }
        }

        private void ConvertIndustries(JobAdSearchCriteria criteria, ISetValues values)
        {
            // Include only if there is a subset of all industries.

            if (criteria.IndustryIds != null && criteria.IndustryIds.Count != 0 && !criteria.IndustryIds.CollectionContains(_industriesQuery.GetIndustries().Select(i => i.Id)))
                values.SetValue(MemberSearchCriteriaKeys.IndustryIds, criteria.IndustryIds.ToArray());
        }

        private static void ConvertSalary(JobAdSearchCriteria criteria, ISetValues values)
        {
            if (criteria.ExcludeNoSalary != JobAdSearchCriteria.DefaultExcludeNoSalary)
                values.SetValue(JobAdSearchCriteriaKeys.ExcludeNoSalary, criteria.ExcludeNoSalary);

            if (criteria.Salary == null)
                return;

            var salary = criteria.Salary.ToRate(SalaryRate.Year);
            values.SetValue(JobAdSearchCriteriaKeys.SalaryLowerBound, salary.LowerBound);
            values.SetValue(JobAdSearchCriteriaKeys.SalaryUpperBound, salary.UpperBound);
        }

        private static void ConvertRecency(JobAdSearchCriteria criteria, ISetValues values)
        {
            if (criteria.Recency != null)
                values.SetValue(JobAdSearchCriteriaKeys.Recency, criteria.Recency.Value.Days);
        }

        private static void ConvertSortOrder(JobAdSearchCriteria criteria, ISetValues values)
        {
            if (criteria.SortCriteria.SortOrder != JobAdSearchCriteria.DefaultSortOrder || criteria.SortCriteria.ReverseSortOrder)
                new JobAdSearchSortCriteriaConverter().Convert(criteria.SortCriteria, values);
        }

        private static void ConvertIncludeSynonyms(JobAdSearchCriteria criteria, ISetValues values)
        {
            if (criteria.IncludeSynonyms != JobAdSearchCriteria.DefaultIncludeSynonyms)
                values.SetValue(JobAdSearchCriteriaKeys.IncludeSynonyms, criteria.IncludeSynonyms);
        }

        private static void ConvertActivity(JobAdSearchCriteria criteria, ISetValues values)
        {
            if (criteria.IsFlagged.HasValue)
                values.SetValue(JobAdSearchCriteriaKeys.IsFlagged, criteria.IsFlagged);

            if (criteria.HasNotes.HasValue)
                values.SetValue(JobAdSearchCriteriaKeys.HasNotes, criteria.HasNotes);

            if (criteria.HasViewed.HasValue)
                values.SetValue(JobAdSearchCriteriaKeys.HasViewed, criteria.HasViewed);

            if (criteria.HasApplied.HasValue)
                values.SetValue(JobAdSearchCriteriaKeys.HasApplied, criteria.HasApplied);
        }

        private static void ConvertCommunity(JobAdSearchCriteria criteria, ISetValues values)
        {
            if (criteria.CommunityId.HasValue)
                values.SetValue(JobAdSearchCriteriaKeys.CommunityId, criteria.CommunityId);
        }

        private void DeconvertLocation(JobAdSearchCriteria criteria, IGetValues values)
        {
            var location = values.GetStringValue(JobAdSearchCriteriaKeys.Location);

            Country country = null;
            var countryId = values.GetIntValue(JobAdSearchCriteriaKeys.CountryId);
            if (countryId != null)
            {
                country = _locationQuery.GetCountry(countryId.Value);
            }
            else
            {
                // Try the old value.

                countryId = values.GetIntValue(JobAdSearchCriteriaKeys.Country);
                if (countryId != null)
                    country = _locationQuery.GetCountry(countryId.Value);
            }

            if (country != null)
                criteria.Location = _locationQuery.ResolveLocation(country, location);
            else if (!string.IsNullOrEmpty(location))
                criteria.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountries()[0], location);

            criteria.Distance = values.GetIntValue(JobAdSearchCriteriaKeys.Distance);
        }

        private static void DeconvertKeywords(JobAdSearchCriteria criteria, IGetValues values, IDeconverterErrors errors)
        {
            try
            {
                // Look for specifics first.

                var anyKeywordsValue = values.GetStringArrayValue(JobAdSearchCriteriaKeys.AnyKeywords);
                var anyKeywords = anyKeywordsValue == null ? null : string.Join(" ", anyKeywordsValue);

                var allKeywords = values.GetStringValue(JobAdSearchCriteriaKeys.AllKeywords);
                var exactPhrase = values.GetStringValue(JobAdSearchCriteriaKeys.ExactPhrase);
                var withoutKeywords = values.GetStringValue(JobAdSearchCriteriaKeys.WithoutKeywords);

                if (!string.IsNullOrEmpty(allKeywords) || !string.IsNullOrEmpty(exactPhrase) || !string.IsNullOrEmpty(anyKeywords) || !string.IsNullOrEmpty(withoutKeywords))
                {
                    criteria.SetKeywords(allKeywords, exactPhrase, anyKeywords, withoutKeywords);
                }
                else
                {
                    // Look for the general second.

                    var keywords = values.GetStringValue(JobAdSearchCriteriaKeys.Keywords);
                    criteria.SetKeywords(keywords);
                }
            }
            catch (UserException ex)
            {
                errors.AddError(ex);
            }
        }

        private static void DeconvertJobTypes(JobAdSearchCriteria criteria, IGetValues values)
        {
            //support old format (JobTypes=29) and new format (FullTime=true&PartTime=true)

            var jobTypes = values.GetFlagsValue<JobTypes>();
            if (jobTypes != null)
            {
                criteria.JobTypes = jobTypes.Value;
            }
            else
            {
                var jobTypesCodes = values.GetIntValue(JobAdSearchCriteriaKeys.JobTypes);

                try
                {
                    jobTypes = (JobTypes) jobTypesCodes;
                    criteria.JobTypes = jobTypes.Value;
                }
                catch (Exception)
                {
                    //never fail for parse
                }
            }
        }

        private static void DeconvertSalary(JobAdSearchCriteria criteria, IGetValues values)
        {
            var includeNoSalary = values.GetBooleanValue(JobAdSearchCriteriaKeys.IncludeNoSalary);
            var excludeNoSalary = values.GetBooleanValue(JobAdSearchCriteriaKeys.ExcludeNoSalary);
            
            if (includeNoSalary != null)
                criteria.ExcludeNoSalary = !includeNoSalary.Value;

            //override with new format "excludeNoSalary"
            if (excludeNoSalary != null)
                criteria.ExcludeNoSalary = excludeNoSalary.Value;

            // Look for old values if the new ones aren't found.

            var lowerBound = values.GetDecimalValue(JobAdSearchCriteriaKeys.SalaryLowerBound) ?? values.GetDecimalValue(JobAdSearchCriteriaKeys.MinSalary);
            var upperBound = values.GetDecimalValue(JobAdSearchCriteriaKeys.SalaryUpperBound) ?? values.GetDecimalValue(JobAdSearchCriteriaKeys.MaxSalary);
            var rate = values.GetValue<SalaryRate>(JobAdSearchCriteriaKeys.SalaryRate);
            if (lowerBound != null || upperBound != null)
            {
                if (rate.HasValue && rate.Value.Equals(SalaryRate.Hour))
                {
                    criteria.Salary = new Salary { LowerBound = lowerBound, UpperBound = upperBound, Currency = Currency.AUD, Rate = SalaryRate.Hour };
                }
                else criteria.Salary = new Salary { LowerBound = lowerBound, UpperBound = upperBound, Currency = Currency.AUD, Rate = SalaryRate.Year };
            }
        }

        private static void DeconvertRecency(JobAdSearchCriteria criteria, IGetValues values)
        {
            var days = values.GetIntValue(JobAdSearchCriteriaKeys.Recency);
            criteria.Recency = days == null ? (TimeSpan?) null : new TimeSpan(days.Value, 0, 0, 0);
        }

        private static void DeconvertSortOrder(JobAdSearchCriteria criteria, IGetValues values, IDeconverterErrors errors)
        {
            criteria.SortCriteria = new JobAdSearchSortCriteriaConverter().Deconvert(values, errors);
        }

        private static void DeconvertIncludeSynonyms(JobAdSearchCriteria criteria, IGetValues values)
        {
            var includeSynonyms = values.GetBooleanValue(JobAdSearchCriteriaKeys.IncludeSynonyms);
            if (includeSynonyms != null)
                criteria.IncludeSynonyms = includeSynonyms.Value;
        }

        private static void DeconvertActivity(JobAdSearchCriteria criteria, IGetValues values)
        {
            criteria.IsFlagged = values.GetBooleanValue(JobAdSearchCriteriaKeys.IsFlagged);
            criteria.HasNotes = values.GetBooleanValue(JobAdSearchCriteriaKeys.HasNotes);
            criteria.HasViewed = values.GetBooleanValue(JobAdSearchCriteriaKeys.HasViewed);
            criteria.HasApplied = values.GetBooleanValue(JobAdSearchCriteriaKeys.HasApplied);
        }

        private static void DeconvertCommunity(JobAdSearchCriteria criteria, IGetValues values)
        {
            criteria.CommunityId = values.GetGuidValue(JobAdSearchCriteriaKeys.CommunityId);
        }
    }
}