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
    public class RemoveJobAdsFromJobAdBlockListsTests
        : JobAdBlockListsTests
    {
        [TestMethod]
        public void TestRemoveJobAdsBlockList()
        {
            TestRemoveJobAds(GetBlockList);
        }

        [TestMethod, ExpectedException(typeof(JobAdBlockListsPermissionsException))]
        public void TestCannotRemoveFromOtherBlockList()
        {
            TestRemoveFromOtherBlockList(GetBlockList);
        }

        [TestMethod]
        public void TestRemoveAllJobAdsBlockList()
        {
            TestRemoveAllJobAds(GetBlockList);
        }

        [TestMethod, ExpectedException(typeof(JobAdBlockListsPermissionsException))]
        public void TestCannotRemoveAllFromOtherBlockList()
        {
            TestRemoveAllFromOtherBlockList(GetBlockList);
        }

        [TestMethod]
        public void TestRemoveAllJobAdsBlockListByType()
        {
            TestRemoveAllJobAdsByType(GetBlockList);
        }

        private void TestRemoveFromOtherBlockList(Func<IMember, JobAdBlockList> getBlockList)
        {
            // Create member and employers.

            var jobAd = CreateJobAds(1)[0];
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);

            // Get the blockList for employer 2 and have them add a candidate.

            var blockList = getBlockList(member2);
            Assert.AreEqual(1, _memberJobAdListsCommand.AddJobAdToBlockList(member2, blockList, jobAd.Id));

            // Employer 1 tries to remove candidate from that blockList.

            Assert.AreEqual(1, _memberJobAdListsCommand.RemoveJobAdFromBlockList(member1, blockList, jobAd.Id));
        }

        private void TestRemoveAllFromOtherBlockList(Func<IMember, JobAdBlockList> getBlockList)
        {
            // Create member and employers.

            var jobAds = CreateJobAds(2);
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);

            // Get the blockList for employer 2 and have them add a candidate.

            var blockList = getBlockList(member2);
            Assert.AreEqual(1, _memberJobAdListsCommand.AddJobAdToBlockList(member2, blockList, jobAds[0].Id));
            Assert.AreEqual(2, _memberJobAdListsCommand.AddJobAdToBlockList(member2, blockList, jobAds[1].Id));

            // Employer 1 tries to remove candidate from that blockList.

            Assert.AreEqual(1, _memberJobAdListsCommand.RemoveAllJobAdsFromBlockList(member1, blockList));
        }

        private void TestRemoveJobAds(Func<IMember, JobAdBlockList> getBlockList)
        {
            var jobAds = CreateJobAds(3);

            // Create blockList.

            var member = CreateMember(1);
            var blockList = getBlockList(member);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _memberJobAdListsCommand.AddJobAdsToBlockList(member, blockList, from j in jobAds select j.Id));
            Assert.AreEqual(1, _memberJobAdListsCommand.RemoveJobAdsFromBlockList(member, blockList, new[] { jobAds[0].Id, jobAds[2].Id }));

            // Assert.

            AssertBlockListJobAds(member, jobAds.Skip(1).Take(1).ToArray(), jobAds.Take(1).Concat(jobAds.Skip(2).Take(1)).ToArray());

            // Remove candidates again.

            Assert.AreEqual(1, _memberJobAdListsCommand.RemoveJobAdsFromBlockList(member, blockList, new[] { jobAds[0].Id, jobAds[2].Id }));

            // Assert.

            AssertBlockListJobAds(member, jobAds.Skip(1).Take(1).ToArray(), jobAds.Take(1).Concat(jobAds.Skip(2).Take(1)).ToArray());
        }

        private void TestRemoveAllJobAds(Func<IMember, JobAdBlockList> getBlockList)
        {
            var jobAds = CreateJobAds(3);

            // Create blockList.

            var member = CreateMember(1);
            var blockList = getBlockList(member);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _memberJobAdListsCommand.AddJobAdsToBlockList(member, blockList, from j in jobAds select j.Id));
            Assert.AreEqual(0, _memberJobAdListsCommand.RemoveAllJobAdsFromBlockList(member, blockList));

            // Assert.

            AssertBlockListJobAds(member, new IJobAd[0], jobAds);

            // Remove candidates again.

            Assert.AreEqual(0, _memberJobAdListsCommand.RemoveAllJobAdsFromBlockList(member, blockList));

            // Assert.

            AssertBlockListJobAds(member, new IJobAd[0], jobAds);
        }

        private void TestRemoveAllJobAdsByType(Func<IMember, JobAdBlockList> getBlockList)
        {
            var jobAds = CreateJobAds(3);

            // Create blockList.

            var member = CreateMember(1);
            var blockList = getBlockList(member);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _memberJobAdListsCommand.AddJobAdsToBlockList(member, blockList, from j in jobAds select j.Id));
            Assert.AreEqual(0, _memberJobAdListsCommand.RemoveAllJobAdsFromBlockList(member, blockList));

            // Assert.

            AssertBlockListJobAds(member, new IJobAd[0], jobAds);

            // Remove candidates again.

            Assert.AreEqual(0, _memberJobAdListsCommand.RemoveAllJobAdsFromBlockList(member, blockList));

            // Assert.

            AssertBlockListJobAds(member, new IJobAd[0], jobAds);
        }

        private IJobAd[] CreateJobAds(int count)
        {
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);
            return jobAds;
        }
    }
}