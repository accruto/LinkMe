using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Folders
{
    [TestClass]
    public class CandidatesTests
        : FoldersTests
    {
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();

        [TestMethod]
        public void TestWithResume()
        {
            // Members with a resume get shown whilst those without do not.

            const int count = 4;
            var members = new Member[count];
            var candidates = new Candidate[count];
            var resumes = new Resume[count];
            for (var index = 0; index < count; ++index)
            {
                members[index] = _memberAccountsCommand.CreateTestMember(index);
                candidates[index] = _candidatesQuery.GetCandidate(members[index].Id);
                if (index < count - 1)
                    resumes[index] = _candidateResumesCommand.AddTestResume(candidates[index]);
            }

            var employer = CreateEmployer();
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            _candidateListsCommand.AddCandidatesToFolder(employer, folder, from m in members select m.Id);

            LogIn(employer);
            Get(GetFolderUrl(folder.Id));

            // The last member does not have a resume and won't be included.

            AssertMembers(members.Take(count - 1).ToArray());

            // Make the resume empty.

            var memberId = members[1].Id;
            _candidateResumesCommand.UpdateResume(candidates[1], new Resume { Id = resumes[1].Id, CreatedTime = DateTime.Now, LastUpdatedTime = DateTime.Now });
            _memberSearchService.UpdateMember(memberId);

            Get(GetFolderUrl(folder.Id));
            AssertMembers((from m in members.Take(count - 1) where m.Id != memberId select m).ToArray());
        }

        [TestMethod]
        public void TestPermanentlyBlocked()
        {
            const int count = 4;
            var members = new Member[count];
            var candidates = new Candidate[count];
            for (var index = 0; index < count; ++index)
            {
                members[index] = CreateMember(index);
                candidates[index] = _candidatesQuery.GetCandidate(members[index].Id);
                _candidateResumesCommand.AddTestResume(candidates[index]);
            }

            var employer = CreateEmployer();
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            _candidateListsCommand.AddCandidatesToFolder(employer, folder, from m in members select m.Id);

            // All members shown.

            LogIn(employer);
            Get(GetFolderUrl(folder.Id));
            AssertMembers(members);

            // Permanently block one.

            var memberId = members[1].Id;
            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetPermanentBlockList(employer), memberId);

            // Should be removed from folder as well.

            Get(GetFolderUrl(folder.Id));
            AssertMembers((from m in members where m.Id != memberId select m).ToArray());
        }

        [TestMethod]
        public void TestTemporarilyBlocked()
        {
            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
            {
                members[index] = CreateMember(index);
                var candidate = _candidatesQuery.GetCandidate(members[index].Id);
                _candidateResumesCommand.AddTestResume(candidate);
            }

            var employer = CreateEmployer();
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            _candidateListsCommand.AddCandidatesToFolder(employer, folder, from m in members select m.Id);

            // All members shown.

            LogIn(employer);
            Get(GetFolderUrl(folder.Id));
            AssertMembers(members);

            // Temporarily block one.

            var memberId = members[1].Id;
            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryBlockList(employer), memberId);

            // Should not be removed from folder.

            Get(GetFolderUrl(folder.Id));
            AssertMembers(members);
        }

        [TestMethod]
        public void TestHidden()
        {
            const int count = 4;
            var members = new Member[count];
            var candidates = new Candidate[count];
            for (var index = 0; index < count; ++index)
            {
                members[index] = CreateMember(index);
                candidates[index] = _candidatesQuery.GetCandidate(members[index].Id);
                _candidateResumesCommand.AddTestResume(candidates[index]);
            }

            var employer = CreateEmployer();
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            _candidateListsCommand.AddCandidatesToFolder(employer, folder, from m in members select m.Id);

            // All members shown.

            LogIn(employer);
            Get(GetFolderUrl(folder.Id));
            AssertMembers(members);

            // Hide one.

            var memberId = members[1].Id;
            members[1].VisibilitySettings.Professional.EmploymentVisibility = members[1].VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Resume);
            _memberAccountsCommand.UpdateMember(members[1]);

            // Should be removed from folder as well.

            Get(GetFolderUrl(folder.Id));
            AssertMembers((from m in members where m.Id != memberId select m).ToArray());
        }

        private void AssertMembers(ICollection<Member> members)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='expanded_search-results']/div[position()=1]");
            Assert.IsNotNull(nodes);
            Assert.AreEqual(members.Count, nodes.Count);
            foreach (var member in members)
            {
                var found = false;
                foreach (var node in nodes)
                {
                    if (node.Attributes["data-memberid"].Value == member.Id.ToString())
                    {
                        found = true;
                        break;
                    }
                }
                
                Assert.AreEqual(true, found);
            }
        }
    }
}
