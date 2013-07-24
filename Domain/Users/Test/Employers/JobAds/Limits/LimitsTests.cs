using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.JobAds;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Limits
{
    [TestClass]
    public class LimitsTests
        : TestClass
    {
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        private readonly IEmployerJobAdsCommand _employerJobAdsCommand = Resolve<IEmployerJobAdsCommand>();
        private readonly IEmployerJobAdsQuery _employerJobAdsQuery = Resolve<IEmployerJobAdsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private const int DailyLimit = 50;
        private const string TitleFormat = "This is the title {0}";
        private const string ContentFormat = "This is the content {0}";

        [TestMethod, ExpectedException(typeof(TooManyJobAdsException))]
        public void TestDailyLimit()
        {
            var employer = CreateEmployer();
            var counts = _jobAdsQuery.GetOpenJobAdCounts(employer.Id);

            Assert.AreEqual(0, counts.Item1);
            Assert.AreEqual(0, counts.Item2);

            JobAd jobAd;

            try
            {
                for (var index = 0; index < DailyLimit; ++index)
                {
                    jobAd = CreateJobAd(employer.Id, 0);

                    // Create and open the job ad.

                    _employerJobAdsCommand.CreateJobAd(employer, jobAd);
                    _employerJobAdsCommand.OpenJobAd(employer, jobAd, true);

                    // Check.

                    AssertJobAd(jobAd, _employerJobAdsQuery.GetJobAd<JobAd>(employer, jobAd.Id));
                }
            }
            catch (Exception)
            {
                Assert.Fail("Too soon.");
            }

            // One more.

            jobAd = CreateJobAd(employer.Id, 0);

            // Create and open the job ad.

            _employerJobAdsCommand.CreateJobAd(employer, jobAd);
            _employerJobAdsCommand.OpenJobAd(employer, jobAd, true);
        }

        private Employer CreateEmployer()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });

            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });

            return employer;
        }

        private static JobAd CreateJobAd(Guid posterId, int index)
        {
            return new JobAd
            {
                PosterId = posterId,
                Title = string.Format(TitleFormat, index),
                Description =
                {
                    Content = string.Format(ContentFormat, index),
                },
            };
        }

        private static void AssertJobAd(JobAd expectedJobAd, JobAd jobAd)
        {
            Assert.AreEqual(expectedJobAd.Id, jobAd.Id);
            Assert.AreEqual(expectedJobAd.PosterId, jobAd.PosterId);
            Assert.AreEqual(expectedJobAd.Title, jobAd.Title);
            Assert.AreEqual(expectedJobAd.Description.Content, jobAd.Description.Content);
        }
    }
}
