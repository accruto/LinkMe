using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Query.Reports.Roles.JobAds.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Reports.Test.Roles.JobAds
{
    [TestClass]
    public class JobAdReportsTests
        : TestClass
    {
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        private readonly IJobAdReportsQuery _jobAdReportsQuery = Resolve<IJobAdReportsQuery>();
        private readonly IJobAdsRepository _jobAdsRepository = Resolve<IJobAdsRepository>();
        private readonly IJobAdViewsRepository _jobAdViewsRepository = Resolve<IJobAdViewsRepository>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        private readonly IApplicationsCommand _applicationsCommand = Resolve<IApplicationsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        [TestMethod]
        public void TestGetOpenJobAds()
        {
            var employer = CreateEmployer(1);
            _jobAdsCommand.PostTestJobAd(employer);
            _jobAdsCommand.PostTestJobAd(employer);
            Assert.AreEqual(2, _jobAdReportsQuery.GetOpenJobAds());

            // Create another.

            var closedJobAd = _jobAdsCommand.PostTestJobAd(employer);
            Assert.AreEqual(3, _jobAdReportsQuery.GetOpenJobAds());

            // Close it.

            _jobAdsCommand.CloseJobAd(closedJobAd);
            Assert.AreEqual(2, _jobAdReportsQuery.GetOpenJobAds());
        }

        [TestMethod]
        public void TestGetJobApplications()
        {
            var employer = CreateEmployer(1);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var application = new InternalApplication
            {
                PositionId = jobAd.Id,
                ApplicantId = Guid.NewGuid(),
                CoverLetterText = "Cover letter"
            };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            application.CreatedTime = DateTime.Now.AddDays(-1);
            _applicationsCommand.UpdateApplication(application);

            application = new InternalApplication
            {
                PositionId = jobAd.Id,
                ApplicantId = Guid.NewGuid(),
                CoverLetterText = "Cover letter"
            };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            application.CreatedTime = DateTime.Now.AddDays(-1);
            _applicationsCommand.UpdateApplication(application);

            Assert.AreEqual(2, _jobAdReportsQuery.GetInternalApplications(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestNoJobAds()
        {
            AssertReport(0, 0, 0, GetDateRange(), Guid.NewGuid());
        }

        [TestMethod]
        public void TestOpenBeforeCloseBefore()
        {
            var dateRange = GetDateRange();
            var employer = CreateEmployer(1);
            var jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.Start.Value.AddDays(-1));
            AssertReport(0, 0, 0, dateRange, employer.Id);
        }

        [TestMethod]
        public void TestOpenBeforeCloseInBetween()
        {
            var dateRange = GetDateRange();
            var employer = CreateEmployer(1);
            var jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(-1));
            AssertReport(0, 1, 0, dateRange, employer.Id);
        }

        [TestMethod]
        public void TestOpenBeforeCloseAfter()
        {
            var dateRange = GetDateRange();
            var employer = CreateEmployer(1);
            var jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));
            AssertReport(0, 0, 0, dateRange, employer.Id);
        }

        [TestMethod]
        public void TestOpenInBetweenCloseInBetween()
        {
            var dateRange = GetDateRange();
            var employer = CreateEmployer(1);
            var jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(1));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(-1));
            AssertReport(1, 1, 0, dateRange, employer.Id);
        }

        [TestMethod]
        public void TestOpenInBetweenCloseAfter()
        {
            var dateRange = GetDateRange();
            var employer = CreateEmployer(1);
            var jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(1));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));
            AssertReport(1, 0, 0, dateRange, employer.Id);
        }

        [TestMethod]
        public void TestOpenAfterCloseAfter()
        {
            var dateRange = GetDateRange();
            var employer = CreateEmployer(1);
            var jobAd = PostJobAd(employer, dateRange.End.Value.AddDays(1));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));
            AssertReport(0, 0, 0, dateRange, employer.Id);
        }

        [TestMethod]
        public void TestMultipleJobAds()
        {
            var dateRange = GetDateRange();
            var employer = CreateEmployer(1);

            var jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(-1));
            AssertReport(0, 1, 0, dateRange, employer.Id);

            jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));
            AssertReport(0, 1, 0, dateRange, employer.Id);

            jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(1));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(-1));
            AssertReport(1, 2, 0, dateRange, employer.Id);

            jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(1));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));
            AssertReport(2, 2, 0, dateRange, employer.Id);

            jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(-1));
            AssertReport(2, 3, 0, dateRange, employer.Id);

            jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));
            AssertReport(2, 3, 0, dateRange, employer.Id);

            jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(1));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(-1));
            AssertReport(3, 4, 0, dateRange, employer.Id);

            jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(1));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));
            AssertReport(4, 4, 0, dateRange, employer.Id);
        }

        [TestMethod]
        public void TestMultiplePosters()
        {
            var dateRange = GetDateRange();
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);

            var jobAd = PostJobAd(employer1, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(-1));
            AssertReport(0, 1, 0, dateRange, employer1.Id);
            AssertReport(0, 0, 0, dateRange, employer2.Id);
            AssertReport(0, 1, 0, dateRange, employer1.Id, employer2.Id);

            jobAd = PostJobAd(employer2, dateRange.Start.Value.AddDays(1));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(-1));
            AssertReport(0, 1, 0, dateRange, employer1.Id);
            AssertReport(1, 1, 0, dateRange, employer2.Id);
            AssertReport(1, 2, 0, dateRange, employer1.Id, employer2.Id);

            jobAd = PostJobAd(employer1, dateRange.Start.Value.AddDays(1));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));
            AssertReport(1, 1, 0, dateRange, employer1.Id);
            AssertReport(1, 1, 0, dateRange, employer2.Id);
            AssertReport(2, 2, 0, dateRange, employer1.Id, employer2.Id);

            jobAd = PostJobAd(employer2, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(-1));
            AssertReport(1, 1, 0, dateRange, employer1.Id);
            AssertReport(1, 2, 0, dateRange, employer2.Id);
            AssertReport(2, 3, 0, dateRange, employer1.Id, employer2.Id);

            jobAd = PostJobAd(employer1, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));
            AssertReport(1, 1, 0, dateRange, employer1.Id);
            AssertReport(1, 2, 0, dateRange, employer2.Id);
            AssertReport(2, 3, 0, dateRange, employer1.Id, employer2.Id);

            jobAd = PostJobAd(employer2, dateRange.Start.Value.AddDays(1));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(-1));
            AssertReport(1, 1, 0, dateRange, employer1.Id);
            AssertReport(2, 3, 0, dateRange, employer2.Id);
            AssertReport(3, 4, 0, dateRange, employer1.Id, employer2.Id);

            jobAd = PostJobAd(employer1, dateRange.Start.Value.AddDays(1));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));
            AssertReport(2, 1, 0, dateRange, employer1.Id);
            AssertReport(2, 3, 0, dateRange, employer2.Id);
            AssertReport(4, 4, 0, dateRange, employer1.Id, employer2.Id);
        }

        [TestMethod]
        public void TestViewBefore()
        {
            var dateRange = GetDateRange();
            var employer = CreateEmployer(1);
            var jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));
            ViewJobAd(jobAd, dateRange.Start.Value.AddDays(-3));
            AssertReport(0, 0, 0, dateRange, employer.Id);
        }

        [TestMethod]
        public void TestViewInBetween()
        {
            var dateRange = GetDateRange();
            var employer = CreateEmployer(1);
            var jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));
            ViewJobAd(jobAd, dateRange.Start.Value.AddDays(2));
            AssertReport(0, 0, 1, dateRange, employer.Id);
        }

        [TestMethod]
        public void TestViewAfter()
        {
            var dateRange = GetDateRange();
            var employer = CreateEmployer(1);
            var jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));
            ViewJobAd(jobAd, dateRange.End.Value.AddDays(2));
            AssertReport(0, 0, 0, dateRange, employer.Id);
        }

        [TestMethod]
        public void TestMultipleViews()
        {
            var dateRange = GetDateRange();
            var employer = CreateEmployer(1);
            var jobAd = PostJobAd(employer, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd, dateRange.End.Value.AddDays(7));

            ViewJobAd(jobAd, dateRange.Start.Value.AddDays(1));
            AssertReport(0, 0, 1, dateRange, employer.Id);

            ViewJobAd(jobAd, dateRange.Start.Value.AddDays(2));
            AssertReport(0, 0, 2, dateRange, employer.Id);

            ViewJobAd(jobAd, dateRange.Start.Value.AddDays(3));
            AssertReport(0, 0, 3, dateRange, employer.Id);

            ViewJobAd(jobAd, dateRange.Start.Value.AddDays(4));
            AssertReport(0, 0, 4, dateRange, employer.Id);

            ViewJobAd(jobAd, dateRange.Start.Value.AddDays(5));
            AssertReport(0, 0, 5, dateRange, employer.Id);

            ViewJobAd(jobAd, dateRange.Start.Value.AddDays(6));
            AssertReport(0, 0, 6, dateRange, employer.Id);
        }

        [TestMethod]
        public void TestMultipleJobAdViews()
        {
            var dateRange = GetDateRange();
            var employer = CreateEmployer(1);
            var jobAd1 = PostJobAd(employer, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd1, dateRange.End.Value.AddDays(7));
            var jobAd2 = PostJobAd(employer, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd2, dateRange.End.Value.AddDays(7));

            ViewJobAd(jobAd1, dateRange.Start.Value.AddDays(1));
            AssertReport(0, 0, 1, dateRange, employer.Id);

            ViewJobAd(jobAd2, dateRange.Start.Value.AddDays(2));
            AssertReport(0, 0, 2, dateRange, employer.Id);

            ViewJobAd(jobAd1, dateRange.Start.Value.AddDays(3));
            AssertReport(0, 0, 3, dateRange, employer.Id);

            ViewJobAd(jobAd2, dateRange.Start.Value.AddDays(4));
            AssertReport(0, 0, 4, dateRange, employer.Id);

            ViewJobAd(jobAd1, dateRange.Start.Value.AddDays(5));
            AssertReport(0, 0, 5, dateRange, employer.Id);

            ViewJobAd(jobAd2, dateRange.Start.Value.AddDays(6));
            AssertReport(0, 0, 6, dateRange, employer.Id);
        }

        [TestMethod]
        public void TestMultiplePostersViews()
        {
            var dateRange = GetDateRange();
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);
            var jobAd1 = PostJobAd(employer1, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd1, dateRange.End.Value.AddDays(7));
            var jobAd2 = PostJobAd(employer2, dateRange.Start.Value.AddDays(-7));
            CloseJobAd(jobAd2, dateRange.End.Value.AddDays(7));

            ViewJobAd(jobAd1, dateRange.Start.Value.AddDays(1));
            AssertReport(0, 0, 1, dateRange, employer1.Id);
            AssertReport(0, 0, 0, dateRange, employer2.Id);
            AssertReport(0, 0, 1, dateRange, employer1.Id, employer2.Id);

            ViewJobAd(jobAd2, dateRange.Start.Value.AddDays(2));
            AssertReport(0, 0, 1, dateRange, employer1.Id);
            AssertReport(0, 0, 1, dateRange, employer2.Id);
            AssertReport(0, 0, 2, dateRange, employer1.Id, employer2.Id);

            ViewJobAd(jobAd1, dateRange.Start.Value.AddDays(3));
            AssertReport(0, 0, 2, dateRange, employer1.Id);
            AssertReport(0, 0, 1, dateRange, employer2.Id);
            AssertReport(0, 0, 3, dateRange, employer1.Id, employer2.Id);

            ViewJobAd(jobAd2, dateRange.Start.Value.AddDays(4));
            AssertReport(0, 0, 2, dateRange, employer1.Id);
            AssertReport(0, 0, 2, dateRange, employer2.Id);
            AssertReport(0, 0, 4, dateRange, employer1.Id, employer2.Id);

            ViewJobAd(jobAd1, dateRange.Start.Value.AddDays(5));
            AssertReport(0, 0, 3, dateRange, employer1.Id);
            AssertReport(0, 0, 2, dateRange, employer2.Id);
            AssertReport(0, 0, 5, dateRange, employer1.Id, employer2.Id);

            ViewJobAd(jobAd2, dateRange.Start.Value.AddDays(6));
            AssertReport(0, 0, 3, dateRange, employer1.Id);
            AssertReport(0, 0, 3, dateRange, employer2.Id);
            AssertReport(0, 0, 6, dateRange, employer1.Id, employer2.Id);
        }

        private void ViewJobAd(IJobAd jobAd, DateTime time)
        {
            var viewing = new JobAdViewing
                              {
                                  Id = Guid.NewGuid(),
                                  ViewerId = Guid.NewGuid(),
                                  JobAdId = jobAd.Id,
                                  Time = time
                              };

            _jobAdViewsRepository.CreateJobAdViewing(viewing);
        }

        private static DateRange GetDateRange()
        {
            return new DateRange(DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10));
        }

        private JobAd PostJobAd(IEmployer employer, DateTime time)
        {
            var jobAd = employer.CreateTestJobAd();
            jobAd.CreatedTime = time;
            _jobAdsCommand.CreateJobAd(jobAd);

            // To get the timing right go directly to the repository.

            _jobAdsRepository.ChangeStatus(jobAd.Id, JobAdStatus.Open, null, time);
            jobAd.Status = JobAdStatus.Open;

            return jobAd;
        }

        private void CloseJobAd(JobAdEntry jobAd, DateTime time)
        {
            _jobAdsRepository.ChangeStatus(jobAd.Id, JobAdStatus.Closed, null, time);
            jobAd.Status = JobAdStatus.Closed;
        }

        private IEmployer CreateEmployer(int index)
        {
            var employer = _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(1));
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });
            return employer;
        }

        private void AssertReport(int opened, int closed, int viewed, DateRange dateRange, params Guid[] posterIds)
        {
            var report = _jobAdReportsQuery.GetJobAdReport(posterIds, dateRange);
            Assert.AreEqual(opened, report.OpenedJobAds.Count);
            Assert.AreEqual(closed, report.ClosedJobAds.Count);
            Assert.AreEqual(viewed, report.Totals.Views);
        }
    }
}