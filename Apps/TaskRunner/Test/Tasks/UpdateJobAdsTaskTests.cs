using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.TaskRunner.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks
{
    [TestClass]
    public class UpdateJobAdsTaskTests
        : TaskTests
    {
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        private readonly IJobAdsRepository _jobAdsRepository = Resolve<IJobAdsRepository>();

        private const string BusinessAnalyst = "business analyst";

        [TestMethod]
        public void TestCloseExpiredJobAds()
        {
            var employer = CreateEmployer();
            var jobAd1 = PostExpiryJobAd(employer, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-5));
            var jobAd2 = PostExpiryJobAd(employer, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(2));
            var jobAd3 = PostExpiryJobAd(employer, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-1));
            var jobAd4 = PostExpiryJobAd(employer, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(6));

            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAd>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAd>(jobAd2.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAd>(jobAd3.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAd>(jobAd4.Id).Status);

            new UpdateJobAdsTask(_jobAdsCommand, _jobAdsQuery).ExecuteTask();

            Assert.AreEqual(JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAd>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAd>(jobAd2.Id).Status);
            Assert.AreEqual(JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAd>(jobAd3.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAd>(jobAd4.Id).Status);

            new UpdateJobAdsTask(_jobAdsCommand, _jobAdsQuery).ExecuteTask();

            Assert.AreEqual(JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAd>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAd>(jobAd2.Id).Status);
            Assert.AreEqual(JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAd>(jobAd3.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAd>(jobAd4.Id).Status);
        }

        [TestMethod]
        public void TestRefreshJobAds()
        {
            var employer = CreateEmployer();
            var jobAd1 = PostRefreshJobAd(employer, DateTime.Now.AddDays(-20), null);
            var jobAd2 = PostRefreshJobAd(employer, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-4));
            var jobAd3 = PostRefreshJobAd(employer, DateTime.Now.AddDays(-20), null);
            var jobAd4 = PostRefreshJobAd(employer, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-20));
            var jobAd5 = PostRefreshJobAd(employer, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-8));

            Assert.AreEqual(null, _jobAdsQuery.GetLastRefreshTime(jobAd1.Id));
            var lastRefreshTime2 = _jobAdsQuery.GetLastRefreshTime(jobAd2.Id);
            Assert.IsNotNull(lastRefreshTime2);
            Assert.AreEqual(DateTime.Now.AddDays(-4).Date, lastRefreshTime2.Value.Date);
            Assert.AreEqual(null, _jobAdsQuery.GetLastRefreshTime(jobAd3.Id));
            var lastRefreshTime4 = _jobAdsQuery.GetLastRefreshTime(jobAd4.Id);
            Assert.IsNotNull(lastRefreshTime4);
            Assert.AreEqual(DateTime.Now.AddDays(-20).Date, lastRefreshTime4.Value.Date);
            var lastRefreshTime5 = _jobAdsQuery.GetLastRefreshTime(jobAd5.Id);
            Assert.IsNotNull(lastRefreshTime5);
            Assert.AreEqual(DateTime.Now.AddDays(-8).Date, lastRefreshTime5.Value.Date);

            new UpdateJobAdsTask(_jobAdsCommand, _jobAdsQuery).ExecuteTask();

            Assert.AreEqual(null, _jobAdsQuery.GetLastRefreshTime(jobAd1.Id));
            lastRefreshTime2 = _jobAdsQuery.GetLastRefreshTime(jobAd2.Id);
            Assert.IsNotNull(lastRefreshTime2);
            Assert.AreEqual(DateTime.Now.AddDays(-4).Date, lastRefreshTime2.Value.Date);
            Assert.AreEqual(null, _jobAdsQuery.GetLastRefreshTime(jobAd3.Id));
            lastRefreshTime4 = _jobAdsQuery.GetLastRefreshTime(jobAd4.Id);
            Assert.IsNotNull(lastRefreshTime4);
            Assert.AreEqual(DateTime.Now.Date, lastRefreshTime4.Value.Date);
            lastRefreshTime5 = _jobAdsQuery.GetLastRefreshTime(jobAd5.Id);
            Assert.IsNotNull(lastRefreshTime5);
            Assert.AreEqual(DateTime.Now.Date, lastRefreshTime5.Value.Date);

            new UpdateJobAdsTask(_jobAdsCommand, _jobAdsQuery).ExecuteTask();

            Assert.AreEqual(null, _jobAdsQuery.GetLastRefreshTime(jobAd1.Id));
            lastRefreshTime2 = _jobAdsQuery.GetLastRefreshTime(jobAd2.Id);
            Assert.IsNotNull(lastRefreshTime2);
            Assert.AreEqual(DateTime.Now.AddDays(-4).Date, lastRefreshTime2.Value.Date);
            Assert.AreEqual(null, _jobAdsQuery.GetLastRefreshTime(jobAd3.Id));
            lastRefreshTime4 = _jobAdsQuery.GetLastRefreshTime(jobAd4.Id);
            Assert.IsNotNull(lastRefreshTime4);
            Assert.AreEqual(DateTime.Now.Date, lastRefreshTime4.Value.Date);
            lastRefreshTime5 = _jobAdsQuery.GetLastRefreshTime(jobAd5.Id);
            Assert.IsNotNull(lastRefreshTime5);
            Assert.AreEqual(DateTime.Now.Date, lastRefreshTime5.Value.Date);

            _jobAdsCommand.CloseJobAd(jobAd4);

            Assert.AreEqual(null, _jobAdsQuery.GetLastRefreshTime(jobAd1.Id));
            lastRefreshTime2 = _jobAdsQuery.GetLastRefreshTime(jobAd2.Id);
            Assert.IsNotNull(lastRefreshTime2);
            Assert.AreEqual(DateTime.Now.AddDays(-4).Date, lastRefreshTime2.Value.Date);
            Assert.AreEqual(null, _jobAdsQuery.GetLastRefreshTime(jobAd3.Id));
            Assert.AreEqual(null, _jobAdsQuery.GetLastRefreshTime(jobAd4.Id));
            lastRefreshTime5 = _jobAdsQuery.GetLastRefreshTime(jobAd5.Id);
            Assert.IsNotNull(lastRefreshTime5);
            Assert.AreEqual(DateTime.Now.Date, lastRefreshTime5.Value.Date);
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        private JobAdEntry PostExpiryJobAd(IEmployer employer, DateTime createdTime, DateTime expiryTime)
        {
            var jobAd = employer.CreateTestJobAd(BusinessAnalyst);
            jobAd.CreatedTime = createdTime;
            _jobAdsCommand.PostJobAd(jobAd);
            jobAd.ExpiryTime = expiryTime;
            _jobAdsCommand.UpdateJobAd(jobAd);
            return jobAd;
        }

        private JobAdEntry PostRefreshJobAd(IEmployer employer, DateTime createdTime, DateTime? lastRefreshTime)
        {
            var jobAd = employer.CreateTestJobAd(BusinessAnalyst);
            jobAd.CreatedTime = createdTime;
            jobAd.Features = lastRefreshTime == null ? JobAdFeatures.None : JobAdFeatures.Refresh;
            _jobAdsCommand.PostJobAd(jobAd);
            if (lastRefreshTime != null)
                _jobAdsRepository.UpdateRefresh(jobAd.Id, lastRefreshTime.Value);
            return jobAd;
        }
    }
}
