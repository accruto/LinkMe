using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views.Accesses
{
    [TestClass]
    public class SingleAccessesTests
        : AccessesTests
    {
        [TestMethod]
        public void TestEmployerCredits()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            TestCredits(employer, employer.Id);
        }

        [TestMethod]
        public void TestOrganisationCredits()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            TestCredits(employer, employer.Organisation.Id);
        }

        [TestMethod]
        public void TestChildOrganisationCredits()
        {
            var administratorId = Guid.NewGuid();
            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, null, administratorId);
            var childOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, parentOrganisation, administratorId);
            var employer = _employersCommand.CreateTestEmployer(0, childOrganisation);
            TestCredits(employer, childOrganisation.Id);
        }

        [TestMethod]
        public void TestParentOrganisationCredits()
        {
            var administratorId = Guid.NewGuid();
            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, null, administratorId);
            var childOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, parentOrganisation, administratorId);
            var employer = _employersCommand.CreateTestEmployer(0, childOrganisation);
            TestCredits(employer, parentOrganisation.Id);
        }

        [TestMethod]
        public void TestEmployerOrganisationCredits()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            TestCredits(employer, employer.Organisation.Id, employer.Id);
        }

        [TestMethod]
        public void TestEmployerOrganisationParentOrganisationCredits()
        {
            var administratorId = Guid.NewGuid();
            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, null, administratorId);
            var childOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, parentOrganisation, administratorId);
            var employer = _employersCommand.CreateTestEmployer(0, childOrganisation);
            TestCredits(employer, parentOrganisation.Id, childOrganisation.Id, employer.Id);
        }

        private void TestCredits(IEmployer employer, params Guid[] ownerIds)
        {
            const int count = 4;
            foreach (var ownerId in ownerIds)
                CreateAllocation(ownerId, count);

            // Access for those credits.

            Member member;
            var index = 0;
            for (; index < ownerIds.Length * count; ++index)
            {
                member = _membersCommand.CreateTestMember(index);
                CheckCanAccessMember(employer, member);
                AccessMember(employer, member);
            }

            // The next one should put them over the limit.

            member = _membersCommand.CreateTestMember(index);
            AssertCheckCanAccessMemberThrow(employer, member, 0, 1);
            AssertAccessMemberThrow(employer, member, 0, 1);
        }
    }
}
