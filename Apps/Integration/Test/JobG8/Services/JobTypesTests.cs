using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Integration.JobG8;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Integration.Test.JobG8.Services
{
    [TestClass]
    public class JobTypesTests
        : AdvertPostServiceTests
    {
        [TestMethod]
        public void TestPermanent()
        {
            var employer = CreateEmployer();
            var request = CreateRequest(employer, EmploymentType.Permanent, null);
            PostAdvert(employer, request);
            var jobAd = AssertJobAd(employer.Id);
            Assert.AreEqual(JobTypes.FullTime, jobAd.Description.JobTypes);
        }

        [TestMethod]
        public void TestTemporary()
        {
            var employer = CreateEmployer();
            var request = CreateRequest(employer, EmploymentType.Temporary, null);
            PostAdvert(employer, request);
            var jobAd = AssertJobAd(employer.Id);
            Assert.AreEqual(JobTypes.Temp, jobAd.Description.JobTypes);
        }

        [TestMethod]
        public void TestContract()
        {
            var employer = CreateEmployer();
            var request = CreateRequest(employer, EmploymentType.Contract, null);
            PostAdvert(employer, request);
            var jobAd = AssertJobAd(employer.Id);
            Assert.AreEqual(JobTypes.Contract, jobAd.Description.JobTypes);
        }

        [TestMethod]
        public void TestAny()
        {
            var employer = CreateEmployer();
            var request = CreateRequest(employer, EmploymentType.Any, null);
            PostAdvert(employer, request);
            var jobAd = AssertJobAd(employer.Id);
            Assert.AreEqual(JobTypes.FullTime, jobAd.Description.JobTypes);
        }

        [TestMethod]
        public void TestPartTime()
        {
            var employer = CreateEmployer();
            var request = CreateRequest(employer, EmploymentType.Permanent, WorkHours.PartTime);
            PostAdvert(employer, request);
            var jobAd = AssertJobAd(employer.Id);
            Assert.AreEqual(JobTypes.PartTime, jobAd.Description.JobTypes);
        }

        [TestMethod]
        public void TestFullTime()
        {
            var employer = CreateEmployer();
            var request = CreateRequest(employer, EmploymentType.Permanent, WorkHours.FullTime);
            PostAdvert(employer, request);
            var jobAd = AssertJobAd(employer.Id);
            Assert.AreEqual(JobTypes.FullTime, jobAd.Description.JobTypes);
        }

        private PostAdvertRequestMessage CreateRequest(Employer employer, EmploymentType employmentType, WorkHours? workHours)
        {
            return new PostAdvertRequestMessage
            {
                UserCredentials = new Credentials
                {
                    Username = _jobG8Query.GetIntegratorUser().LoginId,
                    Password = Password
                },
                PostAdvert = new PostAdvertRequest
                {
                    Adverts = new PostAdverts
                    {
                        AccountNumber = employer.GetLoginId(),
                        PostAdvert = new[] 
                        {
                            new PostAdvert
                            {
                                JobReference = "RefABCD/1235",
                                ClientReference = "RefABCD",
                                Classification = "Accounting",
                                SubClassification = "Accountant",
                                Position = "Chartered Accountant",
                                Description = "<p><b><u>Tired of searching for perfect employment? Looking for a fresh start at a new company? </u></b></p>",
                                Location = "Sydney",
                                Area = "Sydney Inner",
                                PostCode = "2000",
                                Country = "Australia",
                                VisaRequired = VisaRequired.MustBeEligible,
                                PayPeriod = PayPeriod.Annual,
                                PayAmount = 100000, PayAmountSpecified = true,
                                Currency = "AUS",
                                Contact = "John Bloomfield",
                                EmploymentType = employmentType,
                                WorkHoursSpecified = workHours != null,
                                WorkHours = workHours != null ? workHours.Value : default(WorkHours),
                            }
                        }
                    }
                }
            };
        }

        protected JobAd AssertJobAd(Guid jobPosterId)
        {
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(jobPosterId, JobAdStatus.Open));
            Assert.AreEqual(1, jobAds.Count);
            Assert.AreEqual("RefABCD/1235", jobAds[0].Integration.IntegratorReferenceId);
            Assert.AreEqual("RefABCD", jobAds[0].Integration.ExternalReferenceId);
            return jobAds[0];
        }
    }
}