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
    public class AddJobAdsToJobAdFoldersTests
        : JobAdFoldersTests
    {
        [TestMethod]
        public void TestAddJobAdFolder()
        {
            TestAddJobAd(GetFolder);
        }

        [TestMethod]
        public void TestAddJobAdsFolder()
        {
            TestAddJobAds(GetFolder);
        }

        [TestMethod]
        public void TestAddExistingJobAdsFolder()
        {
            TestAddExistingJobAds(GetFolder);
        }

        [TestMethod, ExpectedException(typeof(JobAdFoldersPermissionsException))]
        public void TestCannotAddToOtherFolder()
        {
            TestAddToOtherFolder(GetFolder);
        }

        [TestMethod]
        public void TestAddToMultipleFolders()
        {
            const int count = 10;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var member3 = CreateMember(3);

            var folder1 = GetFolder(member1, 1);
            var folder2 = GetFolder(member2, 2);
            var folder3 = GetFolder(member1, 3);
            var folder4 = GetFolder(member2, 4);
            var folder5 = GetFolder(member3, 4);

            Assert.AreEqual(2, _candidateListsCommand.AddJobAdsToFolder(member1, folder1, new[] { jobAds[0].Id, jobAds[1].Id }));
            Assert.AreEqual(1, _candidateListsCommand.AddJobAdsToFolder(member2, folder2, new[] { jobAds[2].Id }));
            Assert.AreEqual(1, _candidateListsCommand.AddJobAdsToFolder(member1, folder3, new[] { jobAds[3].Id }));
            Assert.AreEqual(2, _candidateListsCommand.AddJobAdsToFolder(member2, folder4, new[] { jobAds[6].Id, jobAds[7].Id }));
            Assert.AreEqual(2, _candidateListsCommand.AddJobAdsToFolder(member3, folder5, new[] { jobAds[8].Id, jobAds[9].Id }));

            AssertFolderEntries(member1, folder1, new[] { jobAds[0], jobAds[1] }, new[] { jobAds[2], jobAds[3], jobAds[4], jobAds[5], jobAds[6], jobAds[7], jobAds[8], jobAds[9] });
            AssertFolderEntries(member2, folder1, new IJobAd[0], new[] { jobAds[0], jobAds[1], jobAds[2], jobAds[3], jobAds[4], jobAds[5], jobAds[6], jobAds[7], jobAds[8], jobAds[9] });
            AssertFolderEntries(member3, folder1, new IJobAd[0], new[] { jobAds[0], jobAds[1], jobAds[2], jobAds[3], jobAds[4], jobAds[5], jobAds[6], jobAds[7], jobAds[8], jobAds[9] });

            AssertFolderEntries(member1, folder2, new IJobAd[0], new[] { jobAds[0], jobAds[1], jobAds[2], jobAds[3], jobAds[4], jobAds[5], jobAds[6], jobAds[7], jobAds[8], jobAds[9] });
            AssertFolderEntries(member2, folder2, new[] { jobAds[2] }, new[] { jobAds[0], jobAds[1], jobAds[3], jobAds[4], jobAds[5], jobAds[6], jobAds[7], jobAds[8], jobAds[9] });
            AssertFolderEntries(member3, folder2, new IJobAd[0], new[] { jobAds[0], jobAds[1], jobAds[2], jobAds[3], jobAds[4], jobAds[5], jobAds[6], jobAds[7], jobAds[8], jobAds[9] });

            AssertFolderEntries(member1, folder3, new[] { jobAds[3] }, new[] { jobAds[0], jobAds[1], jobAds[2], jobAds[4], jobAds[5], jobAds[6], jobAds[7], jobAds[8], jobAds[9] });
            AssertFolderEntries(member2, folder3, new IJobAd[0], new[] { jobAds[0], jobAds[1], jobAds[2], jobAds[3], jobAds[4], jobAds[5], jobAds[6], jobAds[7], jobAds[8], jobAds[9] });
            AssertFolderEntries(member3, folder3, new IJobAd[0], new[] { jobAds[0], jobAds[1], jobAds[2], jobAds[3], jobAds[4], jobAds[5], jobAds[6], jobAds[7], jobAds[8], jobAds[9] });

            AssertFolderEntries(member1, folder4, new IJobAd[0], new[] { jobAds[0], jobAds[1], jobAds[2], jobAds[3], jobAds[4], jobAds[5], jobAds[6], jobAds[7], jobAds[8], jobAds[9] });
            AssertFolderEntries(member2, folder4, new[] { jobAds[6], jobAds[7] }, new[] { jobAds[0], jobAds[1], jobAds[2], jobAds[3], jobAds[4], jobAds[5], jobAds[8], jobAds[9] });
            AssertFolderEntries(member3, folder4, new IJobAd[0], new[] { jobAds[0], jobAds[1], jobAds[2], jobAds[3], jobAds[4], jobAds[5], jobAds[6], jobAds[7], jobAds[8], jobAds[9] });

            AssertFolderEntries(member1, folder5, new IJobAd[0], new[] { jobAds[0], jobAds[1], jobAds[2], jobAds[3], jobAds[4], jobAds[5], jobAds[6], jobAds[7], jobAds[8], jobAds[9] });
            AssertFolderEntries(member2, folder5, new IJobAd[0], new[] { jobAds[0], jobAds[1], jobAds[2], jobAds[3], jobAds[4], jobAds[5], jobAds[6], jobAds[7], jobAds[8], jobAds[9] });
            AssertFolderEntries(member3, folder5, new[] { jobAds[8], jobAds[9] }, new[] { jobAds[0], jobAds[1], jobAds[2], jobAds[3], jobAds[4], jobAds[5], jobAds[6], jobAds[7] });
        }

        private void TestAddToOtherFolder(Func<IMember, int, JobAdFolder> getFolder)
        {
            // Create member and employers.

            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);

            // Get the folder for employer 2.

            var folder = getFolder(member2, 1);

            // Employer 1 tries to add candidate to that folder.

            _candidateListsCommand.AddJobAdToFolder(member1, folder, jobAd.Id);
        }

        private void TestAddJobAd(Func<IMember, int, JobAdFolder> getFolder)
        {
            const int count = 3;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create folder.

            var member = CreateMember(1);
            var folder = getFolder(member, 1);

            // Add candidates.

            for (var index = 0; index < count; ++index)
            {
                var folderCount = _candidateListsCommand.AddJobAdToFolder(member, folder, jobAds[index].Id);
                Assert.AreEqual(index + 1, folderCount);
            }

            // Assert.

            AssertFolderEntries(member, folder, jobAds, new IJobAd[0]);
        }

        private void TestAddJobAds(Func<IMember, int, JobAdFolder> getFolder)
        {
            const int count = 3;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create folder.

            var member = CreateMember(1);
            var folder = getFolder(member, 1);

            // Add candidates.

            Assert.AreEqual(3, _candidateListsCommand.AddJobAdsToFolder(member, folder, from m in jobAds select m.Id));

            // Assert.

            AssertFolderEntries(member, folder, jobAds, new IJobAd[0]);
        }

        private void TestAddExistingJobAds(Func<IMember, int, JobAdFolder> getFolder)
        {
            const int count = 3;
            var employer = CreateEmployer(0);
            var jobAds = new IJobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            // Create folder.

            var member = CreateMember(1);
            var folder = getFolder(member, 1);

            // Add a couple of candidates.

            Assert.AreEqual(1, _candidateListsCommand.AddJobAdToFolder(member, folder, jobAds[0].Id));
            Assert.AreEqual(2, _candidateListsCommand.AddJobAdToFolder(member, folder, jobAds[1].Id));
            Assert.AreEqual(2, _candidateListsCommand.AddJobAdToFolder(member, folder, jobAds[0].Id));

            // Add all again.

            Assert.AreEqual(3, _candidateListsCommand.AddJobAdsToFolder(member, folder, from m in jobAds select m.Id));

            // Assert.

            AssertFolderEntries(member, folder, jobAds, new IJobAd[0]);
        }
    }
}