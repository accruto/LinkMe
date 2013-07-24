using System.Collections.Generic;
using System.Xml;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join
{
    [TestClass]
    public class EmailTests
        : JoinTests
    {
        private ReadOnlyUrl _profileUrl;

        private const string SecondaryEmailAddress = "secondary@test.linkme.net.au";

        [TestInitialize]
        public void TestInitialize()
        {
            _emailServer.ClearEmails();

            _profileUrl = new ReadOnlyApplicationUrl(true, "~/members/profile");
        }

        [TestMethod]
        public void TestNotAlreadyLoggedIn()
        {
            var member = CreateMember(FirstName, LastName, EmailAddress);

            // Enter standard details and join.

            Get(GetJoinUrl());
            var emails = SubmitDetails(member, false);

            // Get the activation url from the email.

            Assert.AreEqual(1, emails.Count);
            var activationEmail = emails[0];
            activationEmail.AssertAddresses(Return, Return, new EmailRecipient(EmailAddress, FirstName + " " + LastName));
            var activationUrl = GetEmailUrl(activationEmail.GetHtmlView().Body);

            // Navigate to the activation page which should activate the account.

            Get(activationUrl);
            AssertUrl(GetEmailUrl("ActivationEmail", _profileUrl));

            // Should now be activated and verified.

            AssertEmailAddress(EmailAddress, true, true);

            // Try to log in again and assert that the home page is reached.

            LogOut();
            LogIn(member, Password);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestAlreadyLoggedIn()
        {
            var member = CreateMember(FirstName, LastName, EmailAddress);
            HomeJoin(member, Password);

            // Enter details.

            var emails = SubmitDetails(member, true);

            // Get the activation url from the email.

            Assert.AreEqual(1, emails.Count);
            var activationEmail = emails[0];
            activationEmail.AssertAddresses(Return, Return, new EmailRecipient(EmailAddress, FirstName + " " + LastName));
            var activationUrl = GetEmailUrl(activationEmail.GetHtmlView().Body);

            // Navigate to the activation page which should activate the account.

            Get(activationUrl);
            AssertUrl(GetEmailUrl("ActivationEmail", _profileUrl));

            // Should now be activated and verified.

            AssertEmailAddress(EmailAddress, true, true);

            // Try to log in again and assert that the home page is reached.

            LogIn(member, Password);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestSecondaryEmailAddress()
        {
            var member = CreateMember(FirstName, LastName, EmailAddress);
            member.EmailAddresses = new List<EmailAddress>
            {
                member.EmailAddresses[0],
                new EmailAddress {Address = SecondaryEmailAddress, IsVerified = false}
            };

            // Enter standard details and join.

            Get(GetJoinUrl());
            var emails = SubmitDetails(member, false);

            // Get the activation url from the email.

            Assert.AreEqual(2, emails.Count);
            var activationEmail = emails[0];
            activationEmail.AssertAddresses(Return, Return, new EmailRecipient(EmailAddress, FirstName + " " + LastName));

            var verificationEmail = emails[1];
            verificationEmail.AssertAddresses(Return, Return, new EmailRecipient(SecondaryEmailAddress, FirstName + " " + LastName));

            // Navigate to the activation page which should activate the account.

            var activationUrl = GetEmailUrl(activationEmail.GetHtmlView().Body);
            Get(activationUrl);
            AssertUrl(GetEmailUrl("ActivationEmail", _profileUrl));

            // Should now be activated and verified.

            AssertEmailAddress(EmailAddress, true, SecondaryEmailAddress, false, true);

            // Try to log in again and assert that the home page is reached.

            LogOut();
            LogIn(member, Password);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestSecondaryEmailAddressActivation()
        {
            var member = CreateMember(FirstName, LastName, EmailAddress);
            member.EmailAddresses = new List<EmailAddress>
            {
                member.EmailAddresses[0],
                new EmailAddress {Address = SecondaryEmailAddress, IsVerified = false}
            };

            // Enter standard details and join.

            Get(GetJoinUrl());
            var emails = SubmitDetails(member, false);

            // Get the activation url from the email.

            Assert.AreEqual(2, emails.Count);
            var activationEmail = emails[0];
            activationEmail.AssertAddresses(Return, Return, new EmailRecipient(EmailAddress, FirstName + " " + LastName));

            var verificationEmail = emails[1];
            verificationEmail.AssertAddresses(Return, Return, new EmailRecipient(SecondaryEmailAddress, FirstName + " " + LastName));

            // Navigate to the activation page which should activate the account using the url in the verification email.
            // Either email address should be enough to activate the account.

            var activationUrl = GetEmailUrl(verificationEmail.GetHtmlView().Body);
            Get(activationUrl);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            // Should now be activated and verified.

            AssertEmailAddress(EmailAddress, false, SecondaryEmailAddress, true, true);

            // Try to log in again and assert that the home page is reached.

            LogOut();
            LogIn(member, Password);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        private IList<MockEmail> SubmitDetails(Member member, bool alreadyLoggedIn)
        {
            var emails = new List<MockEmail>();

            // If account is already created it should be not activated and not verified.

            if (alreadyLoggedIn)
                AssertEmailAddress(EmailAddress, false, false);

            // No emails should have been sent yet.

            _emailServer.AssertNoEmailSent();

            // Join.

            SubmitJoin();
            var instanceId = GetInstanceId();

            // No emails should have been sent yet.

            _emailServer.AssertNoEmailSent();

            // Personal details.

            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            if (alreadyLoggedIn)
                SubmitPersonalDetails(instanceId, member, candidate);
            else
                SubmitPersonalDetails(instanceId, member, candidate, Password);

            // Still not activated and not verified.

            AssertEmailAddress(EmailAddress, false, false);

            // An activation email should have now been sent though.

            emails.Add(_emailServer.AssertEmailSent());

            // Job details.

            UpdateMember(member, Gender, DateOfBirth);
            var resume = CreateResume();
            SubmitJobDetails(instanceId, member, candidate, resume, false, null, false);

            // If there is a secondary email then should get a verification email.

            if (member.EmailAddresses.Count > 1)
                emails.Add(_emailServer.AssertEmailSent());
            else
                _emailServer.AssertNoEmailSent();

            // Go back and forth through process, no extra email should be sent.

            Get(GetPersonalDetailsUrl(instanceId));
            SubmitPersonalDetails(instanceId, member, candidate);
            Get(GetJobDetailsUrl(instanceId));
            SubmitJobDetails(instanceId, member, candidate, CreateResume(), false, null, false);
            _emailServer.AssertNoEmailSent();

            return emails;
        }

        private static ReadOnlyUrl GetEmailUrl(string body)
        {
            var document = new XmlDocument();
            document.LoadXml(body);
            var xmlNode = document.SelectSingleNode("//div[@class='body']/p/a");
            return xmlNode != null ? new ReadOnlyApplicationUrl(xmlNode.Attributes["href"].InnerText) : null;
        }

        private void AssertEmailAddress(string emailAddress, bool isVerified, bool isActivated)
        {
            var member = _membersQuery.GetMember(emailAddress);

            Assert.AreEqual(1, member.EmailAddresses.Count);
            Assert.AreEqual(emailAddress, member.EmailAddresses[0].Address);
            Assert.AreEqual(isVerified, member.EmailAddresses[0].IsVerified);

            Assert.AreEqual(isActivated, member.IsActivated);
        }

        private void AssertEmailAddress(string primaryEmailAddress, bool primaryIsVerified, string secondaryEmailAddress, bool secondaryIsVerified, bool isActivated)
        {
            var member = _membersQuery.GetMember(primaryEmailAddress);

            Assert.AreEqual(2, member.EmailAddresses.Count);
            Assert.AreEqual(primaryEmailAddress, member.EmailAddresses[0].Address);
            Assert.AreEqual(primaryIsVerified, member.EmailAddresses[0].IsVerified);
            Assert.AreEqual(secondaryEmailAddress, member.EmailAddresses[1].Address);
            Assert.AreEqual(secondaryIsVerified, member.EmailAddresses[1].IsVerified);

            Assert.AreEqual(isActivated, member.IsActivated);
        }
    }
}
