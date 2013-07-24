using System;
using System.Collections.Specialized;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers.ContactCredits
{
    [TestClass]
    public abstract class ViewContactCreditsUsageTests
        : ViewCreditsUsageTests
    {
        private ReadOnlyUrl _candidatesUrl;
        private ReadOnlyUrl _searchUrl;
        private ReadOnlyUrl _sendMessagesUrl;
        private ReadOnlyUrl _downloadUrl;
        private ReadOnlyUrl _sendUrl;
        private ReadOnlyUrl _phoneNumbersUrl;

        private const string BusinessAnalyst = "business analyst";

        private HtmlTextBoxTester _keywordsTextBox;
        private HtmlButtonTester _searchButton;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();
            _emailServer.ClearEmails();

            _candidatesUrl = new ReadOnlyApplicationUrl("~/employers/candidates");
            _searchUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");
            _sendMessagesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/sendmessages");
            _downloadUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/downloadresumes");
            _sendUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/sendresumes");
            _phoneNumbersUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/phonenumbers");

            _keywordsTextBox = new HtmlTextBoxTester(Browser, "Keywords");
            _searchButton = new HtmlButtonTester(Browser, "search");
        }

        [TestMethod]
        public void TestSearchNoAllocations()
        {
            TestSearch(PerformSearch, NoAllocate, NoAction, false, false);
        }

        [TestMethod]
        public void TestSearchPhoneNumberNoAllocations()
        {
            TestSearch(PerformSearch, NoAllocate, ViewPhoneNumber, false, false);
        }

        [TestMethod]
        public void TestSearchSendMessageNoAllocations()
        {
            TestSearch(PerformSearch, NoAllocate, SendMessage, false, false);
        }

        [TestMethod]
        public void TestSearchDownloadResumeNoAllocations()
        {
            TestSearch(PerformSearch, NoAllocate, DownloadResume, false, false);
        }

        [TestMethod]
        public void TestSearchEmailResumeNoAllocations()
        {
            TestSearch(PerformSearch, NoAllocate, SendResume, false, false);
        }

        [TestMethod]
        public void TestViewResumeNameNoAllocations()
        {
            TestViewResume(NoAllocate, NoAction, false, false);
        }

        [TestMethod]
        public void TestViewResumePhoneNumberNoAllocations()
        {
            TestViewResume(NoAllocate, ViewPhoneNumber, false, false);
        }

        [TestMethod]
        public void TestSearchNoCredits()
        {
            TestSearch(PerformSearch, NoCredits, NoAction, false, false);
        }

        [TestMethod]
        public void TestSearchPhoneNumberNoCredits()
        {
            TestSearch(PerformSearch, NoCredits, ViewPhoneNumber, false, false);
        }

        [TestMethod]
        public void TestSearchSendMessageNoCredits()
        {
            TestSearch(PerformSearch, NoCredits, SendMessage, false, false);
        }

        [TestMethod]
        public void TestSearchDownloadResumeNoCredits()
        {
            TestSearch(PerformSearch, NoCredits, DownloadResume, false, false);
        }

        [TestMethod]
        public void TestSearchEmailResumeNoCredits()
        {
            TestSearch(PerformSearch, NoCredits, SendResume, false, false);
        }

        [TestMethod]
        public void TestViewResumeNameNoCredits()
        {
            TestViewResume(NoCredits, NoAction, false, false);
        }

        [TestMethod]
        public void TestViewResumePhoneNumberNoCredits()
        {
            TestViewResume(NoCredits, ViewPhoneNumber, false, false);
        }

        [TestMethod]
        public void TestSearchNoCreditsUsedCredit()
        {
            TestSearch(PerformSearch, NoCreditsUsedCredit, NoAction, false, true);
        }

        [TestMethod]
        public void TestSearchPhoneNumberNoCreditsUsedCredit()
        {
            TestSearch(PerformSearch, NoCreditsUsedCredit, ViewPhoneNumber, false, true);
        }

        [TestMethod]
        public void TestSearchSendMessageNoCreditsUsedCredit()
        {
            TestSearch(PerformSearch, NoCreditsUsedCredit, SendMessage, false, true);
        }

        [TestMethod]
        public void TestSearchDownloadResumeNoCreditsUsedCredit()
        {
            TestSearch(PerformSearch, NoCreditsUsedCredit, DownloadResume, false, true);
        }

        [TestMethod]
        public void TestSearchEmailResumeNoCreditsUsedCredit()
        {
            TestSearch(PerformSearch, NoCreditsUsedCredit, SendResume, false, true);
        }

        [TestMethod]
        public void TestViewResumeNameNoCreditsUsedCredit()
        {
            TestViewResume(NoCreditsUsedCredit, NoAction, false, true);
        }

        [TestMethod]
        public void TestViewResumePhoneNumberNoCreditsUsedCredit()
        {
            TestViewResume(NoCreditsUsedCredit, ViewPhoneNumber, false, true);
        }

        [TestMethod]
        public void TestSearchSomeCredits()
        {
            TestSearch(PerformSearch, SomeCredits, NoAction, true, false);
        }

        [TestMethod]
        public void TestSearchPhoneNumberSomeCredits()
        {
            TestSearch(PerformSearch, SomeCredits, ViewPhoneNumber, true, false);
        }

        [TestMethod]
        public void TestSearchSendMessageSomeCredits()
        {
            TestSearch(PerformSearch, SomeCredits, SendMessage, true, false);
        }

        [TestMethod]
        public void TestSearchDownloadResumeSomeCredits()
        {
            TestSearch(PerformSearch, SomeCredits, DownloadResume, true, false);
        }

        [TestMethod]
        public void TestSearchEmailResumeSomeCredits()
        {
            TestSearch(PerformSearch, SomeCredits, SendResume, true, false);
        }

        [TestMethod]
        public void TestViewResumeNameSomeCredits()
        {
            TestViewResume(SomeCredits, NoAction, true, false);
        }

        [TestMethod]
        public void TestViewResumePhoneNumberSomeCredits()
        {
            TestViewResume(SomeCredits, ViewPhoneNumber, true, false);
        }

        [TestMethod]
        public void TestSearchSomeCreditsUsedCredit()
        {
            TestSearch(PerformSearch, SomeCreditsUsedCredit, NoAction, true, true);
        }

        [TestMethod]
        public void TestSearchPhoneNumberSomeCreditsUsedCredit()
        {
            TestSearch(PerformSearch, SomeCreditsUsedCredit, ViewPhoneNumber, true, true);
        }

        [TestMethod]
        public void TestSearchSendMessageSomeCreditsUsedCredit()
        {
            TestSearch(PerformSearch, SomeCreditsUsedCredit, SendMessage, true, true);
        }

        [TestMethod]
        public void TestSearchDownloadResumeSomeCreditsUsedCredit()
        {
            TestSearch(PerformSearch, SomeCreditsUsedCredit, DownloadResume, true, true);
        }

        [TestMethod]
        public void TestSearchEmailResumeSomeCreditsUsedCredit()
        {
            TestSearch(PerformSearch, SomeCreditsUsedCredit, SendResume, true, true);
        }

        [TestMethod]
        public void TestViewResumeNameSomeCreditsUsedCredit()
        {
            TestViewResume(SomeCreditsUsedCredit, NoAction, true, true);
        }

        [TestMethod]
        public void TestViewResumePhoneNumberSomeCreditsUsedCredit()
        {
            TestViewResume(SomeCreditsUsedCredit, ViewPhoneNumber, true, true);
        }

        [TestMethod]
        public void TestSearchUnlimitedCredits()
        {
            TestSearch(PerformSearch, UnlimitedCredits, NoAction, true, false);
        }

        [TestMethod]
        public void TestSearchPhoneNumberUnlimitedCredits()
        {
            TestSearch(PerformSearch, UnlimitedCredits, ViewPhoneNumber, true, false);
        }

        [TestMethod]
        public void TestSearchSendMessageUnlimitedCredits()
        {
            TestSearch(PerformSearch, UnlimitedCredits, SendMessage, true, false);
        }

        [TestMethod]
        public void TestSearchDownloadResumeUnlimitedCredits()
        {
            TestSearch(PerformSearch, UnlimitedCredits, DownloadResume, true, false);
        }

        [TestMethod]
        public void TestSearchEmailResumeUnlimitedCredits()
        {
            TestSearch(PerformSearch, UnlimitedCredits, SendResume, true, false);
        }

        [TestMethod]
        public void TestViewResumeNameUnlimitedCredits()
        {
            TestViewResume(UnlimitedCredits, NoAction, true, false);
        }

        [TestMethod]
        public void TestViewResumePhoneNumberUnlimitedCredits()
        {
            TestViewResume(UnlimitedCredits, ViewPhoneNumber, true, false);
        }

        private static void NoAllocate(IEmployer employer, IMember member, ICreditOwner owner)
        {
        }

        private void NoCredits(IEmployer employer, IMember member, ICreditOwner owner)
        {
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 0 });
        }

        private void NoCreditsUsedCredit(IEmployer employer, Member member, ICreditOwner owner)
        {
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 1 });
            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetEmployerMemberView(employer, member));
        }

        private void SomeCredits(IEmployer employer, IMember member, ICreditOwner owner)
        {
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 10 });
        }

        private void SomeCreditsUsedCredit(IEmployer employer, Member member, ICreditOwner owner)
        {
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 10 });
            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetEmployerMemberView(employer, member));
        }

        private void UnlimitedCredits(IEmployer employer, IMember member, ICreditOwner owner)
        {
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = null });
        }

        private void TestSearch(Action performSearch, Action<IEmployer, Member, ICreditOwner> allocate, Func<Employer, Member, bool, bool, bool> nextAction, bool canContact, bool hasUsedCredit)
        {
            // Create everyone.

            var member = CreateMember(0);

            var employer = CreateEmployer(member);
            var owner = GetCreditOwner(employer);
            allocate(employer, member, owner);

            var administrator = CreateAdministrator();

            // Check before.

            var allocation = GetAllocation<ContactCredit>(owner.Id);
            AssertCredits<ContactCredit>(administrator, employer, member, owner, hasUsedCredit, allocation, allocation == null ? new Allocation[0] : new[] { allocation });

            // Search.

            _emailServer.ClearEmails();
            LogIn(employer);
            performSearch();

            // Do what comes next.

            var usedCredit = nextAction(employer, member, canContact, hasUsedCredit);

            // Check after.

            allocation = GetAllocation<ContactCredit>(owner.Id);
            AssertCredits<ContactCredit>(administrator, employer, member, owner, hasUsedCredit || usedCredit, allocation, allocation == null ? new Allocation[0] : new[] { allocation });
        }

        private void TestViewResume(Action<IEmployer, Member, ICreditOwner> allocate, Func<Employer, Member, bool, bool, bool> nextAction, bool canContact, bool hasUsedCredit)
        {
            // Create the member and employer.

            var member = CreateMember(0);

            var employer = CreateEmployer(member);
            var owner = GetCreditOwner(employer);
            allocate(employer, member, owner);

            var administrator = CreateAdministrator();

            // Check before.

            var allocation = GetAllocation<ContactCredit>(owner.Id);
            AssertCredits<ContactCredit>(administrator, employer, member, owner, hasUsedCredit, allocation, allocation == null ? new Allocation[0] : new[] { allocation });

            // View the resume.

            LogIn(employer);
            var url = _candidatesUrl.AsNonReadOnly();
            url.QueryString.Add("candidateId", member.Id.ToString());
            Get(url);

            // Assert.

            var usedCredit = nextAction(employer, member, canContact, hasUsedCredit);

            // Check credits.

            allocation = GetAllocation<ContactCredit>(owner.Id);
            AssertCredits<ContactCredit>(administrator, employer, member, owner, hasUsedCredit || usedCredit, allocation, allocation == null ? new Allocation[0] : new[] { allocation });
        }

        protected abstract Employer CreateEmployer(Member member);
        protected abstract ICreditOwner GetCreditOwner(Employer employer);

        private static bool NoAction(Employer employer, Member member, bool canContact, bool hasUsedCredit)
        {
            return false;
        }

        private bool ViewPhoneNumber(Employer employer, Member member, bool canContact, bool hasUsedCredit)
        {
            if (canContact || hasUsedCredit)
            {
                // On a click this service gets called and should return the phone number.

                var parameters = new NameValueCollection {{"candidateId", member.Id.ToString() }};
                Post(_phoneNumbersUrl, parameters);
                return true;
            }

            return false;
        }

        private bool SendMessage(Employer employer, Member member, bool canContact, bool hasUsedCredit)
        {
            var currentUrl = new ReadOnlyApplicationUrl(Browser.CurrentUrl.AbsoluteUri);

            // Assert.

            var creditAction = false;
            if (canContact || hasUsedCredit)
            {
                // Send the message.

                var parameters = new NameValueCollection
                {
                    {"candidateId", member.Id.ToString()},
                    {"Subject", "This is the subject."},
                    {"Body", "This is the body."},
                    {"sendCopy", "true"}
                };
                Post(_sendMessagesUrl, parameters);

                // Check the email is sent to the right person.

                var emails = _emailServer.AssertEmailsSent(2);
                Assert.AreEqual(member.EmailAddresses[0].Address, emails[0].To[0].Address);
                emails[0].AssertHtmlViewContains("has found you");
                Assert.AreEqual(employer.EmailAddress.Address, emails[1].To[0].Address);

                creditAction = true;
            }

            Get(currentUrl);
            return creditAction;
        }

        private bool DownloadResume(Employer employer, Member member, bool canContact, bool hasUsedCredit)
        {
            var currentUrl = new ReadOnlyApplicationUrl(Browser.CurrentUrl.AbsoluteUri);

            // Assert.

            var creditAction = false;
            if (canContact || hasUsedCredit)
            {
                var parameters = new NameValueCollection {{ "candidateId", member.Id.ToString() }};
                Post(_downloadUrl, parameters);

                creditAction = true;
            }

            Get(currentUrl);
            return creditAction;
        }

        private bool SendResume(Employer employer, Member member, bool canContact, bool hasUsedCredit)
        {
            var currentUrl = new ReadOnlyApplicationUrl(Browser.CurrentUrl.AbsoluteUri);

            // Assert.

            var creditAction = false;
            if (canContact || hasUsedCredit)
            {
                var parameters = new NameValueCollection {{ "candidateId", member.Id.ToString() }};
                Post(_sendUrl, parameters);
                
                creditAction = true;
            }

            Get(currentUrl);
            return creditAction;
        }

        private void PerformSearch()
        {
            Get(_searchUrl);
            _keywordsTextBox.Text = BusinessAnalyst;
            _searchButton.Click();
        }
    }
}
