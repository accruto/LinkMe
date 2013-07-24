using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.Folders
{
    [TestClass]
    public abstract class CandidateFoldersTests
        : TestClass
    {
        private const string PrivateFolderNameFormat = "PrivateFolder{0}";
        private const string SharedFolderNameFormat = "SharedFolder{0}";

        protected readonly ICandidateFoldersCommand _candidateFoldersCommand = Resolve<ICandidateFoldersCommand>();
        protected readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();
        protected readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        [TestInitialize]
        public void CandidateFoldersTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected Employer CreateEmployer(int index)
        {
            return _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestVerifiedOrganisation(index));
        }

        protected Employer CreateEmployer(int index, IOrganisation organisation)
        {
            return _employersCommand.CreateTestEmployer(index, organisation);
        }

        protected Member CreateMember(int index)
        {
            return _membersCommand.CreateTestMember(index);
        }

        protected CandidateFolder CreatePrivateFolder(IEmployer employer, int index)
        {
            var folder = new CandidateFolder { Name = string.Format(PrivateFolderNameFormat, index) };
            _candidateFoldersCommand.CreatePrivateFolder(employer, folder);
            return folder;
        }

        protected CandidateFolder CreateSharedFolder(IEmployer employer, int index)
        {
            var folder = new CandidateFolder { Name = string.Format(SharedFolderNameFormat, index) };
            _candidateFoldersCommand.CreateSharedFolder(employer, folder);
            return folder;
        }

        protected CandidateFolder GetShortlistFolder(IEmployer employer)
        {
            return _candidateFoldersQuery.GetShortlistFolder(employer);
        }

        protected CandidateFolder GetShortlistFolder(IEmployer employer, int index)
        {
            return GetShortlistFolder(employer);
        }

        protected CandidateFolder GetMobileFolder(IEmployer employer)
        {
            return _candidateFoldersQuery.GetMobileFolder(employer);
        }

        protected CandidateFolder GetMobileFolder(IEmployer employer, int index)
        {
            return GetMobileFolder(employer);
        }

        protected void AssertFolderEntries(IEmployer employer, CandidateFolder folder, ICollection<Member> inFolderMembers, ICollection<Member> notInFolderMembers)
        {
            // IsInFolder

            foreach (var inFolderMember in inFolderMembers)
                Assert.IsTrue(_candidateFoldersQuery.IsInFolder(employer, folder.Id, inFolderMember.Id));
            foreach (var notInFolderMember in notInFolderMembers)
                Assert.IsFalse(_candidateFoldersQuery.IsInFolder(employer, folder.Id, notInFolderMember.Id));

            if (folder.FolderType == FolderType.Mobile)
            {
                foreach (var member in inFolderMembers)
                    Assert.IsTrue(_candidateFoldersQuery.IsInMobileFolder(employer, member.Id));
                foreach (var member in notInFolderMembers)
                    Assert.IsFalse(_candidateFoldersQuery.IsInMobileFolder(employer, member.Id));
            }

            // GetInFolderCandidateIds

            Assert.IsTrue((from m in inFolderMembers select m.Id).CollectionEqual(_candidateFoldersQuery.GetInFolderCandidateIds(employer, folder.Id)));
            if (folder.FolderType == FolderType.Mobile)
                Assert.IsTrue((from m in inFolderMembers select m.Id).CollectionEqual(_candidateFoldersQuery.GetInMobileFolderCandidateIds(employer, from m in inFolderMembers.Concat(notInFolderMembers) select m.Id)));

            // GetInFolderCounts.

            var counts = _candidateFoldersQuery.GetInFolderCounts(employer);
            if (counts.ContainsKey(folder.Id))
                Assert.AreEqual(inFolderMembers.Count, counts[folder.Id]);
            else
                Assert.AreEqual(inFolderMembers.Count, 0);
        }

        protected void AssertFolderEntries(IEmployer employer, ICollection<Member> inFolderMembers, ICollection<Member> notInFolderMembers)
        {
            // IsInFolder

            foreach (var inFolderMember in inFolderMembers)
                Assert.IsTrue(_candidateFoldersQuery.IsInFolder(employer, inFolderMember.Id));
            foreach (var notInFolderMember in notInFolderMembers)
                Assert.IsFalse(_candidateFoldersQuery.IsInFolder(employer, notInFolderMember.Id));

            // GetInFolderCandidateIds

            Assert.IsTrue((from m in inFolderMembers select m.Id).CollectionEqual(_candidateFoldersQuery.GetInFolderCandidateIds(employer)));
        }

        protected static void AssertFolders(IEmployer employer, CandidateFolder[] expectedFolders, IList<CandidateFolder> folders)
        {
            AssertFolders(employer, null, null, expectedFolders, folders);
        }

        protected static void AssertFolders(IEmployer employer, string shortlistFolderName, string mobileFolderName, CandidateFolder[] expectedFolders, IList<CandidateFolder> folders)
        {
            // Should always have a shortlist and favourites folder.

            Assert.AreEqual(expectedFolders.Length + 2, folders.Count);

            // Look for the shortlist folder.

            var shortlistFolder = new CandidateFolder { RecruiterId = employer.Id, FolderType = FolderType.Shortlist, Name = shortlistFolderName };
            AssertFolder(employer, shortlistFolder, (from f in folders where f.FolderType == FolderType.Shortlist select f).Single());

            // Look for the mobile folder.

            var mobileFolder = new CandidateFolder { RecruiterId = employer.Id, FolderType = FolderType.Mobile, Name = mobileFolderName };
            AssertFolder(employer, mobileFolder, (from f in folders where f.FolderType == FolderType.Mobile select f).Single());

            // Look for other folders.

            foreach (var expectedFolder in expectedFolders)
            {
                var expectedFolderId = expectedFolder.Id;
                AssertFolder(employer, expectedFolder, (from f in folders where f.Id == expectedFolderId select f).Single());
            }
        }

        protected static void AssertFolder(IEmployer employer, CandidateFolder expectedFolder, IList<CandidateFolder> folders)
        {
            Assert.AreEqual(1, folders.Count);
            AssertFolder(employer, expectedFolder, folders[0]);
        }

        protected static void AssertFolder(IEmployer employer, CandidateFolder expectedFolder, CandidateFolder folder)
        {
            Assert.AreNotEqual(Guid.Empty, folder.Id);
            Assert.AreNotEqual(DateTime.MinValue, folder.CreatedTime);

            if (!((expectedFolder.FolderType == FolderType.Shortlist || expectedFolder.FolderType == FolderType.Mobile) && expectedFolder.Id == Guid.Empty))
                Assert.AreEqual(expectedFolder.Id, folder.Id);
            Assert.AreEqual(expectedFolder.Name, folder.Name);
            Assert.AreEqual(expectedFolder.FolderType, folder.FolderType);

            switch (expectedFolder.FolderType)
            {
                case FolderType.Shared:
                case FolderType.Private:
                    Assert.IsNotNull(folder.Name);
                    break;
            }

            if (folder.FolderType == FolderType.Shared)
            {
                Assert.AreEqual(employer.Organisation.Id, folder.OrganisationId);
                Assert.AreEqual(expectedFolder.OrganisationId, folder.OrganisationId);
            }
            else
            {
                Assert.AreEqual(employer.Id, folder.RecruiterId);
                Assert.AreEqual(expectedFolder.RecruiterId, folder.RecruiterId);
                Assert.IsNull(folder.OrganisationId);
            }
        }
    }
}