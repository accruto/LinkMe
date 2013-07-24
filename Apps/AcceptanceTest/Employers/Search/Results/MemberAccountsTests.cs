using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using System.Xml;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Management.EmailStatus;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Unity.ServiceModel;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Results
{
    [TestClass]
    public class MemberAccountsTests
        : SearchTests
    {
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();

        private ReadOnlyUrl _adminMembersUrl;
        private ReadOnlyUrl _notActivatedUrl;
        private ReadOnlyUrl _activationSentUrl;
        private ReadOnlyUrl _activationUrl;
        private ReadOnlyUrl _notVerifiedUrl;
        private ReadOnlyUrl _verificationSentUrl;
        private ReadOnlyUrl _verificationUrl;
        private ReadOnlyUrl _visibilityUrl;
        private ReadOnlyUrl _contactDetailsUrl;

        private const string EmailStatusUrl = "http://localhost:8003/Management/Test/EmailStatus/";
        private WebServiceHost _host;

        private HtmlButtonTester _enableButton;
        private HtmlButtonTester _disableButton;
        private HtmlButtonTester _deactivateButton;
        private HtmlButtonTester _resendActivationEmailButton;
        private HtmlButtonTester _resendVerificationEmailButton;

        private const string PrimaryEmailAddress = "homer@test.linkme.net.au";
        private const string SecondaryEmailAddress = "barney@test.linkme.net.au";
        private const string PhoneNumber = "99999999";

        [TestCleanup]
        public void TestCleanup()
        {
            _host.Close();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _host = new WebServiceHost(typeof(EmailStatusService), new Uri(EmailStatusUrl));
            _host.Description.Behaviors.Add(new UnityServiceBehavior(Container.Current));
            _host.Open();

            _adminMembersUrl = new ReadOnlyApplicationUrl(true, "~/administrators/members");
            _notActivatedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notactivated");
            _activationSentUrl = new ReadOnlyApplicationUrl(true, "~/accounts/activationsent");
            _activationUrl = new ReadOnlyApplicationUrl("~/accounts/activation");
            _notVerifiedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notverified");
            _verificationSentUrl = new ReadOnlyApplicationUrl(true, "~/accounts/verificationsent");
            _verificationUrl = new ReadOnlyApplicationUrl("~/accounts/verification");
            _visibilityUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/visibility");
            _contactDetailsUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/contactdetails");

            _enableButton = new HtmlButtonTester(Browser, "enable");
            _disableButton = new HtmlButtonTester(Browser, "disable");
            _deactivateButton = new HtmlButtonTester(Browser, "deactivate");

            _resendActivationEmailButton = new HtmlButtonTester(Browser, "ResendActivationEmail");
            _resendVerificationEmailButton = new HtmlButtonTester(Browser, "ResendVerificationEmail");

            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestEnabled()
        {
            var member = CreateMember(0);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);

            // Disable the member.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            LogIn(administrator);
            Get(GetAdminMemberUrl(member.Id));
            _disableButton.Click();
            LogOut();

            // Search.

            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            // Enable.

            LogIn(administrator);
            Get(GetAdminMemberUrl(member.Id));
            _enableButton.Click();
            LogOut();

            // Search.

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);
        }

        [TestMethod]
        public void TestActivation()
        {
            var member = CreateMember();

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);

            // Deactivate the member.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            LogIn(administrator);
            Get(GetAdminMemberUrl(member.Id));
            _deactivateButton.Click();
            LogOut();

            // Search.

            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            // Activate.

            LogIn(member);
            AssertUrl(_notActivatedUrl);
            _resendActivationEmailButton.Click();
            AssertUrl(_activationSentUrl);
            LogOut();
            var email = _emailServer.AssertEmailSent();
            var url = GetEmailUrl(email.GetHtmlView().Body);
            Get(url);
            AssertUrlWithoutQuery(_activationUrl);

            // Search.

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);
        }

        [TestMethod]
        public void TestVisibility()
        {
            var member = CreateMember();

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);

            // Hide the resume.

            LogIn(member);
            AssertJsonSuccess(Visibility(GetVisibilityParameters(ProfessionalVisibility.All ^ ProfessionalVisibility.Resume)));
            LogOut();

            // Search.

            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            // Show again.

            LogIn(member);
            AssertJsonSuccess(Visibility(GetVisibilityParameters(ProfessionalVisibility.All)));
            LogOut();

            // Search.

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);
        }

        [TestMethod]
        public void TestVerifiedPrimaryEmailAddress()
        {
            var member = CreateMember();
            member.PhoneNumbers = null;
            _memberAccountsCommand.UpdateMember(member);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);

            // Bounce the email address.

            BounceEmail(FileSystem.GetAbsolutePath(@"Apps\AcceptanceTest\Employers\Search\Results\TestData\Primary.eml", RuntimeEnvironment.GetSourceFolder()));

            // Search.

            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            // Verify.

            LogIn(member);
            AssertUrlWithoutQuery(_notVerifiedUrl);
            _resendVerificationEmailButton.Click();
            AssertUrlWithoutQuery(_verificationSentUrl);
            LogOut();
            var email = _emailServer.AssertEmailSent();
            var url = GetEmailUrl(email.GetHtmlView().Body);
            Get(url);
            AssertUrlWithoutQuery(_verificationUrl);

            // Search.

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);
        }

        [TestMethod]
        public void TestVerifiedSecondaryEmailAddress()
        {
            var member = CreateMember();
            member.PhoneNumbers = null;
            member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress });
            _memberAccountsCommand.UpdateMember(member);
            _emailVerificationsCommand.VerifyEmailAddress(member.Id, SecondaryEmailAddress);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);

            // Bounce the email addresses.

            BounceEmail(FileSystem.GetAbsolutePath(@"Apps\AcceptanceTest\Employers\Search\Results\TestData\Primary.eml", RuntimeEnvironment.GetSourceFolder()));

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);

            BounceEmail(FileSystem.GetAbsolutePath(@"Apps\AcceptanceTest\Employers\Search\Results\TestData\Secondary.eml", RuntimeEnvironment.GetSourceFolder()));

            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            // Verify.

            LogIn(member);
            AssertUrlWithoutQuery(_notVerifiedUrl);
            _resendVerificationEmailButton.Click();
            AssertUrlWithoutQuery(_verificationSentUrl);
            LogOut();
            var emails = _emailServer.AssertEmailsSent(2);
            var url = GetEmailUrl(emails[1].GetHtmlView().Body);
            Get(url);
            AssertUrlWithoutQuery(_verificationUrl);

            // Search.

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);
        }

        [TestMethod]
        public void TestPhoneNumber()
        {
            var member = CreateMember();
            member.PhoneNumbers = null;
            member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress });
            _memberAccountsCommand.UpdateMember(member);
            _emailVerificationsCommand.VerifyEmailAddress(member.Id, SecondaryEmailAddress);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);

            // Bounce the email addresses.

            BounceEmail(FileSystem.GetAbsolutePath(@"Apps\AcceptanceTest\Employers\Search\Results\TestData\Primary.eml", RuntimeEnvironment.GetSourceFolder()));

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);

            BounceEmail(FileSystem.GetAbsolutePath(@"Apps\AcceptanceTest\Employers\Search\Results\TestData\Secondary.eml", RuntimeEnvironment.GetSourceFolder()));

            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            // Add a phone number.

            LogIn(member);
            member.PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = PhoneNumber } };
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.VisaStatus = VisaStatus.RestrictedWorkVisa;
            AssertJsonSuccess(ContactDetails(GetContactDetailsParameters(member, candidate)));
            LogOut();

            // Search.

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);
        }

        private ReadOnlyUrl GetAdminMemberUrl(Guid memberId)
        {
            return new ReadOnlyApplicationUrl((_adminMembersUrl.AbsoluteUri + "/").AddUrlSegments(memberId.ToString().ToLower()));
        }

        private static ReadOnlyUrl GetEmailUrl(string body)
        {
            var document = new XmlDocument();
            document.LoadXml(body);
            var xmlNode = document.SelectSingleNode("//div[@class='body']/p/a");
            return xmlNode != null && xmlNode.Attributes != null && xmlNode.Attributes["href"] != null ? new ReadOnlyApplicationUrl(xmlNode.Attributes["href"].InnerText) : null;
        }

        private static NameValueCollection GetVisibilityParameters(ProfessionalVisibility visibility)
        {
            return new NameValueCollection
            {
                {"ShowResume", visibility.IsFlagSet(ProfessionalVisibility.Resume) ? "true" : "false"},
                {"ShowName", visibility.IsFlagSet(ProfessionalVisibility.Name) ? "true" : "false"},
                {"ShowPhoneNumbers", visibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers) ? "true" : "false"},
                {"ShowProfilePhoto", visibility.IsFlagSet(ProfessionalVisibility.ProfilePhoto) ? "true" : "false"},
                {"ShowRecentEmployers", visibility.IsFlagSet(ProfessionalVisibility.RecentEmployers) ? "true" : "false"},
            };
        }

        private JsonResponseModel Visibility(NameValueCollection parameters)
        {
            return Deserialize<JsonResponseModel>(Post(_visibilityUrl, parameters));
        }

        private static NameValueCollection GetContactDetailsParameters(IMember member, ICandidate candidate)
        {
            var primaryEmailAddress = member.GetPrimaryEmailAddress();
            var secondaryEmailAddress = member.GetSecondaryEmailAddress();
            var primaryPhoneNumber = member.GetPrimaryPhoneNumber();
            var secondaryPhoneNumber = member.GetSecondaryPhoneNumber();

            return new NameValueCollection
            {
                {"FirstName", member.FirstName},
                {"LastName", member.LastName},
                {"CountryId", member.Address.Location.Country.Id.ToString(CultureInfo.InvariantCulture)},
                {"Location", member.Address.Location.ToString()},
                {"EmailAddress", primaryEmailAddress == null ? null : primaryEmailAddress.Address},
                {"SecondaryEmailAddress", secondaryEmailAddress == null ? null : secondaryEmailAddress.Address},
                {"PhoneNumber", primaryPhoneNumber.Number},
                {"PhoneNumberType", primaryPhoneNumber.Type.ToString()},
                {"SecondaryPhoneNumber", secondaryPhoneNumber == null ? null : secondaryPhoneNumber.Number},
                {"SecondaryPhoneNumberType", secondaryPhoneNumber == null ? null : secondaryPhoneNumber.Type.ToString()},
                {"Citizenship", null},
                {"VisaStatus", candidate.VisaStatus == null ? null : candidate.VisaStatus.Value.ToString()},
                {"Aboriginal", member.EthnicStatus.IsFlagSet(EthnicStatus.Aboriginal) ? "true" : "false"},
                {"TorresIslander", member.EthnicStatus.IsFlagSet(EthnicStatus.TorresIslander) ? "true" : "false"},
                {"Gender", member.Gender == Gender.Male ? "Male" : member.Gender == Gender.Female ? "Female" : null},
                {"DateOfBirthMonth", member.DateOfBirth == null || member.DateOfBirth.Value.Month == null ? null : member.DateOfBirth.Value.Month.Value.ToString(CultureInfo.InvariantCulture)},
                {"DateOfBirthYear", member.DateOfBirth == null ? null : member.DateOfBirth.Value.Year.ToString(CultureInfo.InvariantCulture)},
            };
        }

        private JsonResponseModel ContactDetails(NameValueCollection parameters)
        {
            return Deserialize<JsonResponseModel>(Post(_contactDetailsUrl, parameters));
        }

        private static void BounceEmail(string fileName)
        {
            var request = (HttpWebRequest)WebRequest.Create(EmailStatusUrl);
            request.Method = "POST";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.ContentType = "text/plain";

            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var requestStream = request.GetRequestStream())
                {
                    StreamUtil.CopyStream(fileStream, requestStream);
                }
            }

            request.GetResponse();
        }

        private Member CreateMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(PrimaryEmailAddress);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            return member;
        }
    }
}
