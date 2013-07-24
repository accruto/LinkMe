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

namespace LinkMe.Domain.Users.Test.Employers.Candidates.FlagLists
{
    [TestClass]
    public abstract class CandidateFlagListsTests
        : TestClass
    {
        protected readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();
        protected readonly ICandidateFlagListsQuery _candidateFlagListsQuery = Resolve<ICandidateFlagListsQuery>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        [TestInitialize]
        public void CandidateFlagListsTestsInitialize()
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

        protected CandidateFlagList GetFlaggedFlagList(IEmployer employer)
        {
            return _candidateFlagListsQuery.GetFlagList(employer);
        }

        protected CandidateFlagList GetFlaggedFlagList(IEmployer employer, int index)
        {
            return GetFlaggedFlagList(employer);
        }

        protected void AssertFlagListEntries(IEmployer employer, ICollection<Member> isFlaggedMembers, ICollection<Member> isNotFlaggedMembers)
        {
            // IsFlagged.

            foreach (var isFlaggedMember in isFlaggedMembers)
                Assert.IsTrue(_candidateFlagListsQuery.IsFlagged(employer, isFlaggedMember.Id));
            foreach (var isNotFlaggedMember in isNotFlaggedMembers)
                Assert.IsFalse(_candidateFlagListsQuery.IsFlagged(employer, isNotFlaggedMember.Id));

            // GetFlaggedCount.

            Assert.AreEqual(isFlaggedMembers.Count, _candidateFlagListsQuery.GetFlaggedCount(employer));

            // GetFlaggedCandidateIds.

            Assert.IsTrue((from m in isFlaggedMembers select m.Id).CollectionEqual(_candidateFlagListsQuery.GetFlaggedCandidateIds(employer)));
            Assert.IsTrue((from m in isFlaggedMembers select m.Id).CollectionEqual(_candidateFlagListsQuery.GetFlaggedCandidateIds(employer, from m in isFlaggedMembers.Concat(isNotFlaggedMembers) select m.Id)));
        }

        protected static void AssertFlagList(IEmployer employer, CandidateFlagList expectedFlagList, IList<CandidateFlagList> flagLists)
        {
            Assert.AreEqual(1, flagLists.Count);
            AssertFlagList(employer, expectedFlagList, flagLists[0]);
        }

        protected static void AssertFlagList(IEmployer employer, CandidateFlagList expectedFlagList, CandidateFlagList flagList)
        {
            Assert.AreNotEqual(Guid.Empty, flagList.Id);
            Assert.AreNotEqual(DateTime.MinValue, flagList.CreatedTime);

            if (expectedFlagList.Id != Guid.Empty)
                Assert.AreEqual(expectedFlagList.Id, flagList.Id);
            Assert.AreEqual(expectedFlagList.Name, flagList.Name);

            Assert.IsNull(flagList.Name);
            Assert.AreEqual(employer.Id, flagList.RecruiterId);
            Assert.AreEqual(expectedFlagList.RecruiterId, flagList.RecruiterId);
        }
    }
}