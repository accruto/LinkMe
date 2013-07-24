using System;
using System.Linq;
using LinkMe.Apps.Api.Areas.Employers.Models.Candidates;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Candidates
{
    [TestClass]
    public class MobileFoldersTests
        : CandidateTests
    {
        private readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();
        private readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();

        private ReadOnlyUrl _mobileCandidatesUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _mobileCandidatesUrl = new ReadOnlyApplicationUrl(true, "~/v1/employers/candidates/folders/mobile");
        }

        [TestMethod]
        public void TestNoCandidates()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            AssertCandidates(GetCandidates());
        }

        [TestMethod]
        public void TestCandidates()
        {
            var employer = CreateEmployer(0);
            var member0 = GetView(employer, CreateMember(0));
            var member1 = GetView(employer, CreateMember(1));

            var folder = _candidateFoldersQuery.GetMobileFolder(employer);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member0.Id);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member1.Id);

            LogIn(employer);
            AssertCandidates(GetCandidates(), member0, member1);
        }

        [TestMethod]
        public void TestAddCandidate()
        {
            var employer = CreateEmployer(0);
            var member0 = GetView(employer, CreateMember(0));
            var member1 = GetView(employer, CreateMember(1));

            LogIn(employer);
            AssertCandidates(employer);
            AssertCandidates(GetCandidates());

            // Add first.

            AddCandidate(member0.Id);
            AssertCandidates(employer, member0);
            AssertCandidates(GetCandidates(), member0);

            // Add second.

            AddCandidate(member1.Id);
            AssertCandidates(employer, member0, member1);
            AssertCandidates(GetCandidates(), member0, member1);

            // Add first again.

            AddCandidate(member0.Id);
            AssertCandidates(employer, member0, member1);
            AssertCandidates(GetCandidates(), member0, member1);
        }

        [TestMethod]
        public void TestRemoveCandidates()
        {
            var employer = CreateEmployer(0);
            var member0 = GetView(employer, CreateMember(0));
            var member1 = GetView(employer, CreateMember(1));

            var folder = _candidateFoldersQuery.GetMobileFolder(employer);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member0.Id);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member1.Id);

            LogIn(employer);
            AssertCandidates(employer, member0, member1);
            AssertCandidates(GetCandidates(), member0, member1);

            // Remove first.

            RemoveCandidate(member0.Id);
            AssertCandidates(employer, member1);
            AssertCandidates(GetCandidates(), member1);

            // Remove again.

            RemoveCandidate(member0.Id);
            AssertCandidates(employer, member1);
            AssertCandidates(GetCandidates(), member1);

            // Remove second.

            RemoveCandidate(member1.Id);
            AssertCandidates(employer);
            AssertCandidates(GetCandidates());
        }

        private void AssertCandidates(IEmployer employer, params EmployerMemberView[] members)
        {
            var candidateIds = _candidateFoldersQuery.GetInMobileFolderCandidateIds(employer);
            Assert.AreEqual(members.Length, candidateIds.Count);
            foreach (var member in members)
            {
                var memberId = member.Id;
                Assert.IsTrue((from c in candidateIds where c == memberId select c).Any());
            }
        }

        private CandidatesResponseModel GetCandidates()
        {
            return Deserialize<CandidatesResponseModel>(Get(_mobileCandidatesUrl), new CandidateModelJavaScriptConverter());
        }

        private void AddCandidate(Guid candidateId)
        {
            var url = _mobileCandidatesUrl.AsNonReadOnly();
            url.Path = url.Path.AddUrlSegments(candidateId.ToString());
            Put(url);
        }

        private void RemoveCandidate(Guid candidateId)
        {
            var url = _mobileCandidatesUrl.AsNonReadOnly();
            url.Path = url.Path.AddUrlSegments(candidateId.ToString());
            Delete(url);
        }
    }
}
