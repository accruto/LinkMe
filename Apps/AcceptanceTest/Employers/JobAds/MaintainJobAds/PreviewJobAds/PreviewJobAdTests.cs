using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.PreviewJobAds
{
    [TestClass]
    public abstract class PreviewJobAdTests
        : MaintainJobAdTests
    {
        protected void AssertJobAd(Guid jobAdId, JobAdStatus expectedStatus, DateTime? expectedExpiryTime)
        {
            var jobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(jobAdId);
            Assert.IsNotNull(jobAd);
            Assert.AreEqual(expectedStatus, jobAd.Status);
            Assert.AreEqual(expectedExpiryTime, jobAd.ExpiryTime);
        }

        protected void AssertExpiryTime(DateTime expiryTime)
        {
            var days = (expiryTime - DateTime.Now.Date.AddDays(1).AddSeconds(-1)).Days;
            AssertPageContains("expire <span class=\"expiry-days\">in " + days + " days</span> on <strong><span class=\"expiry-date\">" + expiryTime.DayOfWeek + " " + expiryTime.ToShortDateString() + "</span></strong>.");
        }

        protected JobAd CreateJobAd(IEmployer employer, JobAdStatus status, DateTime createdTime, DateTime? expiryTime)
        {
            var jobAd = employer.CreateTestJobAd();
            _employerJobAdsCommand.CreateJobAd(employer, jobAd);

            switch (status)
            {
                case JobAdStatus.Open:
                    _employerJobAdsCommand.OpenJobAd(employer, jobAd, false);
                    break;

                case JobAdStatus.Closed:
                    _employerJobAdsCommand.OpenJobAd(employer, jobAd, false);
                    _employerJobAdsCommand.CloseJobAd(employer, jobAd);
                    break;
            }

            jobAd.CreatedTime = createdTime;
            jobAd.ExpiryTime = expiryTime;
            _jobAdsCommand.UpdateJobAd(jobAd);

            return jobAd;
        }
    }
}
