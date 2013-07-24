using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Integration.JobG8;
using LinkMe.Apps.Services.External.JobG8.Queries;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Integration.Test.JobG8
{
    [TestClass]
    public class JobTypesTests
        : AdvertPostServiceTests
    {
        private const string Password = "password";
        private readonly IJobG8Query _jobG8Query = Resolve<IJobG8Query>();

        [TestMethod]
        public void TestPermanent()
        {
            var employer = CreateEmployer(0);
            var request = CreateRequest(employer, EmploymentType.Permanent, null);
            PostAdvert(employer, request);
            var jobAd = AssertJobAd(employer.Id);
            Assert.AreEqual(JobTypes.FullTime, jobAd.Description.JobTypes);
        }

        [TestMethod]
        public void TestTemporary()
        {
            var employer = CreateEmployer(0);
            var request = CreateRequest(employer, EmploymentType.Temporary, null);
            PostAdvert(employer, request);
            var jobAd = AssertJobAd(employer.Id);
            Assert.AreEqual(JobTypes.Temp, jobAd.Description.JobTypes);
        }

        [TestMethod]
        public void TestContract()
        {
            var employer = CreateEmployer(0);
            var request = CreateRequest(employer, EmploymentType.Contract, null);
            PostAdvert(employer, request);
            var jobAd = AssertJobAd(employer.Id);
            Assert.AreEqual(JobTypes.Contract, jobAd.Description.JobTypes);
        }

        [TestMethod]
        public void TestAny()
        {
            var employer = CreateEmployer(0);
            var request = CreateRequest(employer, EmploymentType.Any, null);
            PostAdvert(employer, request);
            var jobAd = AssertJobAd(employer.Id);
            Assert.AreEqual(JobTypes.FullTime, jobAd.Description.JobTypes);
        }

        [TestMethod]
        public void TestPartTime()
        {
            var employer = CreateEmployer(0);
            var request = CreateRequest(employer, EmploymentType.Permanent, WorkHours.PartTime);
            PostAdvert(employer, request);
            var jobAd = AssertJobAd(employer.Id);
            Assert.AreEqual(JobTypes.PartTime, jobAd.Description.JobTypes);
        }

        [TestMethod]
        public void TestFullTime()
        {
            var employer = CreateEmployer(0);
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
    }
}
