using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Update
{
    [TestClass]
    public class AllUpdateJobAdsTests
        : UpdateJobAdsTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private const string FirstNameFormat = "Monty{0}";
        private const string LastNameFormat = "Burns{0}";
        private const string EmailAddressFormat = "mburns{0}@test.linkme.net.au";
        private const string CompanyFormat = "Acme{0}";
        private const string TitleFormat = "This is the title {0}";
        private const string ContentFormat = "This is the content {0}";
        private const string Location1 = "Melbourne VIC 3000";
        private const string Location2 = "Sydney NSW 2000";

        [TestMethod]
        public void TestUpdateAllJobAdObjects()
        {
            var employer = CreateEmployer();

            var country = _locationQuery.GetCountry("Australia");
            var industries = _industriesQuery.GetIndustries();

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = string.Format(TitleFormat, 0),
                ContactDetails = new ContactDetails
                {
                    FirstName = string.Format(FirstNameFormat, 0),
                    LastName = string.Format(LastNameFormat, 0),
                    EmailAddress = string.Format(EmailAddressFormat, 0),
                    CompanyName = string.Format(CompanyFormat, 0),
                },
                Description =
                {
                    Content = string.Format(ContentFormat, 1),
                    Location = _locationQuery.ResolveLocation(country, Location1),
                    Industries = new List<Industry>
                    {
                        industries[0],
                        industries[1],
                    }
                }
            };

            _jobAdsCommand.CreateJobAd(jobAd);

            // Update it.

            jobAd.ContactDetails = new ContactDetails
            {
                FirstName = string.Format(FirstNameFormat, 1),
                LastName = string.Format(LastNameFormat, 1),
                EmailAddress = string.Format(EmailAddressFormat, 1),
                CompanyName = string.Format(CompanyFormat, 1),
            };

            jobAd.Description.Location = _locationQuery.ResolveLocation(country, Location2);
            jobAd.Description.Industries = new List<Industry>
            {
                industries[2]
            };

            _jobAdsCommand.UpdateJobAd(jobAd);
        }
    }
}