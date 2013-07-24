using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds.BlockLists
{
    [TestClass]
    public abstract class JobAdBlockListsTests
        : TestClass
    {
        protected readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();
        protected readonly IJobAdBlockListsQuery _jobAdBlockListsQuery = Resolve<IJobAdBlockListsQuery>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        [TestInitialize]
        public void JobAdBlockListsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected Employer CreateEmployer(int index)
        {
            var employer = _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestVerifiedOrganisation(index));
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });
            return employer;
        }

        protected Member CreateMember(int index)
        {
            return _membersCommand.CreateTestMember(index);
        }

        protected JobAdBlockList GetBlockList(IMember member)
        {
            return _jobAdBlockListsQuery.GetBlockList(member);
        }

        protected void AssertBlockListJobAds(IMember member, ICollection<IJobAd> blockedJobAds, ICollection<IJobAd> notBlockedJobAds)
        {
            // IsBlocked

            foreach (var jobAd in blockedJobAds)
                Assert.IsTrue(_jobAdBlockListsQuery.IsBlocked(member, jobAd.Id));
            foreach (var jobAd in notBlockedJobAds)
                Assert.IsFalse(_jobAdBlockListsQuery.IsBlocked(member, jobAd.Id));

            // GetBlockedJobAdIds

            Assert.IsTrue((from m in blockedJobAds select m.Id).CollectionEqual(_jobAdBlockListsQuery.GetBlockedJobAdIds(member)));

            // GetBlockedCount

            Assert.AreEqual(blockedJobAds.Count, _jobAdBlockListsQuery.GetBlockedCount(member));
        }

        protected static void AssertBlockList(IMember member, JobAdBlockList expectedBlockList, JobAdBlockList blockList)
        {
            Assert.AreNotEqual(Guid.Empty, blockList.Id);
            Assert.AreNotEqual(DateTime.MinValue, blockList.CreatedTime);
            Assert.AreEqual(expectedBlockList.Name, blockList.Name);
            Assert.AreEqual(expectedBlockList.BlockListType, blockList.BlockListType);
            Assert.IsNull(blockList.Name);
            Assert.AreEqual(member.Id, blockList.MemberId);
            Assert.AreEqual(expectedBlockList.MemberId, blockList.MemberId);
        }
    }
}