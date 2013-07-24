using System.Linq;
using LinkMe.Domain.Users.Employers.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Contacts
{
    [TestClass]
    public class MemberAttachmentsTests
        : EmployerMemberContactsTests
    {
        [TestMethod]
        public void TestAttachmentAccess()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1);

            // Check attachments.

            AssertAttachments(employer0);
            AssertAttachments(employer1);

            // Save attachment.

            var attachment = CreateMessageAttachment(employer0, 0);

            // Check attachments.

            AssertAttachments(employer0, attachment);
            AssertAttachments(employer1);
            Assert.IsNull(_employerMemberContactsQuery.GetMessageAttachment(employer1, attachment.Id));
            Assert.AreEqual(0, _employerMemberContactsQuery.GetMessageAttachments(employer1, new[] { attachment.Id }).Count);
        }

        [TestMethod]
        public void TestMultipleAttachments()
        {
            var employer = CreateEmployer(0);

            // Save attachments.

            const int count = 3;
            var attachments = new MemberMessageAttachment[count];
            for (var index = 0; index < count; ++index)
                attachments[index] = CreateMessageAttachment(employer, index);

            // Check attachments.

            AssertAttachments(employer, attachments);
        }

        [TestMethod]
        public void TestDetach()
        {
            var employer = CreateEmployer(0);

            // Save attachments.

            const int count = 3;
            var attachments = new MemberMessageAttachment[count];
            for (var index = 0; index < count; ++index)
                attachments[index] = CreateMessageAttachment(employer, index);

            // Check attachments.

            AssertAttachments(employer, attachments);

            _employerMemberContactsCommand.DeleteMessageAttachment(employer, attachments[2].Id);
            AssertAttachments(employer, attachments.Take(2).ToArray());

            _employerMemberContactsCommand.DeleteMessageAttachment(employer, attachments[0].Id);
            AssertAttachments(employer, attachments[1]);

            _employerMemberContactsCommand.DeleteMessageAttachment(employer, attachments[1].Id);
            AssertAttachments(employer);
        }

        [TestMethod]
        public void TestDetachAccess()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1);

            // Save attachments.

            const int count = 3;
            var attachments = new MemberMessageAttachment[count];
            for (var index = 0; index < count; ++index)
                attachments[index] = CreateMessageAttachment(employer0, index);

            // Check attachments.

            AssertAttachments(employer0, attachments);

            _employerMemberContactsCommand.DeleteMessageAttachment(employer1, attachments[2].Id);
            AssertAttachments(employer0, attachments);
            AssertAttachments(employer1);

            _employerMemberContactsCommand.DeleteMessageAttachment(employer1, attachments[0].Id);
            AssertAttachments(employer0, attachments);
            AssertAttachments(employer1);

            _employerMemberContactsCommand.DeleteMessageAttachment(employer1, attachments[1].Id);
            AssertAttachments(employer0, attachments);
            AssertAttachments(employer1);
        }
    }
}
