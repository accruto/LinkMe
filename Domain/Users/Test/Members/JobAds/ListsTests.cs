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
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds
{
    [TestClass]
    public class ListsTests
        : TestClass
    {
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdFoldersQuery _jobAdFoldersQuery = Resolve<IJobAdFoldersQuery>();
        private readonly IJobAdBlockListsQuery _jobAdBlockListsQuery = Resolve<IJobAdBlockListsQuery>();
        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery = Resolve<IJobAdFlagListsQuery>();
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();

        private JobAdBlockList _blockList;
        private JobAdFlagList _flagList;
        private JobAdFolder _folder1;
        private JobAdFolder _folder2;

        [TestInitialize]
        public void ListsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _blockList = null;
            _flagList = null;
            _folder1 = null;
            _folder2 = null;
        }

        [TestMethod]
        public void TestAddJobAdToBlocklist()
        {
            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var member = CreateMember(0);

            CreateLists(member);
            AssertCounts(member, 0, 0, 0, 0);

            // Add member to lists.

            _memberJobAdListsCommand.AddJobAdToFlagList(member, _flagList, jobAd.Id);
            _memberJobAdListsCommand.AddJobAdToFolder(member, _folder1, jobAd.Id);
            _memberJobAdListsCommand.AddJobAdToFolder(member, _folder2, jobAd.Id);

            AssertCounts(member, 0, 1, 1, 1);

            // Add to permanent blocklist.

            _memberJobAdListsCommand.AddJobAdToBlockList(member, _blockList, jobAd.Id);

            // Check lists are updated.

            AssertCounts(member, 1, 0, 0, 0);
        }

        [TestMethod]
        public void TestAddJobAdToFolder1InBlocklist()
        {
            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var member = CreateMember(0);

            CreateLists(member);
            AssertCounts(member, 0, 0, 0, 0);

            // Add member to lists.

            _memberJobAdListsCommand.AddJobAdToBlockList(member, _blockList, jobAd.Id);

            AssertCounts(member, 1, 0, 0, 0);

            // Add to private folder.

            _memberJobAdListsCommand.AddJobAdToFolder(member, _folder1, jobAd.Id);

            // Check lists are updated.

            AssertCounts(member, 0, 0, 1, 0);
        }

        [TestMethod]
        public void TestAddJobAdToFolder2InBlocklist()
        {
            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var member = CreateMember(0);

            CreateLists(member);
            AssertCounts(member, 0, 0, 0, 0);

            // Add member to lists.

            _memberJobAdListsCommand.AddJobAdToBlockList(member, _blockList, jobAd.Id);

            AssertCounts(member, 1, 0, 0, 0);

            // Add to private folder.

            _memberJobAdListsCommand.AddJobAdToFolder(member, _folder2, jobAd.Id);

            // Check lists are updated.

            AssertCounts(member, 0, 0, 0, 1);
        }

        [TestMethod]
        public void TestAddJobAdToFlagListInBlocklist()
        {
            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var member = CreateMember(0);

            CreateLists(member);
            AssertCounts(member, 0, 0, 0, 0);

            // Add member to lists.

            _memberJobAdListsCommand.AddJobAdToBlockList(member, _blockList, jobAd.Id);

            AssertCounts(member, 1, 0, 0, 0);

            // Add to private folder.

            _memberJobAdListsCommand.AddJobAdToFlagList(member, _flagList, jobAd.Id);

            // Check lists are updated.

            AssertCounts(member, 0, 1, 0, 0);
        }

        private void CreateLists(IMember member)
        {
            _blockList = _jobAdBlockListsQuery.GetBlockList(member);
            _flagList = _jobAdFlagListsQuery.GetFlagList(member);

            var folders = _jobAdFoldersQuery.GetFolders(member);
            _folder1 = folders[1];
            _folder2 = folders[2];
        }

        private void AssertCounts(IMember member, int blockListCount, int flagListCount, int folder1Count, int folder2Count)
        {
            Assert.AreEqual(blockListCount, _jobAdBlockListsQuery.GetBlockedCount(member));
            Assert.AreEqual(flagListCount, _jobAdFlagListsQuery.GetFlaggedCount(member));
            Assert.AreEqual(folder1Count, _jobAdFoldersQuery.GetInFolderCount(member, _folder1.Id));
            Assert.AreEqual(folder2Count, _jobAdFoldersQuery.GetInFolderCount(member, _folder2.Id));
        }

        private Employer CreateEmployer(int index)
        {
            var employer = _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestVerifiedOrganisation(index));
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });
            return employer;
        }

        private Member CreateMember(int index)
        {
            return _membersCommand.CreateTestMember(index);
        }
    }
}