using System;
using System.Globalization;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.SearchEngines
{
    [TestClass]
    public class SearchEnginesTests
        : WebTestClass
    {
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();

        private ReadOnlyUrl _searchEnginesUrl;

        private HtmlTextBoxTester _totalMembersTextBox;
        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlTextBoxTester _isMemberIndexedTextBox;
        private HtmlTextBoxTester _totalJobAdsTextBox;
        private HtmlTextBoxTester _jobAdIdTextBox;
        private HtmlTextBoxTester _isJobAdIndexedTextBox;
        private HtmlButtonTester _searchButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _searchEnginesUrl = new ReadOnlyApplicationUrl(true, "~/administrators/search/engines");

            _totalMembersTextBox = new HtmlTextBoxTester(Browser, "TotalMembers");
            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _isMemberIndexedTextBox = new HtmlTextBoxTester(Browser, "IsMemberIndexed");
            _totalJobAdsTextBox = new HtmlTextBoxTester(Browser, "TotalJobAds");
            _jobAdIdTextBox = new HtmlTextBoxTester(Browser, "JobAdId");
            _isJobAdIndexedTextBox = new HtmlTextBoxTester(Browser, "IsJobAdIndexed");
            _searchButton = new HtmlButtonTester(Browser, "search");
        }

        [TestMethod]
        public void TestMemberSearch()
        {
            var administrator = CreateAdministrator();

            // Add some members.

            var members = CreateMembers(5);

            LogIn(administrator);
            Get(_searchEnginesUrl);
            AssertUrl(_searchEnginesUrl);

            // Total.

            Assert.AreEqual(members.Length.ToString(CultureInfo.InvariantCulture), _totalMembersTextBox.Text);
            Assert.AreEqual("", _loginIdTextBox.Text);
            Assert.AreEqual("", _isMemberIndexedTextBox.Text);

            // Id indexed?

            foreach (var member in members)
            {
                _loginIdTextBox.Text = member.Id.ToString();
                _searchButton.Click();

                Assert.AreEqual(members.Length.ToString(CultureInfo.InvariantCulture), _totalMembersTextBox.Text);
                Assert.AreEqual(member.Id.ToString(), _loginIdTextBox.Text);
                Assert.AreEqual("yes", _isMemberIndexedTextBox.Text);

                _loginIdTextBox.Text = member.GetLoginId();
                _searchButton.Click();

                Assert.AreEqual(members.Length.ToString(CultureInfo.InvariantCulture), _totalMembersTextBox.Text);
                Assert.AreEqual(member.GetLoginId(), _loginIdTextBox.Text);
                Assert.AreEqual("yes", _isMemberIndexedTextBox.Text);
            }

            _loginIdTextBox.Text = "something";
            _searchButton.Click();

            Assert.AreEqual(members.Length.ToString(CultureInfo.InvariantCulture), _totalMembersTextBox.Text);
            Assert.AreEqual("something", _loginIdTextBox.Text);
            Assert.AreEqual("no", _isMemberIndexedTextBox.Text);

            var id = Guid.NewGuid();
            _loginIdTextBox.Text = id.ToString();
            _searchButton.Click();

            Assert.AreEqual(members.Length.ToString(CultureInfo.InvariantCulture), _totalMembersTextBox.Text);
            Assert.AreEqual(id.ToString(), _loginIdTextBox.Text);
            Assert.AreEqual("no", _isMemberIndexedTextBox.Text);
        }

        [TestMethod]
        public void TestJobAdSearch()
        {
            var administrator = CreateAdministrator();

            // Add some job ads.

            var jobAds = CreateJobAds(5);

            LogIn(administrator);
            Get(_searchEnginesUrl);
            AssertUrl(_searchEnginesUrl);

            Assert.AreEqual(jobAds.Length.ToString(CultureInfo.InvariantCulture), _totalJobAdsTextBox.Text);
            Assert.AreEqual("", _jobAdIdTextBox.Text);
            Assert.AreEqual("", _isJobAdIndexedTextBox.Text);

            // Id indexed?

            foreach (var jobAd in jobAds)
            {
                _jobAdIdTextBox.Text = jobAd.Id.ToString();
                _searchButton.Click();

                Assert.AreEqual(jobAds.Length.ToString(CultureInfo.InvariantCulture), _totalJobAdsTextBox.Text);
                Assert.AreEqual(jobAd.Id.ToString(), _jobAdIdTextBox.Text);
                Assert.AreEqual("yes", _isJobAdIndexedTextBox.Text);
            }

            var id = Guid.NewGuid();
            _jobAdIdTextBox.Text = id.ToString();
            _searchButton.Click();

            Assert.AreEqual(jobAds.Length.ToString(CultureInfo.InvariantCulture), _totalJobAdsTextBox.Text);
            Assert.AreEqual(id.ToString(), _jobAdIdTextBox.Text);
            Assert.AreEqual("no", _isJobAdIndexedTextBox.Text);
        }

        private Member[] CreateMembers(int count)
        {
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
            {
                members[index] = _memberAccountsCommand.CreateTestMember(index);
                var candidate = _candidatesQuery.GetCandidate(members[index].Id);
                _candidateResumesCommand.AddTestResume(candidate);
            }

            return members;
        }

        private JobAd[] CreateJobAds(int count)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            var jobAds = new JobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            return jobAds;
        }

        private Administrator CreateAdministrator()
        {
            return _administratorAccountsCommand.CreateTestAdministrator(0);
        }
    }
}
