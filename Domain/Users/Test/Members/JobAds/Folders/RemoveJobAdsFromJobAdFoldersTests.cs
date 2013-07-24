using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds.Folders
{
    [TestClass]
    public class RemoveJobAdsFromJobAdFoldersTests
        : JobAdFoldersTests
    {
        [TestMethod]
        public void TestRemoveJobAdsFolder()
        {
            TestRemoveJobAds(GetFolder);
        }

        [TestMethod, ExpectedException(typeof(JobAdFoldersPermissionsException))]
        public void TestCannotRemoveFromOtherFolder()
        {
            TestRemoveFromOtherFolder(GetFolder);
        }

        [TestMethod]
        public void TestRemoveAllJobAdsFolder()
        {
            TestRemoveAllJobAds(GetFolder);
        }

        [TestMethod, ExpectedException(typeof(JobAdFoldersPermissionsException))]
        public void TestCannotRemoveAllFromOtherFolder()
        {
            TestRemoveAllFromOtherFolder(GetFolder);
        }

        private void TestRemoveFromOtherFolder(Func<IMember, int, JobAdFolder> getFolder)
        {
            // Create member and employers.

            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);

            // Get the folder for employer 2 and have them add a candidate.

            var folder = getFolder(member2, 1);
            Assert.AreEqual(1, _candidateListsCommand.AddJobAdToFolder(member2, folder, jobAd.Id));

            // Employer 1 tries to remove candidate from that folder.

            Assert.AreEqual(1, _candidateListsCommand.RemoveJobAdFromFolder(member1, folder, jobAd.Id));
        }

        private void TestRemoveAllFromOtherFolder(Func<IMember, int, JobAdFolder> getFolder)
        {
            // Create member and employers.

            var employer = CreateEmployer(0);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer);

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);

            // Get the folder for employer 2 and have them add a candidate.

            var folder = getFolder(member2, 1);
            Assert.AreEqual(1, _candidateListsCommand.AddJobAdToFolder(member2, folder, jobAd1.Id));
            Assert.AreEqual(2, _candidateListsCommand.AddJobAdToFolder(member2, folder, jobAd2.Id));

            // Employer 1 tries to remove candidate from that folder.

            Assert.AreEqual(1, _candidateListsCommand.RemoveAllJobAdsFromFolder(member1, folder));
        }

        private void TestRemoveJobAds(Func<IMember, int, JobAdFolder> getFolder)
        {
            const int count = 3;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create folder.

            var member = CreateMember(1);
            var folder = getFolder(member, 1);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _candidateListsCommand.AddJobAdsToFolder(member, folder, from j in jobAds select j.Id));
            Assert.AreEqual(1, _candidateListsCommand.RemoveJobAdsFromFolder(member, folder, new[] { jobAds[0].Id, jobAds[2].Id }));

            // Assert.

            AssertFolderEntries(member, folder, jobAds.Skip(1).Take(1).ToArray(), jobAds.Take(1).Concat(jobAds.Skip(2).Take(1)).ToArray());

            // Remove candidates again.

            Assert.AreEqual(1, _candidateListsCommand.RemoveJobAdsFromFolder(member, folder, new[] { jobAds[0].Id, jobAds[2].Id }));

            // Assert.

            AssertFolderEntries(member, folder, jobAds.Skip(1).Take(1).ToArray(), jobAds.Take(1).Concat(jobAds.Skip(2).Take(1)).ToArray());
        }

        private void TestRemoveAllJobAds(Func<IMember, int, JobAdFolder> getFolder)
        {
            const int count = 3;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create folder.

            var member = CreateMember(1);
            var folder = getFolder(member, 1);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _candidateListsCommand.AddJobAdsToFolder(member, folder, from j in jobAds select j.Id));
            Assert.AreEqual(0, _candidateListsCommand.RemoveAllJobAdsFromFolder(member, folder));

            // Assert.

            AssertFolderEntries(member, folder, new IJobAd[0], jobAds);

            // Remove candidates again.

            Assert.AreEqual(0, _candidateListsCommand.RemoveAllJobAdsFromFolder(member, folder));

            // Assert.

            AssertFolderEntries(member, folder, new IJobAd[0], jobAds);
        }
    }
}