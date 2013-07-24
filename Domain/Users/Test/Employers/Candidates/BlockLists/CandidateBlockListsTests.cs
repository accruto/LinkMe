using System;
using System.Collections.Generic;
using System.Linq;
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
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.BlockLists
{
    [TestClass]
    public abstract class CandidateBlockListsTests
        : TestClass
    {
        protected readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();
        protected readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        [TestInitialize]
        public void CandidateBlockListsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected Employer CreateEmployer(int index)
        {
            return _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestVerifiedOrganisation(index));
        }

        protected Employer CreateEmployer(int index, IOrganisation organisation)
        {
            return _employersCommand.CreateTestEmployer(index, organisation);
        }

        protected Member CreateMember(int index)
        {
            return _membersCommand.CreateTestMember(index);
        }

        protected CandidateBlockList GetTemporaryBlockList(IEmployer employer)
        {
            return _candidateBlockListsQuery.GetTemporaryBlockList(employer);
        }

        protected CandidateBlockList GetPermanentBlockList(IEmployer employer)
        {
            return _candidateBlockListsQuery.GetPermanentBlockList(employer);
        }

        protected void AssertBlockListCandidates(IEmployer employer, CandidateBlockList blockList, ICollection<Member> blockedMembers, ICollection<Member> notBlockedMembers)
        {
            // IsBlocked

            foreach (var member in blockedMembers)
                Assert.IsTrue(_candidateBlockListsQuery.IsBlocked(employer, blockList.Id, member.Id));
            foreach (var member in notBlockedMembers)
                Assert.IsFalse(_candidateBlockListsQuery.IsBlocked(employer, blockList.Id, member.Id));

            if (blockList.BlockListType == BlockListType.Permanent)
            {
                foreach (var member in blockedMembers)
                    Assert.IsTrue(_candidateBlockListsQuery.IsPermanentlyBlocked(employer, member.Id));
                foreach (var member in notBlockedMembers)
                    Assert.IsFalse(_candidateBlockListsQuery.IsPermanentlyBlocked(employer, member.Id));
            }

            // GetBlockedCandidateIds

            Assert.IsTrue((from m in blockedMembers select m.Id).CollectionEqual(_candidateBlockListsQuery.GetBlockedCandidateIds(employer, blockList.Id)));
            if (blockList.BlockListType == BlockListType.Permanent)
                Assert.IsTrue((from m in blockedMembers select m.Id).CollectionEqual(_candidateBlockListsQuery.GetPermanentlyBlockedCandidateIds(employer)));

            // GetEntryCounts

            var counts = _candidateBlockListsQuery.GetBlockedCounts(employer);
            if (counts.ContainsKey(blockList.Id))
                Assert.AreEqual(blockedMembers.Count, counts[blockList.Id]);
            else
                Assert.AreEqual(blockedMembers.Count, 0);
        }

        protected void AssertBlockListCandidates(IEmployer employer, ICollection<Member> blockedMembers, ICollection<Member> notBlockedMembers)
        {
            // IsBlocked

            foreach (var member in blockedMembers)
                Assert.IsTrue(_candidateBlockListsQuery.IsBlocked(employer, member.Id));
            foreach (var member in notBlockedMembers)
                Assert.IsFalse(_candidateBlockListsQuery.IsBlocked(employer, member.Id));

            // GetBlockedCandidateIds

            Assert.IsTrue((from m in blockedMembers select m.Id).CollectionEqual(_candidateBlockListsQuery.GetBlockedCandidateIds(employer)));
        }

        protected static void AssertBlockLists(IEmployer employer, IList<CandidateBlockList> blockLists)
        {
            // Should always have a flagged and shortlist blockList.

            Assert.AreEqual(2, blockLists.Count);

            // Look for the temporary blockList.

            var flaggedBlockList = new CandidateBlockList { RecruiterId = employer.Id, BlockListType = BlockListType.Temporary };
            AssertBlockList(employer, flaggedBlockList, (from f in blockLists where f.BlockListType == BlockListType.Temporary select f).Single());

            // Look for the shortlist blockList.

            var shortlistBlockList = new CandidateBlockList { RecruiterId = employer.Id, BlockListType = BlockListType.Permanent };
            AssertBlockList(employer, shortlistBlockList, (from f in blockLists where f.BlockListType == BlockListType.Permanent select f).Single());
        }

        protected static void AssertBlockList(IEmployer employer, CandidateBlockList expectedBlockList, IList<CandidateBlockList> blockLists)
        {
            Assert.AreEqual(1, blockLists.Count);
            AssertBlockList(employer, expectedBlockList, blockLists[0]);
        }

        protected static void AssertBlockList(IEmployer employer, CandidateBlockList expectedBlockList, CandidateBlockList blockList)
        {
            Assert.AreNotEqual(Guid.Empty, blockList.Id);
            Assert.AreNotEqual(DateTime.MinValue, blockList.CreatedTime);

            if (!((expectedBlockList.BlockListType == BlockListType.Permanent || expectedBlockList.BlockListType == BlockListType.Temporary) && expectedBlockList.Id == Guid.Empty))
                Assert.AreEqual(expectedBlockList.Id, blockList.Id);
            Assert.AreEqual(expectedBlockList.Name, blockList.Name);
            Assert.AreEqual(expectedBlockList.BlockListType, blockList.BlockListType);
            Assert.IsNull(blockList.Name);
            Assert.AreEqual(employer.Id, blockList.RecruiterId);
            Assert.AreEqual(expectedBlockList.RecruiterId, blockList.RecruiterId);
        }
    }
}