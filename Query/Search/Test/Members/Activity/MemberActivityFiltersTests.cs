using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Activity
{
    [TestClass]
    public abstract class MemberActivityFiltersTests
        : TestClass
    {
        private readonly IMemberActivityFiltersQuery _memberActivityFiltersQuery = Resolve<IMemberActivityFiltersQuery>();
        protected readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        protected readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();
        protected readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();
        protected readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();
        protected readonly ICandidateFlagListsQuery _candidateFlagListsQuery = Resolve<ICandidateFlagListsQuery>();
        protected readonly ICandidateNotesCommand _candidateNotesCommand = Resolve<ICandidateNotesCommand>();
        protected readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        protected readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

        [TestInitialize]
        public void MemberActivityFiltersTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected void TestFilter(IEmployer employer, Func<bool?, MemberSearchQuery> createQuery, ICollection<Guid> allMemberIds, ICollection<Guid> isSetMemberIds, ICollection<Guid> isNotSetMemberIds)
        {
            TestFilter(allMemberIds, employer, createQuery(null), allMemberIds);
            TestFilter(isSetMemberIds, employer, createQuery(true), allMemberIds);
            TestFilter(isNotSetMemberIds, employer, createQuery(false), allMemberIds);
        }

        protected void TestFilter(ICollection<Guid> expectedMemberIds, IEmployer employer, MemberSearchQuery query, IEnumerable<Guid> allMemberIds)
        {
            AssertMemberIds(expectedMemberIds, Filter(employer, query, allMemberIds));
        }

        protected void TestFolderFilter(ICollection<Guid> expectedMemberIds, IEmployer employer, Guid folderId, IEnumerable<Guid> allMemberIds)
        {
            AssertMemberIds(expectedMemberIds, FolderFilter(employer, folderId, allMemberIds));
        }

        protected void TestFlagListFilter(ICollection<Guid> expectedMemberIds, IEmployer employer, IEnumerable<Guid> allMemberIds)
        {
            AssertMemberIds(expectedMemberIds, FlagListFilter(employer, allMemberIds));
        }

        protected void TestBlockListFilter(ICollection<Guid> expectedMemberIds, IEmployer employer, Guid blockListId, IEnumerable<Guid> allMemberIds)
        {
            AssertMemberIds(expectedMemberIds, BlockListFilter(employer, blockListId, allMemberIds));
        }

        private ICollection<Guid> Filter(IEmployer employer, MemberSearchQuery query, IEnumerable<Guid> allMemberIds)
        {
            var includeMemberIds = _memberActivityFiltersQuery.GetIncludeMemberIds(employer, query);
            var memberIds = includeMemberIds != null
                ? allMemberIds.Intersect(includeMemberIds)
                : allMemberIds;

            var excludeMemberIds = _memberActivityFiltersQuery.GetExcludeMemberIds(employer, query);
            memberIds = excludeMemberIds != null
                ? memberIds.Except(excludeMemberIds)
                : memberIds;

            return memberIds.ToArray();
        }

        private ICollection<Guid> FolderFilter(IEmployer employer, Guid folderId, IEnumerable<Guid> allMemberIds)
        {
            var query = new MemberSearchQuery();
            var includeMemberIds = _memberActivityFiltersQuery.GetFolderIncludeMemberIds(employer, folderId, query);
            var memberIds = includeMemberIds != null
                ? allMemberIds.Intersect(includeMemberIds)
                : allMemberIds;

            var excludeMemberIds = _memberActivityFiltersQuery.GetFolderExcludeMemberIds(employer, folderId, query);
            memberIds = excludeMemberIds != null
                ? memberIds.Except(excludeMemberIds)
                : memberIds;

            return memberIds.ToArray();
        }

        private ICollection<Guid> FlagListFilter(IEmployer employer, IEnumerable<Guid> allMemberIds)
        {
            var query = new MemberSearchQuery();
            var includeMemberIds = _memberActivityFiltersQuery.GetFlaggedIncludeMemberIds(employer, query);
            var memberIds = includeMemberIds != null
                ? allMemberIds.Intersect(includeMemberIds)
                : allMemberIds;

            var excludeMemberIds = _memberActivityFiltersQuery.GetFlaggedExcludeMemberIds(employer, query);
            memberIds = excludeMemberIds != null
                ? memberIds.Except(excludeMemberIds)
                : memberIds;

            return memberIds.ToArray();
        }

        private ICollection<Guid> BlockListFilter(IEmployer employer, Guid blockListId, IEnumerable<Guid> allMemberIds)
        {
            var query = new MemberSearchQuery();
            var includeMemberIds = _memberActivityFiltersQuery.GetBlockListIncludeMemberIds(employer, blockListId, query);
            var memberIds = includeMemberIds != null
                ? allMemberIds.Intersect(includeMemberIds)
                : allMemberIds;

            var excludeMemberIds = _memberActivityFiltersQuery.GetBlockListExcludeMemberIds(employer, blockListId, query);
            memberIds = excludeMemberIds != null
                ? memberIds.Except(excludeMemberIds)
                : memberIds;

            return memberIds.ToArray();
        }

        private static void AssertMemberIds(ICollection<Guid> expectedMemberIds, ICollection<Guid> memberIds)
        {
            Assert.AreEqual(expectedMemberIds.Count, memberIds.Count);
            foreach (var expectedMemberId in expectedMemberIds)
                Assert.IsTrue(memberIds.Contains(expectedMemberId));
        }
    }
}
