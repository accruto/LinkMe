using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Queries;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Representatives.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Reports.Users.Employers.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Credits
{
    [TestClass]
    public abstract class CreditsTests
        : SearchTests
    {
        protected const string Password = "password";
        private const string PhoneNumber = "0438771641";
        private const string RepresentativePhoneNumber = "0438771642";

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlButtonTester _loginButton;

        private HtmlTextBoxTester _joinLoginIdTextBox;
        private HtmlPasswordTester _joinPasswordTextBox;
        private HtmlPasswordTester _joinConfirmPasswordTextBox;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _phoneNumberTextBox;
        private HtmlTextBoxTester _organisationNameTextBox;
        private HtmlCheckBoxTester _acceptTermsCheckBox;
        private HtmlButtonTester _joinButton;

        private ReadOnlyUrl _loginUrl;
        private ReadOnlyUrl _sendMessagesUrl;
        private ReadOnlyUrl _downloadUrl;
        private ReadOnlyUrl _sendUrl;
        private ReadOnlyUrl _phoneNumbersUrl;
        private ReadOnlyUrl _baseCandidateUrl;

        private readonly IEmployerAccountsQuery _employerAccountsQuery = Resolve<IEmployerAccountsQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IExercisedCreditsQuery _exercisedCreditsQuery = Resolve<IExercisedCreditsQuery>();
        private readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmployerViewsRepository _employerViewsRepository = Resolve<IEmployerViewsRepository>();
        private readonly IEmployerMemberAccessReportsQuery _employerMemberAccessReportsQuery = Resolve<IEmployerMemberAccessReportsQuery>();
        private readonly INetworkingCommand _networkingCommand = Resolve<INetworkingCommand>();
        private readonly IRepresentativesCommand _representativesCommand = Resolve<IRepresentativesCommand>();
        protected readonly IEmployerCreditsCommand _employerCreditsCommand = Resolve<IEmployerCreditsCommand>();
        protected readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

        protected delegate MemberAccessReason? AssertAction(bool isLoggedIn, ref Employer employer, Member member, Member represetative, ref int loggedInViewings, ref int notLoggedInViewings);

        [TestInitialize]
        public void CreditsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();
            _emailServer = EmailHost.Start();
            _emailServer.ClearEmails();

            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _loginButton = new HtmlButtonTester(Browser, "login");

            _joinLoginIdTextBox = new HtmlTextBoxTester(Browser, "JoinLoginId");
            _joinPasswordTextBox = new HtmlPasswordTester(Browser, "JoinPassword");
            _joinConfirmPasswordTextBox = new HtmlPasswordTester(Browser, "JoinConfirmPassword");
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _organisationNameTextBox = new HtmlTextBoxTester(Browser, "OrganisationName");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _acceptTermsCheckBox = new HtmlCheckBoxTester(Browser, "AcceptTerms");
            _joinButton = new HtmlButtonTester(Browser, "join");

            _loginUrl = new ReadOnlyApplicationUrl("~/employers/login");
            _sendMessagesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/sendmessages");
            _downloadUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/downloadresumes");
            _sendUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/sendresumes");
            _phoneNumbersUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/phonenumbers");
            _baseCandidateUrl = new ReadOnlyApplicationUrl("~/candidates/");
        }

        #region TestSearchAfterLoginName

        [TestMethod]
        public void TestSearchAfterLoginName()
        {
            TestSearchAfterLogin(PerformSearch, AssertName, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchAfterLoginNameInvisibleResume()
        {
            TestSearchAfterLogin(PerformSearch, AssertName, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchAfterLoginNameInvisibleName()
        {
            TestSearchAfterLogin(PerformSearch, AssertName, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchAfterLoginNameInvisibilePhoneNumber()
        {
            TestSearchAfterLogin(PerformSearch, AssertName, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchAfterLoginRepresentative

        [TestMethod]
        public void TestSearchAfterLoginRepresentative()
        {
            TestSearchAfterLogin(PerformSearch, AssertRepresentative, true, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchAfterLoginRepresentativeInvisibleResume()
        {
            TestSearchAfterLogin(PerformSearch, AssertRepresentative, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchAfterLoginRepresentativeInvisibleName()
        {
            TestSearchAfterLogin(PerformSearch, AssertRepresentative, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchAfterLoginRepresentativeInvisibilePhoneNumber()
        {
            TestSearchAfterLogin(PerformSearch, AssertRepresentative, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchAfterLoginPhoneNumber

        [TestMethod]
        public void TestSearchAfterLoginPhoneNumber()
        {
            TestSearchAfterLogin(PerformSearch, AssertPhoneNumber, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchAfterLoginPhoneNumberInvisibleResume()
        {
            TestSearchAfterLogin(PerformSearch, AssertPhoneNumber, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchAfterLoginPhoneNumberInvisibleName()
        {
            TestSearchAfterLogin(PerformSearch, AssertPhoneNumber, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchAfterLoginPhoneNumberInvisibilePhoneNumber()
        {
            TestSearchAfterLogin(PerformSearch, AssertPhoneNumber, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchAfterLoginRepresentativePhoneNumber

        [TestMethod]
        public void TestSearchAfterLoginRepresentativePhoneNumber()
        {
            TestSearchAfterLogin(PerformSearch, AssertPhoneNumber, true, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchAfterLoginRepresentativePhoneNumberInvisibleResume()
        {
            TestSearchAfterLogin(PerformSearch, AssertPhoneNumber, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchAfterLoginRepresentativePhoneNumberInvisibleName()
        {
            TestSearchAfterLogin(PerformSearch, AssertPhoneNumber, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchAfterLoginRepresentativePhoneNumberInvisibilePhoneNumber()
        {
            TestSearchAfterLogin(PerformSearch, AssertPhoneNumber, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchAfterLoginSendMessage

        [TestMethod]
        public void TestSearchAfterLoginSendMessage()
        {
            TestSearchAfterLogin(PerformSearch, AssertSendMessage, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchAfterLoginSendMessageInvisibleResume()
        {
            TestSearchAfterLogin(PerformSearch, AssertSendMessage, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchAfterLoginSendMessageInvisibleName()
        {
            TestSearchAfterLogin(PerformSearch, AssertSendMessage, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchAfterLoginSendMessageInvisibilePhoneNumber()
        {
            TestSearchAfterLogin(PerformSearch, AssertSendMessage, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchAfterLoginRepresentativeSendMessage

        [TestMethod]
        public void TestSearchAfterLoginRepresentativeSendMessage()
        {
            TestSearchAfterLogin(PerformSearch, AssertSendMessage, true, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchAfterLoginRepresentativeSendMessageInvisibleResume()
        {
            TestSearchAfterLogin(PerformSearch, AssertSendMessage, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchAfterLoginRepresentativeSendMessageInvisibleName()
        {
            TestSearchAfterLogin(PerformSearch, AssertSendMessage, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchAfterLoginRepresentativeSendMessageInvisibilePhoneNumber()
        {
            TestSearchAfterLogin(PerformSearch, AssertSendMessage, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchAfterLoginDownloadResume

        [TestMethod]
        public void TestSearchAfterLoginDownloadResume()
        {
            TestSearchAfterLogin(PerformSearch, AssertDownloadResume, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchAfterLoginDownloadResumeInvisibleResume()
        {
            TestSearchAfterLogin(PerformSearch, AssertDownloadResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchAfterLoginDownloadResumeInvisibleName()
        {
            TestSearchAfterLogin(PerformSearch, AssertDownloadResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchAfterLoginDownloadResumeInvisibilePhoneNumber()
        {
            TestSearchAfterLogin(PerformSearch, AssertDownloadResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchAfterLoginEmailResume

        [TestMethod]
        public void TestSearchAfterLoginEmailResume()
        {
            TestSearchAfterLogin(PerformSearch, AssertEmailResume, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchAfterLoginEmailResumeInvisibleResume()
        {
            TestSearchAfterLogin(PerformSearch, AssertEmailResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchAfterLoginEmailResumeInvisibleName()
        {
            TestSearchAfterLogin(PerformSearch, AssertEmailResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchAfterLoginEmailResumeInvisibilePhoneNumber()
        {
            TestSearchAfterLogin(PerformSearch, AssertEmailResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchAfterLoginViewResume

        [TestMethod]
        public void TestSearchAfterLoginViewResume()
        {
            TestSearchAfterLogin(PerformSearch, AssertViewResume, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchAfterLoginViewResumeInvisibleResume()
        {
            TestSearchAfterLogin(PerformSearch, AssertViewResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchAfterLoginViewResumeInvisibleName()
        {
            TestSearchAfterLogin(PerformSearch, AssertViewResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchAfterLoginViewResumeInvisibilePhoneNumber()
        {
            TestSearchAfterLogin(PerformSearch, AssertViewResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchBeforeLoginName

        [TestMethod]
        public void TestSearchBeforeLoginName()
        {
            TestSearchBeforeLogin(PerformSearch, AssertName, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchBeforeLoginNameInvisibleResume()
        {
            TestSearchBeforeLogin(PerformSearch, AssertName, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchBeforeLoginNameInvisibleName()
        {
            TestSearchBeforeLogin(PerformSearch, AssertName, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchBeforeLoginNameInvisibilePhoneNumber()
        {
            TestSearchBeforeLogin(PerformSearch, AssertName, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchBeforeLoginRepresentative

        [TestMethod]
        public void TestSearchBeforeLoginRepresentative()
        {
            TestSearchBeforeLogin(PerformSearch, AssertRepresentative, true, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchBeforeLoginRepresentativeInvisibleResume()
        {
            TestSearchBeforeLogin(PerformSearch, AssertRepresentative, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchBeforeLoginRepresentativeInvisibleName()
        {
            TestSearchBeforeLogin(PerformSearch, AssertRepresentative, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchBeforeLoginRepresentativeInvisibilePhoneNumber()
        {
            TestSearchBeforeLogin(PerformSearch, AssertRepresentative, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchBeforeLoginPhoneNumber

        [TestMethod]
        public void TestSearchBeforeLoginPhoneNumber()
        {
            TestSearchBeforeLogin(PerformSearch, AssertPhoneNumber, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchBeforeLoginPhoneNumberInvisibleResume()
        {
            TestSearchBeforeLogin(PerformSearch, AssertPhoneNumber, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchBeforeLoginPhoneNumberInvisibleName()
        {
            TestSearchBeforeLogin(PerformSearch, AssertPhoneNumber, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchBeforeLoginPhoneNumberInvisibilePhoneNumber()
        {
            TestSearchBeforeLogin(PerformSearch, AssertPhoneNumber, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchBeforeLoginRepresentativePhoneNumber

        [TestMethod]
        public void TestSearchBeforeLoginRepresentativePhoneNumber()
        {
            TestSearchBeforeLogin(PerformSearch, AssertPhoneNumber, true, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchBeforeLoginRepresentativePhoneNumberInvisibleResume()
        {
            TestSearchBeforeLogin(PerformSearch, AssertPhoneNumber, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchBeforeLoginRepresentativePhoneNumberInvisibleName()
        {
            TestSearchBeforeLogin(PerformSearch, AssertPhoneNumber, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchBeforeLoginRepresentativePhoneNumberInvisibilePhoneNumber()
        {
            TestSearchBeforeLogin(PerformSearch, AssertPhoneNumber, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchBeforeLoginSendMessage

        [TestMethod]
        public void TestSearchBeforeLoginSendMessage()
        {
            TestSearchBeforeLogin(PerformSearch, AssertSendMessage, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchBeforeLoginSendMessageInvisibleResume()
        {
            TestSearchBeforeLogin(PerformSearch, AssertSendMessage, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchBeforeLoginSendMessageInvisibleName()
        {
            TestSearchBeforeLogin(PerformSearch, AssertSendMessage, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchBeforeLoginSendMessageInvisibilePhoneNumber()
        {
            TestSearchBeforeLogin(PerformSearch, AssertSendMessage, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchBeforeLoginRepresentativeSendMessage

        [TestMethod]
        public void TestSearchBeforeLoginRepresentativeSendMessage()
        {
            TestSearchBeforeLogin(PerformSearch, AssertSendMessage, true, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchBeforeLoginRepresentativeSendMessageInvisibleResume()
        {
            TestSearchBeforeLogin(PerformSearch, AssertSendMessage, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchBeforeLoginRepresentativeSendMessageInvisibleName()
        {
            TestSearchBeforeLogin(PerformSearch, AssertSendMessage, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchBeforeLoginRepresentativeSendMessageInvisibilePhoneNumber()
        {
            TestSearchBeforeLogin(PerformSearch, AssertSendMessage, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchBeforeLoginDownloadResume

        [TestMethod]
        public void TestSearchBeforeLoginDownloadResume()
        {
            TestSearchBeforeLogin(PerformSearch, AssertDownloadResume, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchBeforeLoginDownloadResumeInvisibleResume()
        {
            TestSearchBeforeLogin(PerformSearch, AssertDownloadResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchBeforeLoginDownloadResumeInvisibleName()
        {
            TestSearchBeforeLogin(PerformSearch, AssertDownloadResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchBeforeLoginDownloadResumeInvisibilePhoneNumber()
        {
            TestSearchBeforeLogin(PerformSearch, AssertDownloadResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchBeforeLoginEmailResume

        [TestMethod]
        public void TestSearchBeforeLoginEmailResume()
        {
            TestSearchBeforeLogin(PerformSearch, AssertEmailResume, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchBeforeLoginEmailResumeInvisibleResume()
        {
            TestSearchBeforeLogin(PerformSearch, AssertEmailResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchBeforeLoginEmailResumeInvisibleName()
        {
            TestSearchBeforeLogin(PerformSearch, AssertEmailResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchBeforeLoginEmailResumeInvisibilePhoneNumber()
        {
            TestSearchBeforeLogin(PerformSearch, AssertEmailResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestSearchBeforeLoginViewResume

        [TestMethod]
        public void TestSearchBeforeLoginViewResume()
        {
            TestSearchBeforeLogin(PerformSearch, AssertViewResume, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestSearchBeforeLoginViewResumeInvisibleResume()
        {
            TestSearchBeforeLogin(PerformSearch, AssertViewResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestSearchBeforeLoginViewResumeInvisibleName()
        {
            TestSearchBeforeLogin(PerformSearch, AssertViewResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestSearchBeforeLoginViewResumeInvisiblePhoneNumber()
        {
            TestSearchBeforeLogin(PerformSearch, AssertViewResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeAfterLoginName

        [TestMethod]
        public void TestViewResumeAfterLoginName()
        {
            TestViewResumeAfterLogin(AssertName, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeAfterLoginNameInvisibleResume()
        {
            TestViewResumeAfterLogin(AssertName, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginNameInvisibleName()
        {
            TestViewResumeAfterLogin(AssertName, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginNameInvisibilePhoneNumber()
        {
            TestViewResumeAfterLogin(AssertName, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeAfterLoginPhoneNumber

        [TestMethod]
        public void TestViewResumeAfterLoginPhoneNumber()
        {
            TestViewResumeAfterLogin(AssertPhoneNumber, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeAfterLoginPhoneNumberInvisibleResume()
        {
            TestViewResumeAfterLogin(AssertPhoneNumber, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginPhoneNumberInvisibleName()
        {
            TestViewResumeAfterLogin(AssertPhoneNumber, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginPhoneNumberInvisibilePhoneNumber()
        {
            TestViewResumeAfterLogin(AssertPhoneNumber, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeAfterLoginRepresentativePhoneNumber

        [TestMethod]
        public void TestViewResumeAfterLoginRepresentativePhoneNumber()
        {
            TestViewResumeAfterLogin(AssertPhoneNumber, true, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeAfterLoginRepresentativePhoneNumberInvisibleResume()
        {
            TestViewResumeAfterLogin(AssertPhoneNumber, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginRepresentativePhoneNumberInvisibleName()
        {
            TestViewResumeAfterLogin(AssertPhoneNumber, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginRepresentativePhoneNumberInvisibilePhoneNumber()
        {
            TestViewResumeAfterLogin(AssertPhoneNumber, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeAfterLoginSendMessage

        [TestMethod]
        public void TestViewResumeAfterLoginSendMessage()
        {
            TestViewResumeAfterLogin(AssertSendMessage, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeAfterLoginSendMessageInvisibleResume()
        {
            TestViewResumeAfterLogin(AssertSendMessage, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginSendMessageInvisibleName()
        {
            TestViewResumeAfterLogin(AssertSendMessage, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginSendMessageInvisibilePhoneNumber()
        {
            TestViewResumeAfterLogin(AssertSendMessage, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeAfterLoginRepresentativeSendMessage

        [TestMethod]
        public void TestViewResumeAfterLoginRepresentativeSendMessage()
        {
            TestViewResumeAfterLogin(AssertSendMessage, true, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeAfterLoginRepresentativeSendMessageInvisibleResume()
        {
            TestViewResumeAfterLogin(AssertSendMessage, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginRepresentativeSendMessageInvisibleName()
        {
            TestViewResumeAfterLogin(AssertSendMessage, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginRepresentativeSendMessageInvisibilePhoneNumber()
        {
            TestViewResumeAfterLogin(AssertSendMessage, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeAfterLoginDownloadResume

        [TestMethod]
        public void TestViewResumeAfterLoginDownloadResume()
        {
            TestViewResumeAfterLogin(AssertDownloadResume, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeAfterLoginDownloadResumeInvisibleResume()
        {
            TestViewResumeAfterLogin(AssertDownloadResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginDownloadResumeInvisibleName()
        {
            TestViewResumeAfterLogin(AssertDownloadResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginDownloadResumeInvisibilePhoneNumber()
        {
            TestViewResumeAfterLogin(AssertDownloadResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeAfterLoginEmailResume

        [TestMethod]
        public void TestViewResumeAfterLoginEmailResume()
        {
            TestViewResumeAfterLogin(AssertEmailResume, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeAfterLoginEmailResumeInvisibleResume()
        {
            TestViewResumeAfterLogin(AssertEmailResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginEmailResumeInvisibleName()
        {
            TestViewResumeAfterLogin(AssertEmailResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeAfterLoginEmailResumeInvisibilePhoneNumber()
        {
            TestViewResumeAfterLogin(AssertEmailResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeBeforeLoginName

        [TestMethod]
        public void TestViewResumeBeforeLoginName()
        {
            TestViewResumeBeforeLogin(AssertName, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginNameInvisibleResume()
        {
            TestViewResumeBeforeLogin(AssertName, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginNameInvisibleName()
        {
            TestViewResumeBeforeLogin(AssertName, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginNameInvisibilePhoneNumber()
        {
            TestViewResumeBeforeLogin(AssertName, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeBeforeLoginPhoneNumber

        [TestMethod]
        public void TestViewResumeBeforeLoginPhoneNumber()
        {
            TestViewResumeBeforeLogin(AssertPhoneNumber, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginPhoneNumberInvisibleResume()
        {
            TestViewResumeBeforeLogin(AssertPhoneNumber, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginPhoneNumberInvisibleName()
        {
            TestViewResumeBeforeLogin(AssertPhoneNumber, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginPhoneNumberInvisibilePhoneNumber()
        {
            TestViewResumeBeforeLogin(AssertPhoneNumber, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeBeforeLoginRepresentativePhoneNumber

        [TestMethod]
        public void TestViewResumeBeforeLoginRepresentativePhoneNumber()
        {
            TestViewResumeBeforeLogin(AssertPhoneNumber, true, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginRepresentativePhoneNumberInvisibleResume()
        {
            TestViewResumeBeforeLogin(AssertPhoneNumber, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginRepresentativePhoneNumberInvisibleName()
        {
            TestViewResumeBeforeLogin(AssertPhoneNumber, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginRepresentativePhoneNumberInvisibilePhoneNumber()
        {
            TestViewResumeBeforeLogin(AssertPhoneNumber, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeBeforeLoginSendMessage

        [TestMethod]
        public void TestViewResumeBeforeLoginSendMessage()
        {
            TestViewResumeBeforeLogin(AssertSendMessage, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginSendMessageInvisibleResume()
        {
            TestViewResumeBeforeLogin(AssertSendMessage, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginSendMessageInvisibleName()
        {
            TestViewResumeBeforeLogin(AssertSendMessage, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginSendMessageInvisibilePhoneNumber()
        {
            TestViewResumeBeforeLogin(AssertSendMessage, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeBeforeLoginRepresentativeSendMessage

        [TestMethod]
        public void TestViewResumeBeforeLoginRepresentativeSendMessage()
        {
            TestViewResumeBeforeLogin(AssertSendMessage, true, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginRepresentativeSendMessageInvisibleResume()
        {
            TestViewResumeBeforeLogin(AssertSendMessage, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginRepresentativeSendMessageInvisibleName()
        {
            TestViewResumeBeforeLogin(AssertSendMessage, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginRepresentativeSendMessageInvisibilePhoneNumber()
        {
            TestViewResumeBeforeLogin(AssertSendMessage, true, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeBeforeLoginDownloadResume

        [TestMethod]
        public void TestViewResumeBeforeLoginDownloadResume()
        {
            TestViewResumeBeforeLogin(AssertDownloadResume, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginDownloadResumeInvisibleResume()
        {
            TestViewResumeBeforeLogin(AssertDownloadResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginDownloadResumeInvisibleName()
        {
            TestViewResumeBeforeLogin(AssertDownloadResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginDownloadResumeInvisibilePhoneNumber()
        {
            TestViewResumeBeforeLogin(AssertDownloadResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        #region TestViewResumeBeforeLoginEmailResume

        [TestMethod]
        public void TestViewResumeBeforeLoginEmailResume()
        {
            TestViewResumeBeforeLogin(AssertEmailResume, false, ProfessionalVisibility.All);
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginEmailResumeInvisibleResume()
        {
            TestViewResumeBeforeLogin(AssertEmailResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Resume));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginEmailResumeInvisibleName()
        {
            TestViewResumeBeforeLogin(AssertEmailResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.Name));
        }

        [TestMethod]
        public void TestViewResumeBeforeLoginEmailResumeInvisibilePhoneNumber()
        {
            TestViewResumeBeforeLogin(AssertEmailResume, false, ProfessionalVisibility.All.ResetFlag(ProfessionalVisibility.PhoneNumbers));
        }

        #endregion

        private void TestSearchAfterLogin(Action performSearch, AssertAction assertAction, bool isRepresented, ProfessionalVisibility visibility)
        {
            // Create the member and employer.

            var member = CreateMember();
            var representative = isRepresented ? CreateRepresentative(member) : null;
            var employer = CreateEmployer(member);
            var initialCredits = employer == null ? 0 : GetCredits(employer.Id);
            SetVisibility(member, visibility);
            _emailServer.ClearEmails();

            // Login.

            if (employer != null)
            {
                Get(_loginUrl);
                LogIn(employer);
            }

            // Search.

            performSearch();

            // Assert.

            var loggedInViewings = 0;
            var notLoggedInViewings = 0;
            var reason = AssertSearchPages(employer != null, ref employer, member, representative, assertAction, ref loggedInViewings, ref notLoggedInViewings);
            AssertData(employer, member, reason, loggedInViewings, notLoggedInViewings, initialCredits);
        }

        private void TestSearchBeforeLogin(Action performSearch, AssertAction assertAction, bool isRepresented, ProfessionalVisibility visibility)
        {
            // Create the member and employer.

            var member = CreateMember();
            var representative = isRepresented ? CreateRepresentative(member) : null;
            var employer = CreateEmployer(member);
            var initialCredits = employer == null ? 0 : GetCredits(employer.Id);
            SetVisibility(member, visibility);
            _emailServer.ClearEmails();

            // Search.

            performSearch();

            // Assert.

            var loggedInViewings = 0;
            var notLoggedInViewings = 0;
            var reason = AssertSearchPages(false, ref employer, member, representative, assertAction, ref loggedInViewings, ref notLoggedInViewings);
            AssertData(employer, member, reason, loggedInViewings, notLoggedInViewings, initialCredits);
        }

        private void TestViewResumeAfterLogin(AssertAction assertAction, bool isRepresented, ProfessionalVisibility visibility)
        {
            // Create the member and employer.

            var member = CreateMember();
            var representative = isRepresented ? CreateRepresentative(member) : null;
            var employer = CreateEmployer(member);
            var initialCredits = employer == null ? 0 : GetCredits(employer.Id);
            SetVisibility(member, visibility);
            _emailServer.ClearEmails();

            // Login.

            var isLoggedIn = false;
            if (employer != null)
            {
                Get(_loginUrl);
                LogIn(employer);
                isLoggedIn = true;
            }

            // View the resume.

            var loggedInViewings = !isLoggedIn
                ? 0
                : visibility.IsFlagSet(ProfessionalVisibility.Resume)
                    ? 1
                    : 0;
            var notLoggedInViewings = isLoggedIn
                ? 0
                : visibility.IsFlagSet(ProfessionalVisibility.Resume)
                    ? 1
                    : 0;

            MemberAccessReason? reason = null;
            if (visibility.IsFlagSet(ProfessionalVisibility.Resume))
            {
                Get(GetCandidateUrl(member));
                reason = AssertViewResumePages(isLoggedIn, employer, member, representative, assertAction, ref loggedInViewings, ref notLoggedInViewings);
            }
            else
            {
                Get(HttpStatusCode.NotFound, GetCandidateUrl(member));
                AssertPageContains("The candidate cannot be found.");
            }

            AssertData(employer, member, reason, loggedInViewings, notLoggedInViewings, initialCredits);
        }

        private void TestViewResumeBeforeLogin(AssertAction assertAction, bool isRepresented, ProfessionalVisibility visibility)
        {
            // Create the member and employer.

            var member = CreateMember();
            var representative = isRepresented ? CreateRepresentative(member) : null;
            var employer = CreateEmployer(member);
            var initialCredits = employer == null ? 0 : GetCredits(employer.Id);
            SetVisibility(member, visibility);
            _emailServer.ClearEmails();

            // View the resume.

            var loggedInViewings = 0;
            var notLoggedInViewings = 0;

            if (visibility.IsFlagSet(ProfessionalVisibility.Resume))
            {
                Get(GetCandidateUrl(member));
                notLoggedInViewings = 1;
            }
            else
            {
                Get(HttpStatusCode.NotFound, GetCandidateUrl(member));
            }

            // Assert.

            MemberAccessReason? reason = null;

            // Login if can.

            if (employer != null)
            {
                Get(_loginUrl);
                LogIn(employer);

                if (visibility.IsFlagSet(ProfessionalVisibility.Resume))
                {
                    Get(GetCandidateUrl(member));
                    ++loggedInViewings;

                    reason = AssertViewResumePages(true, employer, member, representative, assertAction, ref loggedInViewings, ref notLoggedInViewings);
                }
                else
                {
                    Get(HttpStatusCode.NotFound, GetCandidateUrl(member));
                }
            }

            AssertData(employer, member, reason, loggedInViewings, notLoggedInViewings, initialCredits);
        }

        private ReadOnlyUrl GetCandidateUrl(IMember member)
        {
            // Assume that the user has no salary and no title but does have a location.

            var sb = new StringBuilder();
            sb.Append(TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndWhiteSpace(member.Address.Location.ToString())).ToLower().Replace(' ', '-'));
            sb.Append("/");
            sb.Append("-");
            sb.Append("/");
            sb.Append("-");
            sb.Append("/");
            sb.Append(member.Id.ToString());
            return new ReadOnlyApplicationUrl(_baseCandidateUrl, sb.ToString());
        }

        protected abstract Employer CreateEmployer(Member member);
        protected abstract bool CanContact { get; }
        protected abstract bool HasUsedCredit { get; }
        protected abstract bool ShouldUseCredit { get; }

        protected virtual Employer CreateEmployerForJoin()
        {
            return null;
        }

        private void LogIn(Employer employer)
        {
            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = employer.GetPassword();
            _loginButton.Click();
        }

        private void Join(Employer employer)
        {
            _joinLoginIdTextBox.Text = employer.GetLoginId();
            _joinPasswordTextBox.Text = "password";
            _joinConfirmPasswordTextBox.Text = "password";
            _firstNameTextBox.Text = employer.FirstName;
            _lastNameTextBox.Text = employer.LastName;
            _emailAddressTextBox.Text = employer.EmailAddress.Address;
            _phoneNumberTextBox.Text = employer.PhoneNumber == null ? null : employer.PhoneNumber.Number;
            _organisationNameTextBox.Text = employer.Organisation.Name;
            _locationTextBox.Text = employer.Organisation.Address.Location.ToString();
            _acceptTermsCheckBox.IsChecked = true;
            _joinButton.Click();

            // Get the id that has now been generated.

            employer.Id = _employerAccountsQuery.GetEmployer(employer.GetLoginId()).Id;
        }

        protected MemberAccessReason? AssertSearchPages(bool isLoggedIn, ref Employer employer, Member member, Member representative, AssertAction assertAction, ref int loggedInViewings, ref int notLoggedInViewings)
        {
            if (member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume))
                AssertResultCounts(1, 1, 1);
            else
                AssertPageContains("No candidates match your current search requirements.");

            // Follow the link.

            var currentUrl = new ReadOnlyApplicationUrl(Browser.CurrentUrl.AbsoluteUri);
            var reason = assertAction(isLoggedIn, ref employer, member, representative, ref loggedInViewings, ref notLoggedInViewings);
            Get(currentUrl);
            if (currentUrl.Path == GetCandidateUrl(member).Path)
            {
                if (isLoggedIn)
                    ++loggedInViewings;
                else
                    ++notLoggedInViewings;
            }

            return reason;
        }

        private MemberAccessReason? AssertViewResumePages(bool isLoggedIn, Employer employer, Member member, Member representative, AssertAction assertAction, ref int loggedInViewings, ref int notLoggedInViewings)
        {
            if (member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume))
            {
                if (CanContact && ShouldUseCredit)
                    AssertPageContains("It costs 1 credit");
                else
                    AssertPageDoesNotContain("It costs 1 credit");
            }
            else
            {
                AssertPageContains("Name hidden");
            }

            return assertAction != null
                ? assertAction(isLoggedIn, ref employer, member, representative, ref loggedInViewings, ref notLoggedInViewings)
                : null;
        }

        protected void AssertData(IHasId<Guid> employer, IHasId<Guid> member, MemberAccessReason? reason, int loggedInViewings, int notLoggedInViewings, int? initialCredits)
        {
            var finalCredits = employer == null ? 0 : GetCredits(employer.Id);
            AssertCredits(reason, initialCredits, finalCredits);
            AssertViewings(loggedInViewings, notLoggedInViewings, employer, member.Id);
            AssertContacts(reason, employer, member.Id, initialCredits, finalCredits);
        }

        protected MemberAccessReason? AssertName(bool isLoggedIn, ref Employer employer, Member member, Member representative, ref int loggedInViewings, ref int notLoggedInViewings)
        {
            if (isLoggedIn && HasUsedCredit)
            {
                if (IsNameVisible(member))
                    AssertPageContains(member.FullName);
                else
                    AssertPageDoesNotContain(member.FullName);
            }
            else
            {
                AssertPageDoesNotContain(member.FullName);
            }

            // These should not be shown unless there is a direct action.

            AssertPageDoesNotContain(member.GetBestEmailAddress().Address);
            if (member.PhoneNumbers != null)
            {
                foreach (var phoneNumber in member.PhoneNumbers)
                    AssertPageDoesNotContain(phoneNumber.Number);
            }
            if (representative != null && representative.PhoneNumbers != null)
            {
                foreach (var phoneNumber in representative.PhoneNumbers)
                    AssertPageDoesNotContain(phoneNumber.Number);
            }

            return null;
        }

        protected MemberAccessReason? AssertRepresentative(bool isLoggedIn, ref Employer employer, Member member, Member representative, ref int loggedInViewings, ref int notLoggedInViewings)
        {
//            if (isLoggedIn && (CanContact || HasUsedCredit) && representative != null && IsPhoneNumberVisible(member))
  //              AssertPageContains("Represented");
    //        else
      //          AssertPageDoesNotContain("Represented");

            return null;
        }

        protected MemberAccessReason? AssertPhoneNumber(bool isLoggedIn, ref Employer employer, Member member, Member representative, ref int loggedInViewings, ref int notLoggedInViewings)
        {
            if (isLoggedIn && (CanContact || HasUsedCredit) && IsPhoneNumberVisible(member))
            {
                // On a click this service gets called and should return the phone number.

                var model = PhoneNumbers(member.Id);
                AssertJsonSuccess(model);

                return MemberAccessReason.PhoneNumberViewed;
            }
            else
            {
                if (member.PhoneNumbers != null)
                {
                    foreach (var phoneNumber in member.PhoneNumbers)
                        AssertPageDoesNotContain(phoneNumber.Number);
                }
                if (representative != null && representative.PhoneNumbers != null)
                {
                    foreach (var phoneNumber in representative.PhoneNumbers)
                        AssertPageDoesNotContain(phoneNumber.Number);
                }

                // Try to call the service in any case to make sure access is not allowed.

                var model = PhoneNumbers(member.Id);

                if (!isLoggedIn)
                    AssertJsonError(model, null, "100", "The user is not logged in.");
                else if (!member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume))
                    AssertJsonError(model, null, "The candidate details have been hidden by the candidate.");
                else if (!member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers))
                    AssertJsonError(model, null, "The candidate details have been hidden by the candidate.");
                else
                    AssertJsonError(model, null, "You need 1 credit to perform this action but you have none available.");

                return null;
            }
        }

        private JsonResponseModel PhoneNumbers(Guid memberId)
        {
            var parameters = new NameValueCollection {{"candidateId", memberId.ToString()}};
            return Deserialize<JsonResponseModel>(Post(_phoneNumbersUrl, parameters));
        }

        protected MemberAccessReason? AssertSendMessage(bool isLoggedIn, ref Employer employer, Member member, Member representative, ref int loggedInViewings, ref int notLoggedInViewings)
        {
            var currentUrl = new ReadOnlyApplicationUrl(Browser.CurrentUrl.AbsoluteUri);

            // Check login.

            JsonResponseModel model;
            if (!isLoggedIn)
            {
                AssertNoContact(member, representative);

                model = SendMessage(member.Id);
                AssertJsonError(model, null, "100", "The user is not logged in.");

                if (employer != null)
                {
                    // Login now.

                    Get(_loginUrl);
                    LogIn(employer);
                    isLoggedIn = true;

                    model = SendMessage(member.Id);
                }
                else
                {
                    employer = CreateEmployerForJoin();
                    if (employer != null)
                    {
                        // Join now.

                        Get(_loginUrl);
                        Join(employer);
                        isLoggedIn = true;

                        model = SendMessage(member.Id);
                    }
                }
            }
            else
            {
                model = SendMessage(member.Id);
            }

            // Assert.

            MemberAccessReason? reason = null;
            if (member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume) && isLoggedIn && (CanContact || HasUsedCredit))
            {
                AssertJsonSuccess(model);
                Get(currentUrl);
                if (currentUrl.Path == GetCandidateUrl(member).Path)
                    ++loggedInViewings;

                // What you see depends on whether a credit has been used.

                if (IsNameVisible(member))
                {
                    AssertPageContains(member.FullName);
//                    if (representative != null)
//                        AssertPageContains(representative.FullName);
                }
                else
                {
                    AssertPageDoesNotContain(member.FullName);
//                    if (representative != null)
//                        AssertPageDoesNotContain(representative.FullName);
                }

                // Check the email is sent to the right person.

                var emails = _emailServer.AssertEmailsSent(2);
                if (representative == null)
                {
                    Assert.AreEqual(member.EmailAddresses[0].Address, emails[0].To[0].Address);
                    emails[0].AssertHtmlViewContains("has found you");
                }
                else
                {
                    Assert.AreEqual(representative.EmailAddresses[0].Address, emails[0].To[0].Address);
                    emails[0].AssertHtmlViewDoesNotContain("has found you");
                    emails[0].AssertHtmlViewContains("has found " + member.FullName);
                }
                Assert.AreEqual(employer.EmailAddress.Address, emails[1].To[0].Address);

                reason = MemberAccessReason.MessageSent;
            }
            else
            {
                if (employer == null)
                    AssertJsonError(model, null, "100", "The user is not logged in.");
                else if (!member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume))
                    AssertJsonError(model, null, "The candidate details have been hidden by the candidate.");
                else
                    AssertJsonError(model, null, "You need 1 credit to perform this action but you have none available.");

                Get(currentUrl);
                if (currentUrl.Path == GetCandidateUrl(member).Path)
                {
                    if (isLoggedIn)
                        ++loggedInViewings;
                    else
                        ++notLoggedInViewings;
                }

                AssertNoContact(member, representative);
            }

            return reason;
        }

        private JsonResponseModel SendMessage(Guid memberId)
        {
            var variables = new NameValueCollection
            {
                {"candidateId", memberId.ToString()},
                {"subject", "This is the subject."},
                {"body", "This is the body."},
                {"sendCopy", "true"}
            };

            return Deserialize<JsonResponseModel>(Post(_sendMessagesUrl, variables));
        }

        protected MemberAccessReason? AssertDownloadResume(bool isLoggedIn, ref Employer employer, Member member, Member representative, ref int loggedInViewings, ref int notLoggedInViewings)
        {
            var currentUrl = new ReadOnlyApplicationUrl(Browser.CurrentUrl.AbsoluteUri);

            // Check login.

            JsonResponseModel model;
            if (!isLoggedIn)
            {
                AssertNoContact(member, representative);

                model = DownloadResume(member.Id);
                AssertJsonError(model, null, "100", "The user is not logged in.");

                if (employer != null)
                {
                    // Login now.

                    Get(_loginUrl);
                    LogIn(employer);
                    isLoggedIn = true;

                    model = DownloadResume(member.Id);
                }
                else
                {
                    employer = CreateEmployerForJoin();
                    if (employer != null)
                    {
                        // Join now.

                        Get(_loginUrl);
                        Join(employer);
                        isLoggedIn = true;

                        model = DownloadResume(member.Id);
                    }
                }
            }
            else
            {
                model = DownloadResume(member.Id);
            }

            // Assert.

            MemberAccessReason? reason = null;
            if (member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume) && isLoggedIn && (CanContact || HasUsedCredit))
            {
                AssertJsonSuccess(model);
                Get(currentUrl);
                if (currentUrl.Path == GetCandidateUrl(member).Path)
                    ++loggedInViewings;

                reason = MemberAccessReason.ResumeDownloaded;
            }
            else
            {
                if (employer == null)
                    AssertJsonError(model, null, "100", "The user is not logged in.");
                else if (!member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume))
                    AssertJsonError(model, null, "The candidate details have been hidden by the candidate.");
                else
                    AssertJsonError(model, null, "You need 1 credit to perform this action but you have none available.");

                Get(currentUrl);
                if (currentUrl.Path == GetCandidateUrl(member).Path)
                {
                    if (isLoggedIn)
                        ++loggedInViewings;
                    else
                        ++notLoggedInViewings;
                }

                AssertNoContact(member, representative);
            }

            return reason;
        }

        private JsonResponseModel DownloadResume(Guid memberId)
        {
            var parameters = new NameValueCollection {{"candidateId", memberId.ToString()}};
            return Deserialize<JsonResponseModel>(Post(_downloadUrl, parameters));
        }

        protected MemberAccessReason? AssertEmailResume(bool isLoggedIn, ref Employer employer, Member member, Member representative, ref int loggedInViewings, ref int notLoggedInViewings)
        {
            var currentUrl = new ReadOnlyApplicationUrl(Browser.CurrentUrl.AbsoluteUri);

            // Check login.

            JsonResponseModel model;
            if (!isLoggedIn)
            {
                AssertNoContact(member, representative);

                model = EmailResume(member.Id);
                AssertJsonError(model, null, "100", "The user is not logged in.");

                if (employer != null)
                {
                    // Login now.

                    Get(_loginUrl);
                    LogIn(employer);
                    isLoggedIn = true;

                    model = EmailResume(member.Id);
                }
                else
                {
                    employer = CreateEmployerForJoin();
                    if (employer != null)
                    {
                        // Join now.

                        Get(_loginUrl);
                        Join(employer);
                        isLoggedIn = true;

                        model = EmailResume(member.Id);
                    }
                }
            }
            else
            {
                model = EmailResume(member.Id);
            }

            // Assert.

            MemberAccessReason? reason = null;
            if (member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume) && isLoggedIn && (CanContact || HasUsedCredit))
            {
                AssertJsonSuccess(model);
                Get(currentUrl);
                if (currentUrl.Path == GetCandidateUrl(member).Path)
                    ++loggedInViewings;
                
                reason = MemberAccessReason.ResumeSent;
            }
            else
            {
                if (employer == null)
                    AssertJsonError(model, null, "100", "The user is not logged in.");
                else if (!member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume))
                    AssertJsonError(model, null, "The candidate details have been hidden by the candidate.");
                else
                    AssertJsonError(model, null, "You need 1 credit to perform this action but you have none available.");

                Get(currentUrl);
                if (currentUrl.Path == GetCandidateUrl(member).Path)
                {
                    if (isLoggedIn)
                        ++loggedInViewings;
                    else
                        ++notLoggedInViewings;
                }
                
                AssertNoContact(member, representative);
            }

            return reason;
        }

        private JsonResponseModel EmailResume(Guid memberId)
        {
            var parameters = new NameValueCollection {{"candidateId", memberId.ToString()}};
            return Deserialize<JsonResponseModel>(Post(_sendUrl, parameters));
        }

        protected MemberAccessReason? AssertViewResume(bool isLoggedIn, ref Employer employer, Member member, Member representative, ref int loggedInViewings, ref int notLoggedInViewings)
        {
            // Find the link.

            var url = GetViewResumeUrl();
            if (!member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume))
            {
                Assert.IsNull(url);
                return null;
            }

            // Check login.

            if (!isLoggedIn)
            {
                if (employer != null)
                {
                    // Login now.

                    Get(_loginUrl);
                    LogIn(employer);
                    isLoggedIn = true;
                }
                else
                {
                    employer = CreateEmployerForJoin();
                    if (employer != null)
                    {
                        // Join now.

                        Get(_loginUrl);
                        Join(employer);
                        isLoggedIn = true;
                    }
                }
            }

            // Assert.

            Get(url);
            if (isLoggedIn)
                ++loggedInViewings;
            else
                ++notLoggedInViewings;

            return AssertViewResumePages(isLoggedIn, employer, member, representative, null, ref loggedInViewings, ref notLoggedInViewings);
        }

        private ReadOnlyUrl GetViewResumeUrl()
        {
            var xmlNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='candidate-name']/a");
            return xmlNode == null ? null : new ReadOnlyApplicationUrl(xmlNode.Attributes["href"].Value);
        }

        private void AssertNoContact(IMember member, IMember representative)
        {
            AssertPageDoesNotContain(member.FullName);
            if (member.PhoneNumbers != null)
            {
                foreach (var phoneNumber in member.PhoneNumbers)
                    AssertPageDoesNotContain(phoneNumber.Number);
            }
            if (representative != null && representative.PhoneNumbers != null)
            {
                foreach (var phoneNumber in representative.PhoneNumbers)
                    AssertPageDoesNotContain(phoneNumber.Number);
            }
        }

        protected void PerformSearch()
        {
            Get(GetSearchUrl(BusinessAnalyst));
        }

        private void AssertCredits(MemberAccessReason? reason, int? initialCredits, int? finalCredits)
        {
            if (reason != null && ShouldUseCredit)
            {
                if (initialCredits == finalCredits)
                    Assert.Fail("A credit should have been used.");
                if (initialCredits == null || finalCredits == null)
                    Assert.Fail("No credit should have been used for unlimited credits.");
                if (initialCredits.Value == 0)
                    Assert.Fail("No credit should have been used when non were allocated.");
                if (finalCredits.Value != initialCredits.Value - 1)
                    Assert.Fail("A single credit should have been used but it appears that more have been.");
            }
            else
            {
                if (initialCredits != finalCredits)
                    Assert.Fail("No credit should have been used.");
            }
        }

        protected int? GetCredits(Guid employerId)
        {
            var credits = 0;

            var allocations = _allocationsQuery.GetActiveAllocations(employerId);
            foreach (var allocation in allocations)
            {
                if (allocation.IsUnlimited)
                    return null;
                credits += allocation.RemainingQuantity.Value;
            }

            return credits;
        }

        private Guid? GetAllocationId(Guid employerId)
        {
            var allocations = _allocationsQuery.GetActiveAllocations(employerId);
            return allocations.Count == 0 ? (Guid?)null : allocations[0].Id;
        }

        private void AssertViewings(int expectedLoggedInViewings, int expectedNotLoggedInViewings, IHasId<Guid> employer, Guid memberId)
        {
            // A viewing.

            Assert.AreEqual(expectedLoggedInViewings + expectedNotLoggedInViewings, _employerMemberAccessReportsQuery.GetMemberViewings(memberId, null));
            
            if (employer != null)
            {
                Assert.AreEqual(expectedLoggedInViewings > 0, _employerViewsRepository.HasViewedMember(employer.Id, memberId));

                var viewedMembers = _employerViewsRepository.GetViewedMemberIds(employer.Id);
                var viewings = _employerViewsRepository.GetMemberViewings(employer.Id, memberId);

                if (expectedLoggedInViewings > 0)
                {
                    Assert.AreEqual(1, viewedMembers.Count);
                    Assert.AreEqual(memberId, viewedMembers[0]);

                    AssertViewings(expectedLoggedInViewings, employer, memberId, viewings);
                }
                else
                {
                    Assert.AreEqual(0, viewedMembers.Count);
                    Assert.AreEqual(0, viewings.Count);
                }
            }
        }

        private static void AssertViewings(int expectedViewings, IHasId<Guid> employer, Guid memberId, ICollection<MemberViewing> viewings)
        {
            Assert.AreEqual(expectedViewings, viewings.Count);
            foreach (var viewing in viewings)
            {
                Assert.AreEqual(memberId, viewing.MemberId);
                Assert.AreEqual(employer == null ? (Guid?)null : employer.Id, viewing.EmployerId);
            }
        }

        private void AssertContacts(MemberAccessReason? reason, IHasId<Guid> employer, Guid memberId, int? initialCredits, int? finalCredits)
        {
            if (reason != null)
            {
                Assert.AreEqual(1, _employerMemberAccessReportsQuery.GetMemberAccesses(memberId, reason.Value));
                Assert.AreEqual(true, employer != null);
                if (employer != null)
                    AssertContacts(reason.Value, employer.Id, memberId, initialCredits, finalCredits, _employerViewsRepository.GetMemberAccesses(employer.Id, memberId));
            }
            else
            {
                Assert.AreEqual(0, _employerMemberAccessReportsQuery.GetMemberAccesses(memberId));
                if (employer != null)
                    Assert.AreEqual(0, _employerViewsRepository.GetMemberAccesses(employer.Id, memberId).Count);
            }
        }

        private void AssertContacts(MemberAccessReason reason, Guid employerId, Guid memberId, int? initialCredits, int? finalCredits, ICollection<MemberAccess> accesses)
        {
            Assert.AreEqual(1, accesses.Count);
            foreach (var access in accesses)
            {
                Assert.AreEqual(employerId, access.EmployerId);
                Assert.AreEqual(memberId, access.MemberId);
                Assert.AreEqual(reason, access.Reason);

                // For a credit to be exercised then:
                // - the allocation had unlimited credits
                // - the final allocation credits is different from the initial allocation credits.

                if ((initialCredits == null && finalCredits == null) || initialCredits.Value != finalCredits.Value)
                {
                    Assert.IsNotNull(access.ExercisedCreditId);
                    var exercisedCredit = _exercisedCreditsQuery.GetExercisedCredit(access.ExercisedCreditId.Value);

                    var allocationId = GetAllocationId(employerId);
                    Assert.IsNotNull(allocationId);
                    Assert.AreEqual(allocationId.Value, exercisedCredit.AllocationId);
                    Assert.AreEqual(employerId, exercisedCredit.ExercisedById);
                    Assert.AreEqual(memberId, exercisedCredit.ExercisedOnId);
                }
                else
                {
                    Assert.AreEqual(null, access.ExercisedCreditId);
                }
            }
        }

        private static bool IsNameVisible(IMember member)
        {
            return member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume)
                && member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Name);
        }

        private static bool IsPhoneNumberVisible(IMember member)
        {
            return member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume)
                && member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers);
        }

        protected Employer CreateEmployer(bool verified)
        {
            var organisation = verified
                ? _organisationsCommand.CreateTestVerifiedOrganisation(0, null, Guid.NewGuid())
                : _organisationsCommand.CreateTestOrganisation(0);
            return _employerAccountsCommand.CreateTestEmployer(1, organisation);
        }

        protected Employer CreateEmployer(bool verified, int? quantity)
        {
            var organisation = verified
                ? _organisationsCommand.CreateTestVerifiedOrganisation(0, null, Guid.NewGuid())
                : _organisationsCommand.CreateTestOrganisation(0);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, organisation);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = quantity });
            return employer;
        }

        protected Member CreateMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            member.PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = PhoneNumber, Type = PhoneNumberType.Mobile } };
            _memberAccountsCommand.UpdateMember(member);
            _memberSearchService.UpdateMember(member.Id);
            return member;
        }

        protected void SetVisibility(Member member, ProfessionalVisibility visibility)
        {
            if (visibility != ProfessionalVisibilitySettings.DefaultEmployment)
            {
                member.VisibilitySettings.Professional.EmploymentVisibility = visibility;
                _memberAccountsCommand.UpdateMember(member);
                _memberSearchService.UpdateMember(member.Id);
            }
        }

        protected Member CreateRepresentative(IHasId<Guid> member)
        {
            var representative = _memberAccountsCommand.CreateTestMember(1);
            representative.PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = RepresentativePhoneNumber, Type = PhoneNumberType.Mobile } };
            _memberAccountsCommand.UpdateMember(representative);

            _networkingCommand.CreateFirstDegreeLink(member.Id, representative.Id);
            _representativesCommand.CreateRepresentative(member.Id, representative.Id);
            return representative;
        }
    }
}