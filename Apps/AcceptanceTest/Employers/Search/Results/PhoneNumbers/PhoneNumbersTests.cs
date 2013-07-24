using System;
using System.Collections.Generic;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Results.PhoneNumbers
{
    [TestClass]
    public abstract class PhoneNumbersTests
        : SearchTests
    {
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        protected enum PhoneNumberVisibility
        {
            Shown,
            Available,
            NotSupplied
        }

        private const string MobileNumber = "11111111";
        private const string HomeNumber = "22222222";

        [TestMethod]
        public void TestUnlimitedUnlocked()
        {
            TestPhoneNumbers(false, null, true, true, false, PhoneNumberVisibility.Shown);
        }

        [TestMethod]
        public void TestUnlimitedLocked()
        {
            TestPhoneNumbers(false, null, false, true, false, PhoneNumberVisibility.Available);
        }

        [TestMethod]
        public void TestUnlimitedUnlockedHidden()
        {
            TestPhoneNumbers(false, null, true, true, true, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestUnlimitedLockedHidden()
        {
            TestPhoneNumbers(false, null, false, true, true, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestLimitedUnlocked()
        {
            TestPhoneNumbers(false, 100, true, true, false, PhoneNumberVisibility.Shown);
        }

        [TestMethod]
        public void TestLimitedLocked()
        {
            TestPhoneNumbers(false, 100, false, true, false, PhoneNumberVisibility.Available);
        }

        [TestMethod]
        public void TestLimitedUnlockedHidden()
        {
            TestPhoneNumbers(false, 100, true, true, true, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestLimitedLockedHidden()
        {
            TestPhoneNumbers(false, 100, false, true, true, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestZeroUnlocked()
        {
            TestPhoneNumbers(false, 1, true, true, false, PhoneNumberVisibility.Shown);
        }

        [TestMethod]
        public void TestZeroLocked()
        {
            TestPhoneNumbers(false, 0, false, true, false, PhoneNumberVisibility.Available);
        }

        [TestMethod]
        public void TestZeroUnlockedHidden()
        {
            TestPhoneNumbers(false, 1, true, true, true, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestZeroLockedHidden()
        {
            TestPhoneNumbers(false, 0, false, true, true, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestAnonymousLocked()
        {
            TestPhoneNumbers(true, 0, false, true, false, PhoneNumberVisibility.Available);
        }

        [TestMethod]
        public void TestAnonymousLockedHidden()
        {
            TestPhoneNumbers(true, 0, false, true, true, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestUnlimitedUnlockedNoPhone()
        {
            TestPhoneNumbers(false, null, true, false, false, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestUnlimitedLockedNoPhone()
        {
            TestPhoneNumbers(false, null, false, false, false, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestUnlimitedUnlockedNoPhoneHidden()
        {
            TestPhoneNumbers(false, null, true, false, true, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestUnlimitedLockedHiddenNoPhone()
        {
            TestPhoneNumbers(false, null, false, false, true, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestLimitedUnlockedNoPhone()
        {
            TestPhoneNumbers(false, 100, true, false, false, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestLimitedLockedNoPhone()
        {
            TestPhoneNumbers(false, 100, false, false, false, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestLimitedUnlockedHiddenNoPhone()
        {
            TestPhoneNumbers(false, 100, true, false, true, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestLimitedLockedHiddenNoPhone()
        {
            TestPhoneNumbers(false, 100, false, false, true, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestZeroUnlockedNoPhone()
        {
            TestPhoneNumbers(false, 1, true, false, false, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestZeroLockedNoPhone()
        {
            TestPhoneNumbers(false, 0, false, false, false, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestZeroUnlockedHiddenNoPhone()
        {
            TestPhoneNumbers(false, 1, true, false, true, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestZeroLockedHiddenNoPhone()
        {
            TestPhoneNumbers(false, 0, false, false, true, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestAnonymousLockedNoPhone()
        {
            TestPhoneNumbers(true, 0, false, false, false, PhoneNumberVisibility.NotSupplied);
        }

        [TestMethod]
        public void TestAnonymousLockedHiddenNoPhone()
        {
            TestPhoneNumbers(true, 0, false, false, true, PhoneNumberVisibility.NotSupplied);
        }

        private void TestPhoneNumbers(bool anonymous, int? credits, bool unlocked, bool phoneNumbers, bool hidden, PhoneNumberVisibility visibility)
        {
            var employer = anonymous ? null : CreateEmployer(credits);
            var member = CreateMember(phoneNumbers, hidden);
            if (unlocked)
                Contact(employer, member);

            // Search.

            if (employer != null)
                LogIn(employer);

            // Test.

            TestPhoneNumbers(member, credits, unlocked, visibility);
        }

        protected abstract void TestPhoneNumbers(Member member, int? credits, bool unlocked, PhoneNumberVisibility visibility);

        private Employer CreateEmployer(int? credits)
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = credits, OwnerId = employer.Id });
            return employer;
        }

        private Member CreateMember(bool phoneNumbers, bool hidden)
        {
            var member = CreateMember(0);
            if (phoneNumbers)
            {
                member.PhoneNumbers = new List<PhoneNumber>
                                          {
                                              new PhoneNumber {Number = MobileNumber, Type = PhoneNumberType.Mobile},
                                              new PhoneNumber {Number = HomeNumber, Type = PhoneNumberType.Home},
                                          };
            }
            else
            {
                member.PhoneNumbers = null;
            }

            if (hidden)
                member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.PhoneNumbers);

            _memberAccountsCommand.UpdateMember(member);
            return member;
        }

        private void Contact(IEmployer employer, Member member)
        {
            var view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
            _employerMemberViewsCommand.AccessMember(_app, employer, view, MemberAccessReason.Unlock);
        }
    }
}