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
    public class AddJobAdsToJobAdFlagListsTests
        : JobAdFlagListsTests
    {
        [TestMethod]
        public void TestAddJobAdFlaggedFlagList()
        {
            TestAddJobAd(GetFlaggedFlagList);
        }

        [TestMethod]
        public void TestAddJobAdsFlaggedFlagList()
        {
            TestAddJobAds(GetFlaggedFlagList);
        }

        [TestMethod]
        public void TestAddExistingJobAdsFlaggedFlagList()
        {
            TestAddExistingJobAds(GetFlaggedFlagList);
        }

        [TestMethod, ExpectedException(typeof(JobAdFlagListsPermissionsException))]
        public void TestCannotAddToOtherFlaggedFlagList()
        {
            TestAddToOtherFlagList(GetFlaggedFlagList);
        }

        private void TestAddToOtherFlagList(Func<IMember, JobAdFlagList> getFlagList)
        {
            // Create member and employers.

            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);

            // Get the flagList for employer 2.

            var flagList = getFlagList(member2);

            // Employer 1 tries to add candidate to that flagList.

            _memberJobAdListsCommand.AddJobAdToFlagList(member1, flagList, jobAd.Id);
        }

        private void TestAddJobAd(Func<IMember, JobAdFlagList> getFlagList)
        {
            const int count = 3;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create flagList.

            var member = CreateMember(1);
            var flagList = getFlagList(member);

            // Add candidates.

            for (var index = 0; index < count; ++index)
            {
                var flagListCount = _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, jobAds[index].Id);
                Assert.AreEqual(index + 1, flagListCount);
            }

            // Assert.

            AssertFlagListEntries(member, jobAds, new IJobAd[0]);
        }

        private void TestAddJobAds(Func<IMember, JobAdFlagList> getFlagList)
        {
            const int count = 3;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create flagList.

            var member = CreateMember(1);
            var flagList = getFlagList(member);

            // Add candidates.

            Assert.AreEqual(3, _memberJobAdListsCommand.AddJobAdsToFlagList(member, flagList, from j in jobAds select j.Id));

            // Assert.

            AssertFlagListEntries(member, jobAds, new IJobAd[0]);
        }

        private void TestAddExistingJobAds(Func<IMember, JobAdFlagList> getFlagList)
        {
            const int count = 3;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create flagList.

            var member = CreateMember(1);
            var flagList = getFlagList(member);

            // Add a couple of candidates.

            Assert.AreEqual(1, _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, jobAds[0].Id));
            Assert.AreEqual(2, _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, jobAds[1].Id));
            Assert.AreEqual(2, _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, jobAds[0].Id));

            // Add all again.

            Assert.AreEqual(3, _memberJobAdListsCommand.AddJobAdsToFlagList(member, flagList, from j in jobAds select j.Id));

            // Assert.

            AssertFlagListEntries(member, jobAds, new IJobAd[0]);
        }
    }
}