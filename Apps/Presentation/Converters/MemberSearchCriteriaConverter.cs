using System;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Presentation.Converters
{
    public class MemberSearchCriteriaConverter
        : Converter<MemberSearchCriteria>
    {
        private readonly ILocationQuery _locationQuery;
        private readonly IIndustriesQuery _industriesQuery;

        public MemberSearchCriteriaConverter(ILocationQuery locationQuery, IIndustriesQuery industriesQuery)
        {
            _locationQuery = locationQuery;
            _industriesQuery = industriesQuery;
        }

        public override void Convert(MemberSearchCriteria criteria, ISetValues values)
        {
            if (criteria == null)
                return;

            ConvertKeywords(criteria, values);
            ConvertName(criteria, values);
            ConvertJobTitles(criteria, values);
            ConvertCompanies(criteria, values);
            ConvertLocation(criteria, values);
            ConvertIncludeSynonyms(criteria, values);

            if (!string.IsNullOrEmpty(criteria.EducationKeywords))
                values.SetValue(MemberSearchCriteriaKeys.EducationKeywords, criteria.EducationKeywords);

            values.SetFlagsValue(criteria.CandidateStatusFlags);
            values.SetFlagsValue(criteria.EthnicStatus);
            values.SetFlagsValue(criteria.VisaStatusFlags);

            if (criteria.JobTypes != MemberSearchCriteria.DefaultJobTypes)
                values.SetFlagsValue(criteria.JobTypes);

            ConvertIndustries(criteria, values);
            ConvertSalary(criteria, values);
            ConvertRecency(criteria, values);
            ConvertActivity(criteria, values);
            ConvertCommunity(criteria, values);
            ConvertSortOrder(criteria, values);
        }

        public override MemberSearchCriteria Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            var criteria = new MemberSearchCriteria();

            DeconvertKeywords(criteria, values, errors);
            DeconvertJobTitles(criteria, values);
            DeconvertCompanies(criteria, values);
            DeconvertLocation(criteria, values);

            DeconvertIncludeSynonyms(criteria, values);
            DeconvertSortOrder(criteria, values, errors);

            criteria.CandidateStatusFlags = values.GetFlagsValue<CandidateStatusFlags>();
            criteria.EducationKeywords = values.GetStringValue(MemberSearchCriteriaKeys.EducationKeywords);
            criteria.EthnicStatus = values.GetFlagsValue<EthnicStatus>();
            criteria.VisaStatusFlags = values.GetFlagsValue<VisaStatusFlags>();

            var jobTypes = values.GetFlagsValue<JobTypes>();
            if (jobTypes != null)
                criteria.JobTypes = jobTypes.Value;

            criteria.IndustryIds = values.GetGuidArrayValue(MemberSearchCriteriaKeys.IndustryIds);

            DeconvertSalary(criteria, values);
            DeconvertRecency(criteria, values);
            DeconvertActivity(criteria, values);
            DeconvertCommunity(criteria, values);
            DeconvertName(criteria, values);

            return criteria;
        }

        private static void ConvertName(MemberSearchCriteria criteria, ISetValues values)
        {
            if (!string.IsNullOrEmpty(criteria.Name))
                values.SetValue(MemberSearchCriteriaKeys.Name, criteria.Name);

            if (criteria.IncludeSimilarNames != MemberSearchCriteria.DefaultIncludeSimilarNames)
                values.SetValue(MemberSearchCriteriaKeys.IncludeSimilarNames, criteria.IncludeSimilarNames);
        }

        private static void ConvertJobTitles(MemberSearchCriteria criteria, ISetValues values)
        {
            if (!string.IsNullOrEmpty(criteria.JobTitle))
                values.SetValue(MemberSearchCriteriaKeys.JobTitle, criteria.JobTitle);

            if (!string.IsNullOrEmpty(criteria.DesiredJobTitle))
                values.SetValue(MemberSearchCriteriaKeys.DesiredJobTitle, criteria.DesiredJobTitle);

            if (criteria.JobTitlesToSearch != MemberSearchCriteria.DefaultJobTitlesToSearch)
                values.SetValue(MemberSearchCriteriaKeys.JobTitlesToSearch, criteria.JobTitlesToSearch);
        }

        private static void ConvertCompanies(MemberSearchCriteria criteria, ISetValues values)
        {
            if (!string.IsNullOrEmpty(criteria.CompanyKeywords))
                values.SetValue(MemberSearchCriteriaKeys.CompanyKeywords, criteria.CompanyKeywords);

            if (criteria.CompaniesToSearch != MemberSearchCriteria.DefaultCompaniesToSearch)
                values.SetValue(MemberSearchCriteriaKeys.CompaniesToSearch, criteria.CompaniesToSearch);
        }

        private static void ConvertLocation(MemberSearchCriteria criteria, ISetValues values)
        {
            if (criteria.Location != null)
            {
                values.SetValue(MemberSearchCriteriaKeys.CountryId, criteria.Location.Country.Id);
                var location = criteria.Location.ToString();
                if (!string.IsNullOrEmpty(location))
                    values.SetValue(MemberSearchCriteriaKeys.Location, location);
            }

            if (criteria.Distance != null && criteria.Distance != MemberSearchCriteria.DefaultDistance)
                values.SetValue(MemberSearchCriteriaKeys.Distance, criteria.Distance);

            if (criteria.IncludeRelocating != MemberSearchCriteria.DefaultIncludeRelocating)
                values.SetValue(MemberSearchCriteriaKeys.IncludeRelocating, criteria.IncludeRelocating);

            if (criteria.IncludeInternational != MemberSearchCriteria.DefaultIncludeInternational)
                values.SetValue(MemberSearchCriteriaKeys.IncludeInternational, criteria.IncludeInternational);
        }

        private static void ConvertKeywords(MemberSearchCriteria criteria, ISetValues values)
        {
            // Look for specifics first.

            if (!string.IsNullOrEmpty(criteria.ExactPhrase) || !string.IsNullOrEmpty(criteria.AnyKeywords) || !string.IsNullOrEmpty(criteria.WithoutKeywords))
            {
                values.SetValue(MemberSearchCriteriaKeys.AnyKeywords, string.IsNullOrEmpty(criteria.AnyKeywords) ? null : criteria.AnyKeywords.Split(new[] { ' ' }));
                values.SetValue(MemberSearchCriteriaKeys.AllKeywords, criteria.AllKeywords);
                values.SetValue(MemberSearchCriteriaKeys.ExactPhrase, criteria.ExactPhrase);
                values.SetValue(MemberSearchCriteriaKeys.WithoutKeywords, criteria.WithoutKeywords);
            }
            else
            {
                // Look for the general second.

                values.SetValue(MemberSearchCriteriaKeys.Keywords, criteria.GetKeywords());
            }
        }

        private void ConvertIndustries(MemberSearchCriteria criteria, ISetValues values)
        {
            // Include only if there is a subset of all industries.

            if (criteria.IndustryIds != null && criteria.IndustryIds.Count != 0 && !criteria.IndustryIds.CollectionContains(_industriesQuery.GetIndustries().Select(i => i.Id)))
                values.SetValue(MemberSearchCriteriaKeys.IndustryIds, criteria.IndustryIds.ToArray());
        }

        private static void ConvertSalary(MemberSearchCriteria criteria, ISetValues values)
        {
            if (criteria.ExcludeNoSalary != MemberSearchCriteria.DefaultExcludeNoSalary)
                values.SetValue(MemberSearchCriteriaKeys.ExcludeNoSalary, criteria.ExcludeNoSalary);

            if (criteria.Salary == null)
                return;

            var salary = criteria.Salary.ToRate(SalaryRate.Year);
            values.SetValue(MemberSearchCriteriaKeys.SalaryLowerBound, salary.LowerBound);
            values.SetValue(MemberSearchCriteriaKeys.SalaryUpperBound, salary.UpperBound);
        }

        private static void ConvertRecency(MemberSearchCriteria criteria, ISetValues values)
        {
            if (criteria.Recency != null)
                values.SetValue(MemberSearchCriteriaKeys.Recency, criteria.Recency.Value.Days);
        }

        private static void ConvertSortOrder(MemberSearchCriteria criteria, ISetValues values)
        {
            if (criteria.SortCriteria.SortOrder != MemberSearchCriteria.DefaultSortOrder || criteria.SortCriteria.ReverseSortOrder)
                new MemberSearchSortCriteriaConverter().Convert(criteria.SortCriteria, values);
        }

        private static void ConvertIncludeSynonyms(MemberSearchCriteria criteria, ISetValues values)
        {
            if (criteria.IncludeSynonyms != MemberSearchCriteria.DefaultIncludeSynonyms)
                values.SetValue(MemberSearchCriteriaKeys.IncludeSynonyms, criteria.IncludeSynonyms);
        }

        private static void ConvertActivity(MemberSearchCriteria criteria, ISetValues values)
        {
            if (criteria.InFolder.HasValue)
                values.SetValue(MemberSearchCriteriaKeys.InFolder, criteria.InFolder);

            if (criteria.IsFlagged.HasValue)
                values.SetValue(MemberSearchCriteriaKeys.IsFlagged, criteria.IsFlagged);

            if (criteria.HasNotes.HasValue)
                values.SetValue(MemberSearchCriteriaKeys.HasNotes, criteria.HasNotes);

            if (criteria.HasViewed.HasValue)
                values.SetValue(MemberSearchCriteriaKeys.HasViewed, criteria.HasViewed);

            if (criteria.IsUnlocked.HasValue)
                values.SetValue(MemberSearchCriteriaKeys.IsUnlocked, criteria.IsUnlocked);
        }

        private static void ConvertCommunity(MemberSearchCriteria criteria, ISetValues values)
        {
            if (criteria.CommunityId.HasValue)
                values.SetValue(MemberSearchCriteriaKeys.CommunityId, criteria.CommunityId);
        }

        private static void DeconvertName(MemberSearchCriteria criteria, IGetValues values)
        {
            criteria.Name = values.GetStringValue(MemberSearchCriteriaKeys.Name);
            var includeSimiliarNames = values.GetBooleanValue(MemberSearchCriteriaKeys.IncludeSimilarNames);
            if (includeSimiliarNames != null)
                criteria.IncludeSimilarNames = includeSimiliarNames.Value;
        }

        private static void DeconvertJobTitles(MemberSearchCriteria criteria, IGetValues values)
        {
            criteria.JobTitle = values.GetStringValue(MemberSearchCriteriaKeys.JobTitle);
            criteria.DesiredJobTitle = values.GetStringValue(MemberSearchCriteriaKeys.DesiredJobTitle);

            var jobTitlesToSearch = values.GetValue<JobsToSearch>(MemberSearchCriteriaKeys.JobTitlesToSearch);
            if (jobTitlesToSearch != null)
                criteria.JobTitlesToSearch = jobTitlesToSearch.Value;
        }

        private static void DeconvertCompanies(MemberSearchCriteria criteria, IGetValues values)
        {
            criteria.CompanyKeywords = values.GetStringValue(MemberSearchCriteriaKeys.CompanyKeywords);
            var companiesToSearch = values.GetValue<JobsToSearch>(MemberSearchCriteriaKeys.CompaniesToSearch);
            if (companiesToSearch != null)
                criteria.CompaniesToSearch = companiesToSearch.Value;
        }

        private void DeconvertLocation(MemberSearchCriteria criteria, IGetValues values)
        {
            var location = values.GetStringValue(MemberSearchCriteriaKeys.Location);

            Country country = null;
            var countryId = values.GetIntValue(MemberSearchCriteriaKeys.CountryId);
            if (countryId != null)
                country = _locationQuery.GetCountry(countryId.Value);

            if (country != null)
                criteria.Location = _locationQuery.ResolveLocation(country, location);
            else if (!string.IsNullOrEmpty(location))
                criteria.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountries()[0], location);

            criteria.Distance = values.GetIntValue(MemberSearchCriteriaKeys.Distance);

            var includeRelocating = values.GetBooleanValue(MemberSearchCriteriaKeys.IncludeRelocating);
            if (includeRelocating != null)
                criteria.IncludeRelocating = includeRelocating.Value;

            var includeInternational = values.GetBooleanValue(MemberSearchCriteriaKeys.IncludeInternational);
            if (includeInternational != null)
                criteria.IncludeInternational = includeInternational.Value;
        }

        private static void DeconvertKeywords(MemberSearchCriteria criteria, IGetValues values, IDeconverterErrors errors)
        {
            try
            {
                // Look for specifics first.

                var anyKeywordsValue = values.GetStringArrayValue(MemberSearchCriteriaKeys.AnyKeywords);
                var anyKeywords = anyKeywordsValue == null ? null : string.Join(" ", anyKeywordsValue);

                var allKeywords = values.GetStringValue(MemberSearchCriteriaKeys.AllKeywords);
                var exactPhrase = values.GetStringValue(MemberSearchCriteriaKeys.ExactPhrase);
                var withoutKeywords = values.GetStringValue(MemberSearchCriteriaKeys.WithoutKeywords);

                if (!string.IsNullOrEmpty(allKeywords) || !string.IsNullOrEmpty(exactPhrase) || !string.IsNullOrEmpty(anyKeywords) || !string.IsNullOrEmpty(withoutKeywords))
                {
                    criteria.SetKeywords(allKeywords, exactPhrase, anyKeywords, withoutKeywords);
                }
                else
                {
                    // Look for the general second.

                    var keywords = values.GetStringValue(MemberSearchCriteriaKeys.Keywords);
                    criteria.SetKeywords(keywords);
                }
            }
            catch (UserException ex)
            {
                errors.AddError(ex);
            }
        }

        private static void DeconvertSalary(MemberSearchCriteria criteria, IGetValues values)
        {
            var excludeNoSalary = values.GetBooleanValue(MemberSearchCriteriaKeys.ExcludeNoSalary);
            if (excludeNoSalary != null)
                criteria.ExcludeNoSalary = excludeNoSalary.Value;

            var lowerBound = values.GetDecimalValue(MemberSearchCriteriaKeys.SalaryLowerBound);
            var upperBound = values.GetDecimalValue(MemberSearchCriteriaKeys.SalaryUpperBound);
            if (lowerBound != null || upperBound != null)
                criteria.Salary = new Salary { LowerBound = lowerBound, UpperBound = upperBound, Currency = Currency.AUD, Rate = SalaryRate.Year };
        }

        private static void DeconvertRecency(MemberSearchCriteria criteria, IGetValues values)
        {
            var days = values.GetIntValue(MemberSearchCriteriaKeys.Recency);
            criteria.Recency = days == null ? (TimeSpan?) null : new TimeSpan(days.Value, 0, 0, 0);
        }

        private static void DeconvertSortOrder(MemberSearchCriteria criteria, IGetValues values, IDeconverterErrors errors)
        {
            criteria.SortCriteria = new MemberSearchSortCriteriaConverter().Deconvert(values, errors);
        }

        private static void DeconvertIncludeSynonyms(MemberSearchCriteria criteria, IGetValues values)
        {
            var includeSynonyms = values.GetBooleanValue(MemberSearchCriteriaKeys.IncludeSynonyms);
            if (includeSynonyms != null)
                criteria.IncludeSynonyms = includeSynonyms.Value;
        }

        private static void DeconvertActivity(MemberSearchCriteria criteria, IGetValues values)
        {
            criteria.InFolder = values.GetBooleanValue(MemberSearchCriteriaKeys.InFolder);
            criteria.IsFlagged = values.GetBooleanValue(MemberSearchCriteriaKeys.IsFlagged);
            criteria.HasNotes = values.GetBooleanValue(MemberSearchCriteriaKeys.HasNotes);
            criteria.HasViewed = values.GetBooleanValue(MemberSearchCriteriaKeys.HasViewed);
            criteria.IsUnlocked = values.GetBooleanValue(MemberSearchCriteriaKeys.IsUnlocked);
        }

        private static void DeconvertCommunity(MemberSearchCriteria criteria, IGetValues values)
        {
            criteria.CommunityId = values.GetGuidValue(MemberSearchCriteriaKeys.CommunityId);
        }
    }
}