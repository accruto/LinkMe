using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    [TestClass]
    public class EmailAddressTests
        : FieldsTests
    {
        private const string NewEmailAddress = "hsimpson@test.linkme.net.au";
        private ReadOnlyUrl _notActivatedUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _notActivatedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notactivated");
        }

        protected override void AssertManualDefault()
        {
            AssertDefault(string.Empty);
        }

        protected override void AssertManualJoinedDefault(IMember member)
        {
            AssertDefault(member.GetBestEmailAddress().Address);
        }

        protected override void AssertUploadDefault(IMember member)
        {
            AssertDefault(member.GetBestEmailAddress().Address);
        }

        protected override void AssertUploadJoinedDefault(IMember member)
        {
            AssertDefault(member.GetBestEmailAddress().Address);
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, bool alreadyLoggedIn)
        {
            if (!alreadyLoggedIn)
            {
                member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = string.Empty } };
                TestErrors(instanceId, member, candidate, false, "Please provide a primary email address.");
                member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = "bogusemail.com" } };
                TestErrors(instanceId, member, candidate, false, "The email address must be valid and have less than 320 characters.");
            }
        }

        protected override void Update(Member member, Candidate candidate, ref string password, bool resumeUploaded)
        {
            Assert.AreNotEqual(member.EmailAddresses[0].Address, NewEmailAddress);
            member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = NewEmailAddress, IsVerified = false } };
        }

        protected override void AssertUpdate(bool alreadyJoined)
        {
            // Log in with new email address.

            LogOut();
            Get(HomeUrl);
            SubmitLogIn(EmailAddress, Password);
            var homeUrl = HomeUrl.AsNonReadOnly();
            homeUrl.Scheme = Uri.UriSchemeHttps;
            AssertUrl(homeUrl);
            AssertPageContains("Login failed. Please try again.");

            SubmitLogIn(NewEmailAddress, Password);
            AssertUrl(_notActivatedUrl);
        }

        private void AssertDefault(string emailAddress)
        {
            Assert.AreEqual(emailAddress, _emailAddressTextBox.Text);
        }
    }
}