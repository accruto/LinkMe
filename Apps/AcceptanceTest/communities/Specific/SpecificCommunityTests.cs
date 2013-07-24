using System;
using System.Xml;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Verticals;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Guests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities.Specific
{
    [TestClass]
    public abstract class SpecificCommunityTests
        : CommunityTests
    {
        private const string EmployerUserId = "employer";
        protected const string MemberEmailAddress = "joinuser@test.linkme.net.au";
        protected const string Password = "password";
        protected const string FirstName = "monty";
        protected const string LastName = "burns";
        protected const string Location = "Armadale VIC 3143";
        protected const string PhoneNumber = "99999999";
        protected const string Salary = "100000";
        protected const string BusinessAnalyst = "business analyst";

        private string _homeJoinFormId;
        private HtmlTextBoxTester _homeFirstNameTextBox;
        private HtmlTextBoxTester _homeLastNameTextBox;
        private HtmlTextBoxTester _homeEmailAddressTextBox;
        private HtmlPasswordTester _homePasswordTextBox;
        private HtmlPasswordTester _homeConfirmPasswordTextBox;
        private HtmlCheckBoxTester _homeAcceptTermsCheckBox;

        private string _joinFormId;

        private string _personalDetailsFormId;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _phoneNumberTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlPasswordTester _confirmPasswordTextBox;
        private HtmlCheckBoxTester _acceptTermsCheckBox;
        private HtmlTextBoxTester _locationTextBox;
        private HtmlTextBoxTester _salaryLowerBoundTextBox;
        private HtmlRadioButtonTester _openToOffersRadioButton;

        protected ReadOnlyUrl _searchUrl;
        private ReadOnlyUrl _activationUrl;
        private ReadOnlyUrl _manageCandidatesUrl;
        private ReadOnlyUrl _personalDetailsUrl;
        private ReadOnlyUrl _jobDetailsUrl;
        private ReadOnlyUrl _profileUrl;
        private ReadOnlyUrl _changePasswordUrl;
        protected ReadOnlyUrl _resetUrl;

        [TestInitialize]
        public void SpecificCommunityTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();
            InitialiseTesters();
            _emailServer.ClearEmails();

            _searchUrl = new ReadOnlyApplicationUrl("~/search/candidates");
            _activationUrl = new ReadOnlyApplicationUrl("~/accounts/activation");
            _manageCandidatesUrl = new ReadOnlyApplicationUrl("~/employers/candidates/manage");
            _personalDetailsUrl = new ReadOnlyApplicationUrl(true, "~/join/personaldetails");
            _jobDetailsUrl = new ReadOnlyApplicationUrl(true, "~/join/jobdetails");
            _profileUrl = new ReadOnlyApplicationUrl("~/guests/profile");

            _changePasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/changepassword");
            _resetUrl = new ReadOnlyApplicationUrl("~/verticals/reset");
        }

        private void InitialiseTesters()
        {
            _homeJoinFormId = "JoinForm";

            _homeFirstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _homeLastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _homeEmailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _homePasswordTextBox = new HtmlPasswordTester(Browser, "JoinPassword");
            _homeConfirmPasswordTextBox = new HtmlPasswordTester(Browser, "JoinConfirmPassword");
            _homeAcceptTermsCheckBox = new HtmlCheckBoxTester(Browser, "AcceptTerms");

            _joinFormId = "JoinForm";

            _personalDetailsFormId = "PersonalDetailsForm";
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _confirmPasswordTextBox = new HtmlPasswordTester(Browser, "ConfirmPassword");
            _acceptTermsCheckBox = new HtmlCheckBoxTester(Browser, "AcceptTerms");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _salaryLowerBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryLowerBound");
            _openToOffersRadioButton = new HtmlRadioButtonTester(Browser, "OpenToOffers");
        }

        protected abstract TestCommunity GetTestCommunity();

        protected virtual bool IsTestCommunityDeleted
        {
            get { return false; }
        }

        [TestMethod]
        public void TestMemberJoinPage()
        {
            TestMemberJoin(JoinPageJoin, false, FillCommunityPersonalDetails, AssertCommunityPersonalDetails, null);
        }

        [TestMethod]
        public void TestMemberJoinHomePage()
        {
            TestMemberJoin(HomePageJoin, true, FillCommunityPersonalDetails, AssertCommunityPersonalDetails, null);
        }

        [TestMethod]
        public void TestMemberLogInCommunity()
        {
            var data = GetTestCommunity().GetCommunityTestData();
            var isDeleted = IsTestCommunityDeleted;
            var community = CreateTestCommunity(data, isDeleted);

            // Create a member.

            var member = _memberAccountsCommand.CreateTestMember(MemberEmailAddress, community.Id);

            // Navigate to the community page.

            var url = GetCommunityUrl(community, null);
            Get(url);

            if (isDeleted)
            {
                // Should have been redirected to the equivalent non-vertical url.

                var redirectUrl = new ReadOnlyApplicationUrl("~/");
                AssertUrl(redirectUrl);
            }
            else if (data.RequiresExternalLogin)
            {
                AssertUrl(new ReadOnlyUrl(data.ExternalLoginUrl, new ReadOnlyQueryString("returnUrl", url.AbsoluteUri)));
                return;
            }
            else
            {
                AssertUrl(GetCommunityHomeUrl(data, community));
            }

            AssertHeader(!isDeleted ? data.HeaderSnippet : null);

            LogIn(member);
            AssertUrl(GetHostUrl(LoggedInMemberHomeUrl, data.Host, isDeleted));
            AssertHeader(!isDeleted ? data.HeaderSnippet : null);
        }

        [TestMethod]
        public void TestExternalLogIn()
        {
            var data = GetTestCommunity().GetCommunityTestData();
            var isDeleted = IsTestCommunityDeleted;
            var community = CreateTestCommunity(data, isDeleted);

            var url = GetCommunityHomeUrl(data, community);
            Get(url);
            if (data.RequiresExternalLogin)
                AssertUrl(new ReadOnlyUrl(data.ExternalLoginUrl, new ReadOnlyQueryString("returnUrl", url.AbsoluteUri)));
        }

        [TestMethod]
        public void TestMemberLogIn()
        {
            var data = GetTestCommunity().GetCommunityTestData();
            var isDeleted = IsTestCommunityDeleted;
            var community = CreateTestCommunity(data, isDeleted);

            if (data.RequiresExternalLogin)
                return;

            // Create a member.

            var member = _memberAccountsCommand.CreateTestMember(MemberEmailAddress, community.Id);

            // Navigate to the community page.

            Get(HomeUrl);
            AssertUrl(HomeUrl);
            AssertHeader(null);

            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertHeader(!isDeleted ? data.HeaderSnippet : null);

            // Try to access the change password url.

            Get(_changePasswordUrl);
            AssertUrl(_changePasswordUrl);
        }

        [TestMethod]
        public void TestEmployerLogIn()
        {
            var data = GetTestCommunity().GetCommunityTestData();
            var isDeleted = IsTestCommunityDeleted;
            var community = CreateTestCommunity(data, isDeleted);

            // Create an employer.

            var employer = CreateEmployer(community);

            // Navigate to the community page.

            Get(HomeUrl);
            AssertUrl(HomeUrl);
            AssertHeader(null);

            LogIn(employer);
            AssertUrl(LoggedInEmployerHomeUrl);
            AssertHeader(!isDeleted && !data.RequiresExternalLogin ? data.HeaderSnippet : null);

            // Try to access the change password url.

            Get(_changePasswordUrl);
            AssertUrl(_changePasswordUrl);
        }

        [TestMethod]
        public void TestHostLandingPage()
        {
            HostTestPage(GetTestCommunity(), false, "");
        }

        [TestMethod]
        public void TestHostJoinPage()
        {
            HostTestPage(GetTestCommunity(), true, "join.aspx");
        }

        [TestMethod]
        public void TestHostLogInPage()
        {
            HostTestPage(GetTestCommunity(), true, "login.aspx");
        }

        [TestMethod]
        public void TestPathLandingPage()
        {
            PathTestPage(GetTestCommunity(), false, "");
        }

        [TestMethod]
        public void TestPathJoinPage()
        {
            PathTestPage(GetTestCommunity(), true, "join.aspx");
        }

        [TestMethod]
        public void TestPathLogInPage()
        {
            PathTestPage(GetTestCommunity(), true, "login.aspx");
        }

        [TestMethod]
        public void TestRequiresExternalLogin()
        {
            TestRequiresExternalLogin(GetTestCommunity());
        }

        [TestMethod]
        public void TestCandidateSearch()
        {
            var data = GetTestCommunity().GetCommunityTestData();
            var isDeleted = IsTestCommunityDeleted;
            var community = CreateTestCommunity(data, isDeleted);

            // Create a member, associating them with the community.

            var member = CreateMember(community);
            _memberSearchService.UpdateMember(member.Id);

            // Create employer.

            var employer = CreateEmployer();
            LogIn(employer);

            // Search.

            Search(community, data, isDeleted);

            // Job application.

            LogOut();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            ApplyForJob(member, jobAd);

            Get(_resetUrl);
            AssertJobApplicant(member, employer, data, isDeleted, jobAd.Id);
        }

        [TestMethod]
        public void TestHostEmployerEnquiriesPage()
        {
            HostTestPage(GetTestCommunity(), false, "employers/enquiries");
        }

        protected Community CreateTestCommunity(CommunityTestData data, bool isDeleted)
        {
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            if (isDeleted)
            {
                var vertical = _verticalsCommand.GetVertical(community);
                vertical.IsDeleted = true;
                _verticalsCommand.UpdateVertical(vertical);
            }

            return community;
        }

        protected Employer CreateEmployer(Community community)
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            organisation.AffiliateId = community.Id;
            _organisationsCommand.UpdateOrganisation(organisation);
            return _employerAccountsCommand.CreateTestEmployer(EmployerUserId, organisation);
        }

        private ReadOnlyUrl GetCommunityHomeUrl(VerticalTestData data, Community community)
        {
            return string.IsNullOrEmpty(data.Host) || IsTestCommunityDeleted ? HomeUrl : GetCommunityUrl(community, "");
        }

        protected void ApplyForJob(Member member, JobAd jobAd)
        {
            Get(GetJobAdUrl(jobAd.Id));

            // Apply for the job.

            ApiLogIn(member);
            Post(GetApplyUrl(jobAd.Id));

            LogOut();
        }

        private static ReadOnlyUrl GetJobAdUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/jobs/" + jobAdId);
        }

        private static ReadOnlyUrl GetApplyUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/api/" + jobAdId + "/applywithprofile");
        }

        private void AssertJobApplicant(ICommunicationRecipient member, IUser employer, VerticalTestData data, bool isDeleted, Guid jobAdId)
        {
            // Log in as employer.

            LogIn(employer);
            Get(new ReadOnlyUrl(string.Format("{0}/{1}", _manageCandidatesUrl, jobAdId)));
            AssertPageContains("<span>New (<span class=\"count new-candidates-count\">1</span>)</span>");
            AssertPageContains(member.FullName);

            AssertCandidateLogo(data, isDeleted);
        }

        private void Search(Community community, VerticalTestData data, bool isDeleted)
        {
            // Search.

            var searchUrl = _searchUrl.AsNonReadOnly();
            searchUrl.QueryString["JobTitle"] = BusinessAnalyst;
            searchUrl.QueryString["CommunityId"] = community.Id.ToString();
            Get(searchUrl);

            // Assert that the member is returned.

            AssertPageContains("Results <span class=\"start\">1</span> - <span class=\"end\">1</span> of <span class=\"total\">1</span>");
            AssertCandidateLogo(data, isDeleted);
        }

        protected Member CreateMember(Community community)
        {
            var member = _memberAccountsCommand.CreateTestMember(0, community != null ? community.Id : (Guid?)null);
            _locationQuery.ResolveLocation(member.Address.Location, Australia, "Melbourne VIC 3000");

            var joinTime = new DateTime(2007, 1, 1);
            member.CreatedTime = joinTime;
            _memberAccountsCommand.UpdateMember(member);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate, joinTime);

            return member;
        }

        protected Employer CreateEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(EmployerUserId, _organisationsCommand.CreateTestOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            return employer;
        }

        protected void TestMemberJoin(Func<Community, CommunityTestData, ReadOnlyUrl> join, bool isHomePageJoin, Action<CommunityTestData, AffiliationItems> fillCommunityPersonalDetails, Action<CommunityTestData, AffiliationItems> assertCommunityPersonalDetails, AffiliationItems personalDetailsItems)
        {
            // Set up the community.

            var data = GetTestCommunity().GetCommunityTestData();
            var isDeleted = IsTestCommunityDeleted;
            var community = CreateTestCommunity(data, isDeleted);

            // Join on the first page.

            var url = join(community, data);
            if (url == null)
                return;

            // Fill in employment details.

            var personalDetailsUrl = _personalDetailsUrl.AsNonReadOnly();
            if (!isDeleted)
                personalDetailsUrl.Host = url.Host;
            AssertUrlWithoutQuery(personalDetailsUrl);
            AssertHeader(!isDeleted ? data.HeaderSnippet : null);

            fillCommunityPersonalDetails(data, personalDetailsItems);

            if (!isHomePageJoin)
            {
                _firstNameTextBox.Text = FirstName;
                _lastNameTextBox.Text = LastName;
                _emailAddressTextBox.Text = MemberEmailAddress;
                _passwordTextBox.Text = Password;
                _confirmPasswordTextBox.Text = Password;
                _acceptTermsCheckBox.IsChecked = true;
            }

            _locationTextBox.Text = Location;
            _phoneNumberTextBox.Text = PhoneNumber;
            _salaryLowerBoundTextBox.Text = Salary;
            _openToOffersRadioButton.IsChecked = true;
            Browser.Submit(_personalDetailsFormId);

            // Add networker details.

            var jobDetailsUrl = _jobDetailsUrl.AsNonReadOnly();
            if (!isDeleted)
                jobDetailsUrl.Host = url.Host;
            AssertUrlWithoutQuery(jobDetailsUrl);
            AssertHeader(!isDeleted ? data.HeaderSnippet : null);

            // Logout.

            LogOut();

            // Ensure that the activation email was sent.

            var email = _emailServer.AssertEmailSent();
            var returnAddress = string.IsNullOrEmpty(data.MemberServicesEmailAddress)
                ? Return
                : new EmailRecipient(data.ReturnEmailAddress, data.EmailDisplayName);
            email.AssertAddresses(returnAddress, returnAddress, new EmailRecipient(MemberEmailAddress, FirstName + " " + LastName));
            if (!isDeleted)
                email.AssertHtmlViewContains(data.ActivationEmailSnippet);
            else
                email.AssertHtmlViewDoesNotContain(data.ActivationEmailSnippet);

            // Get the activation code from the email.

            var activationUrl = GetActivationUrl(email.GetHtmlView().Body);

            // Make sure the host used in the email is that same as which the user was interacting with.

            if (!isDeleted)
                Assert.AreEqual(url.Host, activationUrl.Host);

            // Make sure this is like a new session.

            ClearCookies(activationUrl);

            // Navigate to the activation page which should activate the account.

            Get(activationUrl);
            AssertUrlWithoutQuery(GetHostUrl(_activationUrl, data.Host, isDeleted));
            AssertHeader(!isDeleted ? data.HeaderSnippet : null);

            // Make sure the host after following the link is the same.

            if (!isDeleted)
                Assert.AreEqual(url.Host, Browser.CurrentUrl.Host);

            // Try to log in again and assert that the home page is reached.

            var member = _membersQuery.GetMember(MemberEmailAddress);
            LogIn(member, Password);
            AssertHeader(!isDeleted ? data.HeaderSnippet : null);

            // Check the user.

            AssertMember(MemberEmailAddress, community, isDeleted);

            // Use the standard host and follow the link.  This should still get to the community site.

            LogOut();
            ClearCookies(activationUrl);

            activationUrl.Host = new ApplicationUrl("~/").Host;
            ClearCookies(activationUrl);

            Get(activationUrl);
            AssertUrlWithoutQuery(GetHostUrl(_activationUrl, data.Host, isDeleted));
            AssertHeader(!isDeleted ? data.HeaderSnippet : null);
            if (!isDeleted)
                Assert.AreEqual(url.Host, Browser.CurrentUrl.Host);

            assertCommunityPersonalDetails(data, personalDetailsItems);
        }

        private static void FillCommunityPersonalDetails(CommunityTestData communityTestData, AffiliationItems items)
        {
        }

        private static void AssertCommunityPersonalDetails(CommunityTestData communityTestData, AffiliationItems items)
        {
        }

        protected ReadOnlyUrl JoinPageJoin(Community community, CommunityTestData data)
        {
            // Navigate to the community join page.

            var url = GetCommunityUrl(community, "join.aspx").AsNonReadOnly();
            url.Scheme = Uri.UriSchemeHttps;
            Get(url);

            if (data.RequiresExternalLogin)
            {
                AssertUrl(new ReadOnlyUrl(data.ExternalLoginUrl, new ReadOnlyQueryString("returnUrl", url.AbsoluteUri)));
                return null;
            }

            // Should have been redirected to the equivalent non-vertical url.

            var joinUrl = GetHostUrl(new ReadOnlyApplicationUrl(true, "~/join"), url.Host, IsTestCommunityDeleted);
            AssertUrl(joinUrl);
            AssertHeader(!IsTestCommunityDeleted ? data.HeaderSnippet : null);

            // Get to the eprsonal details page.

            Browser.Submit(_joinFormId);
            return url;
        }

        private ReadOnlyUrl HomePageJoin(Community community, CommunityTestData data)
        {
            // Navigate to the community join page.

            var url = GetCommunityUrl(community, "");
            Get(url);

            if (IsTestCommunityDeleted)
            {
                // Should have been redirected to the equivalent non-vertical url.

                url = new ReadOnlyApplicationUrl("~/");
                AssertUrl(url);
            }
            else if (data.RequiresExternalLogin)
            {
                AssertUrl(new ReadOnlyUrl(data.ExternalLoginUrl, new ReadOnlyQueryString("returnUrl", url.AbsoluteUri)));
                return null;
            }
            else
            {
                AssertUrl(GetCommunityHomeUrl(data, community));
            }

            AssertHeader(!IsTestCommunityDeleted ? data.HeaderSnippet : null);

            _homeFirstNameTextBox.Text = FirstName;
            _homeLastNameTextBox.Text = LastName;
            _homeEmailAddressTextBox.Text = MemberEmailAddress;
            _homePasswordTextBox.Text = Password;
            _homeConfirmPasswordTextBox.Text = Password;
            _homeAcceptTermsCheckBox.IsChecked = true;
            Browser.Submit(_homeJoinFormId);

            Browser.Submit(_joinFormId);

            return url;
        }

        private void HostTestPage(TestCommunity testCommunity, bool secure, string page)
        {
            var data = testCommunity.GetCommunityTestData();
            var isDeleted = IsTestCommunityDeleted;
            var community = CreateTestCommunity(data, isDeleted);

            // Hit some pages.

            AssertDefaultAndProfilePages(null);

            // Hit the landing page.

            var url = GetCommunityHostUrl(community, secure, page);
            if (url == null)
                return;

            Get(url);

            if (isDeleted)
            {
                // Should have been redirected to the equivalent non-vertical url.

                var redirectUrl = string.IsNullOrEmpty(page)
                    ? new ReadOnlyApplicationUrl(false, "~/")
                    : new ReadOnlyApplicationUrl(page.EndsWith(".aspx"), "~/" + (page.EndsWith(".aspx") ? page.Substring(0, page.Length - ".aspx".Length) : page));
                AssertUrl(redirectUrl);

                // No branding.

                AssertHeader(null);
            }
            else
            {
                if (data.RequiresExternalLogin)
                {
                    AssertUrl(new ReadOnlyUrl(data.ExternalLoginUrl, new ReadOnlyQueryString("returnUrl", url.AbsoluteUri)));
                }
                else
                {
                    // Check no redirection.

                    AssertUrl(GetHostUrl(url, data.Host, false));

                    // Check the contents of the page.

                    AssertHeader(data.HeaderSnippet);
                }

                // Hit the pages again.

                AssertDefaultAndProfilePages(data);
            }
        }

        private void PathTestPage(TestCommunity testCommunity, bool secure, string page)
        {
            var data = testCommunity.GetCommunityTestData();
            var isDeleted = IsTestCommunityDeleted;
            var community = CreateTestCommunity(data, isDeleted);

            // Hit some pages.

            AssertDefaultAndProfilePages(null);

            // Hit the landing page.

            var url = GetCommunityPathUrl(community, secure, page);
            if (url == null)
                return;

            Get(url);

            if (isDeleted)
            {
                // Should have been redirected to the equivalent non-vertical url.

                var redirectUrl = string.IsNullOrEmpty(page)
                    ? new ReadOnlyApplicationUrl("~/")
                    : new ReadOnlyApplicationUrl(true, "~/" + page.Substring(0, page.Length - ".aspx".Length));
                AssertUrl(redirectUrl);

                // No branding.

                AssertHeader(null);
            }
            else
            {
                if (data.RequiresExternalLogin)
                {
                    var redirectUrl = string.IsNullOrEmpty(page)
                        ? new ApplicationUrl("~/")
                        : new ApplicationUrl(true, "~/" + page.Substring(0, page.Length - ".aspx".Length));
                    redirectUrl.Host = "localhost." + data.Host;
                    AssertUrl(new ReadOnlyUrl(data.ExternalLoginUrl, new ReadOnlyQueryString("returnUrl", redirectUrl.AbsoluteUri)));
                }
                else
                {
                    // Check the redirection.

                    var hostUrl = GetCommunityHostUrl(community, secure, page);
                    if (hostUrl != null)
                    {
                        // Should have been redirected to the host url.

                        AssertUrl(hostUrl);
                    }
                    else
                    {
                        // Should have been redirected to the equivalent non-vertical url.

                        var redirectUrl = string.IsNullOrEmpty(page)
                            ? new ReadOnlyApplicationUrl(false, "~/")
                            : new ReadOnlyApplicationUrl(true, "~/" + page.Substring(0, page.Length - ".aspx".Length));
                        AssertUrl(redirectUrl);
                    }

                    AssertHeader(data.HeaderSnippet);
                }

                // Hit the pages again.

                AssertDefaultAndProfilePages(data);
            }
        }

        private void TestRequiresExternalLogin(TestCommunity testCommunity)
        {
            var data = testCommunity.GetCommunityTestData();
            var isDeleted = IsTestCommunityDeleted;
            var community = CreateTestCommunity(data, isDeleted);

            // Only works with a host.

            var vertical = _verticalsCommand.GetVertical(community);
            if (string.IsNullOrEmpty(vertical.Host))
                return;

            var url = _verticalsCommand.GetCommunityHostUrl(community, "");
            Get(url);

            if (isDeleted)
            {
                // Should have been redirected to the equivalent non-vertical url.

                var redirectUrl = new ReadOnlyApplicationUrl("~/");
                AssertUrl(redirectUrl);
            }
            else
            {
                if (vertical.RequiresExternalLogin)
                {
                    // Should have been redirected.

                    AssertUrl(new ReadOnlyUrl(vertical.ExternalLoginUrl, new ReadOnlyQueryString("returnUrl", url.AbsoluteUri)));
                }
                else
                {
                    AssertUrl(url);
                }
            }
        }

        private void AssertDefaultAndProfilePages(VerticalTestData data)
        {
            if (data == null || !data.RequiresExternalLogin)
            {
                Get(HomeUrl);
                AssertHeader(data == null ? null : data.HeaderSnippet);
                AssertFavicon(data == null ? null : data.ImageRootFolder, data == null ? null : data.FaviconRelativePath);
                AssertHomePageTitle(data == null ? null : data.HomePageTitle);
                GetPage<Friends>();
                AssertHeader(data == null ? null : data.HeaderSnippet);
                AssertFavicon(data == null ? null : data.ImageRootFolder, data == null ? null : data.FaviconRelativePath);
                Get(_profileUrl);
                AssertHeader(data == null ? null : data.HeaderSnippet);
                AssertFavicon(data == null ? null : data.ImageRootFolder, data == null ? null : data.FaviconRelativePath);
            }
        }

        protected static Url GetActivationUrl(string body)
        {
            var document = new XmlDocument();
            document.LoadXml(body);

            var xmlNode = document.SelectSingleNode("//div[@class='body']/p/a");
            if (xmlNode == null)
                return null;

            return new ApplicationUrl(xmlNode.Attributes["href"].InnerText);
        }

        private void AssertCandidateLogo(VerticalTestData data, bool isDeleted)
        {
            // If there is a logo look for it.

            if (!string.IsNullOrEmpty(data.CandidateImageUrl))
            {
                var url = new ApplicationUrl(data.CandidateImageUrl);
                if (!isDeleted)
                    AssertPageContains(url.Path);
                else
                    AssertPageDoesNotContain(url.Path);
            }
        }

        private static ReadOnlyUrl GetHostUrl(ReadOnlyUrl url, string host, bool isDeleted)
        {
            if (!isDeleted && !string.IsNullOrEmpty(host) && url.Host != host)
            {
                var hostUrl = url.AsNonReadOnly();
                hostUrl.Host = (host.StartsWith("localhost.") ? "" : "localhost.") + host;
                return hostUrl;
            }

            return url;
        }
    }
}
