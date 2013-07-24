using System;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Query.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Activity
{
    [TestClass]
    public class NotesMemberActivityFiltersTests
        : MemberActivityFiltersTests
    {
        [TestMethod]
        public void TestFilterHasNotes()
        {
            var member1 = _membersCommand.CreateTestMember(1);
            var member2 = _membersCommand.CreateTestMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 has notes.

            var note = new CandidateNote {CandidateId = member1.Id, Text = "A note"};
            _candidateNotesCommand.CreatePrivateNote(employer, note);

            // Filter.

            TestFilter(employer, CreateHasNotesQuery, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [TestMethod]
        public void TestFilterBlockHasNotes()
        {
            var member = _membersCommand.CreateTestMember(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member has notes.

            var note = new CandidateNote { CandidateId = member.Id, Text = "A note" };
            _candidateNotesCommand.CreatePrivateNote(employer, note);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateHasNotesQuery(true), new[] { member.Id });

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateHasNotesQuery(true), new[] { member.Id });
        }

        private static MemberSearchQuery CreateHasNotesQuery(bool? value)
        {
            return new MemberSearchQuery { HasNotes = value };
        }
    }
}
