using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.JobAds;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds.FlagLists
{
    [TestClass]
    public class RemoveJobAdsFromJobAdFlagListsTests
        : JobAdFlagListsTests
    {
        [TestMethod]
        public void TestRemoveJobAdsFlaggedFlagList()
        {
            TestRemoveJobAds(GetFlaggedFlagList);
        }

        [TestMethod, ExpectedException(typeof(JobAdFlagListsPermissionsException))]
        public void TestCannotRemoveFromOtherFlaggedFlagList()
        {
            TestRemoveFromOtherFlagList(GetFlaggedFlagList);
        }

        [TestMethod]
        public void TestRemoveAllJobAdsFlaggedFlagList()
        {
            TestRemoveAllJobAds(GetFlaggedFlagList);
        }

        [TestMethod, ExpectedException(typeof(JobAdFlagListsPermissionsException))]
        public void TestCannotRemoveAllFromOtherFlaggedFlagList()
        {
            TestRemoveAllFromOtherFlagList(GetFlaggedFlagList);
        }

        private void TestRemoveFromOtherFlagList(Func<IMember, JobAdFlagList> getFlagList)
        {
            // Create member and employers.

            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);

            // Get the flagList for employer 2 and have them add a candidate.

            var flagList = getFlagList(member2);
            Assert.AreEqual(1, _memberJobAdListsCommand.AddJobAdToFlagList(member2, flagList, jobAd.Id));

            // Employer 1 tries to remove candidate from that flagList.

            Assert.AreEqual(1, _memberJobAdListsCommand.RemoveJobAdFromFlagList(member1, flagList, jobAd.Id));
        }

        private void TestRemoveAllFromOtherFlagList(Func<IMember, JobAdFlagList> getFlagList)
        {
            // Create member and employers.

            var employer = CreateEmployer(0);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer);

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);

            // Get the flagList for employer 2 and have them add a candidate.

            var flagList = getFlagList(member2);
            Assert.AreEqual(1, _memberJobAdListsCommand.AddJobAdToFlagList(member2, flagList, jobAd1.Id));
            Assert.AreEqual(2, _memberJobAdListsCommand.AddJobAdToFlagList(member2, flagList, jobAd2.Id));

            // Employer 1 tries to remove candidate from that flagList.

            Assert.AreEqual(1, _memberJobAdListsCommand.RemoveAllJobAdsFromFlagList(member1, flagList));
        }

        private void TestRemoveJobAds(Func<IMember, JobAdFlagList> getFlagList)
        {
            const int count = 3;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create flagList.

            var member = CreateMember(1);
            var flagList = getFlagList(member);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _memberJobAdListsCommand.AddJobAdsToFlagList(member, flagList, from j in jobAds select j.Id));
            Assert.AreEqual(1, _memberJobAdListsCommand.RemoveJobAdsFromFlagList(member, flagList, new[] { jobAds[0].Id, jobAds[2].Id }));

            // Assert.

            AssertFlagListEntries(member, jobAds.Skip(1).Take(1).ToArray(), jobAds.Take(1).Concat(jobAds.Skip(2).Take(1)).ToArray());

            // Remove candidates again.

            Assert.AreEqual(1, _memberJobAdListsCommand.RemoveJobAdsFromFlagList(member, flagList, new[] { jobAds[0].Id, jobAds[2].Id }));

            // Assert.

            AssertFlagListEntries(member, jobAds.Skip(1).Take(1).ToArray(), jobAds.Take(1).Concat(jobAds.Skip(2).Take(1)).ToArray());
        }

        private void TestRemoveAllJobAds(Func<IMember, JobAdFlagList> getFlagList)
        {
            const int count = 3;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create flagList.

            var member = CreateMember(1);
            var flagList = getFlagList(member);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _memberJobAdListsCommand.AddJobAdsToFlagList(member, flagList, from j in jobAds select j.Id));
            Assert.AreEqual(0, _memberJobAdListsCommand.RemoveAllJobAdsFromFlagList(member, flagList));

            // Assert.

            AssertFlagListEntries(member, new IJobAd[0], jobAds);

            // Remove candidates again.

            Assert.AreEqual(0, _memberJobAdListsCommand.RemoveAllJobAdsFromFlagList(member, flagList));

            // Assert.

            AssertFlagListEntries(member, new IJobAd[0], jobAds);
        }
    }
}