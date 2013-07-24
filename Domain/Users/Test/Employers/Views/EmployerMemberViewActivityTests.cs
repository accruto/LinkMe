using System;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views
{
    [TestClass]
    public class EmployerMemberViewActivityTests
        : TestClass
    {
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly ICandidateFoldersCommand _candidateFoldersCommand = Resolve<ICandidateFoldersCommand>();
        private readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();
        private readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();
        private readonly ICandidateFlagListsQuery _candidateFlagListsQuery = Resolve<ICandidateFlagListsQuery>();
        private readonly ICandidateNotesCommand _candidateNotesCommand = Resolve<ICandidateNotesCommand>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();

        private const string FolderName = "My folder";
        private const string NoteText = "My note text";

        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestInitialize]
        public void EmployerMemberViewActivityTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestHasNotBeenViewed()
        {
            var employer = CreateEmployer(0);
            var member = CreateMember(0);
            TestFlag(employer, member.Id, false, v => v.HasBeenViewed);
        }

        [TestMethod]
        public void TestHasBeenViewed()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var member = CreateMember(0);
            _employerMemberViewsCommand.ViewMember(_app, employer0, member);

            TestFlag(employer0, member.Id, true, v => v.HasBeenViewed);
            TestFlag(employer1, member.Id, false, v => v.HasBeenViewed);
        }

        [TestMethod]
        public void TestHasNotBeenAccessed()
        {
            var employer = CreateEmployer(0);
            var member = CreateMember(0);
            TestFlag(employer, member.Id, false, v => v.HasBeenAccessed);
        }

        [TestMethod]
        public void TestHasBeenAccessed()
        {
            var employer0 = CreateEmployer(0);
            _allocationsCommand.CreateAllocation(new Allocation {CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer0.Id});
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var member = CreateMember(0);
            var view = _employerMemberViewsQuery.GetProfessionalView(employer0, member);

            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer0, view, MemberAccessReason.MessageSent);
            _employerMemberViewsCommand.AccessMember(_app, employer0, view, MemberAccessReason.MessageSent);

            TestFlag(employer0, member.Id, true, v => v.HasBeenAccessed);
            TestFlag(employer1, member.Id, false, v => v.HasBeenAccessed);
        }

        [TestMethod]
        public void TestIsNotInFolder()
        {
            var employer = CreateEmployer(0);
            var member = CreateMember(0);
            TestCount(employer, member.Id, 0, v => v.Folders);
        }

        [TestMethod]
        public void TestIsInShortlistFolder()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var member = CreateMember(0);
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer0);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member.Id);

            TestCount(employer0, member.Id, 1, v => v.Folders);
            TestCount(employer1, member.Id, 0, v => v.Folders);
        }

        [TestMethod]
        public void TestIsInMobileFolder()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var member = CreateMember(0);
            var folder = _candidateFoldersQuery.GetMobileFolder(employer0);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member.Id);

            TestCount(employer0, member.Id, 1, v => v.Folders);
            TestFlag(employer0, member.Id, true, v => v.IsInMobileFolder);
            TestCount(employer1, member.Id, 0, v => v.Folders);
            TestFlag(employer1, member.Id, false, v => v.IsInMobileFolder);
        }

        [TestMethod]
        public void TestIsInPrivateFolder()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var member = CreateMember(0);
            var folder = new CandidateFolder {Name = FolderName};
            _candidateFoldersCommand.CreatePrivateFolder(employer0, folder);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member.Id);

            TestCount(employer0, member.Id, 1, v => v.Folders);
            TestCount(employer1, member.Id, 0, v => v.Folders);
        }

        [TestMethod]
        public void TestIsInSharedFolder()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var member = CreateMember(0);
            var folder = new CandidateFolder { Name = FolderName };
            _candidateFoldersCommand.CreateSharedFolder(employer0, folder);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member.Id);

            TestCount(employer0, member.Id, 1, v => v.Folders);
            TestCount(employer1, member.Id, 1, v => v.Folders);
        }

        [TestMethod]
        public void TestIsInMultipleFolders()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var member = CreateMember(0);
            var folder = new CandidateFolder { Name = FolderName };
            _candidateFoldersCommand.CreateSharedFolder(employer0, folder);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member.Id);

            folder = _candidateFoldersQuery.GetShortlistFolder(employer0);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member.Id);

            TestCount(employer0, member.Id, 2, v => v.Folders);
            TestCount(employer1, member.Id, 1, v => v.Folders);
        }

        [TestMethod]
        public void TestMultipleMembersInFolders()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var member0 = CreateMember(0);
            var member1 = CreateMember(1);

            var folder = new CandidateFolder { Name = FolderName };
            _candidateFoldersCommand.CreateSharedFolder(employer0, folder);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member0.Id);

            folder = _candidateFoldersQuery.GetShortlistFolder(employer0);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member0.Id);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member1.Id);

            TestCount(employer0, member0.Id, 2, v => v.Folders);
            TestCount(employer0, member1.Id, 1, v => v.Folders);
            TestCount(employer1, member0.Id, 1, v => v.Folders);
            TestCount(employer1, member1.Id, 0, v => v.Folders);

            var views = _employerMemberViewsQuery.GetEmployerMemberViews(employer0, new[] {member0.Id, member1.Id});
            Assert.AreEqual(2, views[member0.Id].Folders);
            Assert.AreEqual(1, views[member1.Id].Folders);

            views = _employerMemberViewsQuery.GetEmployerMemberViews(employer1, new[] { member0.Id, member1.Id });
            Assert.AreEqual(1, views[member0.Id].Folders);
            Assert.AreEqual(0, views[member1.Id].Folders);
        }

        [TestMethod]
        public void TestIsNotFlagged()
        {
            var employer = CreateEmployer(0);
            var member = CreateMember(0);
            TestFlag(employer, member.Id, false, v => v.IsFlagged);
        }

        [TestMethod]
        public void TestIsFlagged()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var member = CreateMember(0);
            var flagList = _candidateFlagListsQuery.GetFlagList(employer0);
            _candidateListsCommand.AddCandidateToFlagList(employer0, flagList, member.Id);

            TestFlag(employer0, member.Id, true, v => v.IsFlagged);
            TestFlag(employer1, member.Id, false, v => v.IsFlagged);
        }

        [TestMethod]
        public void TestHasNoNotes()
        {
            var employer = CreateEmployer(0);
            var member = CreateMember(0);
            TestCount(employer, member.Id, 0, v => v.Notes);
        }

        [TestMethod]
        public void TestHasPrivateNotes()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var member = CreateMember(0);
            var note = new CandidateNote { CandidateId = member.Id, Text = NoteText };
            _candidateNotesCommand.CreatePrivateNote(employer0, note);

            TestCount(employer0, member.Id, 1, v => v.Notes);
            TestCount(employer1, member.Id, 0, v => v.Notes);
        }

        [TestMethod]
        public void TestHasSharedNotes()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var member = CreateMember(0);
            var note = new CandidateNote { CandidateId = member.Id, Text = NoteText };
            _candidateNotesCommand.CreateSharedNote(employer0, note);

            TestCount(employer0, member.Id, 1, v => v.Notes);
            TestCount(employer1, member.Id, 1, v => v.Notes);
        }

        [TestMethod]
        public void TestHasMultipleNotes()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var member = CreateMember(0);
            var note = new CandidateNote { CandidateId = member.Id, Text = NoteText };
            _candidateNotesCommand.CreateSharedNote(employer0, note);

            note = new CandidateNote { CandidateId = member.Id, Text = NoteText };
            _candidateNotesCommand.CreatePrivateNote(employer0, note);

            TestCount(employer0, member.Id, 2, v => v.Notes);
            TestCount(employer1, member.Id, 1, v => v.Notes);
        }

        [TestMethod]
        public void TestMultipleMembersHaveNotes()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var member0 = CreateMember(0);
            var member1 = CreateMember(1);
            var note = new CandidateNote { CandidateId = member0.Id, Text = NoteText };
            _candidateNotesCommand.CreateSharedNote(employer0, note);

            note = new CandidateNote { CandidateId = member0.Id, Text = NoteText };
            _candidateNotesCommand.CreatePrivateNote(employer0, note);
            note = new CandidateNote { CandidateId = member1.Id, Text = NoteText };
            _candidateNotesCommand.CreatePrivateNote(employer0, note);

            TestCount(employer0, member0.Id, 2, v => v.Notes);
            TestCount(employer0, member1.Id, 1, v => v.Notes);

            TestCount(employer1, member0.Id, 1, v => v.Notes);
            TestCount(employer1, member1.Id, 0, v => v.Notes);

            var views = _employerMemberViewsQuery.GetEmployerMemberViews(employer0, new[] { member0.Id, member1.Id });
            Assert.AreEqual(2, views[member0.Id].Notes);
            Assert.AreEqual(1, views[member1.Id].Notes);

            views = _employerMemberViewsQuery.GetEmployerMemberViews(employer1, new[] { member0.Id, member1.Id });
            Assert.AreEqual(1, views[member0.Id].Notes);
            Assert.AreEqual(0, views[member1.Id].Notes);
        }

        private void TestFlag(IEmployer employer, Guid memberId, bool expectedValue, Func<EmployerMemberView, bool> getFlag)
        {
            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, memberId);
            Assert.AreEqual(expectedValue, getFlag(view));

            view = _employerMemberViewsQuery.GetEmployerMemberViews(employer, new[] { memberId })[memberId];
            Assert.AreEqual(expectedValue, getFlag(view));
        }

        private void TestCount(IEmployer employer, Guid memberId, int expectedValue, Func<EmployerMemberView, int> getCount)
        {
            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, memberId);
            Assert.AreEqual(expectedValue, getCount(view));

            view = _employerMemberViewsQuery.GetEmployerMemberViews(employer, new[] { memberId })[memberId];
            Assert.AreEqual(expectedValue, getCount(view));
        }

        private Member CreateMember(int index)
        {
            var member = _membersCommand.CreateTestMember(index);
            _candidatesCommand.CreateCandidate(new Candidate {Id = member.Id});
            return member;
        }

        private Employer CreateEmployer(int index)
        {
            return _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestVerifiedOrganisation(index));
        }

        private Employer CreateEmployer(int index, IOrganisation organisation)
        {
            return _employersCommand.CreateTestEmployer(index, organisation);
        }
    }
}
