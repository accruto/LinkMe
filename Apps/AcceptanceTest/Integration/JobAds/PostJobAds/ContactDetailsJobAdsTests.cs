using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.PostJobAds
{
    [TestClass]
    public class ContactDetailsJobAdsTests
        : PostJobAdsTests
    {
        [TestMethod]
        public void TestBadPhoneNumber()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(0);

            var jobAd = CreateJobAd(0, new ContactDetails { PhoneNumber = "xxx" });
            PostJobAds(integratorUser, employer, false, new[] { jobAd }, 1, 0, 0, 0);

            // Phone number should be ignored.

            jobAd.ContactDetails = null;
            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Open));
        }

        private static JobAdElement CreateJobAd(int index, ContactDetails contactDetails)
        {
            return CreateJobAd(index, j => j.ContactDetails = contactDetails);
        }
    }
}
