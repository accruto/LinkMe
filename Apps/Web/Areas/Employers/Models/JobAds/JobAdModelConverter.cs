using System;
using System.Linq;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Web.Areas.Employers.Models.JobAds
{
    public class JobAdModelConverter
        : Converter<JobAdModel>
    {
        private readonly ILocationQuery _locationQuery;

        public JobAdModelConverter(ILocationQuery locationQuery)
        {
            _locationQuery = locationQuery;
        }

        private static class Keys
        {
            public const string Title = "Title";
            public const string PositionTitle = "PositionTitle";
            public const string ExternalReferenceId = "ExternalReferenceId";
            public const string BulletPoint1 = "BulletPoint1";
            public const string BulletPoint2 = "BulletPoint2";
            public const string BulletPoint3 = "BulletPoint3";
            public const string Summary = "Summary";
            public const string Content = "Content";
            public const string CompanyName = "CompanyName";
            public const string HideCompany = "HideCompany";
            public const string Package = "Package";
            public const string ResidencyRequired = "ResidencyRequired";
            public const string ExpiryTime = "ExpiryTime";
            public const string HideContactDetails = "HideContactDetails";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string EmailAddress = "EmailAddress";
            public const string SecondaryEmailAddresses = "SecondaryEmailAddresses";
            public const string FaxNumber = "FaxNumber";
            public const string PhoneNumber = "PhoneNumber";
            public const string IndustryIds = "IndustryIds"; 
            public const string CountryId = "CountryId";
            public const string Location = "Location";
            public const string SalaryLowerBound = "SalaryLowerBound";
            public const string SalaryUpperBound = "SalaryUpperBound";
        }

        public override void Convert(JobAdModel jobAd, ISetValues values)
        {
        }

        public override JobAdModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            var jobAd = new JobAdModel
            {
                Title = values.GetStringValue(Keys.Title),
                PositionTitle = values.GetStringValue(Keys.PositionTitle),
                ExternalReferenceId = values.GetStringValue(Keys.ExternalReferenceId),
                BulletPoints = new[]
                {
                    values.GetStringValue(Keys.BulletPoint1),
                    values.GetStringValue(Keys.BulletPoint2),
                    values.GetStringValue(Keys.BulletPoint3)
                }.Where(b => !string.IsNullOrEmpty(b)).ToArray(),
                Summary = values.GetStringValue(Keys.Summary),
                Content = values.GetStringValue(Keys.Content),
                CompanyName = values.GetStringValue(Keys.CompanyName),
                HideCompany = values.GetBooleanValue(Keys.HideCompany) ?? true,
                Package = values.GetStringValue(Keys.Package),
                ResidencyRequired = values.GetBooleanValue(Keys.ResidencyRequired) ?? true,
                ExpiryTime = values.GetDateTimeValue(Keys.ExpiryTime),
                HideContactDetails = values.GetBooleanValue(Keys.HideContactDetails) ?? true,
                ContactDetails = new ContactDetails
                {
                    FirstName = values.GetStringValue(Keys.FirstName),
                    LastName = values.GetStringValue(Keys.LastName),
                    EmailAddress = values.GetStringValue(Keys.EmailAddress),
                    SecondaryEmailAddresses = values.GetStringValue(Keys.SecondaryEmailAddresses),
                    FaxNumber = values.GetStringValue(Keys.FaxNumber),
                    PhoneNumber = values.GetStringValue(Keys.PhoneNumber),
                },
                IndustryIds = values.GetGuidArrayValue(Keys.IndustryIds)
            };

            DeconvertLocation(jobAd, values);
            DeconvertSalary(jobAd, values);

            var jobTypes = values.GetFlagsValue<JobTypes>();
            if (jobTypes != null)
                jobAd.JobTypes = jobTypes.Value;

            return jobAd;
        }

        private void DeconvertLocation(JobAdModel jobAd, IGetValues values)
        {
            var location = values.GetStringValue(Keys.Location);

            Country country = null;
            var countryId = values.GetIntValue(Keys.CountryId);
            if (countryId != null)
                country = _locationQuery.GetCountry(countryId.Value);

            if (country != null)
                jobAd.Location = _locationQuery.ResolveLocation(country, location);
            else if (!string.IsNullOrEmpty(location))
                jobAd.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountries()[0], location);
        }

        private static void DeconvertSalary(JobAdModel jobAd, IGetValues values)
        {
            var lowerBound = values.GetDecimalValue(Keys.SalaryLowerBound);
            var upperBound = values.GetDecimalValue(Keys.SalaryUpperBound);
            if (lowerBound != null || upperBound != null)
                jobAd.Salary = new Salary { LowerBound = lowerBound, UpperBound = upperBound, Currency = Currency.AUD, Rate = SalaryRate.Year };
        }
    }
}