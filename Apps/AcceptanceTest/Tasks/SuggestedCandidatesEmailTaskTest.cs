/*using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.WebTester.AspTester;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Apps.Presentation.Domain.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.TaskRunner.Tasks.Communications;
using LinkMe.Web.UI.Registered.Employers;
using LinkMe.Web.UI.Unregistered;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Tasks
{
    [TestClass]
    public class SuggestedCandidatesEmailTest
        : WebTestFixture
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IJobAdsRepository _jobAdsRepository = Resolve<IJobAdsRepository>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IEmailsCommand _emailsCommand = Resolve<IEmailsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

        private const int MaxResultsPerJobAd = 3;
        private const string PosterUserid = "employer";
        private const string PosterEmail = "employer@test.linkme.net.au";
        private const string ContactEmail = "contact1@test.linkme.net.au";

        private Employer _employer;
        private IList<Member> _members;
        private JobAd _jobAd;
        private ReadOnlyUrl _searchUrl;
        private ReadOnlyUrl _candidatesUrl;
        private ReadOnlyUrl _newPasswordUrl;

        private TextBoxTester _loginIdTextBox;

        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();

        public override void TestInitialize()
        {
            base.TestInitialize();

            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();
            MemberSearchHost.ClearIndex();

            _loginIdTextBox = new TextBoxTester("LoginId", CurrentWebForm);

            // First candidate to find.

            _members = new List<Member>();
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);

            AddSalaryAndResume(candidate);
            _members.Add(member);

            // Create additional candidates so that the "all canidates" search link is shown.

            for (int i = 0; i < MaxResultsPerJobAd; i++)
            {
                member = _memberAccountsCommand.CreateTestMember("candidate" + i + "@test.linkme.net.au", "password", "First" + i, "Last" + i);
                candidate = _candidatesCommand.GetCandidate(member.Id);
                AddSalaryAndResume(candidate);
                _members.Add(member);
            }

            // Create employer.

            _employer = _employerAccountsCommand.CreateTestEmployer(PosterUserid, _organisationsCommand.CreateTestOrganisation(0));
            var jobPoster = _jobPostersCommand.GetJobPoster(_employer.Id);
            jobPoster.SendSuggestedCandidates = true;
            _jobPostersCommand.UpdateJobPoster(jobPoster);

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = _employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 1000 });

            _searchUrl = new ReadOnlyApplicationUrl("~/search/candidates");
            _candidatesUrl = new ReadOnlyApplicationUrl("~/employers/candidates");
            _newPasswordUrl = new ReadOnlyApplicationUrl("~/accounts/newpassword");
        }

        [TestMethod]
        public void NoJobAds()
        {
            // Create a job ad that doesn't have any candidate matches on the salary - no email should be sent.

            _jobAd = _employer.CreateTestJobAd();
            _jobAd.CreatedTime = DateTime.Now.AddDays(-1);
            _jobAdsCommand.PostJobAd(_jobAd);

            new EmailSuggestedCandidatesTask(_emailsCommand).ExecuteTask();

            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void RegisteredEmployerContact()
        {
            // Create a job that matches with the job poster as the contact.

            _jobAd = _employer.CreateTestJobAd();
            _jobAd.CreatedTime = DateTime.Now.AddDays(-1);
            _jobAd.Description.Salary = null;
            _jobAdsCommand.PostJobAd(_jobAd);

            new EmailSuggestedCandidatesTask(_emailsCommand).ExecuteTask();

            var email = _emailServer.AssertEmailSent();
            //EmailAssertion.AssertBody(email, string.Format(Body1, jobAd.Id, members[0].Id, members[1].Id, members[2].Id));
            email.AssertAddresses(Return, Return, _employer);
            AssertEmailBasics(email);

            // Forgotten password link - email should be prepopulated.

            Url url = FindLink(email.GetHtmlView().Body, "reset your password");
            Get(url);
            AssertUrlWithoutQuery(_newPasswordUrl);

            Assert.AreEqual(_employer.EmailAddress.Address, _loginIdTextBox.Text);

            // Try the search link.

            url = FindLink(email.GetHtmlView().Body, "All suggested candidates for this job ad");
            Get(url);
            AssertPath(_searchUrl);

            // Try Full Candidate View from the email link.

            url = FindLink(email.GetHtmlView().Body, "View Candidate");
            Get(url);
            AssertPath(_candidatesUrl);
/*
            // Try the Send message link - should be asked to log in

            Url emailUrl = FindLink(Browser.CurrentPageText, "Send message");
            GetPage(emailUrl);
            Assert.AreEqual(_accountUrl.Path.ToLower(), url.Path.ToLower());

            // Now log in - should be redirected to the contact form automatically.

            // Sign in.

            _loginIdTextBox.Text = _employer.GetLoginId();
            _passwordTextBox.Text = PosterPassword;
            _loginButton.Click();
            AssertPage<EmployerContactCandidate>();


            // Unsubscribe from the email.

            url = FindLink(email.GetHtmlView().Body, "click here to unsubscribe");
            TryUnsubscribe(url, PosterEmail, true);

            // Run the task again and no email should be received.

            _emailServer.ClearEmails();
            new EmailSuggestedCandidatesTask(_emailsCommand).ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestInvalidCriteria()
        {
            // Create a job ad without a position title.

            var badJobAd = _employer.CreateTestJobAd();
            badJobAd.CreatedTime = DateTime.Now.AddDays(-1);
            badJobAd.UpdatedTime = badJobAd.CreatedTime;
            badJobAd.ExpiryTime = badJobAd.CreatedTime.AddDays(30);
            badJobAd.Description.PositionTitle = null;
            badJobAd.Title = "";
            badJobAd.Description.Salary = null;
            badJobAd.Description.Industries.Clear();
            _jobAdsRepository.CreateJobAd(badJobAd);
            _jobAdsRepository.ChangeStatus(badJobAd.Id, JobAdStatus.Open, DateTime.Now);

            // No email should be sent, but no exception should be thrown either.

            new EmailSuggestedCandidatesTask(_emailsCommand).ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // Create a valid job ad.

            _jobAd = _employer.CreateTestJobAd();
            _jobAd.CreatedTime = DateTime.Now.AddDays(-1);
            _jobAd.Description.Salary = null;
            _jobAdsCommand.PostJobAd(_jobAd);

            // The email should now be sent for the good job ad, even though the bad one fails.

            new EmailSuggestedCandidatesTask(_emailsCommand).ExecuteTask();

            var email = _emailServer.AssertEmailSent();
            //EmailAssertion.AssertBody(email, string.Format(Body1, jobAd.Id, members[0].Id, members[1].Id, members[2].Id));
            email.AssertAddresses(Return, Return, _employer);
            AssertEmailBasics(email);
        }

        [TestMethod]
        public void TestNoPositionTitle()
        {
            // Create a job ad without a position title, but with a title.

            _jobAd = _employer.CreateTestJobAd();
            _jobAd.CreatedTime = DateTime.Now.AddDays(-1);
            _jobAd.Description.PositionTitle = null;
            _jobAd.Description.Salary = null;
            _jobAd.Description.Industries.Clear();
            _jobAdsCommand.PostJobAd(_jobAd);

            // The email should be sent with matches using the title.

            new EmailSuggestedCandidatesTask(_emailsCommand).ExecuteTask();

            var email = _emailServer.AssertEmailSent();
            //EmailAssertion.AssertBody(email, string.Format(Body1, jobAd.Id, members[0].Id, members[1].Id, members[2].Id));
            email.AssertAddresses(Return, Return, _employer);
            AssertEmailBasics(email);
        }

        [TestMethod]
        public void TwoJobAds()
        {
            // Create a job that matches with the job poster as the contact.

            _jobAd = _employer.CreateTestJobAd();
            _jobAd.CreatedTime = DateTime.Now.AddDays(-1);
            _jobAd.Description.Salary = null;
            _jobAdsCommand.PostJobAd(_jobAd);

            // Create another.

            _jobAd = _employer.CreateTestJobAd();
            _jobAd.CreatedTime = DateTime.Now.AddDays(-1);
            _jobAd.Description.Salary = null;
            _jobAdsCommand.PostJobAd(_jobAd);

            new EmailSuggestedCandidatesTask(_emailsCommand).ExecuteTask();

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, _employer);
            AssertEmailBasics(email);

            // Forgotten password link - email should be prepopulated.

            Url url = FindLink(email.GetHtmlView().Body, "reset your password");
            Get(url);
            AssertUrlWithoutQuery(_newPasswordUrl);

            Assert.AreEqual(_employer.EmailAddress.Address, _loginIdTextBox.Text);

            // Try the search link.

            url = FindLink(email.GetHtmlView().Body, "All suggested candidates for this job ad");
            Get(url);
            AssertPath(_searchUrl);

            // Try Full Candidate View from the email link.

            url = FindLink(email.GetHtmlView().Body, "View Candidate");
            Get(url);
            AssertPath(_candidatesUrl);

            // Unsubscribe from the email.

            url = FindLink(email.GetHtmlView().Body, "click here to unsubscribe");
            TryUnsubscribe(url, PosterEmail, true);

            // Run the task again and no email should be received.

            _emailServer.ClearEmails();
            new EmailSuggestedCandidatesTask(_emailsCommand).ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void UnregisteredContact()
        {
            // Create a job a with a contact email that doesn't have a corresponding employer account.

            _jobAd = _employer.CreateTestJobAd();
            _jobAd.CreatedTime = DateTime.Now.AddDays(-1);
            _jobAd.Description.Salary = null;
            _jobAd.ContactDetails.EmailAddress = ContactEmail;
            _jobAdsCommand.PostJobAd(_jobAd);

            // Get the email.

            new EmailSuggestedCandidatesTask(_emailsCommand).ExecuteTask();

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(ContactEmail));
            AssertEmailBasics(email);

            // Forgotten password link shouldn't be included.

            email.AssertHtmlViewDoesNotContain("reset your password");

            // Try Full Candidate View from the email link. User ID should be blank.

            Url url = FindLink(email.GetHtmlView().Body, "View Candidate");
            Get(url);
            AssertPath(_candidatesUrl);

            // Request access to LinkMe.

            url = FindLink(email.GetHtmlView().Body, "request full access");
            Get(url);
            AssertUrl(new ReadOnlyApplicationUrl(true, "~/employers/products/choose"));

            // Unsubscribe from the email.

            url = FindLink(email.GetHtmlView().Body, "click here to unsubscribe");
            TryUnsubscribe(url, ContactEmail, false);

            // Run the task again and no email should be received.

            _emailServer.ClearEmails();
            new EmailSuggestedCandidatesTask(_emailsCommand).ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        private void AssertEmailBasics(MockEmail email)
        {
            email.AssertSubject("Job candidates from LinkMe");
            email.AssertHtmlViewContains("4 suggested candidates");
            var view = _employerMemberViewsQuery.GetEmployerMemberView(_employer, _members[0]);
            email.AssertHtmlViewContains(view.GetDisplayText(false));

            // Turn this off for the moment as it gives xml errors.
            // Should try again later.

            //email.AssertLinks();
        }

        private void TryUnsubscribe(ReadOnlyUrl url, string emailAddress, bool isRegistered)
        {
            Get(url);
            AssertPage<UnsubscribeFromEmail>();
            AssertPageDoesNotContain("already unsubscribed");
            AssertPageContains("Suggested Resumes Email");
            AssertPageContains(emailAddress);

            var btnUnsubscribe = new HtmlButtonTester("btnUnsubscribe", CurrentContent);
            btnUnsubscribe.Click();
            AssertPage<UnsubscribeFromEmail>();
            AssertPageContains("successfully unsubscribed");
            AssertPageContains(NavigationManager.GetUrlForPage<EmployerEditUserProfileForm>().PathAndQuery, true);
            AssertPageContainment("create an account", !isRegistered);
        }

        private static Url FindLink(string html, string linkText)
        {
            int index = html.IndexOf(">" + linkText + "</a>");
            Assert.IsTrue(index != -1);

            index = html.LastIndexOf("href=\"", index);
            Assert.IsTrue(index != -1);

            index += 6;
            int endIndex = html.IndexOf('\"', index);
            Assert.IsTrue(endIndex != -1);

            return new ApplicationUrl(html.Substring(index, endIndex - index));
        }

        private void AddSalaryAndResume(Candidate candidate)
        {
            candidate.DesiredSalary = new Salary { LowerBound = 40000, UpperBound = 50000, Rate = SalaryRate.Year };
            _candidatesCommand.UpdateCandidate(candidate);
            _candidateResumesCommand.AddTestResume(candidate);
        }
    }
}*/