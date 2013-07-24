using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.JobAds;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds.BlockLists
{
    [TestClass]
    public class AddJobAdsToJobAdBlockListsTests
        : JobAdBlockListsTests
    {
        [TestMethod]
        public void TestAddJobAdBlockList()
        {
            TestAddJobAd(GetBlockList);
        }

        [TestMethod]
        public void TestAddJobAdsBlockList()
        {
            TestAddJobAds(GetBlockList);
        }

        [TestMethod]
        public void TestAddExistingJobAdsBlockList()
        {
            TestAddExistingJobAds(GetBlockList);
        }

        [TestMethod, ExpectedException(typeof(JobAdBlockListsPermissionsException))]
        public void TestCannotAddToOtherBlockList()
        {
            TestAddToOtherBlockList(GetBlockList);
        }

        private void TestAddToOtherBlockList(Func<IMember, JobAdBlockList> getBlockList)
        {
            // Create member and employers.

            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);

            // Get the blockList for member 2.

            var blockList = getBlockList(member2);

            // Member 1 tries to add candidate to that blockList.

            _memberJobAdListsCommand.AddJobAdToBlockList(member1, blockList, jobAd.Id);
        }

        private void TestAddJobAd(Func<IMember, JobAdBlockList> getBlockList)
        {
            const int count = 3;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create blockList.

            var member = CreateMember(1);
            var blockList = getBlockList(member);

            // Add candidates.

            for (var index = 0; index < count; ++index)
            {
                var blockListCount = _memberJobAdListsCommand.AddJobAdToBlockList(member, blockList, jobAds[index].Id);
                Assert.AreEqual(index + 1, blockListCount);
            }

            // Assert.

            AssertBlockListJobAds(member, jobAds, new IJobAd[0]);
        }

        private void TestAddJobAds(Func<IMember, JobAdBlockList> getBlockList)
        {
            const int count = 3;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create blockList.

            var member = CreateMember(1);
            var blockList = getBlockList(member);

            // Add candidates.

            Assert.AreEqual(3, _memberJobAdListsCommand.AddJobAdsToBlockList(member, blockList, from j in jobAds select j.Id));

            // Assert.

            AssertBlockListJobAds(member, jobAds, new IJobAd[0]);
        }

        private void TestAddExistingJobAds(Func<IMember, JobAdBlockList> getBlockList)
        {
            const int count = 3;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create blockList.

            var member = CreateMember(1);
            var blockList = getBlockList(member);

            // Add a couple of candidates.

            Assert.AreEqual(1, _memberJobAdListsCommand.AddJobAdToBlockList(member, blockList, jobAds[0].Id));
            Assert.AreEqual(2, _memberJobAdListsCommand.AddJobAdToBlockList(member, blockList, jobAds[1].Id));
            Assert.AreEqual(2, _memberJobAdListsCommand.AddJobAdToBlockList(member, blockList, jobAds[0].Id));

            // Add all again.

            Assert.AreEqual(3, _memberJobAdListsCommand.AddJobAdsToBlockList(member, blockList, from j in jobAds select j.Id));

            // Assert.

            AssertBlockListJobAds(member, jobAds, new IJobAd[0]);
        }
    }
}