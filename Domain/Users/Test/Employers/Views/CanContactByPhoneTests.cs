using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
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
    public class CanContactByPhoneTests
        : TestClass
    {
        private const string MobilePhoneNumberFormat = "1999999{0}";
        private const string HomePhoneNumberFormat = "2999999{0}";
        private const string WorkPhoneNumberFormat = "3999999{0}";

        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly ICreditsRepository _creditsRepository = Resolve<ICreditsRepository>();

        [TestInitialize]
        public void CanContactByPhoneTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNoAllocationNoContact()
        {
            var employer = CreateEmployer(0, false, null);
            TestCanContactByPhone(employer, false, CanContactStatus.YesIfHadCredit);
        }

        [TestMethod]
        public void TestNoAllocationWithContact()
        {
            var employer = CreateEmployer(0, false, null);
            TestCanContactByPhone(employer, true, CanContactStatus.YesIfHadCredit);
        }

        [TestMethod]
        public void TestNoCreditsNoContact()
        {
            var employer = CreateEmployer(0, true, 0);
            TestCanContactByPhone(employer, false, CanContactStatus.YesIfHadCredit);
        }

        [TestMethod]
        public void TestNoCreditsWithContact()
        {
            var employer = CreateEmployer(0, true, 0);
            TestCanContactByPhone(employer, true, CanContactStatus.YesWithoutCredit);
        }

        [TestMethod]
        public void TestLimitedCreditsNoContact()
        {
            var employer = CreateEmployer(0, true, 10);
            TestCanContactByPhone(employer, false, CanContactStatus.YesWithCredit);
        }

        [TestMethod]
        public void TestLimitedCreditsWithContact()
        {
            var employer = CreateEmployer(0, true, 10);
            TestCanContactByPhone(employer, true, CanContactStatus.YesWithoutCredit);
        }

        [TestMethod]
        public void TestUnlimitedCreditsNoContact()
        {
            var employer = CreateEmployer(0, true, null);
            TestCanContactByPhone(employer, false, CanContactStatus.YesWithoutCredit);
        }

        [TestMethod]
        public void TestUnlimitedCreditsWithContact()
        {
            var employer = CreateEmployer(0, true, null);
            TestCanContactByPhone(employer, true, CanContactStatus.YesWithoutCredit);
        }

        private void TestCanContactByPhone(IEmployer employer, bool hasContacted, CanContactStatus status)
        {
            // Has phone number and is visible.

            var member = CreateMember(0, true, true, true);
            if (hasContacted)
                CreateContact(employer.Id, member.Id);
            Assert.AreEqual(status, _employerMemberViewsQuery.GetProfessionalView(employer, member).CanContactByPhone());

            // No phone number.

            member = CreateMember(1, true, true, false);
            if (hasContacted)
                CreateContact(employer.Id, member.Id);
            Assert.AreEqual(CanContactStatus.No, _employerMemberViewsQuery.GetProfessionalView(employer, member).CanContactByPhone());

            // Resume not visible.

            member = CreateMember(2, false, true, true);
            if (hasContacted)
                CreateContact(employer.Id, member.Id);
            Assert.AreEqual(CanContactStatus.No, _employerMemberViewsQuery.GetProfessionalView(employer, member).CanContactByPhone());

            // Phone number not visible.

            member = CreateMember(3, true, false, true);
            if (hasContacted)
                CreateContact(employer.Id, member.Id);
            Assert.AreEqual(CanContactStatus.No, _employerMemberViewsQuery.GetProfessionalView(employer, member).CanContactByPhone());

            // Resume not visible and no phone number.

            member = CreateMember(4, false, true, false);
            if (hasContacted)
                CreateContact(employer.Id, member.Id);
            Assert.AreEqual(CanContactStatus.No, _employerMemberViewsQuery.GetProfessionalView(employer, member).CanContactByPhone());

            // Phone number not visible and no phone number.

            member = CreateMember(5, true, false, false);
            if (hasContacted)
                CreateContact(employer.Id, member.Id);
            Assert.AreEqual(CanContactStatus.No, _employerMemberViewsQuery.GetProfessionalView(employer, member).CanContactByPhone());
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

        private Member CreateMember(int index, bool isResumeVisible, bool isPhoneNumberVisible, bool hasPhoneNumbers)
        {
            var member = _membersCommand.CreateTestMember(index);

            if (hasPhoneNumbers)
            {
                member.PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber { Number = string.Format(MobilePhoneNumberFormat, index), Type = PhoneNumberType.Mobile },
                    new PhoneNumber { Number = string.Format(HomePhoneNumberFormat, index), Type = PhoneNumberType.Home },
                    new PhoneNumber { Number = string.Format(WorkPhoneNumberFormat, index), Type = PhoneNumberType.Work }
                };
            }
            else
            {
                member.PhoneNumbers = null;
            }

            member.VisibilitySettings.Professional.EmploymentVisibility = isResumeVisible
                ? member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Resume)
                : member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Resume);

            member.VisibilitySettings.Professional.EmploymentVisibility = isPhoneNumberVisible
                ? member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.PhoneNumbers)
                : member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.PhoneNumbers);

            _membersCommand.UpdateMember(member);
            return member;
        }
    }
}