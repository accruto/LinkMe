using System;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Query.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Activity
{
    [TestClass]
    public class AllMemberActivityFiltersTests
        : MemberActivityFiltersTests
    {
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();

        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

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

            var query = new MemberSearchQuery();
            TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2], memberIds[3], memberIds[4], memberIds[5] }, employer, query, memberIds);

            // All true.

            query = new MemberSearchQuery { InFolder = true, IsFlagged = true, HasNotes = true, HasViewed = true, IsUnlocked = true };
            TestFilter(new[] { memberIds[5] }, employer, query, memberIds);

            // All false.

            query = new MemberSearchQuery { InFolder = false, IsFlagged = false, HasNotes = false, HasViewed = false, IsUnlocked = false };
            TestFilter(new[] { memberIds[0] }, employer, query, memberIds);

            // In folder.

            query = new MemberSearchQuery { InFolder = true };
            TestFilter(new[] { memberIds[1], memberIds[2], memberIds[3], memberIds[4], memberIds[5] }, employer, query, memberIds);

            query = new MemberSearchQuery { InFolder = false };
            TestFilter(new[] { memberIds[0] }, employer, query, memberIds);

            // Is flagged.

            query = new MemberSearchQuery { IsFlagged = true };
            TestFilter(new[] { memberIds[2], memberIds[3], memberIds[4], memberIds[5] }, employer, query, memberIds);

            query = new MemberSearchQuery { IsFlagged = false };
            TestFilter(new[] { memberIds[0], memberIds[1] }, employer, query, memberIds);

            // Has notes.

            query = new MemberSearchQuery { HasNotes = true };
            TestFilter(new[] { memberIds[3], memberIds[4], memberIds[5] }, employer, query, memberIds);

            query = new MemberSearchQuery { HasNotes = false };
            TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2] }, employer, query, memberIds);

            // Has viewed.

            query = new MemberSearchQuery { HasViewed = true };
            TestFilter(new[] { memberIds[4], memberIds[5] }, employer, query, memberIds);

            query = new MemberSearchQuery { HasViewed = false };
            TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2], memberIds[3] }, employer, query, memberIds);

            // Is unlocked.

            query = new MemberSearchQuery { IsUnlocked = true };
            TestFilter(new[] { memberIds[5] }, employer, query, memberIds);

            query = new MemberSearchQuery { IsUnlocked = false };
            TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2], memberIds[3], memberIds[4] }, employer, query, memberIds);

            // Some true.

            query = new MemberSearchQuery { InFolder = true, HasNotes = true };
            TestFilter(new[] { memberIds[3], memberIds[4], memberIds[5] }, employer, query, memberIds);

            // Some false.

            query = new MemberSearchQuery { HasNotes = false, IsUnlocked = false };
            TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2] }, employer, query, memberIds);

            // Some true, some false.

            query = new MemberSearchQuery { InFolder = true, IsUnlocked = false };
            TestFilter(new[] { memberIds[1], memberIds[2], memberIds[3], memberIds[4] }, employer, query, memberIds);

            query = new MemberSearchQuery { HasNotes = true, IsUnlocked = false };
            TestFilter(new[] { memberIds[3], memberIds[4] }, employer, query, memberIds);

            query = new MemberSearchQuery { InFolder = true, IsFlagged = true, HasViewed = false, IsUnlocked = false };
            TestFilter(new[] { memberIds[2], memberIds[3] }, employer, query, memberIds);

            query = new MemberSearchQuery { InFolder = false, IsFlagged = false, HasViewed = true, IsUnlocked = true };
            TestFilter(new Guid[0], employer, query, memberIds);

            // Block some members.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryBlockList(employer), memberIds[1]);
            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetPermanentBlockList(employer), memberIds[4]);

            // Do it again.

            // None set.

            query = new MemberSearchQuery();
            TestFilter(new[] { memberIds[0], memberIds[2], memberIds[3], memberIds[5] }, employer, query, memberIds);

            // All true.

            query = new MemberSearchQuery { InFolder = true, IsFlagged = true, HasNotes = true, HasViewed = true, IsUnlocked = true };
            TestFilter(new[] { memberIds[5] }, employer, query, memberIds);

            // All false.

            query = new MemberSearchQuery { InFolder = false, IsFlagged = false, HasNotes = false, HasViewed = false, IsUnlocked = false };
            TestFilter(new[] { memberIds[0] }, employer, query, memberIds);

            // In folder.

            query = new MemberSearchQuery { InFolder = true };
            TestFilter(new[] { memberIds[2], memberIds[3], memberIds[5] }, employer, query, memberIds);

            query = new MemberSearchQuery { InFolder = false };
            TestFilter(new[] { memberIds[0] }, employer, query, memberIds);

            // Is flagged.

            query = new MemberSearchQuery { IsFlagged = true };
            TestFilter(new[] { memberIds[2], memberIds[3], memberIds[5] }, employer, query, memberIds);

            query = new MemberSearchQuery { IsFlagged = false };
            TestFilter(new[] { memberIds[0] }, employer, query, memberIds);

            // Has notes.

            query = new MemberSearchQuery { HasNotes = true };
            TestFilter(new[] { memberIds[3], memberIds[5] }, employer, query, memberIds);

            query = new MemberSearchQuery { HasNotes = false };
            TestFilter(new[] { memberIds[0], memberIds[2] }, employer, query, memberIds);

            // Has viewed.

            query = new MemberSearchQuery { HasViewed = true };
            TestFilter(new[] { memberIds[5] }, employer, query, memberIds);

            query = new MemberSearchQuery { HasViewed = false };
            TestFilter(new[] { memberIds[0], memberIds[2], memberIds[3] }, employer, query, memberIds);

            // Is unlocked.

            query = new MemberSearchQuery { IsUnlocked = true };
            TestFilter(new[] { memberIds[5] }, employer, query, memberIds);

            query = new MemberSearchQuery { IsUnlocked = false };
            TestFilter(new[] { memberIds[0], memberIds[2], memberIds[3] }, employer, query, memberIds);

            // Some true.

            query = new MemberSearchQuery { InFolder = true, HasNotes = true };
            TestFilter(new[] { memberIds[3], memberIds[5] }, employer, query, memberIds);

            // Some false.

            query = new MemberSearchQuery { HasNotes = false, IsUnlocked = false };
            TestFilter(new[] { memberIds[0], memberIds[2] }, employer, query, memberIds);

            // Some true, some false.

            query = new MemberSearchQuery { InFolder = true, IsUnlocked = false };
            TestFilter(new[] { memberIds[2], memberIds[3] }, employer, query, memberIds);

            query = new MemberSearchQuery { HasNotes = true, IsUnlocked = false };
            TestFilter(new[] { memberIds[3] }, employer, query, memberIds);

            query = new MemberSearchQuery { InFolder = true, IsFlagged = true, HasViewed = false, IsUnlocked = false };
            TestFilter(new[] { memberIds[2], memberIds[3] }, employer, query, memberIds);

            query = new MemberSearchQuery { InFolder = false, IsFlagged = false, HasViewed = true, IsUnlocked = true };
            TestFilter(new Guid[0], employer, query, memberIds);
        }

        private Member CreateMember(IEmployer employer, int index, bool inFolder, bool isFlagged, bool hasNotes, bool hasBeenViewed, bool isUnlocked)
        {
            var member = _membersCommand.CreateTestMember(index);

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
                var note = new CandidateNote {CandidateId = member.Id, Text = "A note"};
                _candidateNotesCommand.CreatePrivateNote(employer, note);
            }

            // Has been viewed.

            if (hasBeenViewed)
                _employerMemberViewsCommand.ViewMember(_app, employer, member);

            // Has been unlocked.

            if (isUnlocked)
                _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);

            return member;
        }
    }
}
