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
    public class RecordsTests
        : AccessesTests
    {
        private Guid _contactCreditId;

        [TestInitialize]
        public void RecordsTestsInitialize()
        {
            _contactCreditId = _creditsQuery.GetCredit<ContactCredit>().Id;
        }

        [TestMethod]
        public void TestEmployerLimitedCredits()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation {CreditId = _contactCreditId, OwnerId = employer.Id, InitialQuantity = 100});

            // Not accessed yet.

            var member = _membersCommand.CreateTestMember(0);
            var view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
            Assert.AreEqual(ProfessionalContactDegree.NotContacted, view.EffectiveContactDegree);

            // Access.

            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, view, MemberAccessReason.MessageSent);
            _employerMemberViewsCommand.AccessMember(_app, employer, view, MemberAccessReason.MessageSent);

            // Should be 1 exercised credit signifying that credits have been checked
            // and 1 access.

            view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(1, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsTrue(_employerMemberViewsQuery.HasAccessedMember(employer, member.Id));
            Assert.AreEqual(1, _employerMemberViewsQuery.GetAccessedMemberIds(employer).Count);
            Assert.AreEqual(1, _employerMemberViewsQuery.GetMemberAccesses(employer, member.Id).Count);

            // Access again.

            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, view, MemberAccessReason.MessageSent);
            _employerMemberViewsCommand.AccessMember(_app, employer, view, MemberAccessReason.MessageSent);

            // No new exercised credit because that has already been done
            // but a new access.

            view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(1, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsTrue(_employerMemberViewsQuery.HasAccessedMember(employer, member.Id));
            Assert.AreEqual(1, _employerMemberViewsQuery.GetAccessedMemberIds(employer).Count);
            Assert.AreEqual(2, _employerMemberViewsQuery.GetMemberAccesses(employer, member.Id).Count);
        }

        [TestMethod]
        public void TestEmployerUnlimitedCredits()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _contactCreditId, OwnerId = employer.Id, InitialQuantity = null });

            // Not accessed yet.

            var member = _membersCommand.CreateTestMember(0);
            var view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            // Access.

            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, view, MemberAccessReason.MessageSent);
            _employerMemberViewsCommand.AccessMember(_app, employer, view, MemberAccessReason.MessageSent);

            // Should be 1 exercised credit signifying that credits have been checked
            // and 1 access.

            view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(1, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsTrue(_employerMemberViewsQuery.HasAccessedMember(employer, member.Id));
            Assert.AreEqual(1, _employerMemberViewsQuery.GetAccessedMemberIds(employer).Count);
            Assert.AreEqual(1, _employerMemberViewsQuery.GetMemberAccesses(employer, member.Id).Count);

            // Access again.

            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, view, MemberAccessReason.MessageSent);
            _employerMemberViewsCommand.AccessMember(_app, employer, view, MemberAccessReason.MessageSent);

            // No new exercised credit because that has already been done
            // but a new access.

            view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(1, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsTrue(_employerMemberViewsQuery.HasAccessedMember(employer, member.Id));
            Assert.AreEqual(1, _employerMemberViewsQuery.GetAccessedMemberIds(employer).Count);
            Assert.AreEqual(2, _employerMemberViewsQuery.GetMemberAccesses(employer, member.Id).Count);
        }

        [TestMethod]
        public void TestOrganisationLimitedCredits()
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _contactCreditId, OwnerId = organisation.Id, InitialQuantity = 100 });
            var employer0 = _employersCommand.CreateTestEmployer(0, organisation);
            var employer1 = _employersCommand.CreateTestEmployer(0, organisation);

            // Not accessed yet.

            var member = _membersCommand.CreateTestMember(0);
            var view = _employerMemberViewsQuery.GetProfessionalView(employer0, member);
            Assert.AreEqual(ProfessionalContactDegree.NotContacted, view.EffectiveContactDegree);
            view = _employerMemberViewsQuery.GetProfessionalView(employer1, member);
            Assert.AreEqual(ProfessionalContactDegree.NotContacted, view.EffectiveContactDegree);

            // Access by employer0.

            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer0, view, MemberAccessReason.MessageSent);
            _employerMemberViewsCommand.AccessMember(_app, employer0, view, MemberAccessReason.MessageSent);

            // Should be 1 exercised credit signifying that credits have been checked
            // and 1 access.

            view = _employerMemberViewsQuery.GetProfessionalView(employer0, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(1, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer0.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsTrue(_employerMemberViewsQuery.HasAccessedMember(employer0, member.Id));
            Assert.AreEqual(1, _employerMemberViewsQuery.GetAccessedMemberIds(employer0).Count);
            Assert.AreEqual(1, _employerMemberViewsQuery.GetMemberAccesses(employer0, member.Id).Count);

            // Nothing for other employer though they are effectively contacted.

            view = _employerMemberViewsQuery.GetProfessionalView(employer1, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(0, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer1.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsFalse(_employerMemberViewsQuery.HasAccessedMember(employer1, member.Id));
            Assert.AreEqual(0, _employerMemberViewsQuery.GetAccessedMemberIds(employer1).Count);
            Assert.AreEqual(0, _employerMemberViewsQuery.GetMemberAccesses(employer1, member.Id).Count);

            // Access again by employer0.

            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer0, view, MemberAccessReason.MessageSent);
            _employerMemberViewsCommand.AccessMember(_app, employer0, view, MemberAccessReason.MessageSent);

            // No new exercised credit because that has already been done
            // but a new access.

            view = _employerMemberViewsQuery.GetProfessionalView(employer0, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(1, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer0.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsTrue(_employerMemberViewsQuery.HasAccessedMember(employer0, member.Id));
            Assert.AreEqual(1, _employerMemberViewsQuery.GetAccessedMemberIds(employer0).Count);
            Assert.AreEqual(2, _employerMemberViewsQuery.GetMemberAccesses(employer0, member.Id).Count);

            // Nothing changed for other employer.

            view = _employerMemberViewsQuery.GetProfessionalView(employer1, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(0, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer1.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsFalse(_employerMemberViewsQuery.HasAccessedMember(employer1, member.Id));
            Assert.AreEqual(0, _employerMemberViewsQuery.GetAccessedMemberIds(employer1).Count);
            Assert.AreEqual(0, _employerMemberViewsQuery.GetMemberAccesses(employer1, member.Id).Count);

            // Access by employer1.

            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer1, view, MemberAccessReason.MessageSent);
            _employerMemberViewsCommand.AccessMember(_app, employer1, view, MemberAccessReason.MessageSent);

            // Nothing changed for first employer.

            view = _employerMemberViewsQuery.GetProfessionalView(employer0, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(1, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer0.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsTrue(_employerMemberViewsQuery.HasAccessedMember(employer0, member.Id));
            Assert.AreEqual(1, _employerMemberViewsQuery.GetAccessedMemberIds(employer0).Count);
            Assert.AreEqual(2, _employerMemberViewsQuery.GetMemberAccesses(employer0, member.Id).Count);

            // Should be no exercised credit because that was used by the other employer,
            // and 1 access.

            view = _employerMemberViewsQuery.GetProfessionalView(employer1, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(0, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer1.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsTrue(_employerMemberViewsQuery.HasAccessedMember(employer1, member.Id));
            Assert.AreEqual(1, _employerMemberViewsQuery.GetAccessedMemberIds(employer1).Count);
            Assert.AreEqual(1, _employerMemberViewsQuery.GetMemberAccesses(employer1, member.Id).Count);
        }

        [TestMethod]
        public void TestOrganisationUnlimitedCredits()
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _contactCreditId, OwnerId = organisation.Id, InitialQuantity = null });
            var employer0 = _employersCommand.CreateTestEmployer(0, organisation);
            var employer1 = _employersCommand.CreateTestEmployer(0, organisation);

            // Not accessed yet.

            var member = _membersCommand.CreateTestMember(0);
            var view = _employerMemberViewsQuery.GetProfessionalView(employer0, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);
            view = _employerMemberViewsQuery.GetProfessionalView(employer1, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            // Access by employer0.

            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer0, view, MemberAccessReason.MessageSent);
            _employerMemberViewsCommand.AccessMember(_app, employer0, view, MemberAccessReason.MessageSent);

            // Should be 1 exercised credit signifying that credits have been checked
            // and 1 access.

            view = _employerMemberViewsQuery.GetProfessionalView(employer0, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(1, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer0.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsTrue(_employerMemberViewsQuery.HasAccessedMember(employer0, member.Id));
            Assert.AreEqual(1, _employerMemberViewsQuery.GetAccessedMemberIds(employer0).Count);
            Assert.AreEqual(1, _employerMemberViewsQuery.GetMemberAccesses(employer0, member.Id).Count);

            // Nothing for other employer though they are effectively contacted.

            view = _employerMemberViewsQuery.GetProfessionalView(employer1, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(0, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer1.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsFalse(_employerMemberViewsQuery.HasAccessedMember(employer1, member.Id));
            Assert.AreEqual(0, _employerMemberViewsQuery.GetAccessedMemberIds(employer1).Count);
            Assert.AreEqual(0, _employerMemberViewsQuery.GetMemberAccesses(employer1, member.Id).Count);

            // Access again by employer0.

            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer0, view, MemberAccessReason.MessageSent);
            _employerMemberViewsCommand.AccessMember(_app, employer0, view, MemberAccessReason.MessageSent);

            // No new exercised credit because that has already been done
            // but a new access.

            view = _employerMemberViewsQuery.GetProfessionalView(employer0, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(1, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer0.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsTrue(_employerMemberViewsQuery.HasAccessedMember(employer0, member.Id));
            Assert.AreEqual(1, _employerMemberViewsQuery.GetAccessedMemberIds(employer0).Count);
            Assert.AreEqual(2, _employerMemberViewsQuery.GetMemberAccesses(employer0, member.Id).Count);

            // Nothing changed for other employer.

            view = _employerMemberViewsQuery.GetProfessionalView(employer1, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(0, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer1.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsFalse(_employerMemberViewsQuery.HasAccessedMember(employer1, member.Id));
            Assert.AreEqual(0, _employerMemberViewsQuery.GetAccessedMemberIds(employer1).Count);
            Assert.AreEqual(0, _employerMemberViewsQuery.GetMemberAccesses(employer1, member.Id).Count);

            // Access by employer1.

            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer1, view, MemberAccessReason.MessageSent);
            _employerMemberViewsCommand.AccessMember(_app, employer1, view, MemberAccessReason.MessageSent);

            // Nothing changed for first employer.

            view = _employerMemberViewsQuery.GetProfessionalView(employer0, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(1, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer0.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsTrue(_employerMemberViewsQuery.HasAccessedMember(employer0, member.Id));
            Assert.AreEqual(1, _employerMemberViewsQuery.GetAccessedMemberIds(employer0).Count);
            Assert.AreEqual(2, _employerMemberViewsQuery.GetMemberAccesses(employer0, member.Id).Count);

            // Should be no exercised credit because that was used by the other employer,
            // and 1 access.

            view = _employerMemberViewsQuery.GetProfessionalView(employer1, member);
            Assert.AreEqual(ProfessionalContactDegree.Contacted, view.EffectiveContactDegree);

            Assert.AreEqual(0, _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(employer1.Id, new DateTimeRange(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(100))).Count);

            Assert.IsTrue(_employerMemberViewsQuery.HasAccessedMember(employer1, member.Id));
            Assert.AreEqual(1, _employerMemberViewsQuery.GetAccessedMemberIds(employer1).Count);
            Assert.AreEqual(1, _employerMemberViewsQuery.GetMemberAccesses(employer1, member.Id).Count);
        }
    }
}
