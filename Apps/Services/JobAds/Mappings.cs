using System;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Members.JobAds;

namespace LinkMe.Apps.Services.JobAds
{
    public static class Mappings
    {
        public static JobAdFeedElement Map(this MemberJobAdView jobAd, string viewJobAdUrl, string applyJobAdUrl, bool featured)
        {
            return new JobAdFeedElement
            {
                ApplyJobAdUrl = applyJobAdUrl,
                ViewJobAdUrl = viewJobAdUrl,
                Featured = featured,
                BulletPoints = jobAd.Description.BulletPoints == null ? null : jobAd.Description.BulletPoints.ToArray(),
                ContactDetails = jobAd.ContactDetails == null
                    ? null
                    : new ContactDetails { FirstName = jobAd.ContactDetails.FirstName, LastName = jobAd.ContactDetails.LastName, EmailAddress = jobAd.ContactDetails.EmailAddress, FaxNumber = jobAd.ContactDetails.FaxNumber, PhoneNumber = jobAd.ContactDetails.PhoneNumber, SecondaryEmailAddresses = jobAd.ContactDetails.SecondaryEmailAddresses },
                RecruiterCompanyName = jobAd.ContactDetails == null
                    ? null
                    : jobAd.ContactDetails.CompanyName,
                Content = jobAd.Description.Content,
                ExternalApplyUrl = jobAd.Integration.ExternalApplyUrl,
                ExternalReferenceId = jobAd.Integration.ExternalReferenceId,

                // Explicitly do not output the status to the feed.

                Status = null,

                Id = jobAd.Id,
                EmployerCompanyName = string.IsNullOrEmpty(jobAd.Description.CompanyName) ? null : jobAd.Description.CompanyName,
                Industries = jobAd.Description.Industries != null ? (from i in jobAd.Description.Industries select i.Name).ToList() : null,
                JobTypes = jobAd.Description.JobTypes,
                Location = jobAd.Description.Location != null ? jobAd.Description.Location.ToString() : string.Empty,
                PackageDetails = jobAd.Description.Package,
                PositionTitle = jobAd.Description.PositionTitle,
                Postcode = jobAd.Description.Location != null ? (jobAd.Description.Location.Postcode ?? string.Empty) : string.Empty,
                ResidencyRequired = jobAd.Description.ResidencyRequired,
                Salary = jobAd.Description.Salary != null
                    ? new Salary { LowerBound = jobAd.Description.Salary.LowerBound, UpperBound = jobAd.Description.Salary.UpperBound, Rate = jobAd.Description.Salary.Rate, Currency = jobAd.Description.Salary.Currency }
                    : null,
                Summary = jobAd.Description.Summary,
                Title = jobAd.Title,
            };
        }

        public static JobAd Map(this JobAdElement jobAd, IIndustriesQuery industriesQuery, ILocationQuery locationQuery)
        {
            return new JobAd
            {
                Title = jobAd.Title,

                // If no status is supplied assume it is open.

                Status = jobAd.Status == null ? JobAdStatus.Open : jobAd.Status.Value,
                ContactDetails = MapContactDetails(jobAd.ContactDetails),
                Integration =
                {
                    ExternalApplyUrl = jobAd.ExternalApplyUrl,
                    ExternalReferenceId = jobAd.ExternalReferenceId,
                },
                Description =
                {
                    CompanyName = jobAd.EmployerCompanyName,
                    BulletPoints = jobAd.BulletPoints,
                    Content = jobAd.Content,
                    Industries = jobAd.Industries != null
                        ? (from n in jobAd.Industries
                           let i = industriesQuery.GetIndustryByAnyName(n)
                           where i != null
                           select i).ToList()
                        : null,
                    JobTypes = jobAd.JobTypes,
                    Location = Map(jobAd.Location, jobAd.Postcode, locationQuery),
                    Package = jobAd.PackageDetails,
                    PositionTitle = jobAd.PositionTitle,
                    ResidencyRequired = jobAd.ResidencyRequired,
                    Salary = jobAd.Salary != null
                        ? new Salary { LowerBound = jobAd.Salary.LowerBound, UpperBound = jobAd.Salary.UpperBound, Rate = jobAd.Salary.Rate, Currency = Currency.AUD }
                        : null,
                    Summary = jobAd.Summary,
                },
            };
        }

        private static ContactDetails MapContactDetails(ContactDetails contactDetails)
        {
            if (contactDetails == null)
                return null;

            var phoneNumber = contactDetails.PhoneNumber.GetPhoneNumber();
            var faxNumber = contactDetails.FaxNumber.GetPhoneNumber();

            if (string.IsNullOrEmpty(contactDetails.FirstName)
                && string.IsNullOrEmpty(contactDetails.LastName)
                && string.IsNullOrEmpty(contactDetails.EmailAddress)
                && string.IsNullOrEmpty(faxNumber)
                && string.IsNullOrEmpty(phoneNumber)
                && string.IsNullOrEmpty(contactDetails.SecondaryEmailAddresses))
                return null;

            return new ContactDetails
            {
                FirstName = contactDetails.FirstName,
                LastName = contactDetails.LastName,
                EmailAddress = contactDetails.EmailAddress,
                FaxNumber = faxNumber,
                PhoneNumber = phoneNumber,
                SecondaryEmailAddresses = contactDetails.SecondaryEmailAddresses,
            };
        }

        private static LocationReference Map(string location, string postcode, ILocationQuery locationQuery)
        {
            // Need to consolidate location and postcode into a single JobLocation.

            location = (location ?? string.Empty).Trim();
            postcode = (postcode ?? string.Empty).Trim();
            if (postcode.Length != 0)
            {
                // If the postcode is not already part of the location then add it in, but only if it is actually recognised as a postcode.

                if (location.IndexOf(postcode, StringComparison.InvariantCultureIgnoreCase) == -1)
                {
                    var postalCode = locationQuery.GetPostalCode(locationQuery.GetCountry("Australia"), postcode);
                    if (postalCode != null)
                        location += " " + postcode;
                }
            }

            // Resolve the location.

            return locationQuery.ResolveLocation(locationQuery.GetCountry("Australia"), location);
        }
    }
}
