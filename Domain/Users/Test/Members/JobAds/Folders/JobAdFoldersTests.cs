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

namespace LinkMe.Domain.Users.Test.Members.JobAds.Folders
{
    [TestClass]
    public abstract class JobAdFoldersTests
        : TestClass
    {
        protected readonly IJobAdFoldersCommand _jobAdFoldersCommand = Resolve<IJobAdFoldersCommand>();
        protected readonly IJobAdFoldersQuery _jobAdFoldersQuery = Resolve<IJobAdFoldersQuery>();
        protected readonly IMemberJobAdListsCommand _candidateListsCommand = Resolve<IMemberJobAdListsCommand>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        [TestInitialize]
        public void JobAdFoldersTestsInitialize()
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

        protected JobAdFolder GetFolder(IMember member, int index)
        {
            return _jobAdFoldersQuery.GetFolders(member)[index];
        }

        protected void AssertFolderEntries(IMember member, JobAdFolder folder, ICollection<IJobAd> inFolderJobAds, ICollection<IJobAd> notInFolderJobAds)
        {
            // IsInFolder

            foreach (var inFolderJobAd in inFolderJobAds)
                Assert.IsTrue(_jobAdFoldersQuery.IsInFolder(member, folder.Id, inFolderJobAd.Id));
            foreach (var notInFolderJobAd in notInFolderJobAds)
                Assert.IsFalse(_jobAdFoldersQuery.IsInFolder(member, folder.Id, notInFolderJobAd.Id));

            // GetInFolderJobAdIds

            Assert.IsTrue((from m in inFolderJobAds select m.Id).CollectionEqual(_jobAdFoldersQuery.GetInFolderJobAdIds(member, folder.Id)));

            // GetInFolderCounts.

            var counts = _jobAdFoldersQuery.GetInFolderCounts(member);
            Assert.AreEqual(inFolderJobAds.Count, counts.ContainsKey(folder.Id) ? counts[folder.Id] : 0);
        }

        protected static void AssertFolders(IList<JobAdFolder> folders)
        {
            // Should always have 6 folders.

            Assert.AreEqual(6, folders.Count);

            // 1 should be mobile folder.

            var mobileFolder = (from f in folders where f.FolderType == FolderType.Mobile select f).Single();
            AssertMobileFolder(mobileFolder);

            // Others should be private.

            var privateFolders = (from f in folders where f.FolderType != FolderType.Mobile select f).OrderBy(f => f.Name).ToList();
            Assert.AreEqual(5, privateFolders.Count);
            for (var index = 0; index < privateFolders.Count; ++index)
                AssertPrivateFolder(string.Format("Folder {0}", index + 1), privateFolders[index]);
        }

        protected static void AssertMobileFolder(JobAdFolder folder)
        {
            Assert.AreEqual(FolderType.Mobile, folder.FolderType);
            Assert.IsFalse(folder.IsDeleted);
            Assert.IsNull(folder.Name);
        }

        private static void AssertPrivateFolder(string expectedName, JobAdFolder folder)
        {
            Assert.AreEqual(FolderType.Private, folder.FolderType);
            Assert.IsFalse(folder.IsDeleted);
            Assert.AreEqual(expectedName, folder.Name);
        }

        protected static void AssertFolder(IMember member, JobAdFolder expectedFolder, JobAdFolder folder)
        {
            Assert.AreNotEqual(Guid.Empty, folder.Id);
            Assert.AreNotEqual(DateTime.MinValue, folder.CreatedTime);

            Assert.AreEqual(expectedFolder.Name, folder.Name);
            Assert.AreEqual(expectedFolder.FolderType, folder.FolderType);

            switch (expectedFolder.FolderType)
            {
                case FolderType.Private:
                    Assert.IsNotNull(folder.Name);
                    break;
            }

            Assert.AreEqual(member.Id, folder.MemberId);
            Assert.AreEqual(expectedFolder.MemberId, folder.MemberId);
        }
    }
}