using System;
using System.Globalization;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Apps.Services.External.JobSearch
{
    public class JobAdMapper
    {
        private readonly ContentMapper _contentMapper = new ContentMapper(50, 2000);
        private readonly IIndustriesQuery _industriesQuery;
        private readonly OccupationMapper _occupationMapper;

        private const int MaximumExpiryDays = 30;

        public JobAdMapper(IIndustriesQuery industriesQuery)
        {
            _industriesQuery = industriesQuery;
            _occupationMapper = new OccupationMapper(_industriesQuery);
        }

        public AddVacancyRequestBody CreateAddRequestBody(JobAd jobAd)
        {
            var body = new AddVacancyRequestBody
            {
                vacancyTitle = _contentMapper.MapTitle(jobAd.Title),
                vacancyDescription = _contentMapper.MapBody(jobAd.Id, jobAd.Description.BulletPoints, jobAd.Description.Content),
                positionLimit = 1,
                daysToExpiry = Math.Min(GetDaysUntilExpiry(jobAd), MaximumExpiryDays),
                howToApplyCode = "PSD",   //Please See Description
                vacancyType = "H",   //Normal Internet Vacancy
                contactName = jobAd.Visibility.HideContactDetails || string.IsNullOrEmpty(jobAd.ContactDetails.FullName) ? "<Hidden>" : jobAd.ContactDetails.FullName,    //ContactName is compulsory
                returnMatchesFlag = false,
                yourReference = string.Empty,  //MUST specify empty string. NULL is not allowed
            };

            jobAd.ContactDetails.MapPhoneNumber(jobAd.Visibility.HideContactDetails, out body.contactPhoneAreaCode, out body.contactPhoneNumber);
            jobAd.Description.Location.Map(out body.stateCode, out body.vacancyPostcode, out body.vacancySuburb);
            jobAd.Description.JobTypes.Map(out body.workType, out body.duration);

            if (jobAd.Description.Salary != null)
                body.salary = jobAd.Description.Salary.GetDisplayText();

            body.occupationCode = _occupationMapper.Map(jobAd).ToString(CultureInfo.InvariantCulture);

            return body;
        }

        public UpdateVacancyRequestBody CreateUpdateRequestBody(JobAd jobAd)
        {
            var body = new UpdateVacancyRequestBody
            {
                vacancyTitle = jobAd.Title,
                vacancyDescription = _contentMapper.MapBody(jobAd.Id, jobAd.Description.BulletPoints, jobAd.Description.Content),
                positionLimit = 1,
                daysToExpiry = Math.Min(GetDaysUntilExpiry(jobAd), MaximumExpiryDays),
                howToApplyCode = "PSD",
                vacancyType = "H",
                contactName = jobAd.Visibility.HideContactDetails || string.IsNullOrEmpty(jobAd.ContactDetails.FullName) ? "<Hidden>" : jobAd.ContactDetails.FullName,    //ContactName is compulsory
                occupationCode = _occupationMapper.Map(jobAd).ToString(CultureInfo.InvariantCulture)
            };

            jobAd.Description.Location.Map(out body.stateCode, out body.vacancyPostcode, out body.vacancySuburb);
            jobAd.Description.JobTypes.Map(out body.workType, out body.duration);
            jobAd.ContactDetails.MapPhoneNumber(jobAd.Visibility.HideContactDetails, out body.contactPhoneAreaCode, out body.contactPhoneNumber);

            if (jobAd.Description.Salary != null)
                body.salary = jobAd.Description.Salary.GetDisplayText();

            return body;
        }

        private static int GetDaysUntilExpiry(JobAdEntry jobAd)
        {
            return jobAd.ExpiryTime == null
                ? 0
                : jobAd.ExpiryTime.Value > DateTime.Now
                    ? (jobAd.ExpiryTime.Value - DateTime.Now).Days
                    : 0;
        }
    }
}
