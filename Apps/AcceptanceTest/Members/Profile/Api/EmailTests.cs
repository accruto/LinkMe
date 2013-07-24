using System;
using System.Collections.Specialized;
using System.Xml;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    [TestClass]
    public class EmailTests
        : ProfileTests
    {
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();

        private const string NewEmailAddress = "bgumble@test.linkme.net.au";
        private const string SecondaryEmailAddress = "marge@test.linkme.net.au";
        private const string NewSecondaryEmailAddress = "moe@test.linkme.net.au";

        private ReadOnlyUrl _contactDetailsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _contactDetailsUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/contactdetails");

            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestChangePrimaryEmailAddress()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            // Should be activated and email verified.

            AssertEmailAddress(member.Id, member.EmailAddresses[0].Address, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = NewEmailAddress }
            };
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));

            // Check.

            AssertEmailAddress(member.Id, NewEmailAddress, false, true);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(NewEmailAddress, member.FullName));
            var url = GetEmailUrl(email.GetHtmlView().Body);

            // Navigate to the verification page.

            Get(url);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, true);
        }

        [TestMethod]
        public void TestAddSecondaryEmailAddress()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            // Should be activated and email verified.

            var primaryEmailAddress = member.EmailAddresses[0].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = primaryEmailAddress },
                new EmailAddress { Address = NewSecondaryEmailAddress }
            };
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));

            // Check.

            AssertEmailAddress(member.Id, primaryEmailAddress, true, NewSecondaryEmailAddress, false, true);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(NewSecondaryEmailAddress, member.FullName));
            var url = GetEmailUrl(email.GetHtmlView().Body);

            // Navigate to the verification page.

            Get(url);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, primaryEmailAddress, true, NewSecondaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestChangePrimaryAddSecondaryEmailAddress()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            // Should be activated and email verified.

            var emailAddress = member.EmailAddresses[0].Address;
            AssertEmailAddress(member.Id, emailAddress, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = NewEmailAddress },
                new EmailAddress { Address = NewSecondaryEmailAddress }
            };
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));

            // Check.

            AssertEmailAddress(member.Id, NewEmailAddress, false, NewSecondaryEmailAddress, false, true);

            var emails = _emailServer.AssertEmailsSent(2);
            emails[0].AssertAddresses(Return, Return, new EmailRecipient(NewEmailAddress, member.FullName));
            var url0 = GetEmailUrl(emails[0].GetHtmlView().Body);
            emails[1].AssertAddresses(Return, Return, new EmailRecipient(NewSecondaryEmailAddress, member.FullName));
            var url1 = GetEmailUrl(emails[1].GetHtmlView().Body);

            // Navigate to the verification page.

            Get(url0);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, NewSecondaryEmailAddress, false, true);

            Get(url1);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, NewSecondaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestChangeSecondaryEmailAddress()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress });
            _memberAccountsCommand.UpdateMember(member);
            _emailVerificationsCommand.VerifyEmailAddress(member.Id, SecondaryEmailAddress);

            // Should be activated and emails verified.

            var primaryEmailAddress = member.EmailAddresses[0].Address;
            var secondaryEmailAddress = member.EmailAddresses[1].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, true, secondaryEmailAddress, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = primaryEmailAddress },
                new EmailAddress { Address = NewSecondaryEmailAddress }
            };
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));

            // Check.

            AssertEmailAddress(member.Id, primaryEmailAddress, true, NewSecondaryEmailAddress, false, true);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(NewSecondaryEmailAddress, member.FullName));
            var url = GetEmailUrl(email.GetHtmlView().Body);

            // Navigate to the verification page.

            Get(url);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, primaryEmailAddress, true, NewSecondaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestChangeBothEmailAddresses()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress });
            _memberAccountsCommand.UpdateMember(member);
            _emailVerificationsCommand.VerifyEmailAddress(member.Id, SecondaryEmailAddress);

            // Should be activated and emails verified.

            var primaryEmailAddress = member.EmailAddresses[0].Address;
            var secondaryEmailAddress = member.EmailAddresses[1].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, true, secondaryEmailAddress, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = NewEmailAddress },
                new EmailAddress { Address = NewSecondaryEmailAddress }
            };
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));

            // Check.

            AssertEmailAddress(member.Id, NewEmailAddress, false, NewSecondaryEmailAddress, false, true);

            var emails = _emailServer.AssertEmailsSent(2);
            emails[0].AssertAddresses(Return, Return, new EmailRecipient(NewEmailAddress, member.FullName));
            var url0 = GetEmailUrl(emails[0].GetHtmlView().Body);
            emails[1].AssertAddresses(Return, Return, new EmailRecipient(NewSecondaryEmailAddress, member.FullName));
            var url1 = GetEmailUrl(emails[1].GetHtmlView().Body);

            // Navigate to the verification page.

            Get(url0);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, NewSecondaryEmailAddress, false, true);

            Get(url1);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, NewSecondaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestRemoveSecondaryEmailAddress()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress });
            _memberAccountsCommand.UpdateMember(member);
            _emailVerificationsCommand.VerifyEmailAddress(member.Id, SecondaryEmailAddress);

            // Should be activated and emails verified.

            var primaryEmailAddress = member.EmailAddresses[0].Address;
            var secondaryEmailAddress = member.EmailAddresses[1].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, true, secondaryEmailAddress, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = primaryEmailAddress }
            };
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));

            // Check.

            AssertEmailAddress(member.Id, primaryEmailAddress, true, true);

            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestMakeSecondaryPrimaryEmailAddress()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress });
            _memberAccountsCommand.UpdateMember(member);
            _emailVerificationsCommand.VerifyEmailAddress(member.Id, SecondaryEmailAddress);

            // Should be activated and emails verified.

            var primaryEmailAddress = member.EmailAddresses[0].Address;
            var secondaryEmailAddress = member.EmailAddresses[1].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, true, secondaryEmailAddress, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = secondaryEmailAddress }
            };
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));

            // Check.

            AssertEmailAddress(member.Id, secondaryEmailAddress, true, true);

            _emailServer.AssertNoEmailSent();
        }

        private static NameValueCollection GetParameters(IMember member, ICandidate candidate, IResume resume)
        {
            var primaryEmailAddress = member.GetPrimaryEmailAddress();
            var secondaryEmailAddress = member.GetSecondaryEmailAddress();
            var primaryPhoneNumber = member.GetPrimaryPhoneNumber();
            var secondaryPhoneNumber = member.GetSecondaryPhoneNumber();

            return new NameValueCollection
            {
                {"FirstName", member.FirstName},
                {"LastName", member.LastName},
                {"CountryId", member.Address.Location.Country.Id.ToString()},
                {"Location", member.Address.Location.ToString()},
                {"EmailAddress", primaryEmailAddress == null ? null : primaryEmailAddress.Address},
                {"SecondaryEmailAddress", secondaryEmailAddress == null ? null : secondaryEmailAddress.Address},
                {"PhoneNumber", primaryPhoneNumber.Number},
                {"PhoneNumberType", primaryPhoneNumber.Type.ToString()},
                {"SecondaryPhoneNumber", secondaryPhoneNumber == null ? null : secondaryPhoneNumber.Number},
                {"SecondaryPhoneNumberType", secondaryPhoneNumber == null ? null : secondaryPhoneNumber.Type.ToString()},
                {"Citizenship", resume == null ? null : resume.Citizenship},
                {"VisaStatus", candidate.VisaStatus == null ? null : candidate.VisaStatus.Value.ToString()},
                {"Aboriginal", member.EthnicStatus.IsFlagSet(EthnicStatus.Aboriginal) ? "true" : "false"},
                {"TorresIslander", member.EthnicStatus.IsFlagSet(EthnicStatus.TorresIslander) ? "true" : "false"},
                {"Gender", member.Gender == Gender.Male ? "Male" : member.Gender == Gender.Female ? "Female" : null},
                {"DateOfBirthMonth", member.DateOfBirth == null || member.DateOfBirth.Value.Month == null ? null : member.DateOfBirth.Value.Month.Value.ToString()},
                {"DateOfBirthYear", member.DateOfBirth == null ? null : member.DateOfBirth.Value.Year.ToString()},
            };
        }

        private JsonResponseModel ContactDetails(NameValueCollection parameters)
        {
            return Deserialize<JsonResponseModel>(Post(_contactDetailsUrl, parameters));
        }

        protected override Member CreateMember()
        {
            var member = base.CreateMember();

            // As the visa status is required set that as well.

            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.VisaStatus = VisaStatus.RestrictedWorkVisa;
            _candidatesCommand.UpdateCandidate(candidate);

            return member;
        }

        private void AssertEmailAddress(Guid memberId, string emailAddress, bool isVerified, bool isActivated)
        {
            var member = _membersQuery.GetMember(memberId);

            Assert.AreEqual(1, member.EmailAddresses.Count);
            Assert.AreEqual(emailAddress, member.EmailAddresses[0].Address);
            Assert.AreEqual(isVerified, member.EmailAddresses[0].IsVerified);

            Assert.AreEqual(isActivated, member.IsActivated);
        }

        private void AssertEmailAddress(Guid memberId, string primaryEmailAddress, bool primaryIsVerified, string secondaryEmailAddress, bool secondaryIsVerified, bool isActivated)
        {
            var member = _membersQuery.GetMember(memberId);

            Assert.AreEqual(2, member.EmailAddresses.Count);
            Assert.AreEqual(primaryEmailAddress, member.EmailAddresses[0].Address);
            Assert.AreEqual(primaryIsVerified, member.EmailAddresses[0].IsVerified);
            Assert.AreEqual(secondaryEmailAddress, member.EmailAddresses[1].Address);
            Assert.AreEqual(secondaryIsVerified, member.EmailAddresses[1].IsVerified);

            Assert.AreEqual(isActivated, member.IsActivated);
        }

        private static ReadOnlyUrl GetEmailUrl(string body)
        {
            var document = new XmlDocument();
            document.LoadXml(body);
            var xmlNode = document.SelectSingleNode("//div[@class='body']/p/a");
            return xmlNode != null ? new ReadOnlyApplicationUrl(xmlNode.Attributes["href"].InnerText) : null;
        }
    }
}