using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Members;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Folders
{
    [TestClass]
    public class SortOrderTests
        : FoldersTests
    {
        private readonly ICandidateFlagListsQuery _candidateFlagListsQuery = Resolve<ICandidateFlagListsQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IMembersRepository _membersRepository = Resolve<IMembersRepository>();
        private readonly ICandidatesRepository _candidatesRepository = Resolve<ICandidatesRepository>();
        private readonly IResumesRepository _resumesRepository = Resolve<IResumesRepository>();

        private static readonly MemberSortOrder[] SortOrders = new[]
        {
            MemberSortOrder.DateUpdated,
            MemberSortOrder.FirstName,
            MemberSortOrder.Flagged,
        };

        [TestMethod]
        public void TestDefaultSortOrder()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            Get(GetFolderUrl(_candidateFoldersQuery.GetShortlistFolder(employer).Id));
            AssertSortOrders(SortOrders, MemberSortOrder.DateUpdated, false);
        }

        [TestMethod]
        public void TestDateUpdated()
        {
            var member0 = CreateMember(0);
            var candidate0 = _candidatesQuery.GetCandidate(member0.Id);
            var resume0 = _resumesQuery.GetResume(candidate0.ResumeId.Value);
            UpdateMember(member0, candidate0, resume0, DateTime.Now.AddDays(-2));

            var member1 = CreateMember(1);
            var candidate1 = _candidatesQuery.GetCandidate(member0.Id);
            var resume1 = _resumesQuery.GetResume(candidate1.ResumeId.Value);
            UpdateMember(member1, candidate1, resume1, DateTime.Now.AddDays(-1));

            var employer = CreateEmployer();
            TestSortOrder(employer, MemberSortOrder.DateUpdated, member0, member1);
        }

        [TestMethod]
        public void TestFirstName()
        {
            var employer = CreateEmployer();
            var member0 = CreateMember(0);
            var member1 = CreateMember(1);
            TestSortOrder(employer, MemberSortOrder.FirstName, member1, member0);
        }

        [TestMethod]
        public void TestFlagged()
        {
            var employer = CreateEmployer();
            var member0 = CreateMember(0);
            var member1 = CreateMember(1);
            _candidateListsCommand.AddCandidateToFlagList(employer, _candidateFlagListsQuery.GetFlagList(employer), member1.Id);
            TestSortOrder(employer, MemberSortOrder.Flagged, member0, member1);
        }

        private void TestSortOrder(IEmployer employer, MemberSortOrder sortOrder, params Member[] members)
        {
            // Give the employer some credits to see the mmebers.

            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);

            // Add members to folder.

            _candidateListsCommand.AddCandidatesToFolder(employer, folder, from m in members select m.Id);
            LogIn(employer);

            // Test ascending.

            Get(GetFolderUrl(folder.Id, sortOrder, true));
            AssertSortOrders(SortOrders, sortOrder, true);
            AssertMembers(true, members);

            // Test descending, order should be reversed.

            Get(GetFolderUrl(folder.Id, sortOrder, false));
            AssertSortOrders(SortOrders, sortOrder, false);
            AssertMembers(true, members.Reverse().ToArray());
        }

        private ReadOnlyUrl GetFolderUrl(Guid folderId, MemberSortOrder sortOrder, bool isAscending)
        {
            var url = GetFolderUrl(folderId).AsNonReadOnly();
            url.QueryString.Add("SortOrder", sortOrder.ToString());
            url.QueryString.Add("SortOrderDirection", isAscending ? "SortOrderIsAscending" : "SortOrderIsDescending");
            return url;
        }

        private void UpdateMember(Member member, Candidate candidate, Resume resume, DateTime lastUpdatedTime)
        {
            member.CreatedTime = lastUpdatedTime;
            member.LastUpdatedTime = lastUpdatedTime;
            _membersRepository.UpdateMember(member);

            candidate.LastUpdatedTime = lastUpdatedTime;
            _candidatesRepository.UpdateCandidate(candidate);

            resume.LastUpdatedTime = DateTime.Now.AddDays(-2);
            _resumesRepository.UpdateResume(resume);

            _memberSearchService.UpdateMember(member.Id);
        }
    }
}