using System;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Alerts
{
    [TestClass]
    public class JobAdSearchAlertsTests
        : TestClass
    {
        private const string KeywordsFormat = "Business analyst {0}";
        private const string NameFormat = "My search {0}";
        private readonly IJobAdSearchAlertsCommand _jobAdSearchAlertsCommand = Resolve<IJobAdSearchAlertsCommand>();
        private readonly IJobAdSearchAlertsQuery _jobAdSearchAlertsQuery = Resolve<IJobAdSearchAlertsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCreateJobAdSearch()
        {
            var ownerId = Guid.NewGuid();
            var search = CreateJobAdSearch(1);
            _jobAdSearchAlertsCommand.CreateJobAdSearch(ownerId, search);

            AssertSearch(search, _jobAdSearchAlertsQuery.GetJobAdSearch(search.Id));
            Assert.IsNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));
        }

        [TestMethod]
        public void TestCreateJobAdAlert()
        {
            var ownerId = Guid.NewGuid();
            var search = CreateJobAdSearch(1);
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(ownerId, search, DateTime.Now);

            AssertSearch(search, _jobAdSearchAlertsQuery.GetJobAdSearch(search.Id));
            Assert.IsNotNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));
        }

        [TestMethod]
        public void TestUpdateJobAdSearch()
        {
            // Create it.

            var ownerId = Guid.NewGuid();
            var search = CreateJobAdSearch(1);
            _jobAdSearchAlertsCommand.CreateJobAdSearch(ownerId, search);

            // Update it.

            search.Criteria = CreateCriteria(2);
            _jobAdSearchAlertsCommand.UpdateJobAdSearch(ownerId, search);

            AssertSearch(search, _jobAdSearchAlertsQuery.GetJobAdSearch(search.Id));
            Assert.IsNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));
        }

        [TestMethod]
        public void TestUpdateJobAdAlert()
        {
            // Create it.

            var ownerId = Guid.NewGuid();
            var search = CreateJobAdSearch(1);
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(ownerId, search, DateTime.Now);

            // Update it.

            search.Criteria = CreateCriteria(2);
            _jobAdSearchAlertsCommand.UpdateJobAdSearch(ownerId, search);

            AssertSearch(search, _jobAdSearchAlertsQuery.GetJobAdSearch(search.Id));
            Assert.IsNotNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));
        }

        [TestMethod]
        public void TestUpdateJobAdSearchToAlert()
        {
            // Create it.

            var ownerId = Guid.NewGuid();
            var search = CreateJobAdSearch(1);
            _jobAdSearchAlertsCommand.CreateJobAdSearch(ownerId, search);

            // Update it.

            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(ownerId, search.Id, DateTime.Now);

            AssertSearch(search, _jobAdSearchAlertsQuery.GetJobAdSearch(search.Id));
            Assert.IsNotNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));
        }

        [TestMethod]
        public void TestDeleteJobAdSearch()
        {
            // Create it.

            var ownerId = Guid.NewGuid();
            var search = CreateJobAdSearch(1);
            _jobAdSearchAlertsCommand.CreateJobAdSearch(ownerId, search);

            // Delete it.

            _jobAdSearchAlertsCommand.DeleteJobAdSearch(ownerId, search.Id);

            Assert.IsNull(_jobAdSearchAlertsQuery.GetJobAdSearch(search.Id));
            Assert.IsNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));
        }

        [TestMethod]
        public void TestDeleteJobAdAlert()
        {
            // Create it.

            var ownerId = Guid.NewGuid();
            var search = CreateJobAdSearch(1);
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(ownerId, search, DateTime.Now);

            // Delete it.

            _jobAdSearchAlertsCommand.DeleteJobAdSearch(ownerId, search.Id);

            Assert.IsNull(_jobAdSearchAlertsQuery.GetJobAdSearch(search.Id));
            Assert.IsNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));
        }

        [TestMethod]
        public void TestDeleteJustJobAdAlert()
        {
            // Create it.

            var ownerId = Guid.NewGuid();
            var search = CreateJobAdSearch(1);
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(ownerId, search, DateTime.Now);

            // Delete it.

            _jobAdSearchAlertsCommand.DeleteJobAdSearchAlert(ownerId, search.Id);

            AssertSearch(search, _jobAdSearchAlertsQuery.GetJobAdSearch(search.Id));
            Assert.IsNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));
        }

        private static void AssertSearch(JobAdSearch expectedSearch, JobAdSearch search)
        {
            Assert.AreEqual(expectedSearch.Criteria, search.Criteria);
            Assert.AreEqual(expectedSearch.Name, search.Name);
            Assert.AreEqual(expectedSearch.Id, search.Id);
            Assert.AreEqual(expectedSearch.OwnerId, search.OwnerId);
        }

        private static JobAdSearch CreateJobAdSearch(int index)
        {
            return new JobAdSearch
            {
                Criteria = CreateCriteria(index),
                Name = string.Format(NameFormat, index),
            };
        }

        private static JobAdSearchCriteria CreateCriteria(int index)
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(string.Format(KeywordsFormat, index));
            return criteria;
        }
    }
}
