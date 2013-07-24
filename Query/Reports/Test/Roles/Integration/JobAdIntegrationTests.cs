using System;
using LinkMe.Domain;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Roles.Integration;
using LinkMe.Query.Reports.Roles.Integration.Commands;
using LinkMe.Query.Reports.Roles.Integration.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Reports.Test.Roles.Integration
{
    [TestClass]
    public class JobAdIntegrationTests
        : TestClass
    {
        private readonly IJobAdIntegrationReportsCommand _jobAdIntegrationReportsCommand = Resolve<IJobAdIntegrationReportsCommand>();
        private readonly IJobAdIntegrationReportsQuery _jobAdIntegrationReportsQuery = Resolve<IJobAdIntegrationReportsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCreateExportFeedEvent()
        {
            var integratorUserId = Guid.NewGuid();
            var evt = new JobAdExportFeedEvent { IntegratorUserId = integratorUserId, Success = true, JobAds = 5 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt);

            var report = _jobAdIntegrationReportsQuery.GetJobAdIntegrationReports(DayRange.Today);
            Assert.AreEqual(1, report.Count);
            Assert.IsTrue(report.ContainsKey(integratorUserId));
            var integratorReport = report[integratorUserId];
            Assert.AreEqual(1, integratorReport.ExportFeedReport.Events);
            Assert.AreEqual(1, integratorReport.ExportFeedReport.Successes);
            Assert.AreEqual(evt.JobAds, integratorReport.ExportFeedReport.JobAds);
        }

        [TestMethod]
        public void TestCreateExportFeedIdEvent()
        {
            var integratorUserId = Guid.NewGuid();
            var evt = new JobAdExportFeedIdEvent { IntegratorUserId = integratorUserId, Success = true, JobAds = 5 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt);

            var report = _jobAdIntegrationReportsQuery.GetJobAdIntegrationReports(DayRange.Today);
            Assert.AreEqual(1, report.Count);
            Assert.IsTrue(report.ContainsKey(integratorUserId));
            var integratorReport = report[integratorUserId];
            Assert.AreEqual(1, integratorReport.ExportFeedIdReport.Events);
            Assert.AreEqual(1, integratorReport.ExportFeedIdReport.Successes);
            Assert.AreEqual(evt.JobAds, integratorReport.ExportFeedIdReport.JobAds);
        }

        [TestMethod]
        public void TestCreateExportPostEvent()
        {
            var integratorUserId = Guid.NewGuid();
            var evt = new JobAdExportPostEvent { IntegratorUserId = integratorUserId, Success = true, JobAds = 5, Failed = 2, Posted = 3, Updated = 4 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt);

            var report = _jobAdIntegrationReportsQuery.GetJobAdIntegrationReports(DayRange.Today);
            Assert.AreEqual(1, report.Count);
            Assert.IsTrue(report.ContainsKey(integratorUserId));
            var integratorReport = report[integratorUserId];
            Assert.AreEqual(1, integratorReport.ExportPostReport.Events);
            Assert.AreEqual(1, integratorReport.ExportPostReport.Successes);
            Assert.AreEqual(evt.JobAds, integratorReport.ExportPostReport.JobAds);
            Assert.AreEqual(evt.Failed, integratorReport.ExportPostReport.Failed);
            Assert.AreEqual(evt.Posted, integratorReport.ExportPostReport.Posted);
            Assert.AreEqual(evt.Updated, integratorReport.ExportPostReport.Updated);
        }

        [TestMethod]
        public void TestCreateExportCloseEvent()
        {
            var integratorUserId = Guid.NewGuid();
            var evt = new JobAdExportCloseEvent { IntegratorUserId = integratorUserId, Success = true, JobAds = 5, Failed = 2, Closed = 3, NotFound = 4 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt);

            var report = _jobAdIntegrationReportsQuery.GetJobAdIntegrationReports(DayRange.Today);
            Assert.AreEqual(1, report.Count);
            Assert.IsTrue(report.ContainsKey(integratorUserId));
            var integratorReport = report[integratorUserId];
            Assert.AreEqual(1, integratorReport.ExportCloseReport.Events);
            Assert.AreEqual(1, integratorReport.ExportCloseReport.Successes);
            Assert.AreEqual(evt.JobAds, integratorReport.ExportCloseReport.JobAds);
            Assert.AreEqual(evt.Failed, integratorReport.ExportCloseReport.Failed);
            Assert.AreEqual(evt.Closed, integratorReport.ExportCloseReport.Closed);
            Assert.AreEqual(evt.NotFound, integratorReport.ExportCloseReport.NotFound);
        }

        [TestMethod]
        public void TestCreateImportPostEvent()
        {
            var integratorUserId = Guid.NewGuid();
            var evt = new JobAdImportPostEvent { IntegratorUserId = integratorUserId, Success = true, JobAds = 5, Failed = 2, Posted = 3, Updated = 4, Closed = 6, Duplicates = 7, Ignored = 8 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt);

            var report = _jobAdIntegrationReportsQuery.GetJobAdIntegrationReports(DayRange.Today);
            Assert.AreEqual(1, report.Count);
            Assert.IsTrue(report.ContainsKey(integratorUserId));
            var integratorReport = report[integratorUserId];
            Assert.AreEqual(1, integratorReport.ImportPostReport.Events);
            Assert.AreEqual(1, integratorReport.ImportPostReport.Successes);
            Assert.AreEqual(evt.JobAds, integratorReport.ImportPostReport.JobAds);
            Assert.AreEqual(evt.Failed, integratorReport.ImportPostReport.Failed);
            Assert.AreEqual(evt.Posted, integratorReport.ImportPostReport.Posted);
            Assert.AreEqual(evt.Updated, integratorReport.ImportPostReport.Updated);
            Assert.AreEqual(evt.Closed, integratorReport.ImportPostReport.Closed);
            Assert.AreEqual(evt.Duplicates, integratorReport.ImportPostReport.Duplicates);
            Assert.AreEqual(evt.Ignored, integratorReport.ImportPostReport.Ignored);
        }

        [TestMethod]
        public void TestCreateImportCloseEvent()
        {
            var integratorUserId = Guid.NewGuid();
            var evt = new JobAdImportCloseEvent { IntegratorUserId = integratorUserId, Success = true, JobAds = 5, Failed = 2, Closed = 3, NotFound = 4 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt);

            var report = _jobAdIntegrationReportsQuery.GetJobAdIntegrationReports(DayRange.Today);
            Assert.AreEqual(1, report.Count);
            Assert.IsTrue(report.ContainsKey(integratorUserId));
            var integratorReport = report[integratorUserId];
            Assert.AreEqual(1, integratorReport.ImportCloseReport.Events);
            Assert.AreEqual(1, integratorReport.ImportCloseReport.Successes);
            Assert.AreEqual(evt.JobAds, integratorReport.ImportCloseReport.JobAds);
            Assert.AreEqual(evt.Failed, integratorReport.ImportCloseReport.Failed);
            Assert.AreEqual(evt.Closed, integratorReport.ImportCloseReport.Closed);
            Assert.AreEqual(evt.NotFound, integratorReport.ImportCloseReport.NotFound);
        }

        [TestMethod]
        public void TestMultipleExportFeedEvents()
        {
            var integratorUserId = Guid.NewGuid();
            var evt1 = new JobAdExportFeedEvent { IntegratorUserId = integratorUserId, JobAds = 7 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt1);

            var evt2 = new JobAdExportFeedEvent { IntegratorUserId = integratorUserId };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt2);

            var report = _jobAdIntegrationReportsQuery.GetJobAdIntegrationReports(DayRange.Today);
            Assert.AreEqual(1, report.Count);
            Assert.IsTrue(report.ContainsKey(integratorUserId));
            var integratorReport = report[integratorUserId];
            Assert.AreEqual(2, integratorReport.ExportFeedReport.Events);
            Assert.AreEqual(evt1.JobAds + evt2.JobAds, integratorReport.ExportFeedReport.JobAds);
        }

        [TestMethod]
        public void TestMultipleIntegrators()
        {
            var integratorUserId1 = Guid.NewGuid();
            var integratorUserId2 = Guid.NewGuid();

            var evt1 = new JobAdExportFeedEvent { IntegratorUserId = integratorUserId1, JobAds = 3 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt1);

            var evt2 = new JobAdExportFeedEvent { IntegratorUserId = integratorUserId1, JobAds = 6 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt2);

            var evt3 = new JobAdExportFeedEvent { IntegratorUserId = integratorUserId2, JobAds = 12 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt3);

            var report = _jobAdIntegrationReportsQuery.GetJobAdIntegrationReports(DayRange.Today);
            Assert.AreEqual(2, report.Count);

            Assert.IsTrue(report.ContainsKey(integratorUserId1));
            var integratorReport = report[integratorUserId1];
            Assert.AreEqual(2, integratorReport.ExportFeedReport.Events);
            Assert.AreEqual(evt1.JobAds + evt2.JobAds, integratorReport.ExportFeedReport.JobAds);

            Assert.IsTrue(report.ContainsKey(integratorUserId2));
            integratorReport = report[integratorUserId2];
            Assert.AreEqual(1, integratorReport.ExportFeedReport.Events);
            Assert.AreEqual(evt3.JobAds, integratorReport.ExportFeedReport.JobAds);
        }

        [TestMethod]
        public void TestMultipleIntegratorsMultipleEvents()
        {
            var integratorUserId1 = Guid.NewGuid();
            var integratorUserId2 = Guid.NewGuid();

            // JobAdExportFeedEvent

            JobAdIntegrationEvent evt1 = new JobAdExportFeedEvent { IntegratorUserId = integratorUserId1, JobAds = 3 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt1);

            JobAdIntegrationEvent evt2 = new JobAdExportFeedEvent { IntegratorUserId = integratorUserId2, JobAds = 6 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt2);

            // JobAdExportFeedId

            evt1 = new JobAdExportFeedIdEvent { IntegratorUserId = integratorUserId1, JobAds = 3 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt1);

            evt2 = new JobAdExportFeedIdEvent { IntegratorUserId = integratorUserId2, JobAds = 6 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt2);

            // JobAdExportPost

            evt1 = new JobAdExportPostEvent { IntegratorUserId = integratorUserId1, JobAds = 3 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt1);

            evt2 = new JobAdExportPostEvent { IntegratorUserId = integratorUserId2, JobAds = 6 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt2);

            // JobAdExportClose

            evt1 = new JobAdExportCloseEvent { IntegratorUserId = integratorUserId1, JobAds = 3 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt1);

            evt2 = new JobAdExportCloseEvent { IntegratorUserId = integratorUserId2, JobAds = 6 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt2);

            // JobAdImportPost

            evt1 = new JobAdImportPostEvent { IntegratorUserId = integratorUserId1, JobAds = 3 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt1);

            evt2 = new JobAdImportPostEvent { IntegratorUserId = integratorUserId2, JobAds = 6 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt2);

            // JobAdImportCLose

            evt1 = new JobAdImportCloseEvent { IntegratorUserId = integratorUserId1, JobAds = 3 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt1);

            evt2 = new JobAdImportCloseEvent { IntegratorUserId = integratorUserId2, JobAds = 6 };
            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(evt2);

            var report = _jobAdIntegrationReportsQuery.GetJobAdIntegrationReports(DayRange.Today);
            Assert.AreEqual(2, report.Count);

            Assert.IsTrue(report.ContainsKey(integratorUserId1));
            var integratorReport = report[integratorUserId1];
            Assert.AreEqual(1, integratorReport.ExportFeedReport.Events);
            Assert.AreEqual(1, integratorReport.ExportFeedIdReport.Events);
            Assert.AreEqual(1, integratorReport.ExportPostReport.Events);
            Assert.AreEqual(1, integratorReport.ExportCloseReport.Events);
            Assert.AreEqual(1, integratorReport.ImportPostReport.Events);
            Assert.AreEqual(1, integratorReport.ImportCloseReport.Events);

            Assert.IsTrue(report.ContainsKey(integratorUserId2));
            integratorReport = report[integratorUserId2];
            Assert.AreEqual(1, integratorReport.ExportFeedReport.Events);
            Assert.AreEqual(1, integratorReport.ExportFeedIdReport.Events);
            Assert.AreEqual(1, integratorReport.ExportPostReport.Events);
            Assert.AreEqual(1, integratorReport.ExportCloseReport.Events);
            Assert.AreEqual(1, integratorReport.ImportPostReport.Events);
            Assert.AreEqual(1, integratorReport.ImportCloseReport.Events);
        }
    }
}
