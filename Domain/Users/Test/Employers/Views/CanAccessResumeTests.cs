using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views
{
    [TestClass]
    public class CanAccessResumesTests
        : TestClass
    {
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly ICreditsRepository _creditsRepository = Resolve<ICreditsRepository>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        [TestInitialize]
        public void CanAccessResumesTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNoAllocationNoContact()
        {
            var employer = CreateEmployer(0, false, null);
            TestCanAccessResume(employer, false, CanContactStatus.YesIfHadCredit);
        }

        [TestMethod]
        public void TestNoAllocationWithContact()
        {
            var employer = CreateEmployer(0, false, null);
            TestCanAccessResume(employer, true, CanContactStatus.YesIfHadCredit);
        }

        [TestMethod]
        public void TestNoCreditsNoContact()
        {
            var employer = CreateEmployer(0, true, 0);
            TestCanAccessResume(employer, false, CanContactStatus.YesIfHadCredit);
        }

        [TestMethod]
        public void TestNoCreditsWithContact()
        {
            var employer = CreateEmployer(0, true, 0);
            TestCanAccessResume(employer, true, CanContactStatus.YesWithoutCredit);
        }

        [TestMethod]
        public void TestLimitedCreditsNoContact()
        {
            var employer = CreateEmployer(0, true, 10);
            TestCanAccessResume(employer, false, CanContactStatus.YesWithCredit);
        }

        [TestMethod]
        public void TestLimitedCreditsWithContact()
        {
            var employer = CreateEmployer(0, true, 10);
            TestCanAccessResume(employer, true, CanContactStatus.YesWithoutCredit);
        }

        [TestMethod]
        public void TestUnlimitedCreditsNoContact()
        {
            var employer = CreateEmployer(0, true, null);
            TestCanAccessResume(employer, false, CanContactStatus.YesWithoutCredit);
        }

        [TestMethod]
        public void TestUnlimitedCreditsWithContact()
        {
            var employer = CreateEmployer(0, true, null);
            TestCanAccessResume(employer, true, CanContactStatus.YesWithoutCredit);
        }

        private void TestCanAccessResume(IEmployer employer, bool hasContacted, CanContactStatus status)
        {
            // Has resume and is visible.

            var member = CreateMember(0, true, true);
            if (hasContacted)
                CreateContact(employer.Id, member.Id);
            Assert.AreEqual(status, _employerMemberViewsQuery.GetEmployerMemberView(employer, member).CanAccessResume());

            // No resume.

            member = CreateMember(1, true, false);
            if (hasContacted)
                CreateContact(employer.Id, member.Id);
            Assert.AreEqual(CanContactStatus.No, _employerMemberViewsQuery.GetEmployerMemberView(employer, member).CanAccessResume());

            // Resume not visible.

            member = CreateMember(2, false, true);
            if (hasContacted)
                CreateContact(employer.Id, member.Id);
            Assert.AreEqual(CanContactStatus.No, _employerMemberViewsQuery.GetEmployerMemberView(employer, member).CanAccessResume());

            // Resume not visible and no resume.

            member = CreateMember(4, false, false);
            if (hasContacted)
                CreateContact(employer.Id, member.Id);
            Assert.AreEqual(CanContactStatus.No, _employerMemberViewsQuery.GetEmployerMemberView(employer, member).CanAccessResume());
        }

        private Employer CreateEmployer(int index, bool allocateCredits, int? quantity)
        {
            var employer = _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
            if (allocateCredits)
                _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id, InitialQuantity = quantity });
            return employer;
        }

        private void CreateContact(Guid employerId, Guid memberId)
        {
            var exercisedCredit = new ExercisedCredit
            {
                ExercisedById = employerId,
                ExercisedOnId = memberId,
            };

            exercisedCredit.Prepare();
            _creditsRepository.CreateExercisedCredit(exercisedCredit);
        }

        private Member CreateMember(int index, bool isResumeVisible, bool hasResume)
        {
            var member = _membersCommand.CreateTestMember(index);

            var candidate = new Candidate { Id = member.Id };
            _candidatesCommand.CreateCandidate(candidate);
            if (hasResume)
                _candidateResumesCommand.AddTestResume(candidate);

            member.VisibilitySettings.Professional.EmploymentVisibility = isResumeVisible
                ? member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Resume)
                : member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Resume);

            _membersCommand.UpdateMember(member);
            return member;
        }
    }
}