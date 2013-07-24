using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds
{
    [TestClass]
    public class GetJobAdTests
        : TestClass
    {
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();

        [TestInitialize]
        public void GetJobAdTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            CreateEmployer();
        }

        [TestMethod]
        public void TestGetExpiredJobAds()
        {
            var employer = CreateEmployer();

            // Create an open job ad.

            var activeJobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Create an open job ad that has expired.

            var expiredJobAd = employer.CreateTestJobAd();
            _jobAdsCommand.PostJobAd(expiredJobAd);
            expiredJobAd.ExpiryTime = DateTime.Now.AddDays(-1);
            _jobAdsCommand.UpdateJobAd(expiredJobAd);

            Assert.AreEqual(JobAdStatus.Open, activeJobAd.Status);
            Assert.AreEqual(JobAdStatus.Open, expiredJobAd.Status);

            // Get the expired job ads.

            var ids = _jobAdsQuery.GetExpiredJobAdIds();
            Assert.AreEqual(1, ids.Count);
            Assert.AreEqual(expiredJobAd.Id, ids[0]);

            // Close them.

            foreach (var id in ids)
            {
                var closeJobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(id);
                _jobAdsCommand.CloseJobAd(closeJobAd);
            }

            // Check the status.

            var jobAd = _jobAdsCommand.GetJobAd<JobAdEntry>(activeJobAd.Id);
            Assert.AreEqual(JobAdStatus.Open, jobAd.Status);
            jobAd = _jobAdsCommand.GetJobAd<JobAdEntry>(expiredJobAd.Id);
            Assert.AreEqual(JobAdStatus.Closed, jobAd.Status);

            // Do it again.

            Assert.AreEqual(0, _jobAdsQuery.GetExpiredJobAdIds().Count);
        }

        [TestMethod]
        public void TestGetCreatedTimeJobAds()
        {
            var range = new DateTimeRange(DateTime.Today.AddDays(-1), DateTime.Today);
            var employer = CreateEmployer();

            // No job ads.

            var jobAdIds = _jobAdsQuery.GetOpenJobAdIds(range);
            Assert.AreEqual(0, jobAdIds.Count);

            // One job ad yesterday.

            CreateJobAd(employer, DateTime.Now);
            var yesterday = CreateJobAd(employer, DateTime.Now.AddDays(-1));
            CreateJobAd(employer, DateTime.Now.AddDays(-2));

            jobAdIds = _jobAdsQuery.GetOpenJobAdIds(range);
            Assert.AreEqual(1, jobAdIds.Count);
            Assert.AreEqual(yesterday.Id, jobAdIds[0]);

            // Three job ads yesterday.

            var yesterday2 = CreateJobAd(employer, DateTime.Now.AddDays(-1).AddSeconds(1));
            var yesterday3 = CreateJobAd(employer, DateTime.Now.AddDays(-1).AddSeconds(-2));

            jobAdIds = _jobAdsQuery.GetOpenJobAdIds(range);
            Assert.IsTrue(jobAdIds.CollectionEqual(new[] {yesterday.Id, yesterday2.Id, yesterday3.Id}));
        }

        private Employer CreateEmployer()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobPoster = new JobPoster { Id = employer.Id };
            _jobPostersCommand.CreateJobPoster(jobPoster);
            return employer;
        }

        private JobAd CreateJobAd(IEmployer employer, DateTime createdTime)
        {
            var jobAd = employer.CreateTestJobAd();
            jobAd.CreatedTime = createdTime;
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        /*
        private static void AssertJobAdIds(ICollection<JobAdSearchData> jobAds, params JobAd[] expectedAds)
        {
            Assert.AreEqual(expectedAds.Length, jobAds.Count);

            // Slow code, do not copy! O(n^2) time, but it's only for 3 items, so should be here.

            foreach (var expected in expectedAds)
            {
                var found = false;
                foreach (var actual in jobAds)
                {
                    if (actual.Id == expected.Id)
                    {
                        found = true;
                        AssertJobAdEquals(expected, actual);
                        break;
                    }
                }

                Assert.IsTrue(found, "Job ad with ID {0:b} was expected.", expected.Id);
            }
        }

        private static void AssertJobAdEquals(JobAd expected, JobAdSearchData actual)
        {
            Assert.AreEqual(expected.Description.PositionTitle, actual.PositionTitle);
            Assert.AreEqual(expected.Description.Salary, actual.Salary);
            Assert.IsTrue(expected.Description.Industries.NullableCollectionEqual(actual.Industries, CompareIndustries));
            Assert.AreEqual(expected.Integration.ExternalReferenceId, actual.ExternalReferenceId);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.ContactDetails.FirstName, actual.ContactDetails.FirstName);
            Assert.AreEqual(expected.ContactDetails.LastName, actual.ContactDetails.LastName);
            Assert.AreEqual(expected.ContactDetails.EmailAddress, actual.ContactDetails.EmailAddress);
            Assert.AreEqual(expected.ContactDetails.SecondaryEmailAddresses, actual.ContactDetails.SecondaryEmailAddresses);
            Assert.AreEqual(expected.ContactDetails.FaxNumber, actual.ContactDetails.FaxNumber);
            Assert.AreEqual(expected.ContactDetails.PhoneNumber, actual.ContactDetails.PhoneNumber);
        }
*/
    }
}