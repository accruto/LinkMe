using System;
using System.Globalization;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.JobAds;
using LinkMe.Query.Members;
using LinkMe.TaskRunner.Tasks.Maintenance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Maintenance
{
    [TestClass]
    public class UpdateIndexingTests
        : TaskTests
    {
        private readonly IDbConnectionFactory _connectionFactory = Resolve<IDbConnectionFactory>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdsRepository _jobAdsRepository = Resolve<IJobAdsRepository>();
        private readonly IJobAdSearchEngineRepository _jobAdSearchEngineRepository = Resolve<IJobAdSearchEngineRepository>();
        private readonly IMembersRepository _membersRepository = Resolve<IMembersRepository>();
        private readonly ICandidatesRepository _candidatesRepository = Resolve<ICandidatesRepository>();
        private readonly IMemberSearchEngineRepository _memberSearchEngineRepository = Resolve<IMemberSearchEngineRepository>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestUpdateJobAds()
        {
            var modifiedSince = DateTime.Now.AddMinutes(-10);

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Job ad with no indexing before modified since.

            var jobAd1 = CreateJobAd(employer, modifiedSince.AddMinutes(-10), null);

            // Job ad with no indexing after modified since.

            var jobAd2 = CreateJobAd(employer, modifiedSince.AddMinutes(5), null);

            // Job ad with indexing before modified since.

            var jobAd3 = CreateJobAd(employer, modifiedSince.AddMinutes(-10), modifiedSince.AddMinutes(-5));

            // Job ad with indexing after modified since.

            var jobAd4 = CreateJobAd(employer, modifiedSince.AddMinutes(-10), modifiedSince.AddMinutes(5));

            // Job ad with last updated time before modified since.

            var jobAd5 = CreateJobAd(employer, modifiedSince.AddMinutes(-5), modifiedSince.AddMinutes(-10));

            // Job ad with last updated time after modified since.

            var jobAd6 = CreateJobAd(employer, modifiedSince.AddMinutes(5), modifiedSince.AddMinutes(-10));

            Assert.IsTrue(new[] { jobAd4.Id }.CollectionEqual(_jobAdSearchEngineRepository.GetSearchModified(modifiedSince)));

            // Fix them.

            var task = new UpdateIndexingTask(_connectionFactory);
            task.ExecuteTask(new[]{10.ToString(CultureInfo.InvariantCulture)});

            Assert.IsTrue(new[] { jobAd1.Id, jobAd2.Id, jobAd4.Id, jobAd5.Id, jobAd6.Id }.CollectionEqual(_jobAdSearchEngineRepository.GetSearchModified(modifiedSince)));
        }

        [TestMethod]
        public void TestUpdateMembers()
        {
            var modifiedSince = DateTime.Now.AddMinutes(-10);
            var index = 0;

            // Member with no indexing before modified since.

            var member1 = CreateMember(++index, modifiedSince.AddMinutes(-10), null);

            // Member with no indexing after modified since.

            var member2 = CreateMember(++index, modifiedSince.AddMinutes(5), null);

            // Member with indexing before modified since.

            var member3 = CreateMember(++index, modifiedSince.AddMinutes(-10), modifiedSince.AddMinutes(-5));

            // Member with indexing after modified since.

            var member4 = CreateMember(++index, modifiedSince.AddMinutes(-10), modifiedSince.AddMinutes(5));

            // Member with last updated time before modified since.

            var member5 = CreateMember(++index, modifiedSince.AddMinutes(-5), modifiedSince.AddMinutes(-10));

            // Member with last updated time after modified since.

            var member6 = CreateMember(++index, modifiedSince.AddMinutes(5), modifiedSince.AddMinutes(-10));

            Assert.IsTrue(new[] { member4.Id }.CollectionEqual(_memberSearchEngineRepository.GetModified(modifiedSince)));

            // Fix them.

            var task = new UpdateIndexingTask(_connectionFactory);
            task.ExecuteTask(new[] { 10.ToString(CultureInfo.InvariantCulture) });

            Assert.IsTrue(new[] { member1.Id, member2.Id, member4.Id, member5.Id, member6.Id }.CollectionEqual(_memberSearchEngineRepository.GetModified(modifiedSince)));
        }

        private JobAd CreateJobAd(IEmployer employer, DateTime lastUpdatedTime, DateTime? modifiedTime)
        {
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            jobAd.CreatedTime = lastUpdatedTime;
            jobAd.LastUpdatedTime = lastUpdatedTime;
            _jobAdsRepository.UpdateJobAd(jobAd);

            // Delete what is already there.

            using (var dc = new MaintenanceDataContext(_connectionFactory.CreateConnection()))
            {
                var entity = (from i in dc.JobAdIndexingEntities where i.jobAdId == jobAd.Id select i).SingleOrDefault();
                if (entity != null)
                {
                    dc.JobAdIndexingEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }

            if (modifiedTime != null)
            {
                using (var dc = new MaintenanceDataContext(_connectionFactory.CreateConnection()))
                {
                    dc.JobAdIndexingEntities.InsertOnSubmit(new JobAdIndexingEntity { jobAdId = jobAd.Id, modifiedTime = modifiedTime.Value });
                    dc.SubmitChanges();
                }
            }

            return jobAd;
        }

        private Member CreateMember(int index, DateTime lastUpdatedTime, DateTime? modifiedTime)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            member.CreatedTime = lastUpdatedTime;
            member.LastUpdatedTime = lastUpdatedTime;
            _membersRepository.UpdateMember(member);

            var candidate = _candidatesRepository.GetCandidate(member.Id);
            candidate.LastUpdatedTime = lastUpdatedTime;
            _candidatesRepository.UpdateCandidate(candidate);

            // Delete what is already there.

            using (var dc = new MaintenanceDataContext(_connectionFactory.CreateConnection()))
            {
                var entity = (from i in dc.MemberIndexingEntities where i.memberId == member.Id select i).SingleOrDefault();
                if (entity != null)
                {
                    dc.MemberIndexingEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }

            if (modifiedTime != null)
            {
                using (var dc = new MaintenanceDataContext(_connectionFactory.CreateConnection()))
                {
                    dc.MemberIndexingEntities.InsertOnSubmit(new MemberIndexingEntity { memberId = member.Id, modifiedTime = modifiedTime.Value });
                    dc.SubmitChanges();
                }
            }

            return member;
        }
    }
}
