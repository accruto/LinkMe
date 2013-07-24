using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class SecondaryEmailAddressTests
        : FieldsTests
    {
        private const string NewEmailAddress = "secondary@test.linkme.net.au";

        protected override void AssertManualDefault()
        {
            AssertDefault();
        }

        protected override void AssertUploadDefault(IMember member, IResume resume)
        {
            AssertDefault();
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, Resume resume)
        {
            member.EmailAddresses = new List<EmailAddress> { member.EmailAddresses[0], new EmailAddress { Address = "bogusemail.com" } };
            TestErrors(instanceId, member, candidate, resume, "The secondary email address must be valid and have less than 320 characters.");
            member.EmailAddresses = new List<EmailAddress> { member.EmailAddresses[0] };
        }

        protected override void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded)
        {
            Assert.AreEqual(1, member.EmailAddresses.Count);
            member.EmailAddresses.Add(new EmailAddress { Address = NewEmailAddress, IsVerified = false });
        }

        private void AssertDefault()
        {
            Assert.AreEqual(string.Empty, _secondaryEmailAddressTextBox.Text);
        }
    }
}
