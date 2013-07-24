using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Credits
{
    [TestClass]
    public class AnonymousTests
        : CreditsTests
    {
        private bool _isJoin;

        [TestInitialize]
        public void TestInitialize()
        {
            _isJoin = false;
        }

        protected override Employer CreateEmployer(Member member)
        {
            return null;
        }

        protected override Employer CreateEmployerForJoin()
        {
            if (!_isJoin)
                return null;

            var organisation = new Organisation
            {
                Name = "organisation",
                Address = new Address {Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Norlane VIC 3214")}
            };

            return new Employer
            {
                EmailAddress = new EmailAddress { Address = "employer@test.linkme.net.au" },
                CreatedTime = DateTime.Now,
                IsActivated = true,
                IsEnabled = true,
                PhoneNumber = new PhoneNumber { Number = "99999999", Type = PhoneNumberType.Work },
                FirstName = "Homer",
                LastName = "Simpson",
                Organisation = organisation,
                SubRole = EmployerSubRole.Employer,
            };
        }

        protected override bool CanContact
        {
            get { return false; }
        }

        protected override bool HasUsedCredit
        {
            get { return false; }
        }

        protected override bool ShouldUseCredit
        {
            get { return false; }
        }

        [TestMethod]
        public void TestSearchBeforeJoinSendMessage()
        {
            TestSearchBeforeJoin(PerformSearch, AssertSendMessage, false);
        }

        [TestMethod]
        public void TestSearchBeforeJoinRepresentativeSendMessage()
        {
            TestSearchBeforeJoin(PerformSearch, AssertSendMessage, true);
        }

        [TestMethod]
        public void TestSearchBeforeJoinDownloadResume()
        {
            TestSearchBeforeJoin(PerformSearch, AssertDownloadResume, false);
        }

        [TestMethod]
        public void TestSearchBeforeJoinEmailResume()
        {
            TestSearchBeforeJoin(PerformSearch, AssertEmailResume, false);
        }

        [TestMethod]
        public void TestSearchBeforeJoinViewResume()
        {
            TestSearchBeforeJoin(PerformSearch, AssertViewResume, false);
        }

        private void TestSearchBeforeJoin(Action performSearch, AssertAction assertAction, bool isRepresented)
        {
            _isJoin = true;

            // Create the member and employer.

            var member = CreateMember();
            var representative = isRepresented ? CreateRepresentative(member) : null;
            var employer = CreateEmployer(member);
            var initialCredits = employer == null ? 0 : GetCredits(employer.Id);
            _emailServer.ClearEmails();

            // Search.

            performSearch();

            // Assert.

            var loggedInViewings = 0;
            var notLoggedInViewings = 0;
            var reason = AssertSearchPages(false, ref employer, member, representative, assertAction, ref loggedInViewings, ref notLoggedInViewings);
            AssertData(employer, member, reason, loggedInViewings, notLoggedInViewings, initialCredits);
        }
    }
}