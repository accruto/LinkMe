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
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates
{
    [TestClass]
    public class ListsTests
        : TestClass
    {
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ICandidateFoldersCommand _candidateFoldersCommand = Resolve<ICandidateFoldersCommand>();
        private readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();
        private readonly ICandidateFlagListsQuery _candidateFlagListsQuery = Resolve<ICandidateFlagListsQuery>();
        private readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();

        private CandidateBlockList _temporaryBlockList;
        private CandidateBlockList _permanentBlockList;
        private CandidateFlagList _flagList;
        private CandidateFolder _privateFolder;
        private CandidateFolder _sharedFolder;
        private CandidateFolder _shortlistFolder;
        private CandidateFolder _mobileFolder;

        [TestInitialize]
        public void ListsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _temporaryBlockList = null;
            _permanentBlockList = null;
            _flagList = null;
            _privateFolder = null;
            _sharedFolder = null;
            _shortlistFolder = null;
            _mobileFolder = null;
        }

        [TestMethod]
        public void TestAddCandidateToPermanentBlocklist()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(0);

            CreateLists(employer);
            AssertCounts(employer, 0, 0, 0, 0, 0, 0, 0);

            // Add member to lists.

            _candidateListsCommand.AddCandidateToBlockList(employer, _temporaryBlockList, member.Id);
            _candidateListsCommand.AddCandidateToFlagList(employer, _flagList, member.Id);
            _candidateListsCommand.AddCandidateToFolder(employer, _privateFolder, member.Id);
            _candidateListsCommand.AddCandidateToFolder(employer, _sharedFolder, member.Id);
            _candidateListsCommand.AddCandidateToFolder(employer, _shortlistFolder, member.Id);
            _candidateListsCommand.AddCandidateToFolder(employer, _mobileFolder, member.Id);

            AssertCounts(employer, 0, 1, 1, 1, 1, 1, 1);

            // Add to permanent blocklist.

            _candidateListsCommand.AddCandidateToBlockList(employer, _permanentBlockList, member.Id);

            // Check lists are updated.

            AssertCounts(employer, 1, 0, 0, 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestAddCandidateToTemporaryBlocklist()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(0);

            CreateLists(employer);
            AssertCounts(employer, 0, 0, 0, 0, 0, 0, 0);

            // Add member to lists.

            _candidateListsCommand.AddCandidateToFlagList(employer, _flagList, member.Id);
            _candidateListsCommand.AddCandidateToFolder(employer, _privateFolder, member.Id);
            _candidateListsCommand.AddCandidateToFolder(employer, _sharedFolder, member.Id);
            _candidateListsCommand.AddCandidateToFolder(employer, _shortlistFolder, member.Id);
            _candidateListsCommand.AddCandidateToFolder(employer, _mobileFolder, member.Id);

            AssertCounts(employer, 0, 0, 1, 1, 1, 1, 1);

            // Add to temporary blocklist.

            _candidateListsCommand.AddCandidateToBlockList(employer, _temporaryBlockList, member.Id);

            // Check lists are updated.

            AssertCounts(employer, 0, 1, 1, 1, 1, 1, 1);
        }

        [TestMethod]
        public void TestAddCandidateToPrivateFolderInPermanentBlockList()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(0);

            CreateLists(employer);
            AssertCounts(employer, 0, 0, 0, 0, 0, 0, 0);

            // Add member to lists.

            _candidateListsCommand.AddCandidateToBlockList(employer, _permanentBlockList, member.Id);

            AssertCounts(employer, 1, 0, 0, 0, 0, 0, 0);

            // Add to private folder.

            _candidateListsCommand.AddCandidateToFolder(employer, _privateFolder, member.Id);

            // Check lists are updated.

            AssertCounts(employer, 0, 0, 0, 1, 0, 0, 0);
        }

        [TestMethod]
        public void TestAddCandidateToShortlistFolderInPermanentBlockList()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(0);

            CreateLists(employer);
            AssertCounts(employer, 0, 0, 0, 0, 0, 0, 0);

            // Add member to lists.

            _candidateListsCommand.AddCandidateToBlockList(employer, _permanentBlockList, member.Id);

            AssertCounts(employer, 1, 0, 0, 0, 0, 0, 0);

            // Add to private folder.

            _candidateListsCommand.AddCandidateToFolder(employer, _shortlistFolder, member.Id);

            // Check lists are updated.

            AssertCounts(employer, 0, 0, 0, 0, 1, 0, 0);
        }

        [TestMethod]
        public void TestAddCandidateToMobileFolderInPermanentBlockList()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(0);

            CreateLists(employer);
            AssertCounts(employer, 0, 0, 0, 0, 0, 0, 0);

            // Add member to lists.

            _candidateListsCommand.AddCandidateToBlockList(employer, _permanentBlockList, member.Id);

            AssertCounts(employer, 1, 0, 0, 0, 0, 0, 0);

            // Add to private folder.

            _candidateListsCommand.AddCandidateToFolder(employer, _mobileFolder, member.Id);

            // Check lists are updated.

            AssertCounts(employer, 0, 0, 0, 0, 0, 1, 0);
        }

        [TestMethod]
        public void TestAddCandidateToSharedFolderInPermanentBlockList()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(0);

            CreateLists(employer);
            AssertCounts(employer, 0, 0, 0, 0, 0, 0, 0);

            // Add member to lists.

            _candidateListsCommand.AddCandidateToBlockList(employer, _permanentBlockList, member.Id);

            AssertCounts(employer, 1, 0, 0, 0, 0, 0, 0);

            // Add to private folder.

            _candidateListsCommand.AddCandidateToFolder(employer, _sharedFolder, member.Id);

            // Check lists are updated.

            AssertCounts(employer, 0, 0, 0, 0, 0, 0, 1);
        }

        [TestMethod]
        public void TestAddCandidateToFlagListInPermanentBlockList()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(0);

            CreateLists(employer);
            AssertCounts(employer, 0, 0, 0, 0, 0, 0, 0);

            // Add member to lists.

            _candidateListsCommand.AddCandidateToBlockList(employer, _permanentBlockList, member.Id);

            AssertCounts(employer, 1, 0, 0, 0, 0, 0, 0);

            // Add to private folder.

            _candidateListsCommand.AddCandidateToFlagList(employer, _flagList, member.Id);

            // Check lists are updated.

            AssertCounts(employer, 0, 0, 1, 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestAddCandidateToPrivateFolderInTemporaryBlockList()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(0);

            CreateLists(employer);
            AssertCounts(employer, 0, 0, 0, 0, 0, 0, 0);

            // Add member to lists.

            _candidateListsCommand.AddCandidateToBlockList(employer, _temporaryBlockList, member.Id);

            AssertCounts(employer, 0, 1, 0, 0, 0, 0, 0);

            // Add to private folder.

            _candidateListsCommand.AddCandidateToFolder(employer, _privateFolder, member.Id);

            // Check lists are updated.

            AssertCounts(employer, 0, 1, 0, 1, 0, 0, 0);
        }

        [TestMethod]
        public void TestAddCandidateToShortlistFolderInTemporaryBlockList()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(0);

            CreateLists(employer);
            AssertCounts(employer, 0, 0, 0, 0, 0, 0, 0);

            // Add member to lists.

            _candidateListsCommand.AddCandidateToBlockList(employer, _temporaryBlockList, member.Id);

            AssertCounts(employer, 0, 1, 0, 0, 0, 0, 0);

            // Add to private folder.

            _candidateListsCommand.AddCandidateToFolder(employer, _shortlistFolder, member.Id);

            // Check lists are updated.

            AssertCounts(employer, 0, 1, 0, 0, 1, 0, 0);
        }

        [TestMethod]
        public void TestAddCandidateToMobileFolderInTemporaryBlockList()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(0);

            CreateLists(employer);
            AssertCounts(employer, 0, 0, 0, 0, 0, 0, 0);

            // Add member to lists.

            _candidateListsCommand.AddCandidateToBlockList(employer, _temporaryBlockList, member.Id);

            AssertCounts(employer, 0, 1, 0, 0, 0, 0, 0);

            // Add to private folder.

            _candidateListsCommand.AddCandidateToFolder(employer, _mobileFolder, member.Id);

            // Check lists are updated.

            AssertCounts(employer, 0, 1, 0, 0, 0, 1, 0);
        }

        [TestMethod]
        public void TestAddCandidateToSharedFolderInTemporaryBlockList()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(0);

            CreateLists(employer);
            AssertCounts(employer, 0, 0, 0, 0, 0, 0, 0);

            // Add member to lists.

            _candidateListsCommand.AddCandidateToBlockList(employer, _temporaryBlockList, member.Id);

            AssertCounts(employer, 0, 1, 0, 0, 0, 0, 0);

            // Add to private folder.

            _candidateListsCommand.AddCandidateToFolder(employer, _sharedFolder, member.Id);

            // Check lists are updated.

            AssertCounts(employer, 0, 1, 0, 0, 0, 0, 1);
        }

        [TestMethod]
        public void TestAddCandidateToFlagListInTemporaryBlockList()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(0);

            CreateLists(employer);
            AssertCounts(employer, 0, 0, 0, 0, 0, 0, 0);

            // Add member to lists.

            _candidateListsCommand.AddCandidateToBlockList(employer, _temporaryBlockList, member.Id);

            AssertCounts(employer, 0, 1, 0, 0, 0, 0, 0);

            // Add to private folder.

            _candidateListsCommand.AddCandidateToFlagList(employer, _flagList, member.Id);

            // Check lists are updated.

            AssertCounts(employer, 0, 1, 1, 0, 0, 0, 0);
        }

        private void CreateLists(IEmployer employer)
        {
            _temporaryBlockList = _candidateBlockListsQuery.GetTemporaryBlockList(employer);
            _permanentBlockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);
            _flagList = _candidateFlagListsQuery.GetFlagList(employer);

            _privateFolder = new CandidateFolder { Name = "PrivateFolder" };
            _candidateFoldersCommand.CreatePrivateFolder(employer, _privateFolder);

            _sharedFolder = new CandidateFolder { Name = "SharedFolder" };
            _candidateFoldersCommand.CreateSharedFolder(employer, _sharedFolder);

            _shortlistFolder = _candidateFoldersQuery.GetShortlistFolder(employer);
            _mobileFolder = _candidateFoldersQuery.GetMobileFolder(employer);
        }

        private void AssertCounts(IEmployer employer, int permanentCount, int temporaryCount, int flagListCount, int privateCount, int shortlistCount, int mobileCount, int sharedCount)
        {
            Assert.AreEqual(permanentCount, _candidateBlockListsQuery.GetBlockedCount(employer, _permanentBlockList.Id));
            Assert.AreEqual(temporaryCount, _candidateBlockListsQuery.GetBlockedCount(employer, _temporaryBlockList.Id));
            Assert.AreEqual(flagListCount, _candidateFlagListsQuery.GetFlaggedCount(employer));
            Assert.AreEqual(privateCount, _candidateFoldersQuery.GetInFolderCount(employer, _privateFolder.Id));
            Assert.AreEqual(shortlistCount, _candidateFoldersQuery.GetInFolderCount(employer, _shortlistFolder.Id));
            Assert.AreEqual(mobileCount, _candidateFoldersQuery.GetInFolderCount(employer, _mobileFolder.Id));
            Assert.AreEqual(sharedCount, _candidateFoldersQuery.GetInFolderCount(employer, _sharedFolder.Id));
        }

        private Employer CreateEmployer(int index)
        {
            return _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestVerifiedOrganisation(index));
        }

        private Member CreateMember(int index)
        {
            return _membersCommand.CreateTestMember(index);
        }
    }
}