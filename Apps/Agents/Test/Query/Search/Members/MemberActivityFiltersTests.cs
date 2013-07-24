using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Query.Search.Members
{
    [TestClass]
    public class MemberActivityFiltersTests
        : TestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();
        private readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();
        private readonly ICandidateFlagListsQuery _candidateFlagListsQuery = Resolve<ICandidateFlagListsQuery>();
        private readonly ICandidateNotesCommand _candidateNotesCommand = Resolve<ICandidateNotesCommand>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IExecuteMemberSearchCommand _executeMemberSearchCommand = Resolve<IExecuteMemberSearchCommand>();
        private readonly IUpdateMemberSearchCommand _updateMemberSearchCommand = Resolve<IUpdateMemberSearchCommand>();

        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            MemberSearchHost.ClearIndex();
        }

        [TestMethod]
        public void TestFilterInFolders()
        {
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member1.Id);

            // Filter.

            TestFilter(employer, CreateInFolderCriteria, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [TestMethod]
        public void TestFilterFolderId()
        {
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member1.Id);

            // Filter.

            var allMemberIds = new[] { member1.Id, member2.Id };
            TestFilter(allMemberIds, employer, new MemberSearchCriteria());
            TestFolderFilter(new[] { member1.Id }, employer, folder.Id);
        }

        [TestMethod]
        public void TestFilterBlockListId()
        {
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in blockList.

            var blockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);
            _candidateListsCommand.AddCandidateToBlockList(employer, blockList, member1.Id);

            // Filter.

            TestFilter(new[] { member2.Id }, employer, new MemberSearchCriteria());
            TestBlockListFilter(new[] { member1.Id }, employer, blockList.Id);
        }

        [TestMethod]
        public void TestFilterBlockInFolders()
        {
            var member = CreateMember(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member in folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateInFolderCriteria(true));

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateInFolderCriteria(true));
        }

        [TestMethod]
        public void TestFilterIsFlagged()
        {
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in folder.

            var flagList = _candidateFlagListsQuery.GetFlagList(employer);
            _candidateListsCommand.AddCandidateToFlagList(employer, flagList, member1.Id);

            // Filter.

            TestFilter(employer, CreateIsFlaggedCriteria, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [TestMethod]
        public void TestFilterBlockIsFlagged()
        {
            var member = CreateMember(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member in folder.

            var flagList = _candidateFlagListsQuery.GetFlagList(employer);
            _candidateListsCommand.AddCandidateToFlagList(employer, flagList, member.Id);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateIsFlaggedCriteria(true));

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateIsFlaggedCriteria(true));
        }

        [TestMethod]
        public void TestFilterHasNotes()
        {
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 has notes.

            var note = new CandidateNote { CandidateId = member1.Id, Text = "A note" };
            _candidateNotesCommand.CreatePrivateNote(employer, note);

            // Filter.

            TestFilter(employer, CreateHasNotesCriteria, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [TestMethod]
        public void TestFilterBlockHasNotes()
        {
            var member = CreateMember(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member has notes.

            var note = new CandidateNote { CandidateId = member.Id, Text = "A note" };
            _candidateNotesCommand.CreatePrivateNote(employer, note);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateHasNotesCriteria(true));

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateHasNotesCriteria(true));
        }

        [TestMethod]
        public void TestFilterHasViewed()
        {
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 has been viewed.

            _employerMemberViewsCommand.ViewMember(_app, employer, member1);

            // Filter.

            TestFilter(employer, CreateHasViewedCriteria, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [TestMethod]
        public void TestFilterBlockHasViewed()
        {
            var member = CreateMember(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member has been viewed.

            _employerMemberViewsCommand.ViewMember(_app, employer, member);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateHasViewedCriteria(true));

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateHasViewedCriteria(true));
        }

        [TestMethod]
        public void TestFilterIsUnlocked()
        {
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 20, OwnerId = employer.Id });

            // Member1 has been viewed.

            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member1), MemberAccessReason.PhoneNumberViewed);

            // Filter.

            TestFilter(employer, CreateIsUnlockedCriteria, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [TestMethod]
        public void TestFilterBlockIsUnlocked()
        {
            var member = CreateMember(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 20, OwnerId = employer.Id });

            // Member1 has been contacted.

            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateIsUnlockedCriteria(true));

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateIsUnlockedCriteria(true));
        }

        [TestMethod]
        public void TestFilterAll()
        {
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 20, OwnerId = employer.Id });

            // Create members.

            var memberIds = new Guid[6];
            memberIds[0] = CreateMember(employer, 0, false, false, false, false, false).Id;
            memberIds[1] = CreateMember(employer, 1, true, false, false, false, false).Id;
            memberIds[2] = CreateMember(employer, 2, true, true, false, false, false).Id;
            memberIds[3] = CreateMember(employer, 3, true, true, true, false, false).Id;
            memberIds[4] = CreateMember(employer, 4, true, true, true, true, false).Id;
            memberIds[5] = CreateMember(employer, 5, true, true, true, true, true).Id;

            // None set.

            var criteria = new MemberSearchCriteria();
            TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2], memberIds[3], memberIds[4], memberIds[5] }, employer, criteria);

            // All true.

            criteria = new MemberSearchCriteria { InFolder = true, IsFlagged = true, HasNotes = true, HasViewed = true, IsUnlocked = true };
            TestFilter(new[] { memberIds[5] }, employer, criteria);

            // All false.

            criteria = new MemberSearchCriteria { InFolder = false, IsFlagged = false, HasNotes = false, HasViewed = false, IsUnlocked = false };
            TestFilter(new[] { memberIds[0] }, employer, criteria);

            // In folder.

            criteria = new MemberSearchCriteria { InFolder = true };
            TestFilter(new[] { memberIds[1], memberIds[2], memberIds[3], memberIds[4], memberIds[5] }, employer, criteria);

            criteria = new MemberSearchCriteria { InFolder = false };
            TestFilter(new[] { memberIds[0] }, employer, criteria);

            // Is flagged.

            criteria = new MemberSearchCriteria { IsFlagged = true };
            TestFilter(new[] { memberIds[2], memberIds[3], memberIds[4], memberIds[5] }, employer, criteria);

            criteria = new MemberSearchCriteria { IsFlagged = false };
            TestFilter(new[] { memberIds[0], memberIds[1] }, employer, criteria);

            // Has notes.

            criteria = new MemberSearchCriteria { HasNotes = true };
            TestFilter(new[] { memberIds[3], memberIds[4], memberIds[5] }, employer, criteria);

            criteria = new MemberSearchCriteria { HasNotes = false };
            TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2] }, employer, criteria);

            // Has viewed.

            criteria = new MemberSearchCriteria { HasViewed = true };
            TestFilter(new[] { memberIds[4], memberIds[5] }, employer, criteria);

            criteria = new MemberSearchCriteria { HasViewed = false };
            TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2], memberIds[3] }, employer, criteria);

            // Is unlocked.

            criteria = new MemberSearchCriteria { IsUnlocked = true };
            TestFilter(new[] { memberIds[5] }, employer, criteria);

            criteria = new MemberSearchCriteria { IsUnlocked = false };
            TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2], memberIds[3], memberIds[4] }, employer, criteria);

            // Some true.

            criteria = new MemberSearchCriteria { InFolder = true, HasNotes = true };
            TestFilter(new[] { memberIds[3], memberIds[4], memberIds[5] }, employer, criteria);

            // Some false.

            criteria = new MemberSearchCriteria { HasNotes = false, IsUnlocked = false };
            TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2] }, employer, criteria);

            // Some true, some false.

            criteria = new MemberSearchCriteria { InFolder = true, IsUnlocked = false };
            TestFilter(new[] { memberIds[1], memberIds[2], memberIds[3], memberIds[4] }, employer, criteria);

            criteria = new MemberSearchCriteria { HasNotes = true, IsUnlocked = false };
            TestFilter(new[] { memberIds[3], memberIds[4] }, employer, criteria);

            criteria = new MemberSearchCriteria { InFolder = true, IsFlagged = true, HasViewed = false, IsUnlocked = false };
            TestFilter(new[] { memberIds[2], memberIds[3] }, employer, criteria);

            criteria = new MemberSearchCriteria { InFolder = false, IsFlagged = false, HasViewed = true, IsUnlocked = true };
            TestFilter(new Guid[0], employer, criteria);

            // Block some members.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryBlockList(employer), memberIds[1]);
            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetPermanentBlockList(employer), memberIds[4]);

            // Do it again.

            // None set.

            criteria = new MemberSearchCriteria();
            TestFilter(new[] { memberIds[0], memberIds[2], memberIds[3], memberIds[5] }, employer, criteria);

            // All true.

            criteria = new MemberSearchCriteria { InFolder = true, IsFlagged = true, HasNotes = true, HasViewed = true, IsUnlocked = true };
            TestFilter(new[] { memberIds[5] }, employer, criteria);

            // All false.

            criteria = new MemberSearchCriteria { InFolder = false, IsFlagged = false, HasNotes = false, HasViewed = false, IsUnlocked = false };
            TestFilter(new[] { memberIds[0] }, employer, criteria);

            // In folder.

            criteria = new MemberSearchCriteria { InFolder = true };
            TestFilter(new[] { memberIds[2], memberIds[3], memberIds[5] }, employer, criteria);

            criteria = new MemberSearchCriteria { InFolder = false };
            TestFilter(new[] { memberIds[0] }, employer, criteria);

            // Is flagged.

            criteria = new MemberSearchCriteria { IsFlagged = true };
            TestFilter(new[] { memberIds[2], memberIds[3], memberIds[5] }, employer, criteria);

            criteria = new MemberSearchCriteria { IsFlagged = false };
            TestFilter(new[] { memberIds[0] }, employer, criteria);

            // Has notes.

            criteria = new MemberSearchCriteria { HasNotes = true };
            TestFilter(new[] { memberIds[3], memberIds[5] }, employer, criteria);

            criteria = new MemberSearchCriteria { HasNotes = false };
            TestFilter(new[] { memberIds[0], memberIds[2] }, employer, criteria);

            // Has viewed.

            criteria = new MemberSearchCriteria { HasViewed = true };
            TestFilter(new[] { memberIds[5] }, employer, criteria);

            criteria = new MemberSearchCriteria { HasViewed = false };
            TestFilter(new[] { memberIds[0], memberIds[2], memberIds[3] }, employer, criteria);

            // Is unlcoked.

            criteria = new MemberSearchCriteria { IsUnlocked = true };
            TestFilter(new[] { memberIds[5] }, employer, criteria);

            criteria = new MemberSearchCriteria { IsUnlocked = false };
            TestFilter(new[] { memberIds[0], memberIds[2], memberIds[3] }, employer, criteria);

            // Some true.

            criteria = new MemberSearchCriteria { InFolder = true, HasNotes = true };
            TestFilter(new[] { memberIds[3], memberIds[5] }, employer, criteria);

            // Some false.

            criteria = new MemberSearchCriteria { HasNotes = false, IsUnlocked = false };
            TestFilter(new[] { memberIds[0], memberIds[2] }, employer, criteria);

            // Some true, some false.

            criteria = new MemberSearchCriteria { InFolder = true, IsUnlocked = false };
            TestFilter(new[] { memberIds[2], memberIds[3] }, employer, criteria);

            criteria = new MemberSearchCriteria { HasNotes = true, IsUnlocked = false };
            TestFilter(new[] { memberIds[3] }, employer, criteria);

            criteria = new MemberSearchCriteria { InFolder = true, IsFlagged = true, HasViewed = false, IsUnlocked = false };
            TestFilter(new[] { memberIds[2], memberIds[3] }, employer, criteria);

            criteria = new MemberSearchCriteria { InFolder = false, IsFlagged = false, HasViewed = true, IsUnlocked = true };
            TestFilter(new Guid[0], employer, criteria);
        }

        private Member CreateMember(int index)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _updateMemberSearchCommand.AddMember(member.Id);
            return member;
        }

        private Member CreateMember(IEmployer employer, int index, bool inFolder, bool isFlagged, bool hasNotes, bool hasBeenViewed, bool canContact)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // In folder.

            if (inFolder)
            {
                var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
                _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
            }

            // Is flagged.

            if (isFlagged)
            {
                var flagList = _candidateFlagListsQuery.GetFlagList(employer);
                _candidateListsCommand.AddCandidateToFlagList(employer, flagList, member.Id);
            }

            // Has notes.

            if (hasNotes)
            {
                var note = new CandidateNote { CandidateId = member.Id, Text = "A note" };
                _candidateNotesCommand.CreatePrivateNote(employer, note);
            }

            // Has been viewed.

            if (hasBeenViewed)
                _employerMemberViewsCommand.ViewMember(_app, employer, member);

            // Has been contacted.

            if (canContact)
                _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);

            _updateMemberSearchCommand.AddMember(member.Id);
            return member;
        }

        private void TestFilter(IEmployer employer, Func<bool?, MemberSearchCriteria> createCriteria, ICollection<Guid> allMemberIds, ICollection<Guid> isSetMemberIds, ICollection<Guid> isNotSetMemberIds)
        {
            TestFilter(allMemberIds, employer, createCriteria(null));
            TestFilter(isSetMemberIds, employer, createCriteria(true));
            TestFilter(isNotSetMemberIds, employer, createCriteria(false));
        }

        private void TestFilter(ICollection<Guid> expectedMemberIds, IEmployer employer, MemberSearchCriteria criteria)
        {
            AssertMemberIds(expectedMemberIds, Search(employer, criteria));
        }

        private void TestFolderFilter(ICollection<Guid> expectedMemberIds, IEmployer employer, Guid folderId)
        {
            AssertMemberIds(expectedMemberIds, SearchFolder(employer, folderId));
        }

        private void TestBlockListFilter(ICollection<Guid> expectedMemberIds, IEmployer employer, Guid blockListId)
        {
            AssertMemberIds(expectedMemberIds, SearchBlockList(employer, blockListId));
        }

        private ICollection<Guid> Search(IEmployer employer, MemberSearchCriteria criteria)
        {
            return _executeMemberSearchCommand.Search(employer, criteria, null).Results.MemberIds;
        }

        private ICollection<Guid> SearchFolder(IEmployer employer, Guid folderId)
        {
            return _executeMemberSearchCommand.SearchFolder(employer, folderId, null, null).Results.MemberIds;
        }

        private ICollection<Guid> SearchBlockList(IEmployer employer, Guid blockListId)
        {
            return _executeMemberSearchCommand.SearchBlockList(employer, blockListId, null, null).Results.MemberIds;
        }

        private static void AssertMemberIds(ICollection<Guid> expectedMemberIds, ICollection<Guid> memberIds)
        {
            Assert.AreEqual(expectedMemberIds.Count, memberIds.Count);
            foreach (var expectedMemberId in expectedMemberIds)
                Assert.IsTrue(memberIds.Contains(expectedMemberId));
        }

        private static MemberSearchCriteria CreateInFolderCriteria(bool? value)
        {
            return new MemberSearchCriteria { InFolder = value };
        }

        private static MemberSearchCriteria CreateIsFlaggedCriteria(bool? value)
        {
            return new MemberSearchCriteria { IsFlagged = value };
        }

        private static MemberSearchCriteria CreateHasNotesCriteria(bool? value)
        {
            return new MemberSearchCriteria { HasNotes = value };
        }

        private static MemberSearchCriteria CreateHasViewedCriteria(bool? value)
        {
            return new MemberSearchCriteria { HasViewed = value };
        }

        private static MemberSearchCriteria CreateIsUnlockedCriteria(bool? value)
        {
            return new MemberSearchCriteria { IsUnlocked = value };
        }
    }
}