using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.AppliedForExternally
{
    public abstract class AppliedForExternallyTests
        : WebApplyTests
    {
        private readonly ICareerOneQuery _careerOneQuery = Resolve<ICareerOneQuery>();

        protected override JobAd CreateJobAd(IEmployer employer)
        {
            var jobAd = base.CreateJobAd(employer);
            jobAd.Integration.ExternalApplyUrl = "http://google.com";
            jobAd.Integration.IntegratorUserId = _careerOneQuery.GetIntegratorUser().Id;
            _jobAdsCommand.UpdateJobAd(jobAd);
            return jobAd;
        }

        protected void AssertView()
        {
            Assert.IsNotNull(GetAppliedForExternallyNode());
            Assert.IsNull(GetManagedExternallyNode());
            Assert.IsNull(GetManagedInternallyNode());
            Assert.IsNull(GetLoggedInUserNode());
        }

        protected void Apply(JobAd jobAd)
        {
            // Simulate an apply by calling the API and then navigating to the applied page.

            ApiExternallyApplied(jobAd.Id);
            Get(GetAppliedUrl(jobAd.Id));
            AssertPageContains("Go back to the job ad");
            AssertAppliedContact(jobAd.ContactDetails.CompanyName);
        }
    }
}
