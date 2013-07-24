using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds
{
    [TestClass]
    public class JobAdExportTests
        : JobAdsTests
    {
        private readonly IJobAdExportCommand _exportCommand = Resolve<JobAdExportCommand>();
        private readonly IDbConnectionFactory _connectionFactory = Resolve<IDbConnectionFactory>();

        [TestMethod]
        public void TestCanCreateId()
        {
            var jobAdId = CreateJobAd();

            _exportCommand.CreateJobSearchId(jobAdId, 1);

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                using (var reader = DatabaseHelper.ExecuteReader(connection,
                                                                 "SELECT jobAdId, jobSearchVacancyId FROM dbo.JobAdExport"))
                {
                    reader.Read();
                    Assert.AreEqual(jobAdId, reader.GetGuid(0));
                    Assert.AreEqual(1, reader.GetInt64(1));
                    Assert.IsFalse(reader.Read());
                }
            }
        }

        [TestMethod]
        public void TestCanCreateIdForExportedAd()
        {
            var jobAdId = CreateJobAd();
            DatabaseHelper.ExecuteNonQuery(_connectionFactory,
                                           "INSERT INTO dbo.JobAdExport(jobAdId) VALUES(@jobAdId)", "@jobAdId", jobAdId);

            _exportCommand.CreateJobSearchId(jobAdId, 1);

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                using (var reader = DatabaseHelper.ExecuteReader(connection,
                                                                 "SELECT jobAdId, jobSearchVacancyId FROM dbo.JobAdExport"))
                {
                    reader.Read();
                    Assert.AreEqual(jobAdId, reader.GetGuid(0));
                    Assert.AreEqual(1, reader.GetInt64(1));
                    Assert.IsFalse(reader.Read());
                }
            }
        }

        [TestMethod]
        public void TestCanDeleteId()
        {
            var jobAdId = CreateJobAd();

            _exportCommand.CreateJobSearchId(jobAdId, 1);
            _exportCommand.DeleteJobSearchId(jobAdId);

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                using (var reader = DatabaseHelper.ExecuteReader(connection,
                                                                 "SELECT jobAdId, jobSearchVacancyId FROM dbo.JobAdExport"))
                {
                    reader.Read();
                    Assert.AreEqual(jobAdId, reader.GetGuid(0));
                    Assert.IsTrue(reader.IsDBNull(1));
                    Assert.IsFalse(reader.Read());
                }
            }
        }

        [TestMethod]
        public void TestCanGetId()
        {
            var jobAdId = CreateJobAd();

            _exportCommand.CreateJobSearchId(jobAdId, 1);
            var vacancyId = _exportCommand.GetJobSearchId(jobAdId);

            Assert.IsTrue(vacancyId.HasValue);
            Assert.AreEqual(1, vacancyId.Value);
        }

        [TestMethod]
        public void TestCanGetNullWhenNoId()
        {
            var jobAdId = CreateJobAd();

            var vacancyId = _exportCommand.GetJobSearchId(jobAdId);

            Assert.IsFalse(vacancyId.HasValue);
        }

        private Guid CreateJobAd()
        {
            var employer = CreateEmployer();

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = "Manager",
                Description = { Content = "<b>Manager</b>" }
            };

            _jobAdsCommand.CreateJobAd(jobAd);
            return jobAd.Id;
        }
    }
}