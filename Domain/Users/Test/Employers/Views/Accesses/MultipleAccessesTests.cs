using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Test.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views.Accesses
{
    [TestClass]
    public class MultipleAccessesTests
        : AccessesTests
    {
        [TestMethod]
        public void TestMultipleAccesses()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            CreateAllocation(employer.Id, 100);

            // Access.

            const int count = 10;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _membersCommand.CreateTestMember(index);
            _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, members), MemberAccessReason.Unlock);

            // Access again.

            _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, members), MemberAccessReason.Unlock);
        }

        [TestMethod]
        public void TestEmployerCredits()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            TestCredits(employer, 4, 2, employer.Id);
        }

        [TestMethod]
        public void TestOrganisationCredits()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            TestCredits(employer, 4, 2, employer.Organisation.Id);
        }

        [TestMethod]
        public void TestChildOrganisationCredits()
        {
            var administratorId = Guid.NewGuid();
            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, null, administratorId);
            var childOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, parentOrganisation, administratorId);
            var employer = _employersCommand.CreateTestEmployer(0, childOrganisation);
            TestCredits(employer, 4, 2, childOrganisation.Id);
        }

        [TestMethod]
        public void TestParentOrganisationCredits()
        {
            var administratorId = Guid.NewGuid();
            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, null, administratorId);
            var childOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, parentOrganisation, administratorId);
            var employer = _employersCommand.CreateTestEmployer(0, childOrganisation);
            TestCredits(employer, 4, 2, parentOrganisation.Id);
        }

        [TestMethod]
        public void TestEmployerOrganisationCredits()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            TestCredits(employer, 4, 2, employer.Organisation.Id, employer.Id);
        }

        [TestMethod]
        public void TestEmployerOrganisationParentOrganisationCredits()
        {
            var administratorId = Guid.NewGuid();
            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, null, administratorId);
            var childOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, parentOrganisation, administratorId);
            var employer = _employersCommand.CreateTestEmployer(0, childOrganisation);
            TestCredits(employer, 4, 2, parentOrganisation.Id, childOrganisation.Id, employer.Id);
        }

        private void TestCredits(IEmployer employer, int allocationCount, int memberCount, params Guid[] ownerIds)
        {
            foreach (var ownerId in ownerIds)
                CreateAllocation(ownerId, allocationCount);

            // Access for those credits.

            var index = 0;
            var count = memberCount;
            while (count <= ownerIds.Length * allocationCount)
            {
                var members = new Member[memberCount];
                for (var memberIndex = 0; memberIndex < memberCount; ++memberIndex)
                    members[memberIndex] = _membersCommand.CreateTestMember(++index);
                _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, members), MemberAccessReason.Unlock);

                count += memberCount;
            }

            // The next one should put them over the limit.

            AssertAccessMembersThrow(employer, index, memberCount, 0, memberCount);
        }

        protected void AssertAccessMembersThrow(IEmployer employer, int index, int memberCount, int expectedAvailable, int expectedRequired)
        {
            try
            {
                var members = new Member[memberCount];
                for (var memberIndex = 0; memberIndex < memberCount; ++memberIndex)
                    members[memberIndex] = _membersCommand.CreateTestMember(++index);
                _employerMemberViewsCommand.AccessMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, members), MemberAccessReason.Unlock);
                Assert.Fail();
            }
            catch (InsufficientCreditsException ex)
            {
                Assert.AreEqual(expectedAvailable, ex.Available);
                Assert.AreEqual(expectedRequired, ex.Required);
            }
        }
    }
}
