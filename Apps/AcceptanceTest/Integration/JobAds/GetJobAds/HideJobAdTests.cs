using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.GetJobAds
{
    public abstract class HideJobAdTests
        : GetJobAdsTests
    {
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();

        protected const string FirstName = "Homer";
        protected const string LastName = "Simpson";
        protected const string ContactCompanyName = "Acme";
        protected const string EmailAddress = "hsimpson@test.linkme.net.au";
        protected const string FaxNumber = "88888888";
        protected const string PhoneNumber = "99999999";
        protected const string SecondaryEmailAddresses = "hs@test.linkme.net.au";
        protected const string CompanyName = "Ajax";

        protected JobAd PostJobAd(IEmployer employer, IntegratorUser integratorUser, string companyName, string contactCompanyName, bool hideContactDetails, bool hideCompany)
        {
            var jobAd = PostJobAd(employer);

            jobAd.ContactDetails = new ContactDetails
            {
                FirstName = FirstName,
                LastName = LastName,
                CompanyName = contactCompanyName,
                EmailAddress = EmailAddress,
                FaxNumber = FaxNumber,
                PhoneNumber = PhoneNumber,
                SecondaryEmailAddresses = SecondaryEmailAddresses
            };
            jobAd.Description.CompanyName = companyName;

            jobAd.Visibility.HideContactDetails = hideContactDetails;
            jobAd.Visibility.HideCompany = hideCompany;

            if (integratorUser != null)
                jobAd.Integration.IntegratorUserId = integratorUser.Id;

            _jobAdsCommand.UpdateJobAd(jobAd);
            return jobAd;
        }

        protected void AssertHide(string expectedCompanyName, ContactDetails expectedContactDetails, JobAdFeedElement jobAdFeed)
        {
            Assert.AreEqual(expectedCompanyName, jobAdFeed.EmployerCompanyName);

            if (expectedContactDetails == null)
            {
                Assert.IsNull(jobAdFeed.ContactDetails);
                Assert.AreEqual(null, jobAdFeed.RecruiterCompanyName);
            }
            else
            {
                Assert.AreEqual(expectedContactDetails.FirstName, jobAdFeed.ContactDetails.FirstName);
                Assert.AreEqual(expectedContactDetails.LastName, jobAdFeed.ContactDetails.LastName);
                Assert.AreEqual(expectedContactDetails.EmailAddress, jobAdFeed.ContactDetails.EmailAddress);
                Assert.AreEqual(expectedContactDetails.FaxNumber, jobAdFeed.ContactDetails.FaxNumber);
                Assert.AreEqual(expectedContactDetails.PhoneNumber, jobAdFeed.ContactDetails.PhoneNumber);
                Assert.AreEqual(expectedContactDetails.CompanyName, jobAdFeed.RecruiterCompanyName);
            }
        }
    }
}
